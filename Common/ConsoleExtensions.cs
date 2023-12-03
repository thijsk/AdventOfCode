using System;
using System.Diagnostics;

namespace Common
{
    public static class ConsoleX
    {
        [Conditional("DEBUG")]
        public static void WriteLine(object value)
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine(value);
            }
        }

        [Conditional("DEBUG")]
        public static void Write(object value)
        {
            if (Debugger.IsAttached)
            {
                Console.Write(value);
            }
        }

        [Conditional("DEBUG")]
		public static void WriteLine(object value, ConsoleColor color)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = original;
        }

		[Conditional("DEBUG")]
		public static void Write(object value, ConsoleColor color)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ForegroundColor = original;
        }


        [Conditional("DEBUG")]
        public static void ReadLine()
        {
            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }

        [Conditional("DEBUG")]
		public static void WriteLine()
        {
            WriteLine("");
        }

		public static ConsoleColor ForegroundColor
		{
            get => Console.ForegroundColor;
            set => Console.ForegroundColor = value;
		}

		[Conditional("DEBUG")]
		public static void ResetColor()
		{
			Console.ResetColor();
		}
    }
}
