using SandBoxEngine.Brushes;
using SandBoxEngine.Particles;
using System.ComponentModel;
using System.Diagnostics;

namespace SandBoxEngine
{
    public class Engine
    {
        static public Random random = new Random();

        public event EventHandler<LogEventArgs> log;

        private Map map;

        Stopwatch sw = new Stopwatch();
        /// <summary>
        /// minimum milliseconds of processing 1 frame
        /// </summary>
        private int minMsPerFrame;

        public Engine(int x, int y, EventHandler<LogEventArgs> logger, int maxFrameRate = 30)
        {
            map = new Map(x, y);
            log = logger;
            minMsPerFrame = 1000 / maxFrameRate;
        }

        public void addParticle<T>(int x, int y) where T : Particle, new()
        {
            map[y, x] = new T();
        }

        public void addParticle<T>(int x, int y, Brush brush) where T : Particle, new()
        {
            foreach((int YOffset, int XOffset) in brush.Area)
            {
                map[y + YOffset, x + XOffset] = new T();
            }
        }

        public Map CalculateStep()
        {
            sw.Start();

            for (int y = map.YLength - 1; y >= 0; y--)
            {
                for(int x = 0;  x < map.XLength; x++)
                {
                    if (map[y, x] == null)
                        continue;

                    map[y, x].Move(map, x, y);
                }
            }

            sw.Stop();
            if (sw.ElapsedMilliseconds < minMsPerFrame)
            {
                Thread.Sleep(minMsPerFrame - (int)sw.ElapsedMilliseconds);
            }
            sw.Reset();

            return map;
        }

        /// <summary>
        /// Deletes all particles from the map
        /// </summary>
        public void ClearMap()
        {
            map.Clear();
        }

        /// <summary>
        /// Deletes 1 particle from the map
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        public void DeleteParticle(int y, int x)
        {
            map.Delete(y, x);
        }
        /// <summary>
        /// Deletes all particles from the map within the brush area
        /// </summary>
        /// <param name="y"></param>
        /// <param name="x"></param>
        public void DeleteParticle(int y, int x, Brush brush)
        {
            foreach((int YOffset, int XOffset) in brush.Area)
            {
                map.Delete(y + YOffset, x + XOffset);
            }
        }
    }
}
