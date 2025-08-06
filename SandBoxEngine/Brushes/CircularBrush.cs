namespace SandBoxEngine.Brushes
{
    public class CircularBrush : Brush
    {
        public CircularBrush(int radius = 1)
        {
            Size = radius;
        }

        override protected void RecalculateArea(int radius)
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
