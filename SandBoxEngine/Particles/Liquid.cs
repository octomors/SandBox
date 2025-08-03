using System.Reflection;
using System.Runtime.CompilerServices;

namespace SandBoxEngine.Particles
{
    public class Liquid : Particle
    {
        /// <summary>
        /// amount of particles for X axis movement per 1 step
        /// </summary>
        protected int Dispersion;

        public override void Move(Map map, int x, int y)
        {
            //Check the bottom one
            if (map[y + 1, x] == null)
            {
                map.Swap(x, y, x, y + 1);
            }

            if(Engine.random.Next(0, 2) == 1) // 50%
            {
                //Check left side
                (int newY, int newX) = CheckSide(map, x, y, -1);
                map.Swap(x, y, newX, newY);
            }
            else
            {
                //Check rigth side
                (int newY, int newX) = CheckSide(map, x, y, +1);
                map.Swap(x, y, newX, newY);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction">+1 to the right, -1 to the left</param>
        /// <returns>(y, x) next particle position</returns>
        private (int, int) CheckSide(Map map, int currentX, int currentY, int direction)
        {
            for(int i = currentX; i <= currentX + Dispersion * direction; i++)
            {
                if (map[currentY, i + 1] != null || map[currentY, i + 1] is not Stone || map[currentY, i + 1] is not Powder)
                {
                    return (currentY, i);
                }
                if (map[currentY + 1, i] == null)
                {
                    return (currentY + 1, i);
                }
            }
            return (currentY, currentX + Dispersion * direction);
        }
    }
}
