using System.Drawing;

namespace SandBoxEngine.Particles
{
    internal class Sand : Powder
    {
        public Sand()
        {
            ParticleColor = Color.FromArgb(255, 242, 209, 107);
            this.ColorOffset = 10;
        }

        public override void Move(Map map, int x, int y)
        {
            //check the bottom one
            if (map[y - 1, x] is Solid || map[y - 1, x] is Powder)
            {
                //check the left bottom one
                if (map[y - 1, x - 1] is Solid || map[y - 1, x - 1] is Powder)
                {
                    //check the right bottom one
                    if (map[y - 1, x + 1] is Solid || map[y - 1, x + 1] is Powder)
                    {
                        return;
                    }
                    else
                    {
                        map.Swap(x, y, x + 1, y - 1);
                    }
                }
                else
                {
                    map.Swap(x, y, x - 1, y - 1);
                }
            }
            else
            {
                map.Swap(x, y, x, y - 1);
            }

        }
    }
}
