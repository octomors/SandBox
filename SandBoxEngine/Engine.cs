using SandBoxEngine.Particles;
using System.Diagnostics;

namespace SandBoxEngine
{
    public class Engine
    {
        public event EventHandler<LogEventArgs> log;

        private IRenderer renderer;

        private Map map;

        /// <summary>
        /// minimum milliseconds of processing 1 frame
        /// </summary>
        private int minMsPerFrame;

        public Engine(int x, int y, IRenderer renderer, EventHandler<LogEventArgs> logger, int maxFrameRate = 30)
        {
            map = new Map(x, y);
            this.renderer = renderer;
            log = logger;
            minMsPerFrame = 1000 / maxFrameRate;
        }

        public void Start()
        {
            Loop();
        }

        private void Loop()
        {
            Stopwatch sw = new Stopwatch();

            while(true)
            {
                sw.Start();

                CalculateStep();
                renderer.Render(map);

                sw.Stop();

                if(sw.ElapsedMilliseconds < minMsPerFrame)
                {
                    Thread.Sleep(minMsPerFrame -  (int)sw.ElapsedMilliseconds);
                }

                sw.Reset();
            }
        }

        private void CalculateStep()
        {
            for (int y = map.YLength - 1; y >= 0; y--)
            {
                for(int x = 0;  x < map.XLength; x++)
                {
                    if (map[y, x] == null)
                        continue;

                    map[y, x].Move(map, x, y);
                }
            }
        }
    }
}
