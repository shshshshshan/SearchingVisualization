using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchAlgorithmVisualization.Searching
{
    public class Edge
    {
        public Node PointA { get; private set; }
        public Node PointB { get; private set; }

        public int Weight;

        public Edge(Node pointA, Node pointB, int weight) {

            // Set required attributes
            this.Weight = weight;

            // Point A will be the node label lesser than point B
            // Ex: In two edges points 'B' and 'C', point A will be = 'B' and point B will be = 'C'
            int stringDifference = String.Compare(pointA.Label, pointB.Label);

            if (stringDifference <= 0)
            {
                // Two edge points are equal, use any order
                // Same case with when point B is greater than point A
                this.PointA = pointA;
                this.PointB = pointB;
            }
            else
            {
                // Edge point A is greater than Edge point B
                this.PointA = pointB;
                this.PointB = pointA;
            }

        }

        // Determines whether a node is a member of the edge connection based on object address
        public bool HasMember(Node n)
        {
            return n == this.PointA || n == this.PointB;
        }

        // Determines whether a node is a member of the edge connection based on Node Id
        public bool HasMember(int id)
        {
            return id == this.PointA.Id || id == this.PointB.Id;
        }

        // Determines whether a node is a member of the edge connection based on Node Label
        public bool HasMember(string label)
        {
            return label == this.PointA.Label || label == this.PointB.Label;
        }

        // Override the Equals method to check for Edge equality via edge
        public override bool Equals(object? obj)
        {
            if (obj is Edge other) {
                return (this.PointA == other.PointA && this.PointB == other.PointB) ||
                    (this.PointA == other.PointB && this.PointB == other.PointA);
            }

            return false;
        }

        // Equals method to check for Edge equality via point labels
        public bool Equals(string pointALabel, string pointBLabel)
        {
            return (this.PointA.Label == pointALabel && this.PointB.Label == pointBLabel) ||
                (this.PointA.Label == pointBLabel && this.PointB.Label == pointALabel);
        }

        public override int GetHashCode()
        {
            return this.PointA.GetHashCode() ^ this.PointB.GetHashCode();
        }
    }
}
