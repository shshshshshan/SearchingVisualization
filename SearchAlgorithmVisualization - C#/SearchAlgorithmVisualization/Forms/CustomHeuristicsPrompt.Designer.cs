namespace SearchAlgorithmVisualization.Forms
{
    partial class CustomHeuristicsPrompt
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
            PromptLabel = new Label();
            CustomValueTextbox = new TextBox();
            EnterPromptButton = new Button();
            CancelPromptButton = new Button();
            SuspendLayout();
            // 
            // PromptLabel
            // 
            PromptLabel.AutoSize = true;
            PromptLabel.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            PromptLabel.Location = new Point(12, 33);
            PromptLabel.Name = "PromptLabel";
            PromptLabel.Size = new Size(287, 22);
            PromptLabel.TabIndex = 0;
            PromptLabel.Text = "Enter Custom Heuristic Value:";
            // 
            // CustomValueTextbox
            // 
            CustomValueTextbox.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            CustomValueTextbox.Location = new Point(305, 29);
            CustomValueTextbox.MaxLength = 7;
            CustomValueTextbox.Name = "CustomValueTextbox";
            CustomValueTextbox.PlaceholderText = "0.0";
            CustomValueTextbox.Size = new Size(218, 26);
            CustomValueTextbox.TabIndex = 1;
            CustomValueTextbox.Text = "0";
            CustomValueTextbox.TextChanged += CustomValueTextbox_TextChanged;
            // 
            // EnterPromptButton
            // 
            EnterPromptButton.Cursor = Cursors.Hand;
            EnterPromptButton.Location = new Point(418, 61);
            EnterPromptButton.Name = "EnterPromptButton";
            EnterPromptButton.Size = new Size(106, 29);
            EnterPromptButton.TabIndex = 3;
            EnterPromptButton.Text = "Enter";
            EnterPromptButton.UseVisualStyleBackColor = true;
            EnterPromptButton.Click += EnterPromptButton_Click;
            // 
            // CancelPromptButton
            // 
            CancelPromptButton.Cursor = Cursors.Hand;
            CancelPromptButton.Location = new Point(305, 61);
            CancelPromptButton.Name = "CancelPromptButton";
            CancelPromptButton.Size = new Size(106, 29);
            CancelPromptButton.TabIndex = 4;
            CancelPromptButton.Text = "Cancel";
            CancelPromptButton.UseVisualStyleBackColor = true;
            CancelPromptButton.Click += CancelButton_Click;
            // 
            // CustomHeuristicsPrompt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(562, 111);
            ControlBox = false;
            Controls.Add(CancelPromptButton);
            Controls.Add(EnterPromptButton);
            Controls.Add(CustomValueTextbox);
            Controls.Add(PromptLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CustomHeuristicsPrompt";
            Text = "Custom Heuristics";
            Deactivate += CustomHeuristicsPrompt_Deactivate;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label PromptLabel;
        private TextBox CustomValueTextbox;
        private Button EnterPromptButton;
        private Button CancelPromptButton;
    }
}