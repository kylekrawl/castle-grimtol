using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var startBackground = Console.BackgroundColor;
            var startForeground = Console.ForegroundColor;

            var game = new Game();
            
            Console.BackgroundColor = startBackground;
            Console.ForegroundColor = startForeground;
            Console.Clear();
        }
    }
}
