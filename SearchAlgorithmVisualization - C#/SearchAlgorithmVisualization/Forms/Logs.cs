using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SearchAlgorithmVisualization.Searching;

namespace SearchAlgorithmVisualization.Forms
{
    public partial class Logs : Form
    {
        public Logs()
        {
            InitializeComponent();
        }

        // Function to render the current node by label
        public void RenderCurrentNode(string nodeName)
        {
            this.CurrentNodeValue.Text = nodeName;
        }

        // Function to render the current container size
        public void RenderCurrentSize(int size)
        {
            this.CurrentSizeValue.Text = size.ToString();
        }

        // Function to render the current number of possible paths from current node
        public void RenderCurrentNPaths(int nPaths)
        {
            this.CurrentNPathsValue.Text = nPaths.ToString();
        }

        // Function to render the current path cost of the current traversed path
        public void RenderCurrentPathCost(float pathCost)
        {
            this.CurrentPathCostValue.Text = pathCost == float.PositiveInfinity ? "∞" : pathCost.ToString();
        }

        // Function to render the current path nodes of the current traversed path
        public void RenderCurrentPathNodes(List<string> nodeLabels)
        {
            // Clear previous list
            this.CurrentPathElementsList.Items.Clear();

            // Add the new list
            this.CurrentPathElementsList.Items.AddRange(nodeLabels.ToArray());
        }

        // Function to render the current path nodes of the current traversed path
        public void RenderCurrentPathNodes(List<Node> nodes)
        {
            // Clear previous list
            this.CurrentPathElementsList.Items.Clear();

            List<string> nodeLabels = nodes.Select(node => node.Label).ToList();

            // Add the new list
            this.CurrentPathElementsList.Items.AddRange(nodeLabels.ToArray());
        }

        // Function to render the current contents of container (stack/queue)
        public void RenderCurrentContainerContents(List<string> nodeLabels)
        {
            // Clear previous list
            this.CurrentContainerContentsList.Items.Clear();

            // Add the new list
            this.CurrentContainerContentsList.Items.AddRange(nodeLabels.ToArray());
        }

        // Function to render the current contents of container (stack/queue)
        public void RenderCurrentContainerContents(List<Node> nodes)
        {
            // Clear previous list
            this.CurrentContainerContentsList.Items.Clear();

            List<string> nodeLabels = nodes.Select(node => node.Label).ToList();

            // Add the new list
            this.CurrentContainerContentsList.Items.AddRange(nodeLabels.ToArray());
        }

        // Function to render the current source node of the graph algorithm
        public void RenderSourceNode(Node node)
        {
            if (node == null) return;

            this.SourceNodeValue.Text = node.Label;
        }

        // Function to render the current source node of the graph algorithm
        public void RenderSourceNode(string nodeName)
        {
            if (nodeName == null) return;

            this.SourceNodeValue.Text = nodeName;
        }

        // Function to render the current destination node of the graph algorithm
        public void RenderDestinationNode(Node? node)
        {
            if (node == null) return;

            this.DestinationNodeValue.Text = node.Label;
        }

        // Function to render the current source node of the graph algorithm
        public void RenderDestinationNode(string? nodeName)
        {
            if (nodeName == null) return;

            this.DestinationNodeValue.Text = nodeName;
        }

        // Function to render the current algorithm being run
        public void RenderAlgorithmName(string algorithm)
        {
            this.CurrentAlgorithmValue.Text = algorithm;
        }

        // Function to render beam coefficient value
        public void RenderBeamCoefficient(float? coefficient)
        {
            if (coefficient == null) return;

            this.BeamCoefficientValue.Text = coefficient.ToString();
        }

        // Function to render the default values of the logs
        public void RenderDefaultValues()
        {
            this.CurrentNodeValue.Text = "?";
            this.CurrentSizeValue.Text = "?";
            this.CurrentNPathsValue.Text = "?";
            this.CurrentPathCostValue.Text = "?";
            this.CurrentAlgorithmValue.Text = "Algorithm Logs";
            this.SourceNodeValue.Text = "?";
            this.DestinationNodeValue.Text = "?";
            this.BeamCoefficientValue.Text = "?";

            this.CurrentPathElementsList.Items.Clear();
            this.CurrentContainerContentsList.Items.Clear();
        }
    }
}
