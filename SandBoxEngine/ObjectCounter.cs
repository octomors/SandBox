using SandBoxEngine.Particles;

namespace SandBoxEngine
{
    internal class ObjectCounter
    {
        /// <summary>
        /// Counts the number of ojects depicted on the map
        /// </summary>
        /// <param name="map"></param>
        /// <returns>Number of objects</returns>
        public int CountObjects(Map map)
        {
            int InteriorAngles = 0;
            int ExteriorAngles = 0;

            for(int y = 0; y < map.YLength - 1;  y++)
            {
                for(int x =0; x < map.XLength - 1; x++)
                {
                    //The number of pixels inside a 2x2 sliding window
                    int sum = 0;

                    if (map[y, x] is Stone) sum += 1;
                    if (map[y, x + 1] is Stone) sum += 1;
                    if (map[y + 1, x] is Stone) sum += 1;
                    if (map[y + 1, x + 1] is Stone) sum += 1;

                    //Identification of interior and exterior angles
                    if (sum == 3) InteriorAngles += 1;
                    if (sum == 1) ExteriorAngles += 1;
                }
            }

            return (ExteriorAngles - InteriorAngles) / 4;
        }
    }
}
