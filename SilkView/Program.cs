using SandBoxEngine;
using System.Diagnostics;

namespace SilkView
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer();
            Engine engine  = new Engine(192, 108, renderer, log);
        }

        private static void log(object sender, LogEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
