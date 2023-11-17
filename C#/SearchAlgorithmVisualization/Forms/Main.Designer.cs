namespace SearchAlgorithmVisualization
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ControlPanel = new Panel();
            LogsPanel = new Panel();
            LogsLabel = new Label();
            LogsOpenButton = new Button();
            HasTargetNodePanel = new Panel();
            CustomHCostCheckBox = new CheckBox();
            CustomGCostCheckBox = new CheckBox();
            CustomHCostLabel = new Label();
            CustomGCostLabel = new Label();
            HasTargetNodeCheckBox = new CheckBox();
            HasTargetNodeLabel = new Label();
            ResetButton = new Button();
            RunSimulationButton = new Button();
            CreationSectionLabel = new Label();
            ControlPanelMainLabel = new Label();
            CreationModePanel = new Panel();
            NodeEditButton = new Button();
            NodeDeleteButton = new Button();
            EdgeModeRadioButton = new RadioButton();
            NodeModeRadioButton = new RadioButton();
            EdgeModeLabel = new Label();
            NodeModeLabel = new Label();
            AlgorithmsPanel = new Panel();
            AlgorithmDropdown = new ComboBox();
            AlgorithmSelectionLabel = new Label();
            SimulationStatusLabel = new Label();
            SimulationSpeedPanel = new Panel();
            SimulationStepBackButton = new Button();
            SimulationStepForwardButton = new Button();
            SimulationNavigationLabel = new Label();
            SimulationSpeedLabel = new Label();
            SimulationSpeedTrackBar = new TrackBar();
            SimulationContolsLabel = new Label();
            DrawingPanel = new Panel();
            AnimationClock = new System.Windows.Forms.Timer(components);
            ControlPanel.SuspendLayout();
            LogsPanel.SuspendLayout();
            HasTargetNodePanel.SuspendLayout();
            CreationModePanel.SuspendLayout();
            AlgorithmsPanel.SuspendLayout();
            SimulationSpeedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)SimulationSpeedTrackBar).BeginInit();
            SuspendLayout();
            // 
            // ControlPanel
            // 
            ControlPanel.BackColor = Color.DarkOrchid;
            ControlPanel.Controls.Add(LogsPanel);
            ControlPanel.Controls.Add(HasTargetNodePanel);
            ControlPanel.Controls.Add(ResetButton);
            ControlPanel.Controls.Add(RunSimulationButton);
            ControlPanel.Controls.Add(CreationSectionLabel);
            ControlPanel.Controls.Add(ControlPanelMainLabel);
            ControlPanel.Controls.Add(CreationModePanel);
            ControlPanel.Controls.Add(AlgorithmsPanel);
            ControlPanel.Controls.Add(SimulationStatusLabel);
            ControlPanel.Controls.Add(SimulationSpeedPanel);
            ControlPanel.Dock = DockStyle.Right;
            ControlPanel.Location = new Point(1215, 0);
            ControlPanel.Margin = new Padding(0);
            ControlPanel.Name = "ControlPanel";
            ControlPanel.Size = new Size(269, 861);
            ControlPanel.TabIndex = 1;
            ControlPanel.Paint += ControlPanel_Paint;
            // 
            // LogsPanel
            // 
            LogsPanel.BackColor = Color.MediumPurple;
            LogsPanel.Controls.Add(LogsLabel);
            LogsPanel.Controls.Add(LogsOpenButton);
            LogsPanel.Location = new Point(22, 458);
            LogsPanel.Name = "LogsPanel";
            LogsPanel.Size = new Size(220, 57);
            LogsPanel.TabIndex = 8;
            // 
            // LogsLabel
            // 
            LogsLabel.BackColor = Color.MediumPurple;
            LogsLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            LogsLabel.ForeColor = Color.WhiteSmoke;
            LogsLabel.Location = new Point(23, 9);
            LogsLabel.Name = "LogsLabel";
            LogsLabel.Size = new Size(80, 40);
            LogsLabel.TabIndex = 2;
            LogsLabel.Text = "Logs";
            LogsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LogsOpenButton
            // 
            LogsOpenButton.BackColor = Color.MediumPurple;
            LogsOpenButton.Cursor = Cursors.Hand;
            LogsOpenButton.Font = new Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            LogsOpenButton.Image = Properties.Resources.expand_icon16;
            LogsOpenButton.Location = new Point(154, 9);
            LogsOpenButton.Name = "LogsOpenButton";
            LogsOpenButton.Size = new Size(41, 40);
            LogsOpenButton.TabIndex = 0;
            LogsOpenButton.TextAlign = ContentAlignment.TopCenter;
            LogsOpenButton.UseVisualStyleBackColor = false;
            LogsOpenButton.Click += LogsOpenButton_Click;
            // 
            // HasTargetNodePanel
            // 
            HasTargetNodePanel.BackColor = Color.MediumPurple;
            HasTargetNodePanel.Controls.Add(CustomHCostCheckBox);
            HasTargetNodePanel.Controls.Add(CustomGCostCheckBox);
            HasTargetNodePanel.Controls.Add(CustomHCostLabel);
            HasTargetNodePanel.Controls.Add(CustomGCostLabel);
            HasTargetNodePanel.Controls.Add(HasTargetNodeCheckBox);
            HasTargetNodePanel.Controls.Add(HasTargetNodeLabel);
            HasTargetNodePanel.Location = new Point(22, 206);
            HasTargetNodePanel.Name = "HasTargetNodePanel";
            HasTargetNodePanel.Size = new Size(220, 151);
            HasTargetNodePanel.TabIndex = 7;
            // 
            // CustomHCostCheckBox
            // 
            CustomHCostCheckBox.AutoSize = true;
            CustomHCostCheckBox.Cursor = Cursors.Hand;
            CustomHCostCheckBox.Location = new Point(173, 110);
            CustomHCostCheckBox.Name = "CustomHCostCheckBox";
            CustomHCostCheckBox.Size = new Size(15, 14);
            CustomHCostCheckBox.TabIndex = 16;
            CustomHCostCheckBox.UseVisualStyleBackColor = true;
            // 
            // CustomGCostCheckBox
            // 
            CustomGCostCheckBox.AutoSize = true;
            CustomGCostCheckBox.Cursor = Cursors.Hand;
            CustomGCostCheckBox.Location = new Point(173, 70);
            CustomGCostCheckBox.Name = "CustomGCostCheckBox";
            CustomGCostCheckBox.Size = new Size(15, 14);
            CustomGCostCheckBox.TabIndex = 15;
            CustomGCostCheckBox.UseVisualStyleBackColor = true;
            // 
            // CustomHCostLabel
            // 
            CustomHCostLabel.BackColor = Color.MediumPurple;
            CustomHCostLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CustomHCostLabel.ForeColor = Color.WhiteSmoke;
            CustomHCostLabel.Location = new Point(23, 95);
            CustomHCostLabel.Name = "CustomHCostLabel";
            CustomHCostLabel.Size = new Size(131, 40);
            CustomHCostLabel.TabIndex = 14;
            CustomHCostLabel.Text = "Custom H-Cost";
            CustomHCostLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CustomGCostLabel
            // 
            CustomGCostLabel.BackColor = Color.MediumPurple;
            CustomGCostLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CustomGCostLabel.ForeColor = Color.WhiteSmoke;
            CustomGCostLabel.Location = new Point(23, 55);
            CustomGCostLabel.Name = "CustomGCostLabel";
            CustomGCostLabel.Size = new Size(131, 40);
            CustomGCostLabel.TabIndex = 13;
            CustomGCostLabel.Text = "Custom G-Cost";
            CustomGCostLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // HasTargetNodeCheckBox
            // 
            HasTargetNodeCheckBox.AutoSize = true;
            HasTargetNodeCheckBox.Cursor = Cursors.Hand;
            HasTargetNodeCheckBox.Location = new Point(173, 30);
            HasTargetNodeCheckBox.Name = "HasTargetNodeCheckBox";
            HasTargetNodeCheckBox.Size = new Size(15, 14);
            HasTargetNodeCheckBox.TabIndex = 11;
            HasTargetNodeCheckBox.UseVisualStyleBackColor = true;
            // 
            // HasTargetNodeLabel
            // 
            HasTargetNodeLabel.BackColor = Color.MediumPurple;
            HasTargetNodeLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            HasTargetNodeLabel.ForeColor = Color.WhiteSmoke;
            HasTargetNodeLabel.Location = new Point(23, 15);
            HasTargetNodeLabel.Name = "HasTargetNodeLabel";
            HasTargetNodeLabel.Size = new Size(144, 40);
            HasTargetNodeLabel.TabIndex = 10;
            HasTargetNodeLabel.Text = "Has Target Node";
            HasTargetNodeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ResetButton
            // 
            ResetButton.BackColor = Color.MediumPurple;
            ResetButton.Cursor = Cursors.Hand;
            ResetButton.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            ResetButton.ForeColor = Color.WhiteSmoke;
            ResetButton.Location = new Point(33, 781);
            ResetButton.Name = "ResetButton";
            ResetButton.Size = new Size(95, 40);
            ResetButton.TabIndex = 9;
            ResetButton.Text = "Reset";
            ResetButton.UseVisualStyleBackColor = false;
            ResetButton.Click += ResetButton_Click;
            // 
            // RunSimulationButton
            // 
            RunSimulationButton.BackColor = Color.MediumPurple;
            RunSimulationButton.Cursor = Cursors.Hand;
            RunSimulationButton.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            RunSimulationButton.ForeColor = Color.WhiteSmoke;
            RunSimulationButton.Location = new Point(136, 781);
            RunSimulationButton.Name = "RunSimulationButton";
            RunSimulationButton.Size = new Size(99, 40);
            RunSimulationButton.TabIndex = 8;
            RunSimulationButton.Text = "Run";
            RunSimulationButton.UseVisualStyleBackColor = false;
            RunSimulationButton.Click += RunSimulationButton_Click;
            // 
            // CreationSectionLabel
            // 
            CreationSectionLabel.BackColor = Color.MediumPurple;
            CreationSectionLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            CreationSectionLabel.ForeColor = Color.WhiteSmoke;
            CreationSectionLabel.Location = new Point(26, 81);
            CreationSectionLabel.Name = "CreationSectionLabel";
            CreationSectionLabel.Size = new Size(214, 40);
            CreationSectionLabel.TabIndex = 1;
            CreationSectionLabel.Text = "Graph Objects";
            CreationSectionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ControlPanelMainLabel
            // 
            ControlPanelMainLabel.BackColor = Color.Transparent;
            ControlPanelMainLabel.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ControlPanelMainLabel.ForeColor = Color.WhiteSmoke;
            ControlPanelMainLabel.Location = new Point(3, 25);
            ControlPanelMainLabel.Name = "ControlPanelMainLabel";
            ControlPanelMainLabel.Size = new Size(262, 40);
            ControlPanelMainLabel.TabIndex = 0;
            ControlPanelMainLabel.Text = "Control Panel";
            ControlPanelMainLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // CreationModePanel
            // 
            CreationModePanel.BackColor = Color.MediumPurple;
            CreationModePanel.Controls.Add(NodeEditButton);
            CreationModePanel.Controls.Add(NodeDeleteButton);
            CreationModePanel.Controls.Add(EdgeModeRadioButton);
            CreationModePanel.Controls.Add(NodeModeRadioButton);
            CreationModePanel.Controls.Add(EdgeModeLabel);
            CreationModePanel.Controls.Add(NodeModeLabel);
            CreationModePanel.Location = new Point(22, 80);
            CreationModePanel.Name = "CreationModePanel";
            CreationModePanel.Size = new Size(220, 120);
            CreationModePanel.TabIndex = 6;
            // 
            // NodeEditButton
            // 
            NodeEditButton.Cursor = Cursors.Hand;
            NodeEditButton.Image = Properties.Resources.edit_icon16;
            NodeEditButton.Location = new Point(36, 46);
            NodeEditButton.Name = "NodeEditButton";
            NodeEditButton.Size = new Size(26, 23);
            NodeEditButton.TabIndex = 6;
            NodeEditButton.UseVisualStyleBackColor = true;
            NodeEditButton.Click += NodeEditButton_Click;
            // 
            // NodeDeleteButton
            // 
            NodeDeleteButton.Cursor = Cursors.Hand;
            NodeDeleteButton.Image = Properties.Resources.trash_icon16;
            NodeDeleteButton.Location = new Point(68, 46);
            NodeDeleteButton.Name = "NodeDeleteButton";
            NodeDeleteButton.Size = new Size(26, 23);
            NodeDeleteButton.TabIndex = 0;
            NodeDeleteButton.UseVisualStyleBackColor = true;
            NodeDeleteButton.Click += NodeDeleteButton_Click;
            // 
            // EdgeModeRadioButton
            // 
            EdgeModeRadioButton.Cursor = Cursors.Hand;
            EdgeModeRadioButton.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            EdgeModeRadioButton.Location = new Point(175, 79);
            EdgeModeRadioButton.Name = "EdgeModeRadioButton";
            EdgeModeRadioButton.Size = new Size(13, 30);
            EdgeModeRadioButton.TabIndex = 5;
            EdgeModeRadioButton.TextAlign = ContentAlignment.MiddleCenter;
            EdgeModeRadioButton.UseVisualStyleBackColor = true;
            EdgeModeRadioButton.CheckedChanged += EdgeModeRadioButton_CheckedChanged;
            // 
            // NodeModeRadioButton
            // 
            NodeModeRadioButton.Checked = true;
            NodeModeRadioButton.Cursor = Cursors.Hand;
            NodeModeRadioButton.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            NodeModeRadioButton.Location = new Point(175, 43);
            NodeModeRadioButton.Name = "NodeModeRadioButton";
            NodeModeRadioButton.Size = new Size(13, 30);
            NodeModeRadioButton.TabIndex = 4;
            NodeModeRadioButton.TabStop = true;
            NodeModeRadioButton.TextAlign = ContentAlignment.MiddleCenter;
            NodeModeRadioButton.UseVisualStyleBackColor = true;
            NodeModeRadioButton.CheckedChanged += NodeModeRadioButton_CheckedChanged;
            // 
            // EdgeModeLabel
            // 
            EdgeModeLabel.BackColor = Color.Transparent;
            EdgeModeLabel.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            EdgeModeLabel.ForeColor = Color.WhiteSmoke;
            EdgeModeLabel.Location = new Point(85, 69);
            EdgeModeLabel.Name = "EdgeModeLabel";
            EdgeModeLabel.Size = new Size(84, 40);
            EdgeModeLabel.TabIndex = 3;
            EdgeModeLabel.Text = "Edge";
            EdgeModeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // NodeModeLabel
            // 
            NodeModeLabel.BackColor = Color.Transparent;
            NodeModeLabel.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            NodeModeLabel.ForeColor = Color.WhiteSmoke;
            NodeModeLabel.Location = new Point(85, 36);
            NodeModeLabel.Name = "NodeModeLabel";
            NodeModeLabel.Size = new Size(84, 40);
            NodeModeLabel.TabIndex = 2;
            NodeModeLabel.Text = "Node";
            NodeModeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // AlgorithmsPanel
            // 
            AlgorithmsPanel.BackColor = Color.MediumPurple;
            AlgorithmsPanel.Controls.Add(AlgorithmDropdown);
            AlgorithmsPanel.Controls.Add(AlgorithmSelectionLabel);
            AlgorithmsPanel.Location = new Point(22, 363);
            AlgorithmsPanel.Name = "AlgorithmsPanel";
            AlgorithmsPanel.Size = new Size(220, 89);
            AlgorithmsPanel.TabIndex = 7;
            // 
            // AlgorithmDropdown
            // 
            AlgorithmDropdown.BackColor = Color.WhiteSmoke;
            AlgorithmDropdown.Cursor = Cursors.Hand;
            AlgorithmDropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            AlgorithmDropdown.FlatStyle = FlatStyle.System;
            AlgorithmDropdown.Font = new Font("Arial", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            AlgorithmDropdown.FormattingEnabled = true;
            AlgorithmDropdown.Items.AddRange(new object[] { "DFS", "BFS", "Beam", "Branch-and-Bound", "Hill Climbing", "A*" });
            AlgorithmDropdown.Location = new Point(23, 46);
            AlgorithmDropdown.Name = "AlgorithmDropdown";
            AlgorithmDropdown.RightToLeft = RightToLeft.No;
            AlgorithmDropdown.Size = new Size(172, 26);
            AlgorithmDropdown.TabIndex = 3;
            AlgorithmDropdown.SelectedValueChanged += AlgorithmDropdown_SelectedValueChanged;
            // 
            // AlgorithmSelectionLabel
            // 
            AlgorithmSelectionLabel.BackColor = Color.MediumPurple;
            AlgorithmSelectionLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            AlgorithmSelectionLabel.ForeColor = Color.WhiteSmoke;
            AlgorithmSelectionLabel.Location = new Point(3, 4);
            AlgorithmSelectionLabel.Name = "AlgorithmSelectionLabel";
            AlgorithmSelectionLabel.Size = new Size(214, 40);
            AlgorithmSelectionLabel.TabIndex = 2;
            AlgorithmSelectionLabel.Text = "Searching Algorithm";
            AlgorithmSelectionLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SimulationStatusLabel
            // 
            SimulationStatusLabel.BackColor = Color.Transparent;
            SimulationStatusLabel.Font = new Font("Arial", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            SimulationStatusLabel.ForeColor = Color.WhiteSmoke;
            SimulationStatusLabel.Location = new Point(130, 738);
            SimulationStatusLabel.Name = "SimulationStatusLabel";
            SimulationStatusLabel.Size = new Size(67, 40);
            SimulationStatusLabel.TabIndex = 4;
            SimulationStatusLabel.Text = "Status";
            SimulationStatusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SimulationSpeedPanel
            // 
            SimulationSpeedPanel.BackColor = Color.MediumPurple;
            SimulationSpeedPanel.Controls.Add(SimulationStepBackButton);
            SimulationSpeedPanel.Controls.Add(SimulationStepForwardButton);
            SimulationSpeedPanel.Controls.Add(SimulationNavigationLabel);
            SimulationSpeedPanel.Controls.Add(SimulationSpeedLabel);
            SimulationSpeedPanel.Controls.Add(SimulationSpeedTrackBar);
            SimulationSpeedPanel.Controls.Add(SimulationContolsLabel);
            SimulationSpeedPanel.Location = new Point(23, 521);
            SimulationSpeedPanel.Name = "SimulationSpeedPanel";
            SimulationSpeedPanel.Size = new Size(220, 214);
            SimulationSpeedPanel.TabIndex = 8;
            // 
            // SimulationStepBackButton
            // 
            SimulationStepBackButton.BackColor = Color.MediumPurple;
            SimulationStepBackButton.Cursor = Cursors.Hand;
            SimulationStepBackButton.Enabled = false;
            SimulationStepBackButton.Font = new Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            SimulationStepBackButton.Location = new Point(67, 156);
            SimulationStepBackButton.Name = "SimulationStepBackButton";
            SimulationStepBackButton.Size = new Size(46, 35);
            SimulationStepBackButton.TabIndex = 14;
            SimulationStepBackButton.Text = "«";
            SimulationStepBackButton.TextAlign = ContentAlignment.TopCenter;
            SimulationStepBackButton.UseVisualStyleBackColor = false;
            SimulationStepBackButton.Click += SimulationStepBackButton_Click;
            // 
            // SimulationStepForwardButton
            // 
            SimulationStepForwardButton.BackColor = Color.MediumPurple;
            SimulationStepForwardButton.Cursor = Cursors.Hand;
            SimulationStepForwardButton.Enabled = false;
            SimulationStepForwardButton.Font = new Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            SimulationStepForwardButton.Location = new Point(115, 156);
            SimulationStepForwardButton.Name = "SimulationStepForwardButton";
            SimulationStepForwardButton.Size = new Size(46, 35);
            SimulationStepForwardButton.TabIndex = 13;
            SimulationStepForwardButton.Text = "»";
            SimulationStepForwardButton.TextAlign = ContentAlignment.TopCenter;
            SimulationStepForwardButton.UseVisualStyleBackColor = false;
            SimulationStepForwardButton.Click += SimulationStepForwardButton_Click;
            // 
            // SimulationNavigationLabel
            // 
            SimulationNavigationLabel.BackColor = Color.Transparent;
            SimulationNavigationLabel.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SimulationNavigationLabel.ForeColor = Color.WhiteSmoke;
            SimulationNavigationLabel.Location = new Point(22, 126);
            SimulationNavigationLabel.Name = "SimulationNavigationLabel";
            SimulationNavigationLabel.Size = new Size(95, 27);
            SimulationNavigationLabel.TabIndex = 11;
            SimulationNavigationLabel.Text = "Navigation";
            SimulationNavigationLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SimulationSpeedLabel
            // 
            SimulationSpeedLabel.BackColor = Color.Transparent;
            SimulationSpeedLabel.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            SimulationSpeedLabel.ForeColor = Color.WhiteSmoke;
            SimulationSpeedLabel.Location = new Point(22, 43);
            SimulationSpeedLabel.Name = "SimulationSpeedLabel";
            SimulationSpeedLabel.Size = new Size(56, 27);
            SimulationSpeedLabel.TabIndex = 6;
            SimulationSpeedLabel.Text = "Speed";
            SimulationSpeedLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // SimulationSpeedTrackBar
            // 
            SimulationSpeedTrackBar.BackColor = Color.DarkViolet;
            SimulationSpeedTrackBar.Cursor = Cursors.SizeWE;
            SimulationSpeedTrackBar.Location = new Point(22, 73);
            SimulationSpeedTrackBar.Minimum = 1;
            SimulationSpeedTrackBar.Name = "SimulationSpeedTrackBar";
            SimulationSpeedTrackBar.Size = new Size(172, 45);
            SimulationSpeedTrackBar.TabIndex = 10;
            SimulationSpeedTrackBar.Value = 5;
            SimulationSpeedTrackBar.Scroll += SimulationSpeedTrackBar_Scroll;
            // 
            // SimulationContolsLabel
            // 
            SimulationContolsLabel.BackColor = Color.MediumPurple;
            SimulationContolsLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            SimulationContolsLabel.ForeColor = Color.WhiteSmoke;
            SimulationContolsLabel.Location = new Point(3, 3);
            SimulationContolsLabel.Name = "SimulationContolsLabel";
            SimulationContolsLabel.Size = new Size(214, 40);
            SimulationContolsLabel.TabIndex = 4;
            SimulationContolsLabel.Text = "Simulation Controls";
            SimulationContolsLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawingPanel
            // 
            DrawingPanel.BackColor = Color.White;
            DrawingPanel.Dock = DockStyle.Fill;
            DrawingPanel.Location = new Point(0, 0);
            DrawingPanel.Margin = new Padding(0);
            DrawingPanel.Name = "DrawingPanel";
            DrawingPanel.Size = new Size(1215, 861);
            DrawingPanel.TabIndex = 2;
            DrawingPanel.Paint += DrawingPanel_Paint;
            DrawingPanel.MouseDown += DrawingPanel_MouseDown;
            DrawingPanel.MouseLeave += DrawingPanel_MouseLeave;
            DrawingPanel.MouseHover += DrawingPanel_MouseHover;
            DrawingPanel.MouseMove += DrawingPanel_MouseMove;
            // 
            // AnimationClock
            // 
            AnimationClock.Interval = 1000;
            AnimationClock.Tick += AnimationClock_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1484, 861);
            Controls.Add(DrawingPanel);
            Controls.Add(ControlPanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Activity 5 - Searching Algorithms";
            ControlPanel.ResumeLayout(false);
            LogsPanel.ResumeLayout(false);
            HasTargetNodePanel.ResumeLayout(false);
            HasTargetNodePanel.PerformLayout();
            CreationModePanel.ResumeLayout(false);
            AlgorithmsPanel.ResumeLayout(false);
            SimulationSpeedPanel.ResumeLayout(false);
            SimulationSpeedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)SimulationSpeedTrackBar).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel ControlPanel;
        private Label CreationSectionLabel;
        private Label ControlPanelMainLabel;
        private Panel CreationModePanel;
        private RadioButton EdgeModeRadioButton;
        private RadioButton NodeModeRadioButton;
        private Label EdgeModeLabel;
        private Label NodeModeLabel;
        private Panel AlgorithmsPanel;
        private ComboBox AlgorithmDropdown;
        private Label AlgorithmSelectionLabel;
        private Panel DrawingPanel;
        private Button ResetButton;
        private Button RunSimulationButton;
        private System.Windows.Forms.Timer AnimationClock;
        private Label SimulationStatusLabel;
        private TrackBar SimulationSpeedTrackBar;
        private Panel SimulationSpeedPanel;
        private Label SimulationContolsLabel;
        private Panel HasTargetNodePanel;
        private Label HasTargetNodeLabel;
        private CheckBox HasTargetNodeCheckBox;
        private Button LogsOpenButton;
        private Panel LogsPanel;
        private Label LogsLabel;
        private Label CustomHCostLabel;
        private Label CustomGCostLabel;
        private CheckBox CustomHCostCheckBox;
        private CheckBox CustomGCostCheckBox;
        private Button SimulationStepBackButton;
        private Button SimulationStepForwardButton;
        private Label SimulationNavigationLabel;
        private Label SimulationSpeedLabel;
        private Button NodeEditButton;
        private Button NodeDeleteButton;
    }
}