﻿using System.Drawing;

namespace SandBoxEngine.Particles
{
    public class Stone : Solid
    {
        public Stone()
        {
            ParticleColor = Color.FromArgb(255, 136, 140, 141);
            this.ColorOffset = 10;
        }

        public override void Move(Map map, int x, int y)
        {
            return;
        }
    }
}
