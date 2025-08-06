using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            for(int y = -(Size / 2); y < (Size / 2) + Size % 2; y++)
            {
                for(int x = - (Size / 2); x < (Size / 2) + Size % 2; x++)
                {
                    Area.Add((y, x));
                }
            }
        }
    }
}
