namespace SandBoxEngine.Particles
{
    public abstract class Powder : Particle
    {
        public override void Move(Map map, int x, int y)
        {

            //check the bottom one
            if (map[y + 1, x] is Solid or Powder)
            {
                //check the left bottom one
                if (map[y + 1, x - 1] is Solid or Powder)
                {
                    //check the right bottom one
                    if (map[y + 1, x + 1] is Solid or Powder)
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
