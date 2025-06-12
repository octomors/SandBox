using System.Drawing;

namespace SandBoxEngine.Particles
{
    public abstract class Particle
    {
        static protected Random random = new Random();


        /// <summary>
        /// for color randomization 
        /// </summary>
        protected int ColorOffset;

        private Color particleColor;
        public Color ParticleColor
        {
            get
            {
                return Color.FromArgb(particleColor.A,
                    particleColor.R + random.Next(-ColorOffset, ColorOffset),
                    particleColor.G + random.Next(-ColorOffset, ColorOffset),
                    particleColor.B + random.Next(-ColorOffset, ColorOffset));
            }

            protected set => particleColor = value;
        }

        public abstract void Move(Map map, int x, int y);
    }
}
