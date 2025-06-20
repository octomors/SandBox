using System.Reflection;

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
            //bottom
            if (map[y + 1, x] is null)
            {
                map.Swap(x, y, x, y + 1);
                return;
            }

            int leftTarget = x;
            int leftSteps = 0;

            CheckLeftSideSteps(map, x, y, ref leftTarget, ref leftSteps);

            int rightTarget = x;
            int rightSteps = 0;

            ChechRightSideSteps(map, x, y, ref rightTarget, ref rightSteps);

            if (leftSteps > 0 || rightSteps > 0)
            {
                if(leftSteps == rightSteps)
                {
                    if(Engine.random.Next(0,2) == 0)
                    {
                        map.Swap(x, y, leftTarget, y);
                    }
                    else
                    {
                        map.Swap(x, y, rightTarget, y);
                    }
                }
                else if (leftSteps > rightSteps)
                {
                    map.Swap(x, y, leftTarget, y);
                }
                else
                {
                    map.Swap(x, y, rightTarget, y);
                }
            }

        }

        private void CheckLeftSideSteps(Map map, int x, int y, ref int leftTarget, ref int leftSteps)
        {
            // left
            for (int i = 1; i <= Dispersion; i++)
            {
                int newX = x - i;

                if (newX < 0 || map[y, newX] != null)
                    break;

                if (map[y + 1, newX] != null)
                {
                    leftTarget = newX;
                    leftSteps = i;
                }
                else
                {
                    break;
                }
            }
        }

        private void ChechRightSideSteps(Map map, int x, int y, ref int rightTarget, ref int rightSteps)
        {
            // right
            for (int i = 1; i <= Dispersion; i++)
            {
                int newX = x + i;

                if (map[y, newX] != null)
                    break;

                if (map[y + 1, newX] != null)
                {
                    rightTarget = newX;
                    rightSteps = i;
                }
                else
                {
                    break;
                }
            }
        }
    }
}
