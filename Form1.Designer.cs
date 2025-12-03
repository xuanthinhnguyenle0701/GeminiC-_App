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
            dataGridView1 = new DataGridView();
            ofdUserDialog = new OpenFileDialog();
            txtFilePath = new TextBox();
            btnOpenFile = new Button();
            panelPLC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnGenerate
            // 
            btnGenerate.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
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
            txtPrompt.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtPrompt.BackColor = Color.MintCream;
            txtPrompt.Cursor = Cursors.IBeam;
            txtPrompt.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            txtPrompt.ForeColor = SystemColors.ControlText;
            txtPrompt.Location = new Point(27, 523);
            txtPrompt.Multiline = true;
            txtPrompt.Name = "txtPrompt";
            txtPrompt.Size = new Size(761, 155);
            txtPrompt.TabIndex = 4;
            txtPrompt.TextChanged += txtPrompt_TextChanged;
            // 
            // rtbOutput
            // 
            rtbOutput.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            rtbOutput.BackColor = Color.Azure;
            rtbOutput.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbOutput.ForeColor = SystemColors.InfoText;
            rtbOutput.Location = new Point(27, 31);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(948, 300);
            rtbOutput.TabIndex = 0;
            rtbOutput.Text = "";
            // 
            // lblPerformance
            // 
            lblPerformance.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblPerformance.AutoSize = true;
            lblPerformance.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lblPerformance.ForeColor = SystemColors.Desktop;
            lblPerformance.Location = new Point(27, 706);
            lblPerformance.Name = "lblPerformance";
            lblPerformance.Size = new Size(59, 20);
            lblPerformance.TabIndex = 5;
            lblPerformance.Text = "label1";
            lblPerformance.Click += lblPerformance_Click;
            // 
            // cbHangPLC
            // 
            cbHangPLC.FormattingEnabled = true;
            cbHangPLC.Location = new Point(146, 3);
            cbHangPLC.Name = "cbHangPLC";
            cbHangPLC.Size = new Size(313, 28);
            cbHangPLC.TabIndex = 6;
            // 
            // cbLoaiPLC
            // 
            cbLoaiPLC.FormattingEnabled = true;
            cbLoaiPLC.Location = new Point(146, 37);
            cbLoaiPLC.Name = "cbLoaiPLC";
            cbLoaiPLC.Size = new Size(313, 28);
            cbLoaiPLC.TabIndex = 6;
            // 
            // cbNgonNgu
            // 
            cbNgonNgu.FormattingEnabled = true;
            cbNgonNgu.Location = new Point(146, 72);
            cbNgonNgu.Name = "cbNgonNgu";
            cbNgonNgu.Size = new Size(313, 28);
            cbNgonNgu.TabIndex = 6;
            // 
            // cbLoaiKhoi
            // 
            cbLoaiKhoi.FormattingEnabled = true;
            cbLoaiKhoi.Location = new Point(146, 105);
            cbLoaiKhoi.Name = "cbLoaiKhoi";
            cbLoaiKhoi.Size = new Size(313, 28);
            cbLoaiKhoi.TabIndex = 6;
            // 
            // lblHangPLC
            // 
            lblHangPLC.AutoSize = true;
            lblHangPLC.Location = new Point(2, 11);
            lblHangPLC.Name = "lblHangPLC";
            lblHangPLC.Size = new Size(75, 20);
            lblHangPLC.TabIndex = 7;
            lblHangPLC.Text = "PLC Brand";
            // 
            // lblLoaiPLC
            // 
            lblLoaiPLC.AutoSize = true;
            lblLoaiPLC.Location = new Point(2, 45);
            lblLoaiPLC.Name = "lblLoaiPLC";
            lblLoaiPLC.Size = new Size(67, 20);
            lblLoaiPLC.TabIndex = 7;
            lblLoaiPLC.Text = "PLC Type";
            // 
            // lblNgonNgu
            // 
            lblNgonNgu.AutoSize = true;
            lblNgonNgu.Location = new Point(2, 75);
            lblNgonNgu.Name = "lblNgonNgu";
            lblNgonNgu.Size = new Size(135, 20);
            lblNgonNgu.TabIndex = 7;
            lblNgonNgu.Text = "Program Language";
            // 
            // lblLoaiKhoi
            // 
            lblLoaiKhoi.AutoSize = true;
            lblLoaiKhoi.Location = new Point(6, 108);
            lblLoaiKhoi.Name = "lblLoaiKhoi";
            lblLoaiKhoi.Size = new Size(78, 20);
            lblLoaiKhoi.TabIndex = 7;
            lblLoaiKhoi.Text = "Block type";
            // 
            // panelPLC
            // 
            panelPLC.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panelPLC.Controls.Add(cbHangPLC);
            panelPLC.Controls.Add(lblLoaiKhoi);
            panelPLC.Controls.Add(cbLoaiPLC);
            panelPLC.Controls.Add(lblNgonNgu);
            panelPLC.Controls.Add(cbNgonNgu);
            panelPLC.Controls.Add(lblLoaiPLC);
            panelPLC.Controls.Add(cbLoaiKhoi);
            panelPLC.Controls.Add(lblHangPLC);
            panelPLC.Location = new Point(27, 357);
            panelPLC.Margin = new Padding(3, 4, 3, 4);
            panelPLC.Name = "panelPLC";
            panelPLC.Size = new Size(513, 137);
            panelPLC.TabIndex = 8;
            // 
            // cbChuyenMuc
            // 
            cbChuyenMuc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbChuyenMuc.FormattingEnabled = true;
            cbChuyenMuc.Location = new Point(546, 357);
            cbChuyenMuc.Margin = new Padding(3, 4, 3, 4);
            cbChuyenMuc.Name = "cbChuyenMuc";
            cbChuyenMuc.Size = new Size(313, 28);
            cbChuyenMuc.TabIndex = 9;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1025, 34);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(716, 638);
            dataGridView1.TabIndex = 10;
            // 
            // ofdUserDialog
            // 
            ofdUserDialog.FileName = "openFileDialog1";
            // 
            // txtFilePath
            // 
            txtFilePath.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            txtFilePath.BorderStyle = BorderStyle.FixedSingle;
            txtFilePath.Location = new Point(546, 467);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.Size = new Size(279, 27);
            txtFilePath.TabIndex = 11;
            // 
            // btnOpenFile
            // 
            btnOpenFile.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnOpenFile.Location = new Point(831, 467);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(28, 29);
            btnOpenFile.TabIndex = 12;
            btnOpenFile.Text = "...";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSteelBlue;
            ClientSize = new Size(1775, 760);
            Controls.Add(btnOpenFile);
            Controls.Add(txtFilePath);
            Controls.Add(dataGridView1);
            Controls.Add(cbChuyenMuc);
            Controls.Add(panelPLC);
            Controls.Add(lblPerformance);
            Controls.Add(txtPrompt);
            Controls.Add(btnGenerate);
            Controls.Add(rtbOutput);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "C# Generator";
            Load += Form1_Load;
            panelPLC.ResumeLayout(false);
            panelPLC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
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
        private DataGridView dataGridView1;
        private OpenFileDialog ofdUserDialog;
        private TextBox txtFilePath;
        private Button btnOpenFile;
    }
}
