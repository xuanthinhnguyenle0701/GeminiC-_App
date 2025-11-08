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
            rtbOutput = new RichTextBox();
            lblPerformance = new Label();
            cbHangPLC = new ComboBox();
            cbLoaiPLC = new ComboBox();
            cbNgonNgu = new ComboBox();
            cbLoaiKhoi = new ComboBox();
            lblHangPLC = new Label();
            lblLoaiPLC = new Label();
            lblNgonNgu = new Label();
            lblLoaiKhoi = new Label();
            SuspendLayout();
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.MintCream;
            btnGenerate.Cursor = Cursors.Hand;
            btnGenerate.FlatStyle = FlatStyle.Popup;
            btnGenerate.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerate.Location = new Point(832, 597);
            btnGenerate.Name = "btnGenerate";
            btnGenerate.Size = new Size(134, 43);
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
            txtPrompt.Location = new Point(27, 536);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(761, 172);
            txtPrompt.TabIndex = 4;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
            // 
            // rtbOutput
            // 
            rtbOutput.BackColor = Color.Azure;
            rtbOutput.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbOutput.ForeColor = SystemColors.InfoText;
            rtbOutput.Location = new Point(27, 31);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(948, 353);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            // 
            // lblPerformance
            // 
            lblPerformance.AutoSize = true;
            lblPerformance.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPerformance.ForeColor = SystemColors.Desktop;
            lblPerformance.Location = new Point(27, 724);
            lblPerformance.Name = "lblPerformance";
            lblPerformance.Size = new Size(59, 20);
            lblPerformance.TabIndex = 5;
            lblPerformance.Text = "label1";
            // 
            // cbHangPLC
            // 
            cbHangPLC.FormattingEnabled = true;
            cbHangPLC.Location = new Point(173, 390);
            cbHangPLC.Name = "cbHangPLC";
            cbHangPLC.Size = new Size(312, 28);
            cbHangPLC.TabIndex = 6;
            // 
            // cbLoaiPLC
            // 
            cbLoaiPLC.FormattingEnabled = true;
            cbLoaiPLC.Location = new Point(173, 424);
            cbLoaiPLC.Name = "cbLoaiPLC";
            cbLoaiPLC.Size = new Size(312, 28);
            cbLoaiPLC.TabIndex = 6;
            // 
            // cbNgonNgu
            // 
            cbNgonNgu.FormattingEnabled = true;
            cbNgonNgu.Location = new Point(173, 458);
            cbNgonNgu.Name = "cbNgonNgu";
            cbNgonNgu.Size = new Size(312, 28);
            cbNgonNgu.TabIndex = 6;
            // 
            // cbLoaiKhoi
            // 
            cbLoaiKhoi.FormattingEnabled = true;
            cbLoaiKhoi.Location = new Point(173, 492);
            cbLoaiKhoi.Name = "cbLoaiKhoi";
            cbLoaiKhoi.Size = new Size(312, 28);
            cbLoaiKhoi.TabIndex = 6;
            // 
            // lblHangPLC
            // 
            lblHangPLC.AutoSize = true;
            lblHangPLC.Location = new Point(29, 398);
            lblHangPLC.Name = "lblHangPLC";
            lblHangPLC.Size = new Size(72, 20);
            lblHangPLC.TabIndex = 7;
            lblHangPLC.Text = "Hãng PLC";
            // 
            // lblLoaiPLC
            // 
            lblLoaiPLC.AutoSize = true;
            lblLoaiPLC.Location = new Point(29, 432);
            lblLoaiPLC.Name = "lblLoaiPLC";
            lblLoaiPLC.Size = new Size(64, 20);
            lblLoaiPLC.TabIndex = 7;
            lblLoaiPLC.Text = "Loại PLC";
            // 
            // lblNgonNgu
            // 
            lblNgonNgu.AutoSize = true;
            lblNgonNgu.Location = new Point(29, 461);
            lblNgonNgu.Name = "lblNgonNgu";
            lblNgonNgu.Size = new Size(138, 20);
            lblNgonNgu.TabIndex = 7;
            lblNgonNgu.Text = "Ngôn ngữ Lập trình";
            // 
            // lblLoaiKhoi
            // 
            lblLoaiKhoi.AutoSize = true;
            lblLoaiKhoi.Location = new Point(32, 495);
            lblLoaiKhoi.Name = "lblLoaiKhoi";
            lblLoaiKhoi.Size = new Size(69, 20);
            lblLoaiKhoi.TabIndex = 7;
            lblLoaiKhoi.Text = "Loại khối";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1005, 762);
            Controls.Add(lblLoaiKhoi);
            Controls.Add(lblNgonNgu);
            Controls.Add(lblLoaiPLC);
            Controls.Add(lblHangPLC);
            Controls.Add(cbLoaiKhoi);
            Controls.Add(cbNgonNgu);
            Controls.Add(cbLoaiPLC);
            Controls.Add(cbHangPLC);
            Controls.Add(lblPerformance);
            Controls.Add(txtPrompt);
            Controls.Add(btnGenerate);
            Controls.Add(rtbOutput);
            Icon = (Icon)resources.GetObject("$this.Icon");
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
        private ComboBox cbHangPLC;
        private ComboBox cbLoaiPLC;
        private ComboBox cbNgonNgu;
        private ComboBox cbLoaiKhoi;
        private Label lblHangPLC;
        private Label lblLoaiPLC;
        private Label lblNgonNgu;
        private Label lblLoaiKhoi;
    }
}
