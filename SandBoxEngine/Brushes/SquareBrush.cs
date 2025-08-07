namespace SandBoxEngine.Brushes
{
    public class SquareBrush : Brush
    {
        public SquareBrush(int size = 2)
        {
            this.Size = size;
        }

        protected override void RecalculateArea(int size)
        {
            Area.Clear();

            for (int y =  -(size / 2); y < size / 2 + size % 2; y++)
            {
                for (int x = -(size / 2); x < size / 2 + size % 2; x++)
                {
                    Area.Add((y, x));
                }
            }
        }
    }
}
