using SandBoxEngine;
using SandBoxEngine.Particles;

namespace ConsoleView
{
    internal class Renderer
    {
        private Dictionary<Type, ConsoleColor> colors = new Dictionary<Type, ConsoleColor>()
        {
            {typeof(Stone), ConsoleColor.DarkGray },
            {typeof(Sand), ConsoleColor.DarkYellow },
            {typeof(Water), ConsoleColor.Blue },
        };

        private Dictionary<Type, char> chars = new Dictionary<Type, char>()
        {
            {typeof(Stone), '#'},
            {typeof(Sand), '%' },
            {typeof(Water), '@' },
        };

        public void Render(Map map)
        {
            Console.SetCursorPosition(0, 0);

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
