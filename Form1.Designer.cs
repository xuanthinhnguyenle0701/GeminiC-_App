namespace GeminiC__App
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnGenerate = new Button();
            txtPrompt = new TextBox();
            lblPerformance = new Label();
            rtbOutput = new RichTextBox();
            SuspendLayout();
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.MintCream;
            btnGenerate.Cursor = Cursors.Hand;
            btnGenerate.FlatStyle = FlatStyle.Popup;
            btnGenerate.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerate.Location = new Point(726, 312);
            btnGenerate.Margin = new Padding(3, 2, 3, 2);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(117, 32);
            btnGenerate.TabIndex = 2;
            btnGenerate.Text = "Generate";
            btnGenerate.UseVisualStyleBackColor = false;
            btnGenerate.Click += btnGenerate_Click;
            // 
            // txtPrompt
            // 
            txtPrompt.Anchor = AnchorStyles.Left;
            txtPrompt.BackColor = Color.MintCream;
            txtPrompt.Cursor = Cursors.IBeam;
            txtPrompt.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPrompt.ForeColor = SystemColors.ControlText;
            txtPrompt.Location = new Point(24, 297);
            txtPrompt.Margin = new Padding(3, 2, 3, 2);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(667, 47);
            txtPrompt.TabIndex = 4;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
            // 
            // lblPerformance
            // 
            lblPerformance.AutoSize = true;
            lblPerformance.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPerformance.ForeColor = SystemColors.Desktop;
            lblPerformance.Location = new Point(24, 358);
            lblPerformance.Name = "lblPerformance";
            lblPerformance.Size = new Size(52, 17);
            lblPerformance.TabIndex = 5;
            lblPerformance.Text = "label1";
            // 
            // rtbOutput
            // 
            rtbOutput.BackColor = Color.Azure;
            rtbOutput.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbOutput.ForeColor = SystemColors.InfoText;
            rtbOutput.Location = new Point(24, 16);
            rtbOutput.Margin = new Padding(3, 2, 3, 2);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(667, 266);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(992, 412);
            Controls.Add(lblPerformance);
            Controls.Add(txtPrompt);
            Controls.Add(btnGenerate);
            Controls.Add(rtbOutput);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "C# Generator";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbOutput;
        private Button btnGenerate;
        private TextBox txtPrompt;
        private Label lblPerformance;
    }
}
