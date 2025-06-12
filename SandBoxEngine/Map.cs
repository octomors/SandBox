using SandBoxEngine.Particles;

namespace SandBoxEngine
{
    public class Map
    {
        public Particle this[int y, int x]
        {
            get { return matrix [y, x]; }
            set { matrix [y, x] = value; }
        }
        private Particle[,] matrix;


        public Map(int x, int y)
        {
            matrix = new Particle[x, y];
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
            (matrix[y1, x1], matrix[y2, x2]) = (matrix[y2, x2], matrix[y1, x1]);
        }

        public 
    }
}
