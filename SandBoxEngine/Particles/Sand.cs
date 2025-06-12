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
        public override void Move(Particle[,] map, int x, int y)
        {
            if (map[y -1, x] == null)
            {

            }

        }
    }
}
