using SandBoxEngine;
using SandBoxEngine.Particles;

namespace ConsoleView
{
    internal class Renderer
    {
        private Dictionary<Type, ConsoleColor> colors = new Dictionary<Type, ConsoleColor>()
        {
            {typeof(Stone), ConsoleColor.DarkGray },
            {typeof(Glass), ConsoleColor.White },
            {typeof(Sand), ConsoleColor.DarkYellow },
            {typeof(Water), ConsoleColor.Blue },
            {typeof(Acid), ConsoleColor.Green },
        };

        private Dictionary<Type, char> chars = new Dictionary<Type, char>()
        {
            {typeof(Stone), '#'},
            {typeof(Glass), 'N'},
            {typeof(Sand), '%' },
            {typeof(Water), '@' },
            {typeof(Acid), '+' },
        };

        public void Render(Map map)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, 0);
            Console.Write(new String(' ', map.XLength));

            Console.SetCursorPosition(0, 0);
            Console.Write($"Number of objects: {map.ObjectNumber}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(0, 1);

            Type lastParticle = null;
            int stackLength = 0;

            for (int y = 0; y < map.YLength; y++)
            {
                for (int x = 0; x < map.XLength; x++)
                {
                    Type t = map[y, x]?.GetType();
                    if (lastParticle == null)
                    {
                        if (t == lastParticle)
                        {
                            stackLength++;
                        }
                        else
                        {
                            Console.Write(new String(' ', stackLength));
                            lastParticle = t;
                            stackLength = 1;
                        }
                    }
                    else
                    {
                        if (t == lastParticle)
                        {
                            stackLength++;
                        }
                        else
                        {
                            Console.ForegroundColor = colors[lastParticle];
                            Console.Write(new String(chars[lastParticle], stackLength));
                            lastParticle = t;
                            stackLength = 1;
                        }
                    }
                }
            }
        }
    }
}
