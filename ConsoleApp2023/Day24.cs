using System.Diagnostics;
using System.Numerics;
using Common;
using Microsoft.Z3;


namespace ConsoleApp2023;

public class Day24 : IDay
{
    public long Part1()
    {
        PuzzleContext.Answer1 = 13892;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse2d).ToList();

        var testarea= PuzzleContext.UseExample? (new Vector2(7), new Vector2(27)) : (new Vector2(200000000000000), new Vector2(400000000000000));

        long sum = 0L;

        IEnumerable<((Vector2 p, Vector2 v) a, (Vector2 p, Vector2 d) b)> paired = input.SelectMany((x, i) => input.Skip(i + 1), (a, b) => (a, b));
        foreach (var (stoneA, stoneB) in paired)
        {
            //ConsoleX.WriteLine();
            //ConsoleX.WriteLine($"Stone A: {stoneA}");
            //ConsoleX.WriteLine($"Stone B: {stoneB}");

            var tA = StoneIntersectionTime(stoneA, stoneB);
            var tB = StoneIntersectionTime(stoneB, stoneA);

            if (float.IsNaN(tA))
            {
                //ConsoleX.WriteLine("Hailstones' paths are parallel; they never intersect.");
                continue;
            }

            if (tA < 0)
            {
                //ConsoleX.WriteLine("Hailstones' paths crossed in the past for A.");
                continue;
            }
            if (tB < 0)
            {
                //ConsoleX.WriteLine("Hailstones' paths crossed in the past for B.");
                continue;
            }

            var intersectionA = PositionAtT(stoneA, tA);
            var intersectionB = PositionAtT(stoneB, tB);

            if (IsInTestArea(intersectionA, testarea) && IsInTestArea(intersectionB, testarea))
            {
                //ConsoleX.WriteLine($"Hailstones' paths crossed at {intersectionA} {intersectionB}.");
                sum++;
            }
            //ConsoleX.WriteLine($"Hailstones' paths crossed outside test area at {intersectionA}.");
        }

