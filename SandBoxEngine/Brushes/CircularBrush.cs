namespace SandBoxEngine.Brushes
{
    public class CircularBrush : Brush
    {
        private int radius;

        public int Radius 
        {
            get => radius;
            set
            {
                if(radius != value && value > 0)
                {
                    RecalculateArea(value);
                    radius = value;
                }
            }
        }

        public CircularBrush(int radius = 1)
        {
            Radius = radius;
        }

        private void RecalculateArea(int radius)
        {
            Area.Clear();

            for (int y = -radius; y <= radius + 1; y++)
            {
                for (int x = -radius; x <= radius + 1; x++)
                {
                    if (Math.Pow(x, 2) + Math.Pow(y, 2) <= Math.Pow(radius, 2))
                    {
                        Area.Add((y, x));
                    }
                }
            }
        }
    }
}
