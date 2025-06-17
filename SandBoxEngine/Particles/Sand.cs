using System.Drawing;

namespace SandBoxEngine.Particles
{
    public class Sand : Powder
    {
        public Sand()
        {
            ParticleColor = Color.FromArgb(255, 242, 209, 107);
            this.ColorOffset = 10;
        }
    }
}
