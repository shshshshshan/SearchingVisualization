using Microsoft.VisualBasic.Devices;
using SearchAlgorithmVisualization.Forms;
using SearchAlgorithmVisualization.Helpers;
using SearchAlgorithmVisualization.Properties;
using SearchAlgorithmVisualization.Searching;
using System.Diagnostics;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SearchAlgorithmVisualization
{
    public partial class MainForm : Form
    {
        private List<Node> nodes;
        private List<Edge> edges;

        // Utility variables
        private Node? edgePointA = null;
        private Node? edgePointB = null;

        // Utility flags
        private bool InitializeSimulation = false;
        private bool SimulationRunning = false; // Flag to determine if the animation is on-going regardless if paused
        private bool SimulationPaused = false;

        // Flags to determine if heuristics and weight are editable
        private bool NodeHeuristicsEditable = false;
        private bool EdgeWeightEditable = false;

        private Debouncer ResetDebouncer = new Debouncer();
        private Debouncer MouseHoverDebouncer = new Debouncer();
        private Debouncer MouseDefaultDebouncer = new Debouncer();
        private Debouncer LogsDebouncer = new Debouncer();

        // Graphics object for drawing in the drawing panel
        Graphics g;
        BufferedGraphics buffer;

        // Token cancellation objects for cancelling tasks
        CancellationTokenSource? cts;
        CancellationTokenSource? animationCts;

        // Animation variables
        private Node? SearchingStartNode = null;
        private Node? SearchingEndNode = null;
        private Node? SearchingCurrentNode = null;

        private int traversedNodeIndex = 0;
        private List<Node>? SearchPath;
        private Dictionary<string, object>? LogHashmap;

        // Utility contants
        private const int SIMULATION_SPEED_MAX = 10;
        private const int SIMULATION_SPEED_UPPER_LIMIT = 3000;

        // Logs variables
        private bool LogsOpened = false;
        private Logs LogsForm = new Logs();

        // Utility prompt forms
        private CustomWeightPrompt WeightPromptForm = new CustomWeightPrompt();
        private CustomHeuristicsPrompt HeuristicsPromptForm = new CustomHeuristicsPrompt();
        private BeamWidthPrompt BeamWidthmPromptForm = new BeamWidthPrompt();

        // Prompt flags
        private bool PromptCancelled = false;
        private bool PromptSuccess = false;  // Used for when prompt is done

        // Display flags
        private bool ShowHeuristicsText = true;
        private bool ShowWeightsText = true;

        // Edit, delete flags
        private bool NodeDeleteMode = false;
        private int NumberOfRecentlyDeleted = 0; // Track the number of recently deleted nodes so that the naming will not break in the GetNodeName()

        private bool NodeEditMode = false;

        private bool EdgeDeleteMode = false;
        private bool EdgeEditMode = false;

        // Dragging flags
        private Node? NodeDragged = null;
        private bool MouseDragging = false;
        private Stopwatch DragDuration = new Stopwatch();
        private const double DRAG_THRESHOLD = 300F; // Milliseconds at which mousedown duration is considered as dragging

        private List<(int, int)> Directions = new List<(int, int)>
        {
            (0, 0),     // Self
            (0, -1),    // N
            (1, -1),    // NE
            (1, 0),     // E
            (1, 1),     // SE
            (0, 1),     // S
            (-1, 1),    // SW
            (-1, 0),    // W
            (-1, -1)    // NW
        };

        public MainForm()
        {
            InitializeComponent();

            // Initialize graphics drawing object
            this.g = this.DrawingPanel.CreateGraphics();
            this.buffer = BufferedGraphicsManager.Current.Allocate(this.g, this.DrawingPanel.ClientRectangle);

            // Set default settings
            this.DoubleBuffered = true;
            this.ResizeRedraw = false;

            // Init white buffer background
            this.buffer.Graphics.Clear(Color.White);

            // Render simulation status
            this.RenderSimulationStatus();

            // Initialize main attributes
            this.nodes = new List<Node>();
            this.edges = new List<Edge>();

            // Initialize the searching algorithm dropdown default value
            this.AlgorithmDropdown.SelectedIndex = 0;

            // Set default simulation speed minimum, maximum, and default speed value
            this.SimulationSpeedTrackBar.Minimum = 1;
            this.SimulationSpeedTrackBar.Maximum = SIMULATION_SPEED_MAX;
            this.SimulationSpeedTrackBar.Value = SIMULATION_SPEED_MAX / 2;

            // Initialize logs form setup
            this.LogsForm.TopMost = true;

            // Initialize utility prompt forms setup
            this.WeightPromptForm.TopMost = true;
            this.HeuristicsPromptForm.TopMost = true;
            this.BeamWidthmPromptForm.TopMost = true;

            // Subscribe to forms event handlers
            this.WeightPromptForm.CancelButtonPressed += WeightPromptForm_CancelButtonPressed;
            this.WeightPromptForm.EnterButtonPressed += WeightPromptForm_EnterButtonPressed;

            this.HeuristicsPromptForm.CancelButtonPressed += HeuristicsPromptForm_CancelButtonPressed;
            this.HeuristicsPromptForm.EnterButtonPressed += HeuristicsPromptForm_EnterButtonPressed;

            this.BeamWidthmPromptForm.CancelButtonPressed += BeamWidthmPromptForm_CancelButtonPressed;
            this.BeamWidthmPromptForm.EnterButtonPressed += BeamWidthmPromptForm_EnterButtonPressed;

            this.LogsForm.Hotkey += LogsForm_Hotkey;
        }

        private void LogsForm_Hotkey(object? sender, EventArgs e)
        {
            Keys logsKeyPressed = this.LogsForm.GetPressedKey();

            this.HotkeyHandler(logsKeyPressed);
        }

        // Hotkey handler
        private void HotkeyHandler(Keys key)
        {
            // General hotkeys
            if (key == Keys.L)
            {
                // Debouce to prevent spam
                this.LogsDebouncer.Debounce(() => this.ToggleLogs(), 300, SynchronizationContext.Current);
            }

            else if (key == Keys.Up || key == Keys.W)
            {
                if (this.SimulationSpeedTrackBar.Value == this.SimulationSpeedTrackBar.Maximum)
                    return;

                this.SimulationSpeedTrackBar.Value++;
            }

            else if (key == Keys.Down || key == Keys.S)
            {
                if (this.SimulationSpeedTrackBar.Value == this.SimulationSpeedTrackBar.Minimum)
                    return;

                this.SimulationSpeedTrackBar.Value--;
            }

            // Simulation hotkeys
            else if (key == Keys.Space || (!this.SimulationRunning && !this.InitializeSimulation && key == Keys.Enter))
            {
                this.RunSimulationButton_Click(this, EventArgs.Empty);
            }

            else if (key == Keys.Escape)
            {
                this.ResetButton_Click(this, EventArgs.Empty);
            }

            else if (this.SimulationRunning)
            {
                if (this.SimulationStepForwardButton.Enabled && (key == Keys.Right || key == Keys.D || key == Keys.OemPeriod))
                {
                    this.AnimationStepForward();
                }

                else if (this.SimulationStepBackButton.Enabled && (key == Keys.Left || key == Keys.A || key == Keys.Oemcomma))
                {
                    this.AnimationStepBackward();
                }
            }

            // Creation mode
            else if (this.NodeModeRadioButton.Enabled && (key == Keys.N || key == Keys.Q))
            {
                this.NodeModeRadioButton.Checked = true;
                this.EdgeModeRadioButton.Checked = false;
            }

            else if (this.EdgeModeRadioButton.Enabled && key == Keys.E)
            {
                this.EdgeModeRadioButton.Checked = true;
                this.NodeModeRadioButton.Checked = false;
            }

            // Customizing hotkeys
            else if (this.HasTargetNodeCheckBox.Enabled && key == Keys.Z)
            {
                this.HasTargetNodeCheckBox.Checked = !this.HasTargetNodeCheckBox.Checked;
            }

            else if (this.CustomGCostCheckBox.Enabled && key == Keys.X)
            {
                this.CustomGCostCheckBox.Checked = !this.CustomGCostCheckBox.Checked;
            }

            else if (this.CustomHCostCheckBox.Enabled && key == Keys.C)
            {
                this.CustomHCostCheckBox.Checked = !this.CustomHCostCheckBox.Checked;
            }

            // Changing algorithms
            else if (key == Keys.D1)
            {
                this.AlgorithmDropdown.SelectedIndex = 0;
            }

            else if (key == Keys.D2)
            {
                this.AlgorithmDropdown.SelectedIndex = 1;
            }

            else if (key == Keys.D3)
            {
                this.AlgorithmDropdown.SelectedIndex = 2;
            }

            else if (key == Keys.D4)
            {
                this.AlgorithmDropdown.SelectedIndex = 3;
            }

            else if (key == Keys.D5)
            {
                this.AlgorithmDropdown.SelectedIndex = 4;
            }

            else if (key == Keys.D6)
            {
                this.AlgorithmDropdown.SelectedIndex = 5;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            this.HotkeyHandler(key);

            return base.ProcessCmdKey(ref msg, key);
        }

        // Cleanup
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.nodes.Clear();
            this.edges.Clear();

            this.WeightPromptForm.Dispose();
            this.HeuristicsPromptForm.Dispose();
            this.BeamWidthmPromptForm.Dispose();

            this.g.Dispose();
            this.buffer.Dispose();
        }

        // Prompt success is always true for all prompts when enter button is pressed since the validation is done in the prompt
        //   form themselves
        private void WeightPromptForm_EnterButtonPressed(object? sender, EventArgs e)
        {
            this.PromptSuccess = true;
        }

        private void WeightPromptForm_CancelButtonPressed(object? sender, EventArgs e)
        {
            this.PromptCancelled = true;
        }

        private void BeamWidthmPromptForm_EnterButtonPressed(object? sender, EventArgs e)
        {
            this.PromptSuccess = true;
        }

        private void BeamWidthmPromptForm_CancelButtonPressed(object? sender, EventArgs e)
        {
            this.PromptCancelled = true;
        }

        private void HeuristicsPromptForm_EnterButtonPressed(object? sender, EventArgs e)
        {
            this.PromptSuccess = true;
        }

        private void HeuristicsPromptForm_CancelButtonPressed(object? sender, EventArgs e)
        {
            this.PromptCancelled = true;
        }

        // Utility function to get the node name based on the length of a list
        // Name is incremented lexicographically
        // Ex: 'A'++ = 'B', 'Z'++ = 'AA'
        private string GetNodeName<T>(List<T> l)
        {
            // Increment list count to make it start at 'A'
            int listCount = l.Count + this.NumberOfRecentlyDeleted + 1;
            int alphabetLength = 'Z' - 'A' + 1;

            string res = "";

            while (listCount-- > 0)
            {
                res = (char)('A' + listCount % alphabetLength) + res;
                listCount /= alphabetLength;
            }

            return res;
        }

        // Returns the node that is near the mouse
        private Node? GetNodeNearMouse(List<Node> nodes, int mouseX, int mouseY)
        {
            foreach (Node existingNode in nodes)
            {
                if (Helpers.Helpers.dist(existingNode.X, existingNode.Y, mouseX, mouseY) <= existingNode.Radius)
                {
                    return existingNode;
                }
            }

            return null;
        }

        // Returns the edge that is near the mouse
        private Edge? GetEdgeNearMouse(List<Edge> edges, int mouseX, int mouseY)
        {
            // A pixel threshold so that the user doesn't have to accurately click the edge, just an estimation of it
            const int THRESHOLD = 5;

            foreach (Edge existingEdge in edges)
            {
                var (x1, y1) = (existingEdge.PointA.X, existingEdge.PointA.Y);
                var (x2, y2) = (existingEdge.PointB.X, existingEdge.PointB.Y);

                double n = Math.Abs((x2 - x1) * (y1 - mouseY) - (x1 - mouseX) * (y2 - y1));
                double d = Helpers.Helpers.dist(x1, y1, x2, y2);

                double perpendicularDistance = n / d;

                if (perpendicularDistance > THRESHOLD) continue;

                // Calculate the intersection point
                double t = ((mouseX - x1) * (x2 - x1) + (mouseY - y1) * (y2 - y1)) / (Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                var (xi, yi) = (x1 + t * (x2 - x1), y1 + t * (y2 - y1));

                // Check if the intersection point is within the edge's endpoints
                if (xi < Math.Min(x1, x2) || xi > Math.Max(x1, x2) || yi < Math.Min(y1, y2) || yi > Math.Max(y1, y2)) continue;

                return existingEdge;
            }

            return null;
        }

        // Utility function to render a single node
        private void RenderNode(Node? n, Brush? b = null, Pen? p = null, bool renderBuffer = true)
        {
            // Catch invalid parameters
            if (n == null) return;

            const int BORDER_WIDTH = 2;

            Brush brush = b ?? Brushes.Red;
            Pen pen = p ?? new Pen(Brushes.Black, BORDER_WIDTH);

            // Draw the node as a circle with borders

            this.buffer.Graphics.FillEllipse(brush, n.X - n.Radius, n.Y - n.Radius, n.Diameter, n.Diameter);
            this.buffer.Graphics.DrawEllipse(pen, n.X - n.Radius - BORDER_WIDTH / 2, n.Y - n.Radius - BORDER_WIDTH / 2, n.Diameter + BORDER_WIDTH, n.Diameter + BORDER_WIDTH);

            // Calculate text size and position for the label
            Font labelFont = new Font("Arial", n.Diameter * 0.4F, FontStyle.Bold);
            SizeF textSize = this.buffer.Graphics.MeasureString(n.Label, labelFont);

            float labelX = n.X - textSize.Width / 2;
            float labelY = n.Y - textSize.Height / 2;

            this.buffer.Graphics.DrawString(n.Label, labelFont, Brushes.WhiteSmoke, labelX, labelY);

            // Draw the heuristic value as text above the node

            if (this.ShowHeuristicsText)
            {
                SizeF heuristicLabelSize = this.buffer.Graphics.MeasureString(n.Heuristics.ToString(), labelFont);

                float heuristicLabelX = n.X - heuristicLabelSize.Width / 2;
                float heuristicLabelY = (n.Y - heuristicLabelSize.Height / 2) - (n.Radius + n.Radius / 2);

                this.buffer.Graphics.DrawString(n.Heuristics.ToString(), labelFont, Brushes.DarkViolet, heuristicLabelX, heuristicLabelY);
            }

            if (renderBuffer)
            {
                this.buffer.Render();
            }
        }

        // Utility function to render a single edge
        // This function re-renders the source and destination nodes as well to
        //   fix the colors and make the edge appear behind the nodes
        private void RenderEdge(Edge e, float? w = null, Brush? b = null, bool renderNode = true, bool renderBuffer = true)
        {
            // Catch invalid parameters
            if (e == null) return;

            float width = w ?? 3;
            b ??= Brushes.Black;
            Pen pen = new Pen(b, width);

            // Draw the edge connection as lines
            // Draw first to make it appear behind
            this.buffer.Graphics.DrawLine(pen, e.PointA.X, e.PointA.Y, e.PointB.X, e.PointB.Y);

            // Re-draw source and destination to reset it to default
            if (renderNode)
            {
                this.RenderNode(e.PointA, renderBuffer: renderBuffer);
                this.RenderNode(e.PointB, renderBuffer: renderBuffer);
            }

            if (this.ShowWeightsText)
            {
                // Draw the edge weight last to make it appear on top always
                // The edge weight label is drawn at the center of the edge

                // Calculate text size and position for the label
                Font labelFont = new Font("Arial", e.PointA.Diameter * 0.4F, FontStyle.Bold);
                SizeF textSize = this.buffer.Graphics.MeasureString(e.Weight.ToString(), labelFont);

                var (midX, midY) = Helpers.Helpers.midpoint(e.PointA.X, e.PointA.Y, e.PointB.X, e.PointB.Y);

                // Calculate the label position
                float labelX = midX - (textSize.Width / 2);
                float labelY = midY - (textSize.Height / 2);

                // Constant multiplier for label correction
                const float CORRECTION = 0.8f;

                // Adjusting label position based on the orientation of the line
                if (Math.Abs(e.PointA.X - e.PointB.X) > Math.Abs(e.PointA.Y - e.PointB.Y))
                {
                    // Horizontal line - adjust label towards the top
                    labelY -= textSize.Height * CORRECTION;
                }
                else
                {
                    // Vertical line - adjust label towards the left
                    labelX -= textSize.Width * CORRECTION;
                }

                // Render the label
                this.buffer.Graphics.DrawString(e.Weight.ToString(), labelFont, Brushes.Black, labelX, labelY);
            }

            if (renderBuffer)
            {
                this.buffer.Render();
            }
        }

        // Utility function to render the simulation status
        // White => Solving (the simulation status won't render if the algorithm is not done)
        // Navy => Waiting for a starting node
        // Light Yellow => Waiting for a destination mode (if set)
        // Red => Simulation is on-going
        // Light Green => Simulation has ended (default)
        // Orange => Simulation is paused
        private void RenderSimulationStatus()
        {
            // Constant status circle size
            const int SCALE = 10;

            Graphics cg = this.ControlPanel.CreateGraphics();

            // Default brush (Green)
            Brush statusBrush = Brushes.LightGreen;

            if (this.InitializeSimulation)
            {
                if (this.SearchingStartNode == null)
                {
                    statusBrush = Brushes.Navy;
                }
                else if (this.HasTargetNodeCheckBox.Checked && this.SearchingEndNode == null)
                {
                    statusBrush = Brushes.Yellow;
                }
            }
            else if (this.SimulationPaused)
            {
                statusBrush = Brushes.Orange;
            }
            else if (this.SimulationRunning)
            {
                statusBrush = Brushes.Red;
            }

            cg.FillEllipse(statusBrush, this.SimulationStatusLabel.Location.X + this.SimulationStatusLabel.Width * 1.2F - (SCALE / 2), this.SimulationStatusLabel.Location.Y + this.SimulationStatusLabel.Height / 2 - (SCALE / 2), SCALE, SCALE);
        }

        private async void EditNodeHeuristics(Node? n)
        {
            if (n == null) return;

            float? heuristics = null;

            this.HeuristicsPromptForm.Show();

            while (!this.PromptSuccess)
            {
                if (this.PromptCancelled) return;

                await Task.Delay(100);
            }

            heuristics = this.HeuristicsPromptForm.GetInput();

            n.Heuristics = heuristics ?? -1;
            n.IsDefaultHeuristicValue = !heuristics.HasValue;
        }

        private async void DrawingPanel_MouseDown(object sender, MouseEventArgs e)
        {
            // Only works for left click
            if (e.Button == MouseButtons.Right) { return; }

            // Set constant node radius
            const int SCALE = 15;

            // Mouse coords upon click on canvas
            int mouseX = e.X;
            int mouseY = e.Y;

            // Reset prompt flags
            this.PromptCancelled = false;
            this.PromptSuccess = false;

            // For selecting a starting node
            // This is when "run" button is clicked
            if (this.InitializeSimulation)
            {
                // For start node selection
                if (this.SearchingStartNode == null)
                {
                    this.SearchingStartNode = this.GetNodeNearMouse(this.nodes, mouseX, mouseY);
                    this.RenderNode(this.SearchingStartNode, Brushes.Blue);
                }
                else if (this.HasTargetNodeCheckBox.Checked && this.SearchingEndNode == null)
                {
                    this.SearchingEndNode = this.GetNodeNearMouse(this.nodes, mouseX, mouseY);
                    this.RenderNode(this.SearchingEndNode, Brushes.Yellow);
                }

                // Render simulation status after click of a selected node
                this.RenderSimulationStatus();

                return;
            }

            // For Node Deleting
            else if (this.NodeDeleteMode)
            {
                Node? n = this.GetNodeNearMouse(this.nodes, mouseX, mouseY);

                if (n == null) return;

                // Find the node in the list and remove it
                this.nodes.Remove(n);
                this.NumberOfRecentlyDeleted++;

                // Find the edge in the list and remove the connections that has the selected node
                this.edges = this.edges.Where(e => !e.HasMember(n)).ToList();

                // Rerender entities
                this.RenderEntities();

                return;
            }

            // For Node Editing
            else if (this.NodeEditMode)
            {
                Node? n = this.GetNodeNearMouse(this.nodes, mouseX, mouseY);

                if (n == null) return;

                // Add dragability to nodes
                this.MouseDragging = true;
                this.NodeDragged = n;
                this.DragDuration.Reset();
                this.DragDuration.Start();

                // Rerender entities
                this.RenderEntities();

                return;
            }

            // For Edge Editing
            else if (this.EdgeEditMode)
            {
                if (!this.EdgeWeightEditable) return;

                Edge? selected = this.GetEdgeNearMouse(this.edges, mouseX, mouseY);

                if (selected == null) return;

                int? weight = null;

                this.WeightPromptForm.Show();

                while (!this.PromptSuccess)
                {
                    if (this.PromptCancelled) return;

                    await Task.Delay(100);
                }

                weight = this.WeightPromptForm.GetInput();

                selected.Weight = weight ?? 1;

                // Rerender entities
                this.RenderEntities();

                return;
            }

            // For Edge Deleting
            else if (this.EdgeDeleteMode)
            {
                Edge? selected = this.GetEdgeNearMouse(this.edges, mouseX, mouseY);

                if (selected == null) return;

                // Find the node in the list and remove it
                this.edges.Remove(selected);

                // Rerender entities
                this.RenderEntities();

                return;
            }


            // Draw a node or edge based on selected creation mode

            // Node creation
            if (this.NodeModeRadioButton.Checked)
            {
                // Prevent creation if node overflows at the edges of the canvas
                // TOP, RIGHT, BOTTOM, LEFT
                if (mouseY <= SCALE ||
                    mouseX >= this.DrawingPanel.Width - SCALE ||
                    mouseY >= this.DrawingPanel.Height - SCALE ||
                    mouseX <= SCALE)
                {

                    return;
                }

                // Loop through all nodes and prevent creation of two close nodes
                foreach (Node existingNode in this.nodes)
                {
                    if (Helpers.Helpers.dist(existingNode.X, existingNode.Y, mouseX, mouseY) <= existingNode.Diameter)
                    {
                        return;
                    }
                }

                float? heuristics = null;

                // Prompt user for custom heuristics of node if custom heuristics is set
                if (this.CustomHCostCheckBox.Checked)
                {
                    this.HeuristicsPromptForm.Show();

                    while (!this.PromptSuccess)
                    {
                        if (this.PromptCancelled) return;

                        await Task.Delay(100);
                    }

                    heuristics = this.HeuristicsPromptForm.GetInput();
                }

                Node n = new Node(GetNodeName(this.nodes), mouseX, mouseY, SCALE, heuristics);
                this.nodes.Add(n);
                this.RenderNode(n);
            }

            // Edge creation
            else if (this.EdgeModeRadioButton.Checked)
            {
                // If there are less than 2 nodes, don't do anything
                if (this.nodes.Count < 2) return;

                Node? selectedNode = this.GetNodeNearMouse(this.nodes, mouseX, mouseY);

                if (this.edgePointA == null)
                {
                    this.edgePointA = selectedNode;
                    this.RenderNode(this.edgePointA, Brushes.Blue);
                }
                else if (this.edgePointB == null)
                {
                    if (selectedNode == null) return;

                    this.edgePointB = selectedNode;

                    // Cancel everything if edge A == edge B
                    if (this.edgePointA == this.edgePointB)
                    {
                        this.RenderNode(this.edgePointA);
                        this.edgePointA = this.edgePointB = null;
                        return;
                    }

                    int? edgeWeight = null;

                    // Prompt user for custom weight of edge if custom weight is set
                    if (this.CustomGCostCheckBox.Checked)
                    {
                        this.WeightPromptForm.Show();

                        while (!this.PromptSuccess)
                        {
                            if (this.PromptCancelled)
                            {
                                this.RenderNode(this.edgePointA);
                                this.edgePointA = this.edgePointB = null;
                                return;
                            }

                            await Task.Delay(100);
                        }

                        edgeWeight = this.WeightPromptForm.GetInput();
                    }

                    Edge connection = new Edge(this.edgePointA, this.edgePointB, edgeWeight ?? 1);

                    // Check all other connections and prevent the addition of existing pairs
                    // This is because the graph is undirected
                    // PointA and destination will not matter
                    if (this.edges.Contains(connection))
                    {
                        this.RenderNode(this.edgePointA);
                        this.edgePointA = this.edgePointB = null;
                        return;
                    }

                    // Render the edge
                    this.edges.Add(connection);
                    this.RenderEdge(connection);

                    // Clear the existing edge points for the next input
                    this.edgePointA = this.edgePointB = null;
                }

                // For when user pressed outside of the prompt window
                // When Edge point B != null, Edge point A also not null
                else if (this.CustomGCostCheckBox.Checked && this.edgePointB != null)
                {
                    this.PromptCancelled = true;
                    this.RenderNode(this.edgePointA);
                    this.edgePointA = this.edgePointB = null;
                    return;
                }
            }
        }

        // Rerenders all entities (nodes, edges)
        private void RenderEntities()
        {
            this.buffer.Graphics.Clear(Color.White);

            // Loop through all edges and draw them
            // Drawing edges first to make it appear behind the nodes
            foreach (Edge connection in this.edges)
            {
                this.RenderEdge(connection, renderBuffer: false);
            }

            // Loop through all nodes and draw them
            foreach (Node n in this.nodes)
            {
                // Special render when simulation is running
                if (this.SimulationRunning &&
                    this.SearchPath != null &&
                    this.traversedNodeIndex > 0 &&
                    this.traversedNodeIndex < this.SearchPath.Count &&
                    this.SearchPath[this.traversedNodeIndex] == n)
                {
                    this.RenderNode(n, Brushes.Purple, renderBuffer: false);
                }

                // Default render
                else
                {
                    this.RenderNode(n, renderBuffer: false);
                }
            }

            // Redraw the starting search node and ending node if available
            if (this.SearchingStartNode != null)
            {
                this.RenderNode(this.SearchingStartNode, Brushes.Blue, renderBuffer: false);
            }

            if (this.SearchingEndNode != null)
            {
                this.RenderNode(this.SearchingEndNode, Brushes.Yellow, renderBuffer: false);
            }

            this.buffer.Render();
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            this.RenderEntities();
        }

        private void NodeModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.NodeModeRadioButton.Checked) return;

            // Reset edit or delete mode
            this.NodeDeleteMode = this.NodeEditMode = this.EdgeDeleteMode = this.EdgeEditMode = false;

            // Reset the setting of edge points
            if (this.edgePointA != null)
            {
                this.RenderNode(this.edgePointA);
            }
            else if (this.edgePointB != null)
            {
                this.RenderNode(this.edgePointB);
            }

            this.edgePointA = this.edgePointB = null;
        }

        private void EdgeModeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (!this.EdgeModeRadioButton.Checked) return;

            // Reset edit or delete mode
            this.NodeDeleteMode = this.NodeEditMode = this.EdgeDeleteMode = this.EdgeEditMode = false;
        }

        // This will clear all nodes and edges in the graph
        private void ResetButton_Click(object sender, EventArgs e)
        {
            // This is for cancel button mode
            if (this.InitializeSimulation)
            {
                // Cancel the process before the animation
                this.cts?.Cancel();
                return;
            }

            // This is for stop animation button mode
            else if (this.SimulationRunning)
            {
                // Start clock for when user presses pause before stopping animation
                this.AnimationClock.Start();

                // Cancel the animation
                this.animationCts?.Cancel();
                return;
            }

            // Debounce the clear method to avoid spam clicks
            this.ResetDebouncer.Debounce(() => this.ResetLists(), 300, SynchronizationContext.Current);
        }

        // Function that will clear the contents of the nodes and edges list
        private void ResetLists()
        {
            this.nodes.Clear();
            this.nodes.Capacity = 10;

            this.edges.Clear();
            this.edges.Capacity = 10;

            this.NumberOfRecentlyDeleted = 0;

            this.RenderEntities();
        }

        // Disable other control panel controls when simulation is initializing
        // Used by simulation running
        private void DisableControlPanelControls()
        {
            this.NodeModeRadioButton.Checked = this.EdgeModeRadioButton.Checked = false;
            this.NodeModeRadioButton.Enabled = this.EdgeModeRadioButton.Enabled = false;
            this.AlgorithmDropdown.Enabled = false;
            this.HasTargetNodeCheckBox.Enabled = false;
            this.CustomHCostCheckBox.Enabled = false;
            this.CustomGCostCheckBox.Enabled = false;

            this.NodeDeleteButton.Enabled = false;
            this.NodeEditButton.Enabled = false;
            this.EdgeDeleteButton.Enabled = false;
            this.EdgeEditButton.Enabled = false;

            this.ResetButton.Text = "Cancel";
        }

        // Restores the default control panel controls when simulation is cancelled or finished
        // Used by simulation running
        private void RestoreControlPanelControls()
        {
            this.NodeModeRadioButton.Checked = true;
            this.NodeModeRadioButton.Enabled = this.EdgeModeRadioButton.Enabled = true;
            this.AlgorithmDropdown.Enabled = true;
            this.HasTargetNodeCheckBox.Enabled = true;
            this.CustomHCostCheckBox.Enabled = false;
            this.CustomGCostCheckBox.Enabled = false;

            this.NodeDeleteButton.Enabled = true;
            this.NodeEditButton.Enabled = true;
            this.EdgeDeleteButton.Enabled = true;
            this.EdgeEditButton.Enabled = true;

            this.ResetButton.Text = "Reset";
        }

        // Function to update simulation navigation buttons availability
        private void UpdateSimulationNavigationButtonsAvailability()
        {
            // Enable step back and step forward only when animation is paused to avoid incompatibilities
            // Check first if current traversed index > 0, then turn on step back
            // Check also if current traversed index is < search path length - 1, then turn on step forward
            // Traversed node index is decremented by 1 in the condition since when the clock is stopped, 
            //   the animation tick renderer already rendered the current node and incremented the traversed node index
            if (this.traversedNodeIndex - 1 > 0)
            {
                this.SimulationStepBackButton.Enabled = true;
                this.SimulationStepBackButton.Text = "«";
            }
            else
            {
                this.SimulationStepBackButton.Enabled = false;
                this.SimulationStepBackButton.Text = "-";
            }

            if (this.SearchPath != null && this.traversedNodeIndex < this.SearchPath.Count)
            {
                this.SimulationStepForwardButton.Enabled = true;
                this.SimulationStepForwardButton.Text = "»";
            }
            else
            {
                this.SimulationStepForwardButton.Enabled = false;
                this.SimulationStepForwardButton.Text = "-";
            }
        }

        // Run simulation
        private async void RunSimulationButton_Click(object sender, EventArgs e)
        {
            // For simulation pause mode
            if (this.SimulationRunning)
            {
                this.SimulationPaused = !this.SimulationPaused;
                this.RunSimulationButton.Text = this.SimulationPaused ? "Resume" : "Pause";

                if (this.SimulationPaused)
                {
                    this.AnimationClock.Stop();

                    this.UpdateSimulationNavigationButtonsAvailability();
                }
                else
                {
                    // Enable step back and step forward only when animation is paused to avoid incompatibilities
                    this.SimulationStepBackButton.Enabled = false;
                    this.SimulationStepForwardButton.Enabled = false;

                    this.AnimationClock.Start();
                }

                return;
            }

            // If there are no nodes or already simulating, don't do anything
            // Button is disabled during setup phase of animation but this is just a guard
            if (this.nodes.Count == 0 || this.InitializeSimulation) return;

            // Initialize cancellation token
            this.cts = new CancellationTokenSource();

            // Reset prompt flags
            this.PromptCancelled = false;
            this.PromptSuccess = false;

            // Disable the run button
            this.RunSimulationButton.Enabled = false;
            this.RunSimulationButton.Cursor = Cursors.No;

            // Reset other settings
            this.NodeEditMode = false;
            this.NodeDeleteMode = false;
            this.EdgeEditMode = false;
            this.EdgeDeleteMode = false;

            // Calculate the path based on selected algorithm
            var selected = this.AlgorithmDropdown.SelectedItem.ToString();

            // Path result
            List<Node>? path = null;

            // Algorithm result
            Dictionary<string, object>? algorithmResult = null;

            // Set initialize simulation
            this.InitializeSimulation = true;
            this.RenderSimulationStatus();

            // Disable other controls when simulation is initializing
            // Changing also the reset button to cancel button
            if (this.InitializeSimulation)
            {
                this.DisableControlPanelControls();
            }

            // Wait for use to click on a starting node for searching
            while (this.SearchingStartNode == null || (this.HasTargetNodeCheckBox.Checked && this.SearchingEndNode == null))
            {

                // Check if user cancelled the action
                if (cts.Token.IsCancellationRequested)
                {
                    // Clean up and restore defaults
                    this.RestoreControlPanelControls();

                    // Restore or update control panel settings
                    this.UpdateControlPanelSettings();

                    this.RunSimulationButton.Enabled = true;
                    this.RunSimulationButton.Cursor = Cursors.Hand;

                    this.SearchingStartNode = null;
                    this.SearchingEndNode = null;
                    this.InitializeSimulation = false;

                    this.cts.Dispose();
                    this.cts = null;

                    return;
                }

                await Task.Delay(100);
            }

            // Initialize algorithm class
            Algorithms algorithms = new Algorithms(this.nodes, this.edges);

            // Init beam coefficient outside to be referred later on by logs
            int? coefficient = null;

            switch (selected)
            {
                // Wait the resulting path in case it takes too long to solve

                case "DFS":

                    algorithmResult = await Task.Run(() =>
                    {
                        // DFS with logs
                        return algorithms.DFSWithLogs(this.SearchingStartNode, this.SearchingEndNode);
                    });

                    path = (List<Node>?)algorithmResult["path"];

                    break;

                case "BFS":

                    algorithmResult = await Task.Run(() =>
                    {
                        return algorithms.BFSWithLogs(this.SearchingStartNode, this.SearchingEndNode);
                    });

                    path = (List<Node>?)algorithmResult["path"];

                    break;

                case "Beam":

                    if (this.SearchingEndNode == null)
                    {
                        throw new Exception("Trying to execute Beam algorithm without a destination node.");
                    }

                    // Await for a beam width input if there is, restore and clean up if beam width is cancelled
                    // Prompt user for custom heuristics of node is custom heuristics is set
                    this.BeamWidthmPromptForm.Show();

                    while (!this.PromptSuccess)
                    {
                        if (this.PromptCancelled)
                        {
                            // Clean up and restore defaults
                            this.RestoreControlPanelControls();

                            // Restore or update control panel settings
                            this.UpdateControlPanelSettings();

                            this.RunSimulationButton.Enabled = true;
                            this.RunSimulationButton.Cursor = Cursors.Hand;

                            this.RenderNode(this.SearchingStartNode);
                            this.RenderNode(this.SearchingEndNode);

                            this.SearchingStartNode = null;
                            this.SearchingEndNode = null;
                            this.InitializeSimulation = false;

                            this.cts.Dispose();
                            this.cts = null;

                            return;
                        }

                        await Task.Delay(100);
                    }

                    coefficient = this.BeamWidthmPromptForm.GetInput();

                    // Heuristic cost is set inside the algorithm
                    // Default h-cost of node depends if custom h-cost is set
                    algorithmResult = await Task.Run(() =>
                    {
                        return algorithms.BeamWithLogs(this.SearchingStartNode, this.SearchingEndNode, coefficient ?? 2);
                    });

                    path = (List<Node>?)algorithmResult["path"];

                    break;

                case "Branch-and-Bound":

                    algorithmResult = await Task.Run(() =>
                    {
                        return algorithms.BranchAndBoundWithLogs(this.SearchingStartNode, this.SearchingEndNode);
                    });

                    path = (List<Node>?)algorithmResult["path"];

                    break;

                case "Hill Climbing":

                    if (this.SearchingEndNode == null)
                    {
                        throw new Exception("Trying to execute Hill Climbing algorithm without a destination node.");
                    }

                    // This is just Beam with a Beam Coefficient of 1
                    algorithmResult = await Task.Run(() =>
                    {
                        return algorithms.BeamWithLogs(this.SearchingStartNode, this.SearchingEndNode, 1);
                    });

                    path = (List<Node>?)algorithmResult["path"];

                    break;

                case "A*":

                    if (this.SearchingEndNode == null)
                    {
                        throw new Exception("Trying to execute A* algorithm without a destination node.");
                    }

                    algorithmResult = await Task.Run(() =>
                    {
                        return algorithms.AStarWithLogs(this.SearchingStartNode, this.SearchingEndNode);
                    });

                    path = (List<Node>?)algorithmResult["path"];

                    break;

                default:
                    throw new Exception("Unimplemented algorithm selected.");
            }

            // Cleanup and restore if there's no path
            if (path == null)
            {
                // Restore defaults
                this.RestoreControlPanelControls();

                // Cleanup
                this.SearchingStartNode = null;
                this.SearchingEndNode = null;
                this.InitializeSimulation = false;
                this.cts.Dispose();
                this.cts = null;

                return;
            }

            // Remove ['path'] in algorithmResult and
            // Set it is this.LogsHashmap
            algorithmResult?.Remove("path");
            this.LogHashmap = algorithmResult;

            // Animate the path
            foreach (Node n in path)
            {
                Console.WriteLine(n.Label);
            }

            // Reset logs values
            this.LogsForm.RenderDefaultValues();

            // Render logs algorithm name, beam coefficient (if applicable), starting and ending searching node
            this.LogsForm.RenderAlgorithmName(selected + " Logs");
            this.LogsForm.RenderSourceNode(this.SearchingStartNode);
            this.LogsForm.RenderDestinationNode(this.SearchingEndNode);
            this.LogsForm.RenderBeamCoefficient(coefficient);

            // Enable the button again once the animation is about to start
            // Since this button is used to pause the animation
            this.RunSimulationButton.Enabled = true;
            this.RunSimulationButton.Cursor = Cursors.Hand;

            // Initially refresh the scene
            this.RenderEntities();

            this.AnimatePath(path);

            // Cleanup and restore defaults are handled by AnimatePath
        }

        // Function that will animate the path
        private void AnimatePath(List<Node> path)
        {
            // Initialize animation variables
            this.SearchPath = path;
            this.traversedNodeIndex = 0;
            this.InitializeSimulation = false;
            this.SimulationRunning = true;
            this.SimulationPaused = false;
            this.RenderSimulationStatus();

            // Simulation navigation reset
            this.SimulationStepBackButton.Enabled = false;
            this.SimulationStepForwardButton.Enabled = false;

            // Change the cancel button to stop button when animation is started
            this.ResetButton.Text = "Stop";

            // Change the run button to pause button when animation is started
            this.RunSimulationButton.Text = "Pause";

            // Initialize animation cancellation token source
            this.animationCts = new CancellationTokenSource();

            // Set the animation interval to the default speed
            // Upper limit speed is 3 seconds
            // Lower limit speed is 300 milliseconds
            // SIMULATION_SPEED_MAX / 2 is the default
            // SIMULATION SPEED_MAX / 2 / SIMULATION SPEED = 0.5
            this.AnimationClock.Interval = (int)(SIMULATION_SPEED_UPPER_LIMIT * 0.5F);

            // Enable and start the clock first before setting correct interval
            this.AnimationClock.Enabled = true;
            this.AnimationClock.Start();
        }

        // Animation utility, this will render the animation based on ticks
        private void AnimationClock_Tick(object sender, EventArgs e)
        {
            this.AnimateCurrentNode();
        }

        // Animation function main
        private void AnimateCurrentNode(int? traversalIndex = null)
        {
            if (this.animationCts == null)
            {
                // Something is wrong
                throw new Exception($"Animation cancellation token is not initialized at line {new StackTrace(new StackFrame(true)).GetFrame(0)?.GetFileLineNumber()} in Main.cs");
            }

            if ((traversalIndex != null && traversalIndex < 0) || (this.SearchPath != null && traversalIndex >= this.SearchPath.Count))
            {
                // Cannot pass a traversal index of negative value or over the length of the search path
                throw new Exception($"Cannot animate current node index of value {traversalIndex} at line {new StackTrace(new StackFrame(true)).GetFrame(0)?.GetFileLineNumber()} in Main.cs");
            }

            int searchIndex = traversalIndex ?? this.traversedNodeIndex;

            // Set correct animation speed based on set speed
            // This is needed since animation speed is set to default prior to starting the animation
            // Set once
            if (searchIndex == 0)
            {
                this.AnimationClock.Interval = this.GetAnimationSpeed();
            }

            // Set animation speed back to default when current node is last node in the path
            else if (this.SearchPath != null && searchIndex == this.SearchPath.Count - 1)
            {
                this.AnimationClock.Interval = (int)(SIMULATION_SPEED_UPPER_LIMIT * 0.5F);
            }

            // Stop animation if search path is null or
            // Current traversed index is out of bounds
            if (this.SearchPath == null || searchIndex >= this.SearchPath.Count || this.animationCts.IsCancellationRequested)
            {
                this.AnimationClock.Stop();

                // Render the default for the last node if there is
                if (this.SearchPath != null && searchIndex > 0)
                {
                    this.RenderNode(this.SearchPath[searchIndex - 1], renderBuffer: false);
                }

                // Clean up and restore defaults

                // Restore defaults
                this.RestoreControlPanelControls();
                this.RenderNode(this.SearchingStartNode, renderBuffer: false);
                this.RenderNode(this.SearchingEndNode, renderBuffer: false);
                this.RunSimulationButton.Text = "Run";

                this.buffer.Render();

                // Restore or update control panel settings
                this.UpdateControlPanelSettings();

                // Simulation navigation reset
                this.SimulationStepBackButton.Enabled = false;
                this.SimulationStepForwardButton.Enabled = false;

                // Cleanup
                this.AnimationClock.Enabled = false;
                this.SearchingStartNode = null;
                this.SearchingEndNode = null;
                this.SimulationRunning = false;
                this.SimulationPaused = false;
                this.RenderSimulationStatus();

                this.cts?.Dispose();
                this.cts = default;

                this.animationCts?.Dispose();
                this.animationCts = default;

                return;
            }

            // Animate :>

            this.SearchingCurrentNode = this.SearchPath[searchIndex];

            // Render the defaults for the previous node
            // Don't render defaults for the starting and ending search node
            if (searchIndex > 0)
            {
                Node recent = this.SearchPath[searchIndex - 1];

                // Correct color for start node
                if (recent.Label == this.SearchingStartNode?.Label)
                {
                    this.RenderNode(recent, Brushes.Blue, renderBuffer: false);
                }

                // Correct color for end node
                else if (recent.Label == this.SearchingEndNode?.Label)
                {
                    this.RenderNode(recent, Brushes.Yellow, renderBuffer: false);
                }

                // Default color
                else
                {
                    this.RenderNode(recent, renderBuffer: false);
                }
            }

            // Render the defaults for the next node
            // Don't render defaults for the starting and ending search node
            // This is mainly used for step back in animation
            if (this.SearchPath != null && searchIndex < this.SearchPath.Count - 1)
            {
                Node next = this.SearchPath[searchIndex + 1];

                // Correct color for start node
                if (next.Label == this.SearchingStartNode?.Label)
                {
                    this.RenderNode(next, Brushes.Blue, renderBuffer: false);
                }

                // Correct color for end node
                else if (next.Label == this.SearchingEndNode?.Label)
                {
                    this.RenderNode(next, Brushes.Yellow, renderBuffer: false);
                }

                // Default color
                else
                {
                    this.RenderNode(next, renderBuffer: false);
                }
            }

            // Render the logs
            this.LogsForm.RenderCurrentNode(this.SearchingCurrentNode.Label ?? "?");

            if (this.LogHashmap != null && this.LogHashmap.ContainsKey(searchIndex.ToString()))
            {
                // Iteration dictionary
                Dictionary<string, object> iterDict = (Dictionary<string, object>)this.LogHashmap[searchIndex.ToString()];

                // ['container']
                this.LogsForm.RenderCurrentContainerContents((List<Node>)iterDict["container"]);

                // ['size']
                this.LogsForm.RenderCurrentSize((int)iterDict["size"]);

                // ['n_paths']
                this.LogsForm.RenderCurrentNPaths((int)iterDict["n_paths"]);

                // ['pathCost']
                this.LogsForm.RenderCurrentPathCost((float)iterDict["pathCost"]);

                // ['pathElements']
                this.LogsForm.RenderCurrentPathNodes((List<string>)iterDict["pathElements"]);

                // Highlight the edge of path traversed
                // Highlight path first
                this.HighlightTraversedPath((List<string>)iterDict["pathElements"]);

                // Highlight traversable nodes from current
                this.HighlightTraversableNodes((List<string>?)iterDict["paths"]);
            }


            // Render the current node with a different colored brush
            Brush currentNodeBrush = this.SearchingEndNode?.Label == this.SearchingCurrentNode.Label ? Brushes.Turquoise :
                this.SearchingStartNode?.Label == this.SearchingCurrentNode.Label ? Brushes.CadetBlue : Brushes.Purple;
            this.RenderNode(this.SearchingCurrentNode, currentNodeBrush, renderBuffer: false);

            // Update global index to local index if passed in a different different index
            // Else increment the default index
            if (traversalIndex == null)
                this.traversedNodeIndex++;
            else
            {
                this.traversedNodeIndex = searchIndex;
                this.traversedNodeIndex++;
            }

            // Update simulation navigation status per iteration
            this.UpdateSimulationNavigationButtonsAvailability();

            this.buffer.Render();
        }

        // Highlight the nodes that are available from the current node
        // This is used in animation
        private void HighlightTraversableNodes(List<string>? nodes, Brush? b = null)
        {
            if (nodes == null) return;

            b ??= Brushes.Orange;

            // Render default for non-neighbor nodes
            // Dark-orange for neighbor nodes
            foreach (Node n in this.nodes)
            {
                // Render defaults to reset it
                this.RenderNode(n, renderBuffer: false);
            }

            foreach (string s in nodes)
            {
                Node? n = this.nodes.Find(n => n.Label == s);

                if (n == null) continue;

                this.RenderNode(n, b, renderBuffer: false);
            }
        }

        // Highlight the edges that a path traversed
        // This is used in animation
        private void HighlightTraversedPath(List<string>? nodes, Brush? b = null)
        {
            if (nodes == null) return;

            b ??= Brushes.BlueViolet;

            foreach (Edge e in this.edges)
            {
                // Render defaults to reset
                this.RenderEdge(e, renderBuffer: false);
            }

            for (int i = 0; i + 1 < nodes.Count; i++)
            {
                Edge? e = this.edges.Find(e => e.HasMember(nodes[i]) && e.HasMember(nodes[i + 1]));

                if (e == null) continue;

                this.RenderEdge(e, b: Brushes.DarkViolet, renderBuffer: false);
            }
        }

        // Drawing stuff over the control panel
        private void ControlPanel_Paint(object sender, PaintEventArgs e)
        {
            // Rendering the simulation status
            this.RenderSimulationStatus();
        }

        // Function to get the hovered node
        private Node? GetHoveredNode(List<Node> nodes, int mouseX, int mouseY)
        {
            // Essentially gets the node at current mouseX and mouseY
            Node? hovered = this.GetNodeNearMouse(nodes, mouseX, mouseY);

            return hovered;
        }

        // Function to get the hovered edge
        private Edge? GetHoveredEdge(List<Edge> edges, int mouseX, int mouseY)
        {
            // Essentially gets the edge that the mouse touched
            // In the case where the mouse clicks at a location at the cross-section of two edges,
            //   the more recently created edge from the two will be taken

            Edge? hovered = this.GetEdgeNearMouse(edges, mouseX, mouseY);

            return hovered;
        }

        // Function to change the cursor when a node or edge is hovered
        private void CursorHandWhenNodeOrEdgeHovered()
        {
            // Only implement if the creation mode is edge mode
            // Only implement if the program is waiting for a starting or ending search node
            if (!this.InitializeSimulation &&
                !this.EdgeModeRadioButton.Checked &&
                !this.NodeDeleteMode &&
                !this.NodeEditMode &&
                !this.EdgeDeleteMode &&
                !this.EdgeEditMode
               )
            {
                // Change cursor to default
                // Debounce to prevent too much calls
                this.MouseDefaultDebouncer.Debounce(() => { this.Cursor = Cursors.Default; }, 5, SynchronizationContext.Current);

                return;
            }

            // Node movement cursor only when drag is started
            else if (this.NodeEditMode && this.NodeDragged != null)
            {
                // Change cursor to size-all
                // Debounce to prevent too much calls
                this.MouseDefaultDebouncer.Debounce(() => { this.Cursor = Cursors.SizeAll; }, 5, SynchronizationContext.Current);

                return;
            }

            // Record mouse position upon call of function
            Point mousePosition = this.DrawingPanel.PointToClient(Control.MousePosition);

            // Traverse and check if there is a node hovered
            // Debounce it to prevent costly linear seach
            Node? hoveredNode = null;
            Edge? hoveredEdge = null;

            if (this.EdgeDeleteMode || this.EdgeEditMode)
            {
                hoveredEdge = this.MouseHoverDebouncer.Debounce(() => this.GetHoveredEdge(this.edges, mousePosition.X, mousePosition.Y), 5);
            }
            else
            {
                hoveredNode = this.MouseHoverDebouncer.Debounce(() => this.GetHoveredNode(this.nodes, mousePosition.X, mousePosition.Y), 5);
            }

            if (hoveredNode != null || hoveredEdge != null)
            {
                this.Cursor = Cursors.Hand;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        // Used for checking if hovered on a node or edge
        private void DrawingPanel_MouseHover(object sender, EventArgs e)
        {
            this.CursorHandWhenNodeOrEdgeHovered();
        }

        private void DrawingPanel_MouseMove(object sender, MouseEventArgs e)
        {
            this.CursorHandWhenNodeOrEdgeHovered();

            // For node dragability
            if (this.NodeEditMode)
            {
                if (this.MouseDragging && this.NodeDragged != null)
                {
                    // Check if mouse is out of bounds 
                    if (e.Y <= this.NodeDragged.Radius ||
                    e.X >= this.DrawingPanel.Width - this.NodeDragged.Radius ||
                    e.Y >= this.DrawingPanel.Height - this.NodeDragged.Radius ||
                    e.X <= this.NodeDragged.Radius)
                    {
                        return;
                    }

                    this.NodeDragged.X = e.X;
                    this.NodeDragged.Y = e.Y;

                    this.RenderEntities();
                }
            }
        }

        private (int, int) FindNewNodePosition(int X, int Y)
        {
            if (this.NodeDragged == null) return (X, Y);

            // Check for out of bounds
            if (Y <= this.NodeDragged.Radius ||
                X >= this.DrawingPanel.Width - this.NodeDragged.Radius ||
                Y >= this.DrawingPanel.Height - this.NodeDragged.Radius ||
                X <= this.NodeDragged.Radius)
            {
                return (X, Y);
            }

            foreach (var direction in this.Directions)
            {
                var newX = X + direction.Item1 * this.NodeDragged.Diameter;
                var newY = Y + direction.Item2 * this.NodeDragged.Diameter;

                if (FindNewNodePositionUtil(newX, newY))
                {
                    return (newX, newY);
                }
            }

            // If no vacant spot is found in the immediate vicinity, recursively search in all this.Directions except self
            for (int i = 1; i < this.Directions.Count; i++)
            {
                var newX = X + this.Directions[i].Item1 * this.NodeDragged.Diameter;
                var newY = Y + this.Directions[i].Item2 * this.NodeDragged.Diameter;

                var result = FindNewNodePosition(newX, newY);

                if (result != (newX, newY))  // If a vacant spot is found, return it
                {
                    return result;
                }
            }

            // If no vacant spot is found after checking all possible positions, return the original position
            return (X, Y);
        }

        private bool FindNewNodePositionUtil(int X, int Y)
        {
            if (this.NodeDragged == null) return false;

            // Check for out of bounds
            if (Y <= this.NodeDragged.Radius ||
                X >= this.DrawingPanel.Width - this.NodeDragged.Radius ||
                Y >= this.DrawingPanel.Height - this.NodeDragged.Radius ||
                X <= this.NodeDragged.Radius)
            {
                return false;
            }

            bool vacant = true;

            foreach (Node existingNode in this.nodes)
            {
                if (existingNode == this.NodeDragged) continue;

                if (Helpers.Helpers.dist(existingNode.X, existingNode.Y, X, Y) <= existingNode.Diameter)
                {
                    vacant = false;
                    break;
                }
            }

            return vacant;
        }

        private void DrawingPanel_MouseUp(object sender, MouseEventArgs e)
        {
            // For node dragability
            if (this.MouseDragging && this.NodeDragged != null)
            {
                // Check if mouse is out of bounds 
                if (!(e.Y <= this.NodeDragged.Radius ||
                e.X >= this.DrawingPanel.Width - this.NodeDragged.Radius ||
                e.Y >= this.DrawingPanel.Height - this.NodeDragged.Radius ||
                e.X <= this.NodeDragged.Radius))
                {
                    var (newX, newY) = this.FindNewNodePosition(e.X, e.Y);

                    this.NodeDragged.X = newX;
                    this.NodeDragged.Y = newY;
                }

                this.RenderEntities();

                double elapsed = this.DragDuration.Elapsed.TotalMilliseconds;

                if (this.NodeHeuristicsEditable && elapsed <= DRAG_THRESHOLD)
                {
                    this.EditNodeHeuristics(this.NodeDragged);
                }

                this.MouseDragging = false;
                this.NodeDragged = null;
                this.DragDuration.Stop();
            }
        }

        private void DrawingPanel_MouseLeave(object sender, EventArgs e)
        {
            // Reset cursor when it leaves the drawing panel
            this.Cursor = Cursors.Default;
        }

        private void SimulationSpeedTrackBar_Scroll(object sender, EventArgs e)
        {
            this.AnimationClock.Interval = this.GetAnimationSpeed();
        }

        // Utility function to get the current animation speed
        private int GetAnimationSpeed()
        {
            // Set correct clock interval based on simulation speed value
            // Upper limit speed is 3 seconds
            // Lower limit speed is 300 milliseconds
            // Lower simulation speed trackbar value, the slower it is
            // 1.1 is used to invert the value since 1 - (10 / 10) = 0 
            // Yields interval of 0 (invalid)
            return (int)(SIMULATION_SPEED_UPPER_LIMIT * (1.1 - this.SimulationSpeedTrackBar.Value / (float)this.SimulationSpeedTrackBar.Maximum));
        }

        // Toggle open the logs panel
        private void LogsOpenButton_Click(object sender, EventArgs e)
        {
            // Debouce to prevent spam
            this.LogsDebouncer.Debounce(() => this.ToggleLogs(), 300, SynchronizationContext.Current);
        }

        // Utility function to toggle the logs
        // This is so that the toggle function can be debounced
        private void ToggleLogs()
        {
            // Close logs
            if (this.LogsOpened)
            {
                // Change button icon to expand icon
                this.LogsOpenButton.Image = Resources.expand_icon16;
                this.LogsForm.Hide();
            }

            // Open logs
            else
            {
                // Change button icon to collapse icon
                this.LogsOpenButton.Image = Resources.collapse_icon16;
                this.LogsForm.Show();
            }

            // Toggle flag
            this.LogsOpened = !this.LogsOpened;
        }

        // Event handler when algorithm dropdown changes selected value
        private void AlgorithmDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            this.UpdateControlPanelSettings();
        }

        // Utility function to update default control panel settings based on algorithm selected
        private void UpdateControlPanelSettings()
        {
            var selected = this.AlgorithmDropdown.SelectedItem.ToString();

            // Restore default state
            this.RestoreDefaultControlPanelState();

            switch (selected)
            {
                // Node edit button is enabled for all cases to enable the node dragging ability

                case "DFS":
                case "BFS":
                    // Render default controls for DFS and BFS
                    // Similar settings
                    this.CustomHCostCheckBox.Enabled = false;
                    this.CustomGCostCheckBox.Enabled = false;
                    this.CustomHCostCheckBox.Checked = false;
                    this.CustomGCostCheckBox.Checked = false;

                    this.EdgeEditButton.Enabled = false;

                    this.NodeHeuristicsEditable = false;
                    this.EdgeWeightEditable = false;

                    this.ShowWeightsText = false;
                    this.ShowHeuristicsText = false;

                    break;

                case "Beam":
                case "Hill Climbing":
                    // Render default controls for Beam and Hill Climbing
                    this.CustomGCostCheckBox.Enabled = false;
                    this.CustomGCostCheckBox.Checked = false;
                    this.HasTargetNodeCheckBox.Enabled = false;
                    this.HasTargetNodeCheckBox.Checked = true;

                    this.EdgeEditButton.Enabled = false;

                    this.NodeHeuristicsEditable = true;
                    this.EdgeWeightEditable = false;

                    this.ShowWeightsText = false;
                    this.ShowHeuristicsText = true;

                    break;

                case "Branch-and-Bound":
                    // Render default controls for Branch and Bound
                    this.CustomGCostCheckBox.Checked = true;
                    this.CustomHCostCheckBox.Enabled = false;
                    this.CustomHCostCheckBox.Checked = false;
                    this.HasTargetNodeCheckBox.Enabled = true;
                    this.HasTargetNodeCheckBox.Checked = true;

                    this.NodeHeuristicsEditable = false;
                    this.EdgeWeightEditable = true;

                    this.ShowWeightsText = true;
                    this.ShowHeuristicsText = false;

                    break;

                case "A*":
                    // Render default controls for A* (A-star)
                    this.CustomGCostCheckBox.Checked = true;
                    this.CustomHCostCheckBox.Checked = true;
                    this.HasTargetNodeCheckBox.Enabled = false;
                    this.HasTargetNodeCheckBox.Checked = true;

                    this.NodeHeuristicsEditable = true;
                    this.EdgeWeightEditable = true;

                    this.ShowWeightsText = true;
                    this.ShowHeuristicsText = true;

                    break;

                default:
                    throw new Exception("Unimplemented algorithm is selected.");
            }

            // Refresh drawing panel
            this.DrawingPanel.Invalidate();
        }

        // Utility function to reset control panel controls state
        // Used to set the initial image of the control panel
        private void RestoreDefaultControlPanelState()
        {
            this.ShowWeightsText = true;
            this.ShowHeuristicsText = true;
            this.HasTargetNodeCheckBox.Enabled = true;
            this.CustomHCostCheckBox.Enabled = true;
            this.CustomGCostCheckBox.Enabled = true;

            this.NodeEditButton.Enabled = true;
            this.EdgeEditButton.Enabled = true;

            this.HasTargetNodeCheckBox.Checked = false;

            this.NodeHeuristicsEditable = false;
            this.EdgeWeightEditable = false;
        }

        private void SimulationStepBackButton_Click(object sender, EventArgs e)
        {
            this.AnimationStepBackward();
        }


        private void SimulationStepForwardButton_Click(object sender, EventArgs e)
        {
            this.AnimationStepForward();
        }

        // Used to step forward through the animation
        // Only when animation is paused
        // Only also when traversed node index < search path length - 1
        private void AnimationStepForward()
        {
            if (this.SearchPath != null && this.traversedNodeIndex >= this.SearchPath.Count) return;

            // Call animation tick once to show the changes
            // Using the current index since at this point, the index is incremented already after animation
            this.AnimateCurrentNode(this.traversedNodeIndex);

            // Update simulation navigations availability
            this.UpdateSimulationNavigationButtonsAvailability();
        }

        // Used to step back through the animation
        // Only when animation is paused
        // Only also when traversed node index > 0
        private void AnimationStepBackward()
        {
            if (this.traversedNodeIndex <= 0) return;

            // Call animation tick once to show the changes
            // Using -2 index since the current index is already updated by the animation at this point
            this.AnimateCurrentNode(this.traversedNodeIndex - 2);

            // Update simulation navigations availability
            this.UpdateSimulationNavigationButtonsAvailability();
        }

        // Sets deletion mode and waits for user to click a node to be deleted
        private void NodeDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.NodeDeleteMode) return;

            // Set delete flag and unset all other flags if active
            this.NodeEditMode = this.EdgeEditMode = this.EdgeDeleteMode = false;

            this.NodeDeleteMode = true;

            // Reset checked state for the radio buttons as they will be the one to cancel the deletion
            this.NodeModeRadioButton.Checked = this.EdgeModeRadioButton.Checked = false;
        }

        private void NodeEditButton_Click(object sender, EventArgs e)
        {
            if (this.NodeEditMode) return;

            // Set delete flag and unset all other flags if active
            this.NodeDeleteMode = this.EdgeEditMode = this.EdgeDeleteMode = false;

            this.NodeEditMode = true;

            // Reset checked state for the radio buttons as they will be the one to cancel the deletion
            this.NodeModeRadioButton.Checked = this.EdgeModeRadioButton.Checked = false;
        }

        private void EdgeEditButton_Click(object sender, EventArgs e)
        {
            if (this.EdgeEditMode) return;

            // Set delete flag and unset all other flags if active
            this.NodeDeleteMode = this.NodeEditMode = this.EdgeDeleteMode = false;

            this.EdgeEditMode = true;

            // Reset checked state for the radio buttons as they will be the one to cancel the deletion
            this.NodeModeRadioButton.Checked = this.EdgeModeRadioButton.Checked = false;
        }

        private void EdgeDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.EdgeDeleteMode) return;

            // Set delete flag and unset all other flags if active
            this.NodeDeleteMode = this.NodeEditMode = this.EdgeEditMode = false;

            this.EdgeDeleteMode = true;

            // Reset checked state for the radio buttons as they will be the one to cancel the deletion
            this.NodeModeRadioButton.Checked = this.EdgeModeRadioButton.Checked = false;
        }
    }
}