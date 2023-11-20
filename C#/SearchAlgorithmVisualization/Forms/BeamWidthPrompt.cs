using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SearchAlgorithmVisualization.Forms
{
    public partial class BeamWidthPrompt : Form
    {
        public event EventHandler? CancelButtonPressed;

        public event EventHandler? EnterButtonPressed;

        public BeamWidthPrompt()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys key)
        {
            if (key == Keys.Escape)
            {
                this.CancelButtonPressed?.Invoke(this, EventArgs.Empty);
                this.Hide();
            }

            else if (key == Keys.Enter)
            {
                this.EnterButtonPressed?.Invoke(this, EventArgs.Empty);
                this.Hide();
            }

            return base.ProcessCmdKey(ref msg, key);
        }

        public int? GetInput()
        {
            if (int.TryParse(this.CustomValueTextbox.Text, out int value))
                return value;

            return null;
        }

        // Validate user input
        private void CustomValueTextbox_TextChanged(object sender, EventArgs e)
        {
            if (this.CustomValueTextbox.Text.Length <= 0) return;

            if (int.TryParse(this.CustomValueTextbox.Text, out int value))
                return;

            this.CustomValueTextbox.Text = this.CustomValueTextbox.Text.Substring(0, this.CustomValueTextbox.TextLength - 1);
        }

        private void CancelPromptButton_Click(object sender, EventArgs e)
        {
            this.CancelButtonPressed?.Invoke(this, EventArgs.Empty);
            this.Hide();
        }

        private void EnterPromptButton_Click(object sender, EventArgs e)
        {
            this.EnterButtonPressed?.Invoke(this, EventArgs.Empty);
            this.Hide();
        }

        private void BeamWidthPrompt_Deactivate(object sender, EventArgs e)
        {
            this.CancelButtonPressed?.Invoke(this, EventArgs.Empty);
            this.Hide();
        }
    }
}
