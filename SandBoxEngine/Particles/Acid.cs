namespace SandBoxEngine.Particles
{
    public class Acid : Liquid
    {
        public Acid()
        {
            Dispersion = 5;
        }

        public override void Move(Map map, int x, int y)
        {
            TryToMelt(map, y - 1, x);
            TryToMelt(map, y + 1, x);
            TryToMelt(map, y, x + 1);
            TryToMelt(map, y, x - 1);

            base.Move(map, x, y);
        }

        private void TryToMelt(Map map, int y, int x)
        {
            if (map[y, x] is Solid and not Glass or Powder)
            {
                map[y, x].MovesBeforeDisappearing = 3;
            }
        }
    }
}
