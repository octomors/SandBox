using SandBoxEngine;
using SandBoxEngine.Particles;
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
            Console.WriteLine("Maximize this window (F11) and press any key");
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

                    switch (key)
                    {
                        case ConsoleKey.Spacebar:
                            paused = !paused;
                            break;
                        case ConsoleKey.S:
                            var pos = GetMouserPosition();
                            Console.SetCursorPosition(0, 0);
                            Console.WriteLine(pos.y / AspectY);
                            engine.addParticle<Sand>((int)(pos.x / AspectX), (int)(pos.y / AspectY));
                            break;

                        case ConsoleKey.D:
                            var pos1 = GetMouserPosition();
                            engine.addParticle<Stone>((int)(pos1.x / AspectX), (int)(pos1.y / AspectY));
                            break;


                    }
                }

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
