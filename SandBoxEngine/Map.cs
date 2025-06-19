using SandBoxEngine.Particles;

namespace SandBoxEngine
{
    public class Map
    {
        public Particle this[int y, int x]
        {
            get
            {
                if (y >= 0 && y < matrix.GetLength(0)
                    && x >= 0 && x < matrix.GetLength(1))
                    return matrix[y, x];
                else return null;
            }
            set
            {
                if (y >= 0 && y < matrix.GetLength(0)
                    && x >= 0 && x < matrix.GetLength(1))
                    matrix[y, x] = value;
            }
        }
        private Particle[,] matrix;

        public int XLength { get => matrix.GetLength(1); }
        public int YLength { get => matrix.GetLength(0); }


        public Map(int x, int y)
        {
            matrix = new Particle[y, x];
        }

        /// <summary>
        /// Swap 2 elements
        /// </summary>
        /// <param name="x1">1 element X coordinate</param>
        /// <param name="y1">1 element Y coordinate</param>
        /// <param name="x2">2 element X coordinate</param>
        /// <param name="y2">2 element Y coordinate</param>
        public void Swap(int x1, int y1, int x2, int y2)
        {
            if (y1 >= 0 && y1 < matrix.GetLength(0)
                    && x1 >= 0 && x1 < matrix.GetLength(1)
                    && y2 >= 0 && y2 < matrix.GetLength(0)
                    && x2 >= 0 && x2 < matrix.GetLength(1))
            {
                (matrix[y1, x1], matrix[y2, x2]) = (matrix[y2, x2], matrix[y1, x1]);
            }
        }
    }
}
