namespace SandBoxEngine.Brushes
{
    public abstract class Brush
    {
        private int size;

        /// <summary>
        /// all (x, y) points that brush covers
        /// </summary>
        public List<(int,int)> Area { get; set; } = new List<(int, int)> ();

        public int Size {
            get => size;
            set
            {
                if (value != size && size >= 0)
                {
                    RecalculateArea(value);
                    size = value;
                }
            }
        }

        /// <summary>
        /// called every time the size changes
        /// </summary>
        /// <param name="size"></param>
        abstract protected void RecalculateArea(int size);
    }
}
