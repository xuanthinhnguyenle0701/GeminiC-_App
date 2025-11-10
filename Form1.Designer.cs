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
            panelPLC = new Panel();
            cbChuyenMuc = new ComboBox();
            panelPLC.SuspendLayout();
            SuspendLayout();
            // 
            // btnGenerate
            // 
            btnGenerate.BackColor = Color.MintCream;
            btnGenerate.Cursor = Cursors.Hand;
            btnGenerate.FlatStyle = FlatStyle.Popup;
            btnGenerate.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGenerate.Location = new Point(728, 448);
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
            txtPrompt.Location = new Point(24, 402);
            txtPrompt.Margin = new Padding(3, 2, 3, 2);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(666, 130);
            txtPrompt.TabIndex = 4;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
            // 
            // rtbOutput
            // 
            rtbOutput.BackColor = Color.Azure;
            rtbOutput.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbOutput.ForeColor = SystemColors.InfoText;
            rtbOutput.Location = new Point(24, 23);
            rtbOutput.Margin = new Padding(3, 2, 3, 2);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(830, 240);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            // 
            // lblPerformance
            // 
            lblPerformance.AutoSize = true;
            lblPerformance.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPerformance.ForeColor = SystemColors.Desktop;
            lblPerformance.Location = new Point(24, 543);
            lblPerformance.Name = "lblPerformance";
            lblPerformance.Size = new Size(52, 17);
            lblPerformance.TabIndex = 5;
            lblPerformance.Text = "label1";
            // 
            // cbHangPLC
            // 
            cbHangPLC.FormattingEnabled = true;
            cbHangPLC.Location = new Point(128, 2);
            cbHangPLC.Margin = new Padding(3, 2, 3, 2);
            cbHangPLC.Name = "cbHangPLC";
            cbHangPLC.Size = new Size(274, 23);
            cbHangPLC.TabIndex = 6;
            // 
            // cbLoaiPLC
            // 
            cbLoaiPLC.FormattingEnabled = true;
            cbLoaiPLC.Location = new Point(128, 28);
            cbLoaiPLC.Margin = new Padding(3, 2, 3, 2);
            cbLoaiPLC.Name = "cbLoaiPLC";
            cbLoaiPLC.Size = new Size(274, 23);
            cbLoaiPLC.TabIndex = 6;
            // 
            // cbNgonNgu
            // 
            cbNgonNgu.FormattingEnabled = true;
            cbNgonNgu.Location = new Point(128, 54);
            cbNgonNgu.Margin = new Padding(3, 2, 3, 2);
            cbNgonNgu.Name = "cbNgonNgu";
            cbNgonNgu.Size = new Size(274, 23);
            cbNgonNgu.TabIndex = 6;
            // 
            // cbLoaiKhoi
            // 
            cbLoaiKhoi.FormattingEnabled = true;
            cbLoaiKhoi.Location = new Point(128, 79);
            cbLoaiKhoi.Margin = new Padding(3, 2, 3, 2);
            cbLoaiKhoi.Name = "cbLoaiKhoi";
            cbLoaiKhoi.Size = new Size(274, 23);
            cbLoaiKhoi.TabIndex = 6;
            // 
            // lblHangPLC
            // 
            lblHangPLC.AutoSize = true;
            lblHangPLC.Location = new Point(2, 8);
            lblHangPLC.Name = "lblHangPLC";
            lblHangPLC.Size = new Size(60, 15);
            lblHangPLC.TabIndex = 7;
            lblHangPLC.Text = "Hãng PLC";
            // 
            // lblLoaiPLC
            // 
            lblLoaiPLC.AutoSize = true;
            lblLoaiPLC.Location = new Point(2, 34);
            lblLoaiPLC.Name = "lblLoaiPLC";
            lblLoaiPLC.Size = new Size(53, 15);
            lblLoaiPLC.TabIndex = 7;
            lblLoaiPLC.Text = "Loại PLC";
            // 
            // lblNgonNgu
            // 
            lblNgonNgu.AutoSize = true;
            lblNgonNgu.Location = new Point(2, 56);
            lblNgonNgu.Name = "lblNgonNgu";
            lblNgonNgu.Size = new Size(111, 15);
            lblNgonNgu.TabIndex = 7;
            lblNgonNgu.Text = "Ngôn ngữ Lập trình";
            // 
            // lblLoaiKhoi
            // 
            lblLoaiKhoi.AutoSize = true;
            lblLoaiKhoi.Location = new Point(5, 81);
            lblLoaiKhoi.Name = "lblLoaiKhoi";
            lblLoaiKhoi.Size = new Size(55, 15);
            lblLoaiKhoi.TabIndex = 7;
            lblLoaiKhoi.Text = "Loại khối";
            // 
            // panelPLC
            // 
            panelPLC.Controls.Add(cbHangPLC);
            panelPLC.Controls.Add(lblLoaiKhoi);
            panelPLC.Controls.Add(cbLoaiPLC);
            panelPLC.Controls.Add(lblNgonNgu);
            panelPLC.Controls.Add(cbNgonNgu);
            panelPLC.Controls.Add(lblLoaiPLC);
            panelPLC.Controls.Add(cbLoaiKhoi);
            panelPLC.Controls.Add(lblHangPLC);
            panelPLC.Location = new Point(68, 294);
            panelPLC.Name = "panelPLC";
            panelPLC.Size = new Size(449, 103);
            panelPLC.TabIndex = 8;
            // 
            // cbChuyenMuc
            // 
            cbChuyenMuc.FormattingEnabled = true;
            cbChuyenMuc.Location = new Point(196, 268);
            cbChuyenMuc.Name = "cbChuyenMuc";
            cbChuyenMuc.Size = new Size(274, 23);
            cbChuyenMuc.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(879, 562);
            Controls.Add(cbChuyenMuc);
            Controls.Add(panelPLC);
            Controls.Add(lblPerformance);
            Controls.Add(txtPrompt);
            Controls.Add(btnGenerate);
            Controls.Add(rtbOutput);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "C# Generator";
            Load += Form1_Load;
            panelPLC.ResumeLayout(false);
            panelPLC.PerformLayout();
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
        private Panel panelPLC;
        private ComboBox cbChuyenMuc;
    }
}
