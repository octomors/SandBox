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
        private int movesBeforeDisappearing = -1;

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

        /// <summary>
        /// Number of moves before the particle self-destructs, can be set only once.
        /// </summary>
        public int MovesBeforeDisappearing
        { 
            get => movesBeforeDisappearing; 
            set
            {
                if(movesBeforeDisappearing == -1 )
                {
                    movesBeforeDisappearing = value;
                }
            }
        }
        public virtual void Move(Map map, int x, int y)
        {
            if (MovesBeforeDisappearing < 0)
            {
                return;
            }
            else if (MovesBeforeDisappearing == 0)
            {
                map[y, x] = null;
            }
            else
            {
                movesBeforeDisappearing--;
            }

        }
    }
}
