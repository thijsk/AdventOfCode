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
        
        public static readonly (int x, int y)[] AllCardinal = { Left, Right, Up, Down };
        public static readonly (int x, int y)[] AllOrdinal = { LeftUp, LeftDown, RightUp, RightDown };
        public static readonly (int x, int y)[] All = { Left, Right, Up, Down, LeftUp, LeftDown, RightUp, RightDown };

        public static readonly (int x, int y) North = Up;
        public static readonly (int x, int y) South = Down;
        public static readonly (int x, int y) East = Right;
        public static readonly (int x, int y) West = Left;

        public static readonly (int x, int y) NorthEast = RightUp;
        public static readonly (int x, int y) NorthWest = LeftUp;
        public static readonly (int x, int y) SouthEast = RightDown;
        public static readonly (int x, int y) SouthWest = LeftDown;
    }
}
