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

        var paired = input.SelectMany((x, i) => input.Skip(i + 1), (a, b) => (a, b));
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
                continue;
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

        return SolveWithZ3(input);
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

        return sx.Int64 + sy.Int64 + sz.Int64;
    }

    private static Vector2 PositionAtT((Vector2 p, Vector2 d) stoneA, float t)
    {
        return stoneA.p + (stoneA.d * t);
    }

    private bool IsInTestArea(Vector2 p, (Vector2 a, Vector2 b) testarea)
    {
        bool xInRange = p.X >= testarea.a.X && p.X <= testarea.b.X;
        bool yInRange = p.Y >= testarea.a.Y && p.Y <= testarea.b.Y;

        return xInRange && yInRange;
    }
    
    static float StoneIntersectionTime((Vector2 p, Vector2 d) a, (Vector2 p, Vector2 d) b)
    {
        // Calculate the intersection time using vector algebra
        float denominator = a.d.X * b.d.Y - a.d.Y * b.d.X;

        if (Math.Abs(denominator) < float.Epsilon)
        {
            // Lines are parallel, no intersection
            return float.NaN;
        }

        var numerator = (a.p.Y - b.p.Y) * b.d.X - (a.p.X - b.p.X) * b.d.Y;
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