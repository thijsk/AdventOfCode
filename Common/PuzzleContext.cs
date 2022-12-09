using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class PuzzleContext
    {
        private static string[] _input;
        private static string[] _example;

        public static string[] Input
        {
            get => UseExample ? Example : _input;
            set => _input = value;
        }

        public static string[] Example
        {
            get => _example;
            set => _example = value;
        }

        public static bool UseExample { get; set; }

        public static long Answer1 { get; set; }

        public static long Answer2 { get; set; }
    }
}
