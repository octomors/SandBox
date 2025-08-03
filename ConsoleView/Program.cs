using SandBoxEngine;
using SandBoxEngine.Particles;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ConsoleView
{
    internal class Program
    {
        static private bool paused = false;
        /// <summary>
        /// pixels per symbol on X axis
        /// </summary>
        static private float AspectX = 0;
        /// <summary>
        /// pixels per symbol on Y axis
        /// </summary>
        static private float AspectY = 0;

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Console.WriteLine(
                "Maximize this window (F11) and press any key\n" +
                "Hints:\n" +
                "SpaceBar - stop processing\n" +
                "S - Place Sand\n" +
                "R - Place Rock\n");
            Console.ReadKey();
            Console.Clear();


            Renderer r = new Renderer();
            Engine engine = new Engine(209, 54, (o, args) => Console.WriteLine(args.Message));
            AspectX = 1920f / 209f;
            AspectY = 1080f / 54f;


            ConsoleKey key = ConsoleKey.None;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                    var MousePos = GetMouserPosition();

                    switch (key)
                    {
                        case ConsoleKey.Spacebar:
                            paused = !paused;
                            break;
                        case ConsoleKey.S:
                            engine.addParticle<Sand>((int)(MousePos.x / AspectX), (int)(MousePos.y / AspectY));
                            break;

                        case ConsoleKey.R:
                            engine.addParticle<Stone>((int)(MousePos.x / AspectX), (int)(MousePos.y / AspectY));
                            break;
                        case ConsoleKey.W:
                            engine.addParticle<Water>((int)(MousePos.x / AspectX), (int)(MousePos.y / AspectY));
                            break;


                    }
                }

                while (Console.KeyAvailable) Console.ReadKey(true);

                if (!paused)
                {
                    r.Render(engine.CalculateStep());
                }
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static (int x, int y) GetMouserPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            return (lpPoint.X, lpPoint.Y);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public static implicit operator Point(POINT point)
        {
            return new Point(point.X, point.Y);
        }
    }
}
