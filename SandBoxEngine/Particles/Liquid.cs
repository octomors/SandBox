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
            base.Move(map, x, y);

            if(Engine.random.Next(0, 2) == 1) // 50%
            {
                //Check left side
                (int newY, int newX) = CheckSide(map, x, y, -1);
                map.Swap(x, y, newX, newY);
            }
            else
            {
                //Check right side
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
            for(int x = currentX; x != currentX + (Dispersion + 1) * direction; x += direction)
            {
                //if the bottom one is not a barrier
                if (map[currentY + 1, x] is not Solid and not Powder and not Liquid)
                {
                    return (currentY + 1, x);
                }

                //if the adjacent one is a barrier
                if (map[currentY, x + direction] is Solid or Powder)
                {
                    return (currentY, x);
                }

                //passed if the bottom cell is a barrier and the adjacent one is not
            }

            //if all adjacent cells were not barriers, but all the bottom ones were
            return (currentY, currentX + (Dispersion + 1) * direction);
        }
    }
}
