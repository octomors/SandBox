using SandBoxEngine;

namespace ConsoleView
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Maximize this window and press any key");
            Console.ReadKey();
            Console.Clear();

            Renderer r = new Renderer();
            Engine engine = new Engine(209,54, r, (o, args) => Console.WriteLine(args.Message));

            
            engine.Start();
        }
    }
}
