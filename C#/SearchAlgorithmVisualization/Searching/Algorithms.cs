using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SearchAlgorithmVisualization.Searching
{
    public class Algorithms
    {
        private List<Node> Nodes;
        private List<Edge> Edges;

        public Algorithms(List<Node> nodes, List<Edge> edges) 
        {
            if (nodes == null || edges == null)
            {
                throw new Exception("Cannot instantiate algorithm class with null nodes or edges");
            }

            this.Nodes = nodes;
            this.Edges = edges;
        }

        // Utility function to sort given edges by lexical representation of label
        // Ex: 'B' > 'A', 'Z' < 'AA', 'AA' > 'Y', 'BA' > 'AZ'
        private static List<Edge> SortEdges(List<Edge> edges)
        {
            return edges.OrderBy(e => e.PointA.Label.Length)
                        .ThenBy(e => e.PointA.Label)
                        .ThenBy(e => e.PointB.Label.Length)
                        .ThenBy(e => e.PointB.Label).ToList();
        }

        // Utility function to sort given nodes by:
        //   Label Length
        //   Lexical representation of label
        //     Ex: 'B' > 'A', 'Z' < 'AA', 'AA' > 'Y', 'BA' > 'AZ'
        private static List<Node> SortNodes(List<Node> nodes)
        {
            return nodes.OrderBy(n => n.Label.Length)
                        .ThenBy(n => n.Label).ToList();
        }

        // Utility function to create an adjacency list given nodes and edges (optional)
        private static Dictionary<Node, List<Tuple<Node, int>>> AdjacencyList(List<Node> nodes, List<Edge>? edges = null)
        {
            if (nodes == null || nodes.Count == 0)
            {
                throw new Exception("Cannot create adjacency list of 0 nodes.");
            }

            Dictionary<Node, List<Tuple<Node, int>>> list = new Dictionary<Node, List<Tuple<Node, int>>>();

            // Initialize default adjacency list values
            foreach (Node n in nodes)
            {
                list[n] = new List<Tuple<Node, int>>();
            }

            // Return with default list values when there are no given edges
            if (edges == null) return list;

            // Add edge weights in the adjacency matrix
            foreach (Edge e in edges)
            {
                list[e.PointA].Add(Tuple.Create(e.PointB, e.Weight));
                list[e.PointB].Add(Tuple.Create(e.PointA, e.Weight));
            }

            return list;
        }

        // Utility function to create an adjacency matrix given nodes and edges (optional)
        private static int[,] AdjacencyMatrix(List<Node> nodes, List<Edge>? edges = null)
        {
            if (nodes == null || nodes.Count == 0)
            {
                throw new Exception("Cannot create adjacency matrix of 0 nodes.");
            }

            int matrixDimension = nodes.Count;
            int[,] matrix = new int[matrixDimension, matrixDimension];

            // Initialize default adjacency matrix values
            for (int i = 0; i < matrixDimension; i++)
            {
                for (int j = 0; j < matrixDimension; j++)
                {
                    if (i == j) matrix[i, j] = 0;
                    else matrix[i, j] = -1; 
                }
            }

            // Return with default list values when there are no given edges
            if (edges == null) return matrix;

            // Add edge weights in the adjacency matrix
            foreach (Edge e in edges)
            {
                matrix[e.PointA.Id, e.PointB.Id] = e.Weight;
                matrix[e.PointB.Id, e.PointA.Id] = e.Weight;
            }

            return matrix;
        }

        // Original Space-State-Search DFS (no logs)
        public List<Node> DFS(Node start, Node? end = null)
        {
            if (start == null)
            {
                throw new Exception("Empty start node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (end != null && !this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the stack
            Stack<Node> stack = new Stack<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially push the starting node to the stack
            stack.Push(start);

            // Initialize the starting node's ancestry 
            ancestry[stack.Peek().GUID] = new List<string>();

            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                path.Add(node);

                if (end != null && node.Label == end.Label) break;

                // Loop the edges backwards to traverse the graph left-to-right (alphabetically)
                for (int i = edges.Count - 1; i >= 0; i--)
                {
                    Edge e = edges[i];

                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Add it to the stack
                    stack.Push(copy);
                }
            }

            return path;
        }

        // Space-State-Search DFS with logs
        public Dictionary<string, object> DFSWithLogs(Node start, Node? end = null)
        {
            if (start == null)
            {
                throw new Exception("Empty start node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (end != null && !this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Initialize result container
            // Returned: { 'path' : List<Node>,
            //             '0' : Dictionary<string, object> # Iteration 1 with value of Dictionary<string, object>,
            //             ... : Dictionary<string, object> # Iteration N with value of Dictionary<string, object> }
            // 
            // Each iteration's value in the result container is a dictionary with the following structure:
            // ['container'] = contents (list<nodes>) of the container (stack/queue) at current iteration 
            // ['size'] = size of the container (stack/queue) at current iteration
            // ['pathElements'] = list of node labels (current node's path) (simply loops back the ancestry of the current node)
            // ['pathCost'] = the cost of the current path (not applicable for this algorithm)
            // ['paths'] = list of nodes (all available paths from current node)
            // ['n_paths'] = number of available paths from current node
            Dictionary<string, object> result = new Dictionary<string, object>();

            // Iteration counter
            int iteration = 0;

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the stack
            Stack<Node> stack = new Stack<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially push the starting node to the stack
            stack.Push(start);

            // Initialize the starting node's ancestry 
            ancestry[stack.Peek().GUID] = new List<string>();

            while (stack.Count > 0)
            {
                // Iteration values
                Dictionary<string, object> iterDict = new Dictionary<string, object>();

                Node node = stack.Pop();
                path.Add(node);

                // ['size']
                iterDict["size"] = stack.Count;

                // Create List<string> for path elements 
                // Include current node to path
                List<string> pathElements = new List<string> { node.Label };

                foreach (string ancestor in ancestry[node.GUID])
                {
                    pathElements.Add(ancestor);
                }

                // ['pathElements']
                iterDict["pathElements"] = pathElements;

                // Set path cost but infinite value
                iterDict["pathCost"] = float.PositiveInfinity;

                // ['paths']
                // Initialize possible paths from current node here to create a default paths (list<nodes>) list
                iterDict["paths"] = new List<string>();

                // Initialize number of possible paths from current node here to create a default value
                iterDict["n_paths"] = 0;

                if (end != null && node.Label == end.Label) break;

                // Loop the edges backwards to traverse the graph left-to-right (alphabetically)
                for (int i = edges.Count - 1; i >= 0; i--)
                {
                    Edge e = edges[i];

                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Add the other as part of current node's possible path list
                    ((List<string>)(iterDict["paths"])).Add(other.Label);

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Add it to the stack
                    stack.Push(copy);
                }

                // ['container']
                // Update container after adding all adjacent nodes
                iterDict["container"] = stack.ToList();

                // Update number of possible paths after traversing all adjacent nodes
                iterDict["n_paths"] = ((List<string>)(iterDict["paths"])).Count;

                // Add the iteration dictionary to the corresponding index
                result[iteration.ToString()] = iterDict;

                // Update iteration
                iteration++;
            }

            // Add the final resulting path to the result container
            result["path"] = path;

            return result;
        }

        // Space-State-Search BFS (no logs)
        public List<Node> BFS(Node start, Node? end = null)
        {
            if (start == null)
            {
                throw new Exception("Empty start node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (end != null && !this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the queue
            Queue<Node> queue = new Queue<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially enqueue the starting node
            queue.Enqueue(start);

            // Initialize the starting node's ancestry 
            ancestry[queue.Peek().GUID] = new List<string>();

            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                path.Add(node);

                if (end != null && node.Label == end.Label) break;

                // Loop each edge
                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Add it to the queue
                    queue.Enqueue(copy);
                }
            }

            return path;
        }

        // Space-State-Search BFS with logs
        public Dictionary<string, object> BFSWithLogs(Node start, Node? end = null)
        {
            if (start == null)
            {
                throw new Exception("Empty start node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (end != null && !this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Initialize result container
            // Returned: { 'path' : List<Node>,
            //             '0' : Dictionary<string, object> # Iteration 1 with value of Dictionary<string, object>,
            //             ... : Dictionary<string, object> # Iteration N with value of Dictionary<string, object> }
            // 
            // Each iteration's value in the result container is a dictionary with the following structure:
            // ['container'] = contents (list<nodes>) of the container (stack/queue) at current iteration 
            // ['size'] = size of the container (stack/queue) at current iteration
            // ['pathElements'] = list of node labels (current node's path) (simply loops back the ancestry of the current node)
            // ['pathCost'] = the cost of the current path (not applicable for this algorithm)
            // ['paths'] = list of nodes (all available paths from current node)
            // ['n_paths'] = number of available paths from current node
            Dictionary<string, object> result = new Dictionary<string, object>();

            // Iteration counter
            int iteration = 0;

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the queue
            Queue<Node> queue = new Queue<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially push the starting node to the queue
            queue.Enqueue(start);

            // Initialize the starting node's ancestry 
            ancestry[queue.Peek().GUID] = new List<string>();

            while (queue.Count > 0)
            {
                // Iteration values
                Dictionary<string, object> iterDict = new Dictionary<string, object>();

                Node node = queue.Dequeue();
                path.Add(node);

                // ['size']
                iterDict["size"] = queue.Count;

                // Create List<string> for path elements 
                // Include current node to path
                List<string> pathElements = new List<string> { node.Label };

                foreach (string ancestor in ancestry[node.GUID])
                {
                    pathElements.Add(ancestor);
                }

                // ['pathElements']
                iterDict["pathElements"] = pathElements;

                // Set path cost but infinite value
                iterDict["pathCost"] = float.PositiveInfinity;

                // ['paths']
                // Initialize possible paths from current node here to create a default paths (list<node>) list
                iterDict["paths"] = new List<string>();

                // Initialize number of possible paths from current node here to create a default value
                iterDict["n_paths"] = 0;

                if (end != null && node.Label == end.Label) break;

                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Add the other as part of current node's possible path list
                    ((List<string>)(iterDict["paths"])).Add(other.Label);

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Add it to the queue
                    queue.Enqueue(copy);
                }

                // ['container']
                // Update container after adding all adjacent nodes
                iterDict["container"] = queue.ToList();

                // Update number of possible paths after traversing all adjacent nodes
                iterDict["n_paths"] = ((List<string>)(iterDict["paths"])).Count;

                // Add the iteration dictionary to the corresponding index
                result[iteration.ToString()] = iterDict;

                // Update iteration
                iteration++;
            }

            // Add the final resulting path to the result container
            result["path"] = path;

            return result;
        }

        // Utility function: Sorted Insertion for nodes
        // Returns the index of the correct placement for the new node
        // Sort order is by Node.Heuristics then by Node.Label.Length then by Node.Label
        // Sort oder is least to greatest
        // Refer to "SortNodes" method for the more readable ordering
        private int SortedInsertionIndexBeam(List<Node> nodes, Node node)
        {
            int insertIndex = 0;

            while (insertIndex < nodes.Count &&
                   !(node.Heuristics.CompareTo(nodes[insertIndex].Heuristics) < 0 ||
                    (node.Heuristics == nodes[insertIndex].Heuristics &&
                     (node.Label.Length < nodes[insertIndex].Label.Length ||
                      (node.Label.Length == nodes[insertIndex].Label.Length &&
                       string.Compare(node.Label, nodes[insertIndex].Label) < 0)))))
            {
                insertIndex++;
            }

            return insertIndex;
        }

        public List<Node> Beam(Node start, Node end, int coefficient)
        {
            // Assign correct heuristics unless custom heuristics
            // Set default heuristics for unset nodes
            foreach (Node n in this.Nodes)
            {
                if (!n.IsDefaultHeuristicValue) continue;

                // Calculate the distance of each node to the destination node
                int distance = Helpers.Helpers.dist(n.X, n.Y, end.X, end.Y);

                n.Heuristics = distance;
            }

            if (coefficient < 0)
            {
                throw new Exception("Beam coefficient must be a non-negative integer.");
            }

            // Return a path to self only if coefficient is 0
            else if (coefficient == 0) return new List<Node> { start };

            if (start == null)
            {
                throw new Exception("Empty start node");
            } else if (end == null)
            {
                throw new Exception("Empty destination node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (!this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the list as a list queue
            List<Node> listQueue = new List<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially enqueue the starting node
            listQueue.Add(start);

            // Initialize the starting node's ancestry 
            ancestry[listQueue[0].GUID] = new List<string>();

            while (listQueue.Count > 0)
            {
                Node node = listQueue[0];
                listQueue.RemoveAt(0);
                path.Add(node);

                if (node.Label == end.Label) break;

                // Loop each edge
                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Add it to the listQueue
                    // Addition of new node is sorted upon addition
                    // Sort key is first by node heuristic cost then by node name length then by node name
                    int insertIndex = this.SortedInsertionIndexBeam(listQueue, copy);
                    listQueue.Insert(insertIndex, copy);
                }

                // After enqueuing, prune the branches to W branches
                if (listQueue.Count > coefficient)
                {
                    listQueue.RemoveRange(coefficient, listQueue.Count - coefficient);
                }
            }

            return path;
        }

        // Beam Search with logs
        public Dictionary<string, object> BeamWithLogs(Node start, Node end, int coefficient)
        {

            // Assign correct heuristics unless custom heuristics
            // Set default heuristics for unset nodes
            foreach (Node n in this.Nodes)
            {
                if (!n.IsDefaultHeuristicValue) continue;

                // Calculate the distance of each node to the destination node
                int distance = Helpers.Helpers.dist(n.X, n.Y, end.X, end.Y);

                n.Heuristics = distance;
            }

            if (coefficient < 0)
            {
                throw new Exception("Beam coefficient must be a non-negative integer.");
            }

            // Return a path to self only if coefficient is 0
            else if (coefficient == 0) return new Dictionary<string, object> {
                { "path", new List<Node> { start } }
            };

            if (start == null)
            {
                throw new Exception("Empty start node");
            }
            else if (end == null)
            {
                throw new Exception("Empty destination node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (!this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Initialize result container
            // Returned: { 'path' : List<Node>,
            //             '0' : Dictionary<string, object> # Iteration 1 with value of Dictionary<string, object>,
            //             ... : Dictionary<string, object> # Iteration N with value of Dictionary<string, object> }
            // 
            // Each iteration's value in the result container is a dictionary with the following structure:
            // ['container'] = contents (list<nodes>) of the container (stack/queue) at current iteration 
            // ['size'] = size of the container (stack/queue) at current iteration
            // ['pathElements'] = list of node labels (current node's path) (simply loops back the ancestry of the current node)
            // ['pathCost'] = the cost of the current path (not applicable for this algorithm)
            // ['paths'] = list of nodes (all available paths from current node)
            // ['n_paths'] = number of available paths from current node
            Dictionary<string, object> result = new Dictionary<string, object>();

            // Iteration counter
            int iteration = 0;

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the list as a list queue
            List<Node> listQueue = new List<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially enqueue the starting node
            listQueue.Add(start);

            // Initialize the starting node's ancestry 
            ancestry[listQueue[0].GUID] = new List<string>();

            while (listQueue.Count > 0)
            {
                // Iteration values
                Dictionary<string, object> iterDict = new Dictionary<string, object>();

                Node node = listQueue[0];
                listQueue.RemoveAt(0);
                path.Add(node);

                // ['size']
                iterDict["size"] = listQueue.Count;

                // Create List<string> for path elements 
                // Include current node to path
                List<string> pathElements = new List<string> { node.Label };

                foreach (string ancestor in ancestry[node.GUID])
                {
                    pathElements.Add(ancestor);
                }

                // ['pathElements']
                iterDict["pathElements"] = pathElements;

                // Set path cost to node's heuristics for beam
                iterDict["pathCost"] = node.Heuristics;

                // ['paths']
                // Initialize possible paths from current node here to create a default paths (list<node>) list
                iterDict["paths"] = new List<string>();

                // Initialize number of possible paths from current node here to create a default value
                iterDict["n_paths"] = 0;

                if (node.Label == end.Label) break;

                // Loop each edge
                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Add the other as part of current node's possible path list
                    ((List<string>)(iterDict["paths"])).Add(other.Label);

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Add it to the listQueue
                    // Addition of new node is sorted upon addition
                    // Sort key is first by node heuristic cost then by node name length then by node name
                    int insertIndex = this.SortedInsertionIndexBeam(listQueue, copy);
                    listQueue.Insert(insertIndex, copy);
                }

                // After enqueuing, prune the branches to W branches
                if (listQueue.Count > coefficient)
                {
                    listQueue.RemoveRange(coefficient, listQueue.Count - coefficient);
                }

                // ['container']
                // Update container after adding all adjacent nodes
                iterDict["container"] = listQueue.ToList();

                // Update number of possible paths after traversing all adjacent nodes
                iterDict["n_paths"] = ((List<string>)(iterDict["paths"])).Count;

                // Add the iteration dictionary to the corresponding index
                result[iteration.ToString()] = iterDict;

                // Update iteration
                iteration++;
            }

            // Add the final resulting path to the result container
            result["path"] = path;

            return result;
        }

        // Sorted Insertion for branch and bound
        // The sort order is different from beam
        // Sort order is firt by accumulated weight, then by node label length, then by node label
        // Returns the correct index where it should be added
        private int SortedInsertionIndex(List<Node> nodes, Node node, Dictionary<string, float> accumulatedWeight)
        {
            int insertIndex = 0;

            while (insertIndex < nodes.Count &&
                   !(accumulatedWeight[node.GUID].CompareTo(accumulatedWeight[nodes[insertIndex].GUID]) < 0 ||
                    (accumulatedWeight[node.GUID] == accumulatedWeight[nodes[insertIndex].GUID] &&
                     (node.Label.Length < nodes[insertIndex].Label.Length ||
                      (node.Label.Length == nodes[insertIndex].Label.Length &&
                       string.Compare(node.Label, nodes[insertIndex].Label) < 0)))))
            {
                insertIndex++;
            }


            return insertIndex;
        }

        public List<Node> BranchAndBound(Node start, Node? end = null)
        {
            if (start == null)
            {
                throw new Exception("Empty start node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (end != null && !this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the list
            List<Node> listQueue = new List<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();
            
            // Keep track of the accumulated weight of each nodes
            Dictionary<string, float> accumulatedWeight = new Dictionary<string, float>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially enqueue the starting node
            listQueue.Add(start);

            // Initialize the starting node's ancestry 
            ancestry[listQueue[0].GUID] = new List<string>();

            // Initialize the starting node's accumulated weight
            accumulatedWeight[listQueue[0].GUID] = 0;

            while (listQueue.Count > 0)
            {
                Node node = listQueue[0];
                listQueue.RemoveAt(0);
                path.Add(node);

                if (end != null && node.Label == end.Label) break;

                // Loop each edge
                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Accumulated weight for current adjacent node = current node accumulated weight + current adjancent node accumulated weight
                    accumulatedWeight[copy.GUID] = (int)(e.Weight + accumulatedWeight[node.GUID]);

                    // Add the copy node in a sorted manner
                    // The sort order is first by accumulated weight, then by node label length, then by name
                    int insertIndex = this.SortedInsertionIndex(listQueue, copy, accumulatedWeight);
                    listQueue.Insert(insertIndex, copy);
                }
            }

            return path;
        }

        public Dictionary<string, object> BranchAndBoundWithLogs(Node start, Node? end = null)
        {
            if (start == null)
            {
                throw new Exception("Empty start node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (end != null && !this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Initialize result container
            // Returned: { 'path' : List<Node>,
            //             '0' : Dictionary<string, object> # Iteration 1 with value of Dictionary<string, object>,
            //             ... : Dictionary<string, object> # Iteration N with value of Dictionary<string, object> }
            // 
            // Each iteration's value in the result container is a dictionary with the following structure:
            // ['container'] = contents (list<nodes>) of the container (stack/queue) at current iteration 
            // ['size'] = size of the container (stack/queue) at current iteration
            // ['pathElements'] = list of node labels (current node's path) (simply loops back the ancestry of the current node)
            // ['pathCost'] = the cost of the current path (not applicable for this algorithm)
            // ['paths'] = list of nodes (all available paths from current node)
            // ['n_paths'] = number of available paths from current node
            Dictionary<string, object> result = new Dictionary<string, object>();

            // Iteration counter
            int iteration = 0;

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the list
            List<Node> listQueue = new List<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the accumulated weight of each nodes
            // Float is used to match the logs data type
            Dictionary<string, float> accumulatedWeight = new Dictionary<string, float>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially enqueue the starting node
            listQueue.Add(start);

            // Initialize the starting node's ancestry 
            ancestry[listQueue[0].GUID] = new List<string>();

            // Initialize the starting node's accumulated weight
            accumulatedWeight[listQueue[0].GUID] = 0;

            while (listQueue.Count > 0)
            {
                // Iteration values
                Dictionary<string, object> iterDict = new Dictionary<string, object>();

                Node node = listQueue[0];
                listQueue.RemoveAt(0);
                path.Add(node);

                // ['size']
                iterDict["size"] = listQueue.Count;

                // Create List<string> for path elements 
                // Include current node to path
                List<string> pathElements = new List<string> { node.Label };

                foreach (string ancestor in ancestry[node.GUID])
                {
                    pathElements.Add(ancestor);
                }

                // ['pathElements']
                iterDict["pathElements"] = pathElements;

                // Set path cost to accumulated path cost
                iterDict["pathCost"] = accumulatedWeight[node.GUID];

                // ['paths']
                // Initialize possible paths from current node here to create a default paths (list<node>) list
                iterDict["paths"] = new List<string>();

                // Initialize number of possible paths from current node here to create a default value
                iterDict["n_paths"] = 0;

                if (end != null && node.Label == end.Label) break;

                // Loop each edge
                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Add the other as part of current node's possible path list
                    ((List<string>)(iterDict["paths"])).Add(other.Label);

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Accumulated weight for current adjacent node = edge weight + current adjancent node accumulated weight
                    accumulatedWeight[copy.GUID] = (int)(e.Weight + accumulatedWeight[node.GUID]);

                    // Add the copy node in a sorted manner
                    // The sort order is first by accumulated weight, then by node label length, then by name
                    int insertIndex = this.SortedInsertionIndex(listQueue, copy, accumulatedWeight);
                    listQueue.Insert(insertIndex, copy);
                }

                // ['container']
                // Update container after adding all adjacent nodes
                iterDict["container"] = listQueue.ToList();

                // Update number of possible paths after traversing all adjacent nodes
                iterDict["n_paths"] = ((List<string>)(iterDict["paths"])).Count;

                // Add the iteration dictionary to the corresponding index
                result[iteration.ToString()] = iterDict;

                // Update iteration
                iteration++;
            }

            // Add the final resulting path to the result container
            result["path"] = path;

            return result;
        }

        public List<Node> AStar(Node start, Node end)
        {
            // Assign correct heuristics unless custom heuristics
            // Set default heuristics for unset nodes
            foreach (Node n in this.Nodes)
            {
                if (!n.IsDefaultHeuristicValue) continue;

                // Calculate the distance of each node to the destination node
                int distance = Helpers.Helpers.dist(n.X, n.Y, end.X, end.Y);

                n.Heuristics = distance;
            }

            if (start == null)
            {
                throw new Exception("Empty start node");
            }
            else if (end == null)
            {
                throw new Exception("Empty destination node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (!this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the list
            List<Node> listQueue = new List<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the accumulated g-cost of the node as well as the f-cost
            Dictionary<string, int> accumulatedGCost = new Dictionary<string, int>();
            Dictionary<string, float> accumulatedFCost = new Dictionary<string, float>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially enqueue the starting node
            listQueue.Add(start);

            // Initialize the starting node's ancestry 
            ancestry[listQueue[0].GUID] = new List<string>();

            // Initialize the starting node's g-cost and f-cost
            accumulatedGCost[listQueue[0].GUID] = 0;
            accumulatedFCost[listQueue[0].GUID] = 0;

            while (listQueue.Count > 0)
            {
                Node node = listQueue[0];
                listQueue.RemoveAt(0);
                path.Add(node);

                if (end != null && node.Label == end.Label) break;

                // Loop each edge
                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Update accumulated g-cost
                    accumulatedGCost[copy.GUID] = (int)(e.Weight + accumulatedGCost[node.GUID]);

                    // Update the h-cost
                    accumulatedFCost[copy.GUID] = (accumulatedGCost[copy.GUID] + copy.Heuristics);

                    // Add the copy node in a sorted manner
                    // The sort order is first by accumulated weight, then by node label length, then by name
                    int insertIndex = this.SortedInsertionIndex(listQueue, copy, accumulatedFCost);
                    listQueue.Insert(insertIndex, copy);
                }
            }

            return path;
        }

        public Dictionary<string, object> AStarWithLogs(Node start, Node end)
        {
            // Assign correct heuristics unless custom heuristics
            // Set default heuristics for unset nodes
            foreach (Node n in this.Nodes)
            {
                if (!n.IsDefaultHeuristicValue) continue;

                // Calculate the distance of each node to the destination node
                int distance = Helpers.Helpers.dist(n.X, n.Y, end.X, end.Y);

                n.Heuristics = distance;
            }

            if (start == null)
            {
                throw new Exception("Empty start node");
            }
            else if (end == null)
            {
                throw new Exception("Empty destination node");
            }

            if (!this.Nodes.Contains(start))
            {
                throw new Exception("Starting node is not a part of the graph.");
            }
            else if (!this.Nodes.Contains(end))
            {
                throw new Exception("Destination node is not part of the graph.");
            }

            // Initialize result container
            // Returned: { 'path' : List<Node>,
            //             '0' : Dictionary<string, object> # Iteration 1 with value of Dictionary<string, object>,
            //             ... : Dictionary<string, object> # Iteration N with value of Dictionary<string, object> }
            // 
            // Each iteration's value in the result container is a dictionary with the following structure:
            // ['container'] = contents (list<nodes>) of the container (stack/queue) at current iteration 
            // ['size'] = size of the container (stack/queue) at current iteration
            // ['pathElements'] = list of node labels (current node's path) (simply loops back the ancestry of the current node)
            // ['pathCost'] = the cost of the current path (not applicable for this algorithm)
            // ['paths'] = list of nodes (all available paths from current node)
            // ['n_paths'] = number of available paths from current node
            Dictionary<string, object> result = new Dictionary<string, object>();

            // Iteration counter
            int iteration = 0;

            // Sort the edges
            List<Edge> edges = SortEdges(this.Edges);

            // Keep track of the nodes in the list
            List<Node> listQueue = new List<Node>();

            // Keep track of the ancestry of each nodes
            Dictionary<string, List<string>> ancestry = new Dictionary<string, List<string>>();

            // Keep track of the accumulated g-cost of the node as well as the f-cost
            Dictionary<string, int> accumulatedGCost = new Dictionary<string, int>();
            Dictionary<string, float> accumulatedFCost = new Dictionary<string, float>();

            // Keep track of the path
            List<Node> path = new List<Node>();

            // Initially enqueue the starting node
            listQueue.Add(start);

            // Initialize the starting node's ancestry 
            ancestry[listQueue[0].GUID] = new List<string>();

            // Initialize the starting node's g-cost and f-cost
            accumulatedGCost[listQueue[0].GUID] = 0;
            accumulatedFCost[listQueue[0].GUID] = 0;

            while (listQueue.Count > 0)
            {
                // Iteration values
                Dictionary<string, object> iterDict = new Dictionary<string, object>();

                Node node = listQueue[0];
                listQueue.RemoveAt(0);
                path.Add(node);

                // ['size']
                iterDict["size"] = listQueue.Count;

                // Create List<string> for path elements 
                // Include current node to path
                List<string> pathElements = new List<string> { node.Label };

                foreach (string ancestor in ancestry[node.GUID])
                {
                    pathElements.Add(ancestor);
                }

                // ['pathElements']
                iterDict["pathElements"] = pathElements;

                // Set path cost to the total f-cost
                iterDict["pathCost"] = accumulatedFCost[node.GUID];

                // ['paths']
                // Initialize possible paths from current node here to create a default paths (list<node>) list
                iterDict["paths"] = new List<string>();

                // Initialize number of possible paths from current node here to create a default value
                iterDict["n_paths"] = 0;

                if (end != null && node.Label == end.Label) break;

                // Loop each edge
                foreach (Edge e in edges)
                {
                    // Skip if current edge does not contain the current node
                    if (!e.HasMember(node.Label)) continue;

                    // Node of the other point of the edge connection based on label
                    // Since node can be from different address
                    Node other = node.Label == e.PointA.Label ? e.PointB : e.PointA;

                    // Check the label of the other edge if it is an ancestor of the current node
                    // Skip if present
                    if (ancestry[node.GUID].Contains(other.Label)) continue;

                    // Add the other as part of current node's possible path list
                    ((List<string>)(iterDict["paths"])).Add(other.Label);

                    // Copy the other edge of the node to create a new reference
                    Node copy = new Node(other);

                    // Initialize the copy's ancestry
                    // Add over the ancestry of the current node and the current node itself
                    ancestry[copy.GUID] = new List<string>
                    {
                        node.Label
                    };
                    ancestry[copy.GUID].AddRange(ancestry[node.GUID]);

                    // Update accumulated g-cost
                    accumulatedGCost[copy.GUID] = (int)(e.Weight + accumulatedGCost[node.GUID]);

                    // Update the h-cost
                    accumulatedFCost[copy.GUID] = (accumulatedGCost[copy.GUID] + copy.Heuristics);

                    // Add the copy node in a sorted manner
                    // The sort order is first by accumulated weight, then by node label length, then by name
                    int insertIndex = this.SortedInsertionIndex(listQueue, copy, accumulatedFCost);
                    listQueue.Insert(insertIndex, copy);
                }

                // ['container']
                // Update container after adding all adjacent nodes
                iterDict["container"] = listQueue.ToList();

                // Update number of possible paths after traversing all adjacent nodes
                iterDict["n_paths"] = ((List<string>)(iterDict["paths"])).Count;

                // Add the iteration dictionary to the corresponding index
                result[iteration.ToString()] = iterDict;

                // Update iteration
                iteration++;
            }

            // Add the final resulting path to the result container
            result["path"] = path;

            return result;
        }

    }
}