        return sum;
    }


    public long Part2()
    {
        PuzzleContext.Answer2 = 843888100572888;
        PuzzleContext.UseExample = false;

        var input = PuzzleContext.Input.Select(Parse3d).ToArray();

        var mathAns = SolveWithMath(input);
        var z3Ans = SolveWithZ3(input);
        
        Debug.Assert(z3Ans == mathAns);
        return mathAns;
    }

    private long SolveWithMath(((long X, long Y, long Z) p, (long X, long Y, long Z) d)[] input)
    {
        var vectors = input.Select(i =>
        {
            var p2 = (i.p.X, i.p.Y);
            var d2 = (i.d.X, i.d.Y);
            var p3 = (i.p.X, i.p.Y, i.p.Z);
            var d3 = (i.d.X, i.d.Y, i.d.Z);
            return (p2, d2, p3, d3);
        }).ToList();

        var searchRange = Enumerable.Range(-1000, 2000).ToList();

        var samples = vectors.SelectMany((x, i) => vectors.Skip(i + 1), (a, b) => (a, b)).Take(3).ToList();
     
        foreach (long x in searchRange)
        {
            foreach (long y in searchRange)
            {
                var d2 = (X: x, Y: y);

                var intersects2 = samples.Select(s => (s: s, i: Intersects(s.a, d2, s.b))).ToList();
               
                if (!intersects2.All(i => i.i.intersects))
                    continue;
                
                foreach (long z in searchRange)
                {
                    var d3 = (X: x, Y: y, Z: z);

                    var points = intersects2.Select(i =>
                    {
                        var (a, b) = i.s;
                        var ad = Subtract(a.d3, d3);
                        var bd = Subtract(b.d3, d3);
                        var ap = a.p3;
                        var bp = b.p3;

                        var pointA = PositionAtT((ap, ad), i.i.ta.Value);
                        var pointB = PositionAtT((bp, bd), i.i.tb.Value);

                        return (pointA, pointB);
                    }).ToList();

                    if (points.Any(p => p.pointA != p.pointB))
                        continue;

                    var point = points.First().pointA;
                    return (long)point.X + (long)point.Y + (long)point.Z;
                }
            }
        }

        return 0L;
    }

    private (bool intersects, (BigInteger X, BigInteger Y)? p, BigInteger? ta, BigInteger? tb) Intersects(
        ((long X, long Y) p2, (long X, long Y) d2, (long X, long Y, long Z) p3, (long X, long Y, long Z) d3) a,
        (long X, long Y) d2,
        ((long X, long Y) p2, (long X, long Y) d2, (long X, long Y, long Z) p3, (long X, long Y, long Z) d3) b)
    {
        var ad = Subtract(a.d2, d2);
        var bd = Subtract(b.d2, d2);
        var ap = a.p2;
        var bp = b.p2;

        var ta = StoneIntersectionTime((ap, ad), (bp, bd));
        if (!ta.HasValue || ta < 0)
            return (false, null, null, null);
        var tb = StoneIntersectionTime((bp, bd), (ap, ad));
        if (!tb.HasValue || tb < 0)
            return (false, null, null, null);

        var pointA = PositionAtT((ap, ad), ta.Value);
        var pointB = PositionAtT((bp, bd), tb.Value);

        if (pointA != pointB)
            return (false, null, null, null);
        return (true, pointA, ta, tb);
    }

    private static (T X, T Y, T Z) Add<T>((T X, T Y, T Z) a, (T X, T Y, T Z) b) where T : INumber<T>
    {
        return (a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    }

    private static (T X, T Y) Add<T>((T X, T Y) a, (T X, T Y) b) where T : INumber<T>
    {
        return (a.X + b.X, a.Y + b.Y);
    }

    private static (T X, T Y, T Z) Multiply<T>((T X, T Y, T Z) v, T t) where T : INumber<T>
    {
        return ((v.X * t), (v.Y * t), (v.Z * t));
    }

    private static (T X, T Y) Multiply<T>((T X, T Y) v, T t) where T : INumber<T>
    {
        return ((v.X * t), (v.Y * t));
    }

    private (long X, long Y) Subtract((long X, long Y) a, (long X, long Y) b)
    {
        return (a.X - b.X, a.Y - b.Y);
    }

    private (long X, long Y, long Z) Subtract((long X, long Y, long Z) a, (long X, long Y, long Z) b)
    {
        return (a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    }
    
    private static long SolveWithZ3(((long X, long Y, long Z) p, (long X, long Y, long Z) d)[] input)
    {
        // I'm totally going to cheat this just like everyone else...
        // by using the Z3 solver
        // This was the worst AoC puzzle ever.

        var ctx = new Context();
        var s = ctx.MkSolver();

        var x = ctx.MkIntConst("x");
        var y = ctx.MkIntConst("y");
        var z = ctx.MkIntConst("z");
        var vx = ctx.MkIntConst("vx");
        var vy = ctx.MkIntConst("vy");
        var vz = ctx.MkIntConst("vz");

        for (int i = 0; i < input.Length; i++)
        {
            var t = ctx.MkIntConst($"t{i}");
            var p = input[i];

            var px = ctx.MkInt(p.p.X);
            var py = ctx.MkInt(p.p.Y);
            var pz = ctx.MkInt(p.p.Z);

            var pvx = ctx.MkInt(p.d.X);
            var pvy = ctx.MkInt(p.d.Y);
            var pvz = ctx.MkInt(p.d.Z);

            var xLeft = ctx.MkAdd(x, ctx.MkMul(t, vx)); // x + t * vx
            var yLeft = ctx.MkAdd(y, ctx.MkMul(t, vy)); // y + t * vy
            var zLeft = ctx.MkAdd(z, ctx.MkMul(t, vz)); // z + t * vz

            var xRight = ctx.MkAdd(px, ctx.MkMul(t, pvx)); // px + t * pvx
            var yRight = ctx.MkAdd(py, ctx.MkMul(t, pvy)); // py + t * pvy
            var zRight = ctx.MkAdd(pz, ctx.MkMul(t, pvz)); // pz + t * pvz

            s.Add(t >= 0);
            s.Add(ctx.MkEq(xLeft, xRight)); // x + t * vx = px + t * pvx
            s.Add(ctx.MkEq(yLeft, yRight)); // y + t * vy = py + t * pvy
            s.Add(ctx.MkEq(zLeft, zRight)); // z + t * vz = pz + t * pvz
        }

        Debug.Assert(s.Check() == Status.SATISFIABLE);

        var m = s.Model;

        var sx = (IntNum) m.Eval(x);
        var sy = (IntNum) m.Eval(y);
        var sz = (IntNum) m.Eval(z);

        ConsoleX.WriteLine(m);

        return sx.Int64 + sy.Int64 + sz.Int64;
    }

    private static Vector2 PositionAtT((Vector2 p, Vector2 v) stoneA, float t)
    {
        return stoneA.p + (stoneA.v * t);
    }

    private static (BigInteger X, BigInteger Y, BigInteger Z) PositionAtT(((long X, long Y, long Z) p, (long X, long Y, long Z) d) a, BigInteger t)
    {
        return Add<BigInteger>(a.p, Multiply<BigInteger>(a.d, t));
    }

    private static (BigInteger X, BigInteger Y) PositionAtT(((long X, long Y) p, (long X, long Y) d) a, BigInteger t)
    {
        return Add<BigInteger>(a.p, Multiply<BigInteger>(a.d, t));
    }
    
    private bool IsInTestArea(Vector2 p, (Vector2 a, Vector2 b) testarea)
    {
        bool xInRange = p.X >= testarea.a.X && p.X <= testarea.b.X;
        bool yInRange = p.Y >= testarea.a.Y && p.Y <= testarea.b.Y;

        return xInRange && yInRange;
    }

    private BigInteger? StoneIntersectionTime(((long X, long Y) p, (long X, long Y) v) a, ((long X, long Y) p, (long X, long Y) v) b)
    {
        BigInteger D = (a.v.X * -1 * b.v.Y) - (a.v.Y * -1 * b.v.X);

        if (D == 0)
            return null ;

        var Qx = (-1 * b.v.Y * (b.p.X - a.p.X)) - (-1 * b.v.X * (b.p.Y - a.p.Y));
        var Qy = (a.v.X * (b.p.Y - a.p.Y)) - (a.v.Y * (b.p.X - a.p.X));

        var t = Qx / D;
        var s = Qy / D;

        return t;
    }

    static float StoneIntersectionTime((Vector2 p, Vector2 v) a, (Vector2 p, Vector2 v) b)
    {
        // Calculate the intersection time using vector algebra
        float denominator = a.v.X * b.v.Y - a.v.Y * b.v.X;

        if (Math.Abs(denominator) < float.Epsilon)
        {
            // Lines are parallel, no intersection
            return float.NaN;
        }

        var numerator = (a.p.Y - b.p.Y) * b.v.X - (a.p.X - b.p.X) * b.v.Y;
        return numerator / denominator;
    }

    private (Vector2 p, Vector2 d) Parse2d(string line)
    {
        var (ps, ds) = line.Split('@', StringSplitOptions.TrimEntries);
        var pl = ps.Split<long>(',');
        var dl = ds.Split<long>(',');

        var point = new Vector2(pl[0], pl[1]);
        var direction = new Vector2(dl[0], dl[1]);

        return (point, direction);
    }
    
    private ((long X, long Y, long Z ) p, (long X, long Y, long Z) d) Parse3d(string line)
    {
        var(ps, ds) = line.Split('@', StringSplitOptions.TrimEntries);
        var pl = ps.Split<long>(',');
        var dl = ds.Split<long>(',');

        return ((pl[0], pl[1], pl[2]), (dl[0], dl[1], dl[2]));
    }

}