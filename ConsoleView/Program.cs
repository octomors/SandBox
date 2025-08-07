using SandBoxEngine;
using SandBoxEngine.Brushes;
using SandBoxEngine.Particles;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Markup;

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
            PrintTutorial();
            Console.ReadKey();
            Console.Clear();


            Renderer r = new Renderer();
            Engine engine = new Engine(209, 54, (o, args) => Console.WriteLine(args.Message));
            AspectX = 1920f / 209f;
            AspectY = 1080f / 54f;
            var selectedBrush = new SquareBrush();


            ConsoleKey key = ConsoleKey.None;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true).Key;
                    var MousePos = GetMouserPosition();
                    (int x, int y) selectedPoint = ((int)(MousePos.x / AspectX), (int)(MousePos.y / AspectY));

                    switch (key)
                    {
                        case ConsoleKey.Spacebar:
                            paused = !paused;
                            break;
                        case ConsoleKey.Delete:
                            engine.ClearMap();
                            break;
                        case ConsoleKey.UpArrow:
                            selectedBrush.Size += 1;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedBrush.Size -= 1;
                            break;

                        case ConsoleKey.V:
                            engine.DeleteParticle(selectedPoint.y, selectedPoint.x, selectedBrush);
                            break;
                        case ConsoleKey.S:
                            engine.addParticle<Sand>(selectedPoint.x, selectedPoint.y,  selectedBrush);
                            break;
                        case ConsoleKey.R:
                            engine.addParticle<Stone>(selectedPoint.x, selectedPoint.y, selectedBrush);
                            break;
                        case ConsoleKey.W:
                            engine.addParticle<Water>(selectedPoint.x, selectedPoint.y, selectedBrush);
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

        static public void PrintTutorial()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Maximize this window (F11) and press any key\n" +
                "Hints:\n" +
                "SpaceBar - Stop processing\n" +
                "Delete - Clear the map\n" +
                "Up and Down Arrows - change brush radius\n");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("V - Place Void\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("S - Place Sand\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("R - Place Rock\n");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("W - Place Water\n");

        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static (int x, int y) GetMouserPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            // If error handling is needed
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
