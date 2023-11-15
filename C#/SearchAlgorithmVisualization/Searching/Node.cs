using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmVisualization.Searching
{
    public class Node
    {
        public string Label;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Radius { get; private set; }

        // Id here is not really unique, this is to identify that two node objects of the same label will 
        //   have the same id.
        // This is done to minimize confusion when reading code
        public int Id
        {
            get
            {
                return this.Label.GetHashCode();
            }
        }

        // Returns a globally unique ID or a GUID
        // This is used to reference that two nodes with the same label are different
        // Could be done by referencing object address (unsafe, not recommended)
        public string GUID { get; private set; }

        // Diameter of the node
        // Used for drawing in the canvas
        public int Diameter { get { return this.Radius * 2; } }

        public float Heuristics;

        // Determines if the user initially set the heuristic value or not
        // Used to manually calculate heuristics by the algorithm
        // This is so that user-set heuristics won't get overridden
        public bool IsDefaultHeuristicValue { get; private set; }

        public Node(string label, int x, int y, int radius, float? heuristics) {

            // Set required attributes
            this.Label = label;

            // Set utility attributes
            this.X = x;
            this.Y = y;
            this.Radius = radius;

            // Set optional attributes
            this.Heuristics = heuristics ?? -1;
            this.IsDefaultHeuristicValue = heuristics == null;

            // Set default attributes
            this.GUID = Guid.NewGuid().ToString();
        }

        public Node(Node copy)
        {
            // Set required attributes
            this.Label = copy.Label;

            // Set utility attributes
            this.X = copy.X;
            this.Y = copy.Y;
            this.Radius = copy.Radius;

            // Set optional attributes
            this.Heuristics = copy.Heuristics;
            this.IsDefaultHeuristicValue = copy.IsDefaultHeuristicValue;

            // Set default attributes
            this.GUID = Guid.NewGuid().ToString();
        }
    }
}
