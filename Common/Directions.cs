using System;

namespace Common
{
    public static class Directions
    {
        public static readonly (int x, int y) Left = (0, -1);
        public static readonly (int x, int y) Right = (0, 1);
        public static readonly (int x, int y) Up = (-1, 0);
        public static readonly (int x, int y) Down = (1, 0);

        public static readonly (int x, int y) LeftUp = (-1, -1);
        public static readonly (int x, int y) LeftDown = (1, -1);
        public static readonly (int x, int y) RightUp = (-1, 1);
        public static readonly (int x, int y) RightDown = (1, 1);
        
        public static readonly (int x, int y)[] AllCardinal = [Left, Right, Up, Down];
        public static readonly (int x, int y)[] AllOrdinal = [LeftUp, LeftDown, RightUp, RightDown];
        public static readonly (int x, int y)[] All = [Left, Right, Up, Down, LeftUp, LeftDown, RightUp, RightDown];

        public static readonly (int x, int y)[] UpDown = [Up, Down];
		public static readonly (int x, int y)[] LeftRight = [Left, Right];

		public static readonly (int x, int y) North = Up;
        public static readonly (int x, int y) South = Down;
        public static readonly (int x, int y) East = Right;
        public static readonly (int x, int y) West = Left;

        public static readonly (int x, int y) NorthEast = RightUp;
        public static readonly (int x, int y) NorthWest = LeftUp;
        public static readonly (int x, int y) SouthEast = RightDown;
        public static readonly (int x, int y) SouthWest = LeftDown;

        public static (int x, int y) Reverse((int x, int y) direction)
        {
            return (-direction.x, -direction.y);
        }

        public static (int x, int y) TurnRight(this (int x, int y) facing)
        {
	        if (facing == Up) return Right;
			if (facing == Right) return Down;
			if (facing == Down) return Left;
			if (facing == Left) return Up;
			if (facing == LeftUp) return RightUp;
			if (facing == RightUp) return RightDown;
			if (facing == RightDown) return LeftDown;
			if (facing == LeftDown) return LeftUp;
			throw new ArgumentException("Invalid facing");
		}

		public static (int x, int y) TurnLeft(this (int x, int y) facing)
		{
			if (facing == Up) return Left;
			if (facing == Left) return Down;
			if (facing == Down) return Right;
			if (facing == Right) return Up;
			if (facing == LeftUp) return LeftDown;
			if (facing == LeftDown) return RightDown;
			if (facing == RightDown) return RightUp;
			if (facing == RightUp) return LeftUp;
			throw new ArgumentException("Invalid facing");
		}
	}
}
