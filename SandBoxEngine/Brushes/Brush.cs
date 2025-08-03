namespace SandBoxEngine.Brushes
{
    public class Brush
    {
        /// <summary>
        /// all (x, y) points that brush covers
        /// </summary>
        public List<(int,int)> Area { get; set; } = new List<(int, int)> ();
    }
}
