namespace SearchAlgorithmVisualization.Forms
{
    partial class Logs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CurrentNodeLabel = new Label();
            CurrentNodeValue = new Label();
            CurrentSizeLabel = new Label();
            CurrentSizeHelperLabel = new Label();
            CurrentSizeValue = new Label();
            CurrentNPathsLabel = new Label();
            CurrentNPathsValue = new Label();
            CurrentNPathsHelperLabel = new Label();
            CurrentPathCostLabel = new Label();
            CurrentPathCostValue = new Label();
            CurrentPathCostHelperLabel = new Label();
            CurrentPathElementsLabel = new Label();
            panel1 = new Panel();
            CurrentContainerContentsList = new ListBox();
            CurrentContainerContentsHelperLabel2 = new Label();
            CurrentContainerContentsHelperLabel = new Label();
            CurrentContainerContentsLabel2 = new Label();
            CurrentContainerContentsLabel = new Label();
            CurrentPathNodesHelperLabel2 = new Label();
            CurrentPathNodesLabel2 = new Label();
            CurrentPathElementsList = new ListBox();
            CurrentPathElementsHelperLabel = new Label();
            SourceNodeLabel = new Label();
            DestinationNodeLabel = new Label();
            SourceNodeValue = new Label();
            DestinationNodeValue = new Label();
            CurrentAlgorithmValue = new Label();
            label1 = new Label();
            BeamCoefficientValue = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // CurrentNodeLabel
            // 
            CurrentNodeLabel.AutoSize = true;
            CurrentNodeLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentNodeLabel.Location = new Point(24, 65);
            CurrentNodeLabel.Name = "CurrentNodeLabel";
            CurrentNodeLabel.Size = new Size(141, 22);
            CurrentNodeLabel.TabIndex = 0;
            CurrentNodeLabel.Text = "Current Node:";
            // 
            // CurrentNodeValue
            // 
            CurrentNodeValue.AutoSize = true;
            CurrentNodeValue.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentNodeValue.ForeColor = Color.Purple;
            CurrentNodeValue.Location = new Point(171, 65);
            CurrentNodeValue.Name = "CurrentNodeValue";
            CurrentNodeValue.Size = new Size(22, 22);
            CurrentNodeValue.TabIndex = 1;
            CurrentNodeValue.Text = "?";
            // 
            // CurrentSizeLabel
            // 
            CurrentSizeLabel.AutoSize = true;
            CurrentSizeLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentSizeLabel.Location = new Point(24, 160);
            CurrentSizeLabel.Name = "CurrentSizeLabel";
            CurrentSizeLabel.Size = new Size(228, 22);
            CurrentSizeLabel.TabIndex = 2;
            CurrentSizeLabel.Text = "Current Container Size:";
            // 
            // CurrentSizeHelperLabel
            // 
            CurrentSizeHelperLabel.AutoSize = true;
            CurrentSizeHelperLabel.Font = new Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            CurrentSizeHelperLabel.Location = new Point(24, 182);
            CurrentSizeHelperLabel.Name = "CurrentSizeHelperLabel";
            CurrentSizeHelperLabel.Size = new Size(205, 16);
            CurrentSizeHelperLabel.TabIndex = 3;
            CurrentSizeHelperLabel.Text = "Current size of the stack or queue";
            // 
            // CurrentSizeValue
            // 
            CurrentSizeValue.AutoSize = true;
            CurrentSizeValue.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentSizeValue.ForeColor = Color.Purple;
            CurrentSizeValue.Location = new Point(258, 160);
            CurrentSizeValue.Name = "CurrentSizeValue";
            CurrentSizeValue.Size = new Size(22, 22);
            CurrentSizeValue.TabIndex = 4;
            CurrentSizeValue.Text = "?";
            // 
            // CurrentNPathsLabel
            // 
            CurrentNPathsLabel.AutoSize = true;
            CurrentNPathsLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentNPathsLabel.Location = new Point(24, 204);
            CurrentNPathsLabel.Name = "CurrentNPathsLabel";
            CurrentNPathsLabel.Size = new Size(294, 22);
            CurrentNPathsLabel.TabIndex = 5;
            CurrentNPathsLabel.Text = "Current Available Paths Count:";
            // 
            // CurrentNPathsValue
            // 
            CurrentNPathsValue.AutoSize = true;
            CurrentNPathsValue.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentNPathsValue.ForeColor = Color.Purple;
            CurrentNPathsValue.Location = new Point(324, 204);
            CurrentNPathsValue.Name = "CurrentNPathsValue";
            CurrentNPathsValue.Size = new Size(22, 22);
            CurrentNPathsValue.TabIndex = 6;
            CurrentNPathsValue.Text = "?";
            // 
            // CurrentNPathsHelperLabel
            // 
            CurrentNPathsHelperLabel.AutoSize = true;
            CurrentNPathsHelperLabel.Font = new Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            CurrentNPathsHelperLabel.Location = new Point(24, 226);
            CurrentNPathsHelperLabel.Name = "CurrentNPathsHelperLabel";
            CurrentNPathsHelperLabel.Size = new Size(293, 16);
            CurrentNPathsHelperLabel.TabIndex = 7;
            CurrentNPathsHelperLabel.Text = "Number of available paths based on current node";
            // 
            // CurrentPathCostLabel
            // 
            CurrentPathCostLabel.AutoSize = true;
            CurrentPathCostLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentPathCostLabel.Location = new Point(24, 246);
            CurrentPathCostLabel.Name = "CurrentPathCostLabel";
            CurrentPathCostLabel.Size = new Size(183, 22);
            CurrentPathCostLabel.TabIndex = 8;
            CurrentPathCostLabel.Text = "Current Path Cost:";
            // 
            // CurrentPathCostValue
            // 
            CurrentPathCostValue.AutoSize = true;
            CurrentPathCostValue.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentPathCostValue.ForeColor = Color.Purple;
            CurrentPathCostValue.Location = new Point(213, 246);
            CurrentPathCostValue.Name = "CurrentPathCostValue";
            CurrentPathCostValue.Size = new Size(22, 22);
            CurrentPathCostValue.TabIndex = 9;
            CurrentPathCostValue.Text = "?";
            // 
            // CurrentPathCostHelperLabel
            // 
            CurrentPathCostHelperLabel.AutoSize = true;
            CurrentPathCostHelperLabel.Font = new Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            CurrentPathCostHelperLabel.Location = new Point(24, 268);
            CurrentPathCostHelperLabel.Name = "CurrentPathCostHelperLabel";
            CurrentPathCostHelperLabel.Size = new Size(200, 16);
            CurrentPathCostHelperLabel.TabIndex = 10;
            CurrentPathCostHelperLabel.Text = "Accumulated cost of current path";
            // 
            // CurrentPathElementsLabel
            // 
            CurrentPathElementsLabel.AutoSize = true;
            CurrentPathElementsLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentPathElementsLabel.ForeColor = Color.WhiteSmoke;
            CurrentPathElementsLabel.Location = new Point(22, 26);
            CurrentPathElementsLabel.Name = "CurrentPathElementsLabel";
            CurrentPathElementsLabel.Size = new Size(128, 22);
            CurrentPathElementsLabel.TabIndex = 11;
            CurrentPathElementsLabel.Text = "Current Path";
            // 
            // panel1
            // 
            panel1.BackColor = Color.DarkOrchid;
            panel1.Controls.Add(CurrentContainerContentsList);
            panel1.Controls.Add(CurrentContainerContentsHelperLabel2);
            panel1.Controls.Add(CurrentContainerContentsHelperLabel);
            panel1.Controls.Add(CurrentContainerContentsLabel2);
            panel1.Controls.Add(CurrentContainerContentsLabel);
            panel1.Controls.Add(CurrentPathNodesHelperLabel2);
            panel1.Controls.Add(CurrentPathNodesLabel2);
            panel1.Controls.Add(CurrentPathElementsList);
            panel1.Controls.Add(CurrentPathElementsHelperLabel);
            panel1.Controls.Add(CurrentPathElementsLabel);
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(409, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(391, 327);
            panel1.TabIndex = 12;
            // 
            // CurrentContainerContentsList
            // 
            CurrentContainerContentsList.BackColor = Color.MediumPurple;
            CurrentContainerContentsList.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentContainerContentsList.ForeColor = Color.WhiteSmoke;
            CurrentContainerContentsList.FormattingEnabled = true;
            CurrentContainerContentsList.ItemHeight = 22;
            CurrentContainerContentsList.Location = new Point(182, 107);
            CurrentContainerContentsList.Name = "CurrentContainerContentsList";
            CurrentContainerContentsList.Size = new Size(177, 202);
            CurrentContainerContentsList.TabIndex = 21;
            CurrentContainerContentsList.TabStop = false;
            CurrentContainerContentsList.UseTabStops = false;
            // 
            // CurrentContainerContentsHelperLabel2
            // 
            CurrentContainerContentsHelperLabel2.AutoSize = true;
            CurrentContainerContentsHelperLabel2.Font = new Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            CurrentContainerContentsHelperLabel2.ForeColor = Color.WhiteSmoke;
            CurrentContainerContentsHelperLabel2.Location = new Point(182, 87);
            CurrentContainerContentsHelperLabel2.Name = "CurrentContainerContentsHelperLabel2";
            CurrentContainerContentsHelperLabel2.Size = new Size(144, 16);
            CurrentContainerContentsHelperLabel2.TabIndex = 20;
            CurrentContainerContentsHelperLabel2.Text = "container (stack/queue)";
            // 
            // CurrentContainerContentsHelperLabel
            // 
            CurrentContainerContentsHelperLabel.AutoSize = true;
            CurrentContainerContentsHelperLabel.Font = new Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            CurrentContainerContentsHelperLabel.ForeColor = Color.WhiteSmoke;
            CurrentContainerContentsHelperLabel.Location = new Point(182, 71);
            CurrentContainerContentsHelperLabel.Name = "CurrentContainerContentsHelperLabel";
            CurrentContainerContentsHelperLabel.Size = new Size(140, 16);
            CurrentContainerContentsHelperLabel.TabIndex = 19;
            CurrentContainerContentsHelperLabel.Text = "Current contents of the";
            // 
            // CurrentContainerContentsLabel2
            // 
            CurrentContainerContentsLabel2.AutoSize = true;
            CurrentContainerContentsLabel2.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentContainerContentsLabel2.ForeColor = Color.WhiteSmoke;
            CurrentContainerContentsLabel2.Location = new Point(182, 45);
            CurrentContainerContentsLabel2.Name = "CurrentContainerContentsLabel2";
            CurrentContainerContentsLabel2.Size = new Size(101, 22);
            CurrentContainerContentsLabel2.TabIndex = 18;
            CurrentContainerContentsLabel2.Text = "Contents:";
            // 
            // CurrentContainerContentsLabel
            // 
            CurrentContainerContentsLabel.AutoSize = true;
            CurrentContainerContentsLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentContainerContentsLabel.ForeColor = Color.WhiteSmoke;
            CurrentContainerContentsLabel.Location = new Point(182, 24);
            CurrentContainerContentsLabel.Name = "CurrentContainerContentsLabel";
            CurrentContainerContentsLabel.Size = new Size(177, 22);
            CurrentContainerContentsLabel.TabIndex = 17;
            CurrentContainerContentsLabel.Text = "Current Container";
            // 
            // CurrentPathNodesHelperLabel2
            // 
            CurrentPathNodesHelperLabel2.AutoSize = true;
            CurrentPathNodesHelperLabel2.Font = new Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            CurrentPathNodesHelperLabel2.ForeColor = Color.WhiteSmoke;
            CurrentPathNodesHelperLabel2.Location = new Point(22, 87);
            CurrentPathNodesHelperLabel2.Name = "CurrentPathNodesHelperLabel2";
            CurrentPathNodesHelperLabel2.Size = new Size(113, 16);
            CurrentPathNodesHelperLabel2.TabIndex = 16;
            CurrentPathNodesHelperLabel2.Text = "current path taken";
            // 
            // CurrentPathNodesLabel2
            // 
            CurrentPathNodesLabel2.AutoSize = true;
            CurrentPathNodesLabel2.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentPathNodesLabel2.ForeColor = Color.WhiteSmoke;
            CurrentPathNodesLabel2.Location = new Point(22, 48);
            CurrentPathNodesLabel2.Name = "CurrentPathNodesLabel2";
            CurrentPathNodesLabel2.Size = new Size(76, 22);
            CurrentPathNodesLabel2.TabIndex = 15;
            CurrentPathNodesLabel2.Text = "Nodes:";
            // 
            // CurrentPathElementsList
            // 
            CurrentPathElementsList.BackColor = Color.MediumPurple;
            CurrentPathElementsList.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentPathElementsList.ForeColor = Color.WhiteSmoke;
            CurrentPathElementsList.FormattingEnabled = true;
            CurrentPathElementsList.ItemHeight = 22;
            CurrentPathElementsList.Location = new Point(22, 107);
            CurrentPathElementsList.Name = "CurrentPathElementsList";
            CurrentPathElementsList.Size = new Size(128, 202);
            CurrentPathElementsList.TabIndex = 14;
            CurrentPathElementsList.TabStop = false;
            CurrentPathElementsList.UseTabStops = false;
            // 
            // CurrentPathElementsHelperLabel
            // 
            CurrentPathElementsHelperLabel.AutoSize = true;
            CurrentPathElementsHelperLabel.Font = new Font("Arial", 9.75F, FontStyle.Italic, GraphicsUnit.Point);
            CurrentPathElementsHelperLabel.ForeColor = Color.WhiteSmoke;
            CurrentPathElementsHelperLabel.Location = new Point(22, 71);
            CurrentPathElementsHelperLabel.Name = "CurrentPathElementsHelperLabel";
            CurrentPathElementsHelperLabel.Size = new Size(98, 16);
            CurrentPathElementsHelperLabel.TabIndex = 13;
            CurrentPathElementsHelperLabel.Text = "All nodes of the";
            // 
            // SourceNodeLabel
            // 
            SourceNodeLabel.AutoSize = true;
            SourceNodeLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SourceNodeLabel.Location = new Point(24, 97);
            SourceNodeLabel.Name = "SourceNodeLabel";
            SourceNodeLabel.Size = new Size(137, 22);
            SourceNodeLabel.TabIndex = 13;
            SourceNodeLabel.Text = "Source Node:";
            // 
            // DestinationNodeLabel
            // 
            DestinationNodeLabel.AutoSize = true;
            DestinationNodeLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            DestinationNodeLabel.Location = new Point(24, 129);
            DestinationNodeLabel.Name = "DestinationNodeLabel";
            DestinationNodeLabel.Size = new Size(175, 22);
            DestinationNodeLabel.TabIndex = 14;
            DestinationNodeLabel.Text = "Destination Node:";
            // 
            // SourceNodeValue
            // 
            SourceNodeValue.AutoSize = true;
            SourceNodeValue.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            SourceNodeValue.ForeColor = Color.Purple;
            SourceNodeValue.Location = new Point(167, 97);
            SourceNodeValue.Name = "SourceNodeValue";
            SourceNodeValue.Size = new Size(22, 22);
            SourceNodeValue.TabIndex = 15;
            SourceNodeValue.Text = "?";
            // 
            // DestinationNodeValue
            // 
            DestinationNodeValue.AutoSize = true;
            DestinationNodeValue.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            DestinationNodeValue.ForeColor = Color.Purple;
            DestinationNodeValue.Location = new Point(205, 129);
            DestinationNodeValue.Name = "DestinationNodeValue";
            DestinationNodeValue.Size = new Size(22, 22);
            DestinationNodeValue.TabIndex = 16;
            DestinationNodeValue.Text = "?";
            // 
            // CurrentAlgorithmValue
            // 
            CurrentAlgorithmValue.Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            CurrentAlgorithmValue.Location = new Point(12, 16);
            CurrentAlgorithmValue.Name = "CurrentAlgorithmValue";
            CurrentAlgorithmValue.Size = new Size(354, 32);
            CurrentAlgorithmValue.TabIndex = 17;
            CurrentAlgorithmValue.Text = "Algorithm Logs";
            CurrentAlgorithmValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(24, 288);
            label1.Name = "label1";
            label1.Size = new Size(179, 22);
            label1.TabIndex = 18;
            label1.Text = "Beam Coefficient: ";
            // 
            // BeamCoefficientValue
            // 
            BeamCoefficientValue.AutoSize = true;
            BeamCoefficientValue.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            BeamCoefficientValue.ForeColor = Color.Purple;
            BeamCoefficientValue.Location = new Point(202, 288);
            BeamCoefficientValue.Name = "BeamCoefficientValue";
            BeamCoefficientValue.Size = new Size(22, 22);
            BeamCoefficientValue.TabIndex = 19;
            BeamCoefficientValue.Text = "?";
            // 
            // Logs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Plum;
            ClientSize = new Size(800, 327);
            ControlBox = false;
            Controls.Add(BeamCoefficientValue);
            Controls.Add(label1);
            Controls.Add(CurrentAlgorithmValue);
            Controls.Add(DestinationNodeValue);
            Controls.Add(SourceNodeValue);
            Controls.Add(DestinationNodeLabel);
            Controls.Add(SourceNodeLabel);
            Controls.Add(panel1);
            Controls.Add(CurrentPathCostHelperLabel);
            Controls.Add(CurrentPathCostValue);
            Controls.Add(CurrentPathCostLabel);
            Controls.Add(CurrentNPathsHelperLabel);
            Controls.Add(CurrentNPathsValue);
            Controls.Add(CurrentNPathsLabel);
            Controls.Add(CurrentSizeValue);
            Controls.Add(CurrentSizeHelperLabel);
            Controls.Add(CurrentSizeLabel);
            Controls.Add(CurrentNodeValue);
            Controls.Add(CurrentNodeLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Logs";
            Text = "Algorithm Logs";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label CurrentNodeLabel;
        private Label CurrentNodeValue;
        private Label CurrentSizeLabel;
        private Label CurrentSizeHelperLabel;
        private Label CurrentSizeValue;
        private Label CurrentNPathsLabel;
        private Label CurrentNPathsValue;
        private Label CurrentNPathsHelperLabel;
        private Label CurrentPathCostLabel;
        private Label CurrentPathCostValue;
        private Label CurrentPathCostHelperLabel;
        private Label CurrentPathElementsLabel;
        private Panel panel1;
        private Label CurrentPathElementsHelperLabel;
        private ListBox CurrentPathElementsList;
        private Label CurrentPathNodesLabel2;
        private Label CurrentContainerContentsLabel2;
        private Label CurrentContainerContentsLabel;
        private Label CurrentPathNodesHelperLabel2;
        private Label CurrentContainerContentsHelperLabel2;
        private Label CurrentContainerContentsHelperLabel;
        private ListBox CurrentContainerContentsList;
        private Label SourceNodeLabel;
        private Label DestinationNodeLabel;
        private Label SourceNodeValue;
        private Label DestinationNodeValue;
        private Label CurrentAlgorithmValue;
        private Label label1;
        private Label BeamCoefficientValue;
    }
}