using System;
using System.Diagnostics;

namespace Common
{
    public static class ConsoleX
    {
        public static void WriteLine(object value)
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine(value);
            }
        }

        public static void ReadLine()
        {
            if (Debugger.IsAttached)
            {
                Console.ReadLine();
            }
        }

        public static void WriteLine()
        {
            WriteLine("");
        }
    }
}
