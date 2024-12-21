using System.Diagnostics;
using System.Text;
using Common;

namespace ConsoleApp2024;

public class Day21 : IDay
{
	char[,] numpadKeys = new[,]
	{
		{'7','8','9'},
		{'4','5','6'},
		{'1','2','3'},
		{' ','0','A'}
	};

	char[,] dpadKeys = new[,]
	{
		{' ', '^', 'A' },
		{'<', 'v', '>' }
	};

	public long Part1()
	{
		PuzzleContext.Answer1 = 219254;
		PuzzleContext.UseExample = false;

		var codes= PuzzleContext.Input;



		var numpad = new KeyPad(numpadKeys, dpadKeys);
		var dpad1 = new KeyPad(dpadKeys, dpadKeys);
		var dpad2 = new KeyPad(dpadKeys, dpadKeys);

		var sum = 0L;


		foreach (var code in codes)
		{
			ConsoleX.WriteLine(code);
			var directions1 = numpad.GetCode(code);
			var directions2 = dpad1.GetCode(directions1);
			var directions3 = dpad2.GetCode(directions2);
			ConsoleX.WriteLine(directions3);
			var length = directions3.Length;

			sum += length * int.Parse(code.Trim('A'));
		}

		return sum;
	}

	public long Part2()
	{
		PuzzleContext.Answer2 = 264518225304496;
		PuzzleContext.UseExample = false;

		var input = PuzzleContext.Input;

		var keypad = new Keypad2(numpadKeys, dpadKeys);

		return input.Sum(code => keypad.GetCodeCost(code, 25) * int.Parse(code.Trim('A')));
	}
	
	private class KeyPad
	{
		private (int x, int y) _currentPosition;
		private (int x, int y) _currentDirection;
		private readonly char[,] _grid;
		private readonly char[,] _dpad;

		public KeyPad(char[,] grid, char[,] dpad)
		{
			_currentPosition = grid.Find('A').First();
			_currentDirection = (0, 0);
			_grid = grid;
			_dpad = dpad;
		}

		public string GetCode(string keys)
		{
			var code = new StringBuilder();
			
			foreach (var key in keys)
			{
				if (key != _grid[_currentPosition.x, _currentPosition.y])
				{
					var (path, weight) = _grid.Dijkstra<char, ((int x, int y) position, (int x, int y) direction)>((_currentPosition, _currentDirection), GetNeighbors, s => _grid[s.position.x, s.position.y] == key);

					foreach (var step in path.Select(p => p.position).Reverse())
					{
						code.Append(GetDirection(_currentPosition, step));
						_currentPosition = step;
					}
				}
				code.Append('A');
			}
			return code.ToString();
		}

		private char GetDirection((int x, int y) current, (int x, int y) step)
		{
			var direction = step.Subtract(current);

			return DirectionToChar(direction);
		}

		private static char DirectionToChar((int, int) direction)
		{
			if (direction == Directions.Up)
			{
				return '^';
			}
			if (direction == Directions.Down)
			{
				return 'v';
			}
			if (direction == Directions.Left)
			{
				return '<';
			}
			if (direction == Directions.Right)
			{
				return '>';
			}
			throw new Exception("Invalid direction");
		}

		private IEnumerable<(((int x, int y) position, (int x, int y) direction) point, long weight)> GetNeighbors(char[,] grid, ((int x, int y) position, (int x, int y) direction) current)
		{
			return grid.GetNeighbors(current.position).Where(n => grid[n.x,n.y] != ' ').Select(n =>
			{
				var nd = n.Subtract(current.position);
				return ((n, nd),
					GetWeight(current.direction, nd));
			});
		}

		private long GetWeight((int x, int y) current, (int x, int y) target)
		{
			if (current == (0, 0)) return 1;

			var currentKey = DirectionToChar(current);
			var targetKey = DirectionToChar(target);
			if (currentKey == targetKey) return 1;

			var currentPosition = _dpad.Find(currentKey).First();
			var targetPosition = _dpad.Find(targetKey).First();
			return currentPosition.ManhattanDistance(targetPosition)+1;
		}
	}

}

public class Keypad2
{
	private readonly char[,] _numpadKeys;
	private readonly char[,] _dpadKeys;

	private readonly Dictionary<char, (int x, int y)> _directions;

	public Keypad2(char[,] numpadKeys, char[,] dpadKeys)
	{
		_numpadKeys = numpadKeys;
		_dpadKeys = dpadKeys;

		_directions = new Dictionary<char, (int x, int y)>
		{
			{'^', Directions.Up},
			{'v', Directions.Down},
			{'<', Directions.Left},
			{'>', Directions.Right}
		};
	}

	public long GetCodeCost(string code, int maxdepth)
	{
		var start = 'A';
		long sum = 0;
		foreach (var instruction in code)
		{
			sum += GetCost(start, instruction, 0, maxdepth);
			start = instruction;
		}
		return sum;
	}

	Dictionary<(char from, char to, int depth), long>  _costCache = new ();
	private long GetCost(char from, char to, int depth, int maxdepth)
	{
		if (_costCache.TryGetValue((from, to, depth), out var cost))
		{
			return cost;
		}

		var keypad = depth == 0 ? _numpadKeys : _dpadKeys;

		var paths = GeneratePaths(keypad, from, to);
		if (depth == maxdepth)
		{
			return paths[0].Length;
		}

	    cost = paths.Min(p =>
		{
			var start = 'A';
			long sum = 0;
			foreach (var instruction in p)
			{
				sum += GetCost(start, instruction, depth+1, maxdepth);
				start = instruction;
			}
			return sum;
		});

		_costCache[(from, to, depth)] = cost;
		return cost;
	}

	private string[] GeneratePaths(char[,] keypad, char from, char to)
	{
		if (from == to)
		{
			return ["A"];
		}

		var fromi = keypad.Find(from).First();
		var toi = keypad.Find(to).First();

		var xi = toi.x - fromi.x;
		var yi = toi.y - fromi.y;

		var path = new StringBuilder();
		path.Append(xi > 0 ? 'v' : '^', Math.Abs(xi));
		path.Append(yi > 0 ? '>' : '<', Math.Abs(yi));

		var pathStr = path.ToString();
		var options = pathStr.GetPermutations().Distinct().Where(p => IsValid(keypad, fromi, toi, p)).Select(p => p + "A");
		Debug.Assert(options.Any());
		return options.ToArray();
	}

	private bool IsValid(char[,] keypad, (int x, int y) fromi, (int x, int y) toi, string path)
	{
		var current = fromi;
		foreach (var step in path)
		{
			current = current.Add(_directions[step]);
			if (keypad[current.x, current.y] == ' ')
			{
				return false;
			}
		}
		Debug.Assert(current == toi);
		return true;
	}
}

//<vA<AA>>^AvAA^<A>Av<<A>>^AvA^Av<<A>>^AAvA<A>^A<A>Av<<A>A>^AAAvA^<A>A
//<vA<AA>>^AvAA<^A>Av<<A>>^AvA^Av<<A>>^AAvA<A>^A<A>Av<<A>A>^AAAvA<^A>A
//<vA<AA>>^AvAA<^A>A<v<A>>^AvA^A<vA>^A<v<A>^A>AAvA^A<v<A>A>^AAAvA<^A>A