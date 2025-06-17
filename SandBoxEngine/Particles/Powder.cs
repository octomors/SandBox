namespace SandBoxEngine.Particles
{
    public abstract class Powder : Particle
    {
        public override void Move(Map map, int x, int y)
        {
            if (y + 1 < 0)
                return;

            //check the bottom one
            if (map[y + 1, x] is Solid || map[y + 1, x] is Powder)
            {
                //check the left bottom one
                if (map[y + 1, x - 1] is Solid || map[y + 1, x - 1] is Powder)
                {
                    //check the right bottom one
                    if (map[y + 1, x + 1] is Solid || map[y + 1, x + 1] is Powder)
                    {
                        return;
                    }
                    else
                    {
                        map.Swap(x, y, x + 1, y + 1);
                    }
                }
                else
                {
                    map.Swap(x, y, x - 1, y + 1);
                }
            }
            else
            {
                map.Swap(x, y, x, y + 1);
            }
        }
    }
}
