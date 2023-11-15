using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmVisualization.Helpers
{
    public class Helpers
    {
        public static int dist(int x1, int y1, int x2, int y2)
        {
            return (int)Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        public static Tuple<int, int> midpoint(int x1, int y1, int x2, int y2)
        {
            int midX = (x1 + x2) / 2;
            int midY = (y1 + y2) / 2;

            return Tuple.Create(midX, midY);
        }
    }
}
