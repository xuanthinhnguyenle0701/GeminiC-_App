using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic; // <-- THÊM THƯ VIỆN NÀY

namespace GeminiC__App
{
    public partial class Form1 : Form
    {
        // --- API & PATH (Giữ nguyên) ---
        private const string ApiKey = "AIzaSyBNQonrLz5eqNjwJsDqz8WCsQRrHxtjxZ0"; // THAY KEY CỦA BẠN VÀO ĐÂY
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=" + ApiKey;
        private string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneratedPLC_Code.scl");

        // --- CÁC CONTROL CŨ (Giữ nguyên) ---
        private Size originalFormSize;
        private Rectangle originalbtnGenerate;
        private Rectangle originalrtbOutput;
        private Rectangle originaltxtPrompt;
        private Rectangle originalLblPerformance;

        private float originalGenerateFont;
        private float originalLabelFont;

        // --- Khai báo các control (Giữ nguyên) ---
        private ComboBox cbHangPLC;
        private ComboBox cbLoaiPLC;
        private ComboBox cbNgonNgu;
        private Label lblHangPLC;
        private Label lblLoaiPLC;
        private Label lblNgonNgu;
        private ComboBox cbLoaiKhoi;
        private Label lblLoaiKhoi;

        // --- Biến lưu vị trí (Giữ nguyên) ---
        private Rectangle originalCbHangPLC;
        private Rectangle originalCbLoaiPLC;
        private Rectangle originalCbNgonNgu;
        private Rectangle originalLblHangPLC;
        private Rectangle originalLblLoaiPLC;
        private Rectangle originalLblNgonNgu;
        private Rectangle originalCbLoaiKhoi;
        private Rectangle originalLblLoaiKhoi;

        // *** THAY ĐỔI: Khai báo Dictionary lưu template ***
        private Dictionary<string, string> promptTemplates = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            this.Text = "PLC AI Code Generator (JSON Mode)";

            // *** THÊM MỚI: Gọi hàm đọc file JSON ***
            LoadPromptTemplates();

            // --- (Phần code thêm ComboBox và sắp xếp layout giữ nguyên) ---
            #region Khởi tạo Giao diện Động (Dynamic UI)
            int controlWidth = 220;
            int controlHeight = 25;
            int labelWidth = 80;
            int startX = 12;
            int startY = 12;
            int padding = 6;
            rtbOutput.Location = new Point(startX, startY);
            rtbOutput.Size = new Size(this.ClientSize.Width - (startX * 2), 200);
            int topOffset = rtbOutput.Bottom + 10;
            lblHangPLC = new Label();
            lblHangPLC.Text = "Hãng PLC:";
            lblHangPLC.Location = new Point(startX, topOffset + 4);
            lblHangPLC.Size = new Size(labelWidth, controlHeight);
            this.Controls.Add(lblHangPLC);
            cbHangPLC = new ComboBox();
            cbHangPLC.Location = new Point(startX + labelWidth, topOffset);
            cbHangPLC.Size = new Size(controlWidth, controlHeight);
            cbHangPLC.Items.AddRange(new string[] { "Siemens", "Schneider", "Allen-Bradley" });
            cbHangPLC.DropDownStyle = ComboBoxStyle.DropDownList;
            cbHangPLC.SelectedIndex = 0;
            this.Controls.Add(cbHangPLC);
            lblLoaiPLC = new Label();
            lblLoaiPLC.Text = "Loại PLC:";
            lblLoaiPLC.Location = new Point(startX, topOffset + controlHeight + padding + 4);
            lblLoaiPLC.Size = new Size(labelWidth, controlHeight);
            this.Controls.Add(lblLoaiPLC);
            cbLoaiPLC = new ComboBox();
            cbLoaiPLC.Location = new Point(startX + labelWidth, topOffset + controlHeight + padding);
            cbLoaiPLC.Size = new Size(controlWidth, controlHeight);
            cbLoaiPLC.Items.AddRange(new string[] { "SIMATIC S7-1500", "SIMATIC S7-1200", "Modicon M340", "ControlLogix" });
            cbLoaiPLC.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLoaiPLC.SelectedIndex = 0;
            this.Controls.Add(cbLoaiPLC);
            lblNgonNgu = new Label();
            lblNgonNgu.Text = "Ngôn ngữ:";
            lblNgonNgu.Location = new Point(startX, topOffset + (controlHeight + padding) * 2 + 4);
            lblNgonNgu.Size = new Size(labelWidth, controlHeight);
            this.Controls.Add(lblNgonNgu);
            cbNgonNgu = new ComboBox();
            cbNgonNgu.Location = new Point(startX + labelWidth, topOffset + (controlHeight + padding) * 2);
            cbNgonNgu.Size = new Size(controlWidth, controlHeight);
            cbNgonNgu.Items.AddRange(new string[] { "SCL (Structured Text)", "ST (Structured Text)", "Ladder (LAD)" });
            cbNgonNgu.DropDownStyle = ComboBoxStyle.DropDownList;
            cbNgonNgu.SelectedIndex = 0;
            this.Controls.Add(cbNgonNgu);
            lblLoaiKhoi = new Label();
            lblLoaiKhoi.Text = "Loại khối:";
            lblLoaiKhoi.Location = new Point(startX, topOffset + (controlHeight + padding) * 3 + 4);
            lblLoaiKhoi.Size = new Size(labelWidth, controlHeight);
            this.Controls.Add(lblLoaiKhoi);
            cbLoaiKhoi = new ComboBox();
            cbLoaiKhoi.Location = new Point(startX + labelWidth, topOffset + (controlHeight + padding) * 3);
            cbLoaiKhoi.Size = new Size(controlWidth, controlHeight);
            cbLoaiKhoi.Items.AddRange(new string[] { "FUNCTION_BLOCK (FB)", "FUNCTION (FC)" });
            cbLoaiKhoi.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLoaiKhoi.SelectedIndex = 0;
            this.Controls.Add(cbLoaiKhoi);
            txtPrompt.Location = new Point(startX, cbLoaiKhoi.Bottom + 10);
            txtPrompt.Size = new Size(this.ClientSize.Width - btnGenerate.Width - (startX * 3), txtPrompt.Height);
            btnGenerate.Location = new Point(this.ClientSize.Width - btnGenerate.Width - startX, txtPrompt.Top);
            lblPerformance.Location = new Point(startX, txtPrompt.Bottom + 5);
            #endregion

            // *** CẬP NHẬT: Câu chào mừng ***
            lblPerformance.Text = "";
            rtbOutput.Text = $" Chào mừng đến với đồ án AI-PLC!\n (Đã tải thành công {promptTemplates.Count} templates từ file JSON)\n" +
                             "------------------------------------------------------------------------------------------------\n" +
                             "1. Chọn Hãng, Loại PLC, Ngôn ngữ và Loại khối (FB/FC).\n" +
                             "2. Nhập yêu cầu logic điều khiển bên dưới.\n" +
                             "3. Nhấn 'Generate' để AI tạo code.\n";

            // --- (Lưu vị trí control - Giữ nguyên) ---
            #region Lưu vị trí ban đầu
            originalFormSize = this.Size;
            originalbtnGenerate = new Rectangle(btnGenerate.Location, btnGenerate.Size);
            originalGenerateFont = btnGenerate.Font.Size;
            originaltxtPrompt = new Rectangle(txtPrompt.Location, txtPrompt.Size);
            originalrtbOutput = new Rectangle(rtbOutput.Location, rtbOutput.Size);
            originalLblPerformance = new Rectangle(lblPerformance.Location, lblPerformance.Size);
            originalLabelFont = lblPerformance.Font.Size;
            originalCbHangPLC = new Rectangle(cbHangPLC.Location, cbHangPLC.Size);
            originalCbLoaiPLC = new Rectangle(cbLoaiPLC.Location, cbLoaiPLC.Size);
            originalCbNgonNgu = new Rectangle(cbNgonNgu.Location, cbNgonNgu.Size);
            originalLblHangPLC = new Rectangle(lblHangPLC.Location, lblHangPLC.Size);
            originalLblLoaiPLC = new Rectangle(lblLoaiPLC.Location, lblLoaiPLC.Size);
            originalLblNgonNgu = new Rectangle(lblNgonNgu.Location, lblNgonNgu.Size);
            originalCbLoaiKhoi = new Rectangle(cbLoaiKhoi.Location, cbLoaiKhoi.Size);
            originalLblLoaiKhoi = new Rectangle(lblLoaiKhoi.Location, lblLoaiKhoi.Size);
            this.Resize += Form1_Resize;
            #endregion
        }

        // --- (Hàm Resize và ResizeControl giữ nguyên) ---
        #region Xử lý Resize
        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeControl(btnGenerate, originalbtnGenerate, originalGenerateFont);
            ResizeControl(txtPrompt, originaltxtPrompt);
            ResizeControl(rtbOutput, originalrtbOutput);
            ResizeControl(lblPerformance, originalLblPerformance, originalLabelFont);
            ResizeControl(cbHangPLC, originalCbHangPLC);
            ResizeControl(cbLoaiPLC, originalCbLoaiPLC);
            ResizeControl(cbNgonNgu, originalCbNgonNgu);
            ResizeControl(lblHangPLC, originalLblHangPLC);
            ResizeControl(lblLoaiPLC, originalLblLoaiPLC);
            ResizeControl(lblNgonNgu, originalLblNgonNgu);
            ResizeControl(cbLoaiKhoi, originalCbLoaiKhoi);
            ResizeControl(lblLoaiKhoi, originalLblLoaiKhoi);
        }
        private void ResizeControl(Control control, Rectangle originalRect, float? originalFontSize = null)
        {
            float xRatio = (float)this.Width / originalFormSize.Width;
            float yRatio = (float)this.Height / originalFormSize.Height;
            int newX = (int)(originalRect.X * xRatio);
            int newY = (int)(originalRect.Y * yRatio);
            int newWidth = (int)(originalRect.Width * xRatio);
            int newHeight = (int)(originalRect.Height * yRatio);
            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, newHeight);
            if (originalFontSize.HasValue)
            {
                float newFontSize = Math.Min(xRatio, yRatio) * originalFontSize.Value;
                control.Font = new Font(control.Font.FontFamily, newFontSize, control.Font.Style);
            }
        }
        #endregion

        // *** THÊM MỚI: Hàm đọc file JSON ***
        private void LoadPromptTemplates()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "PromptTemplates.json");
            try
            {
                string jsonText = File.ReadAllText(filePath);
                var collection = JsonConvert.DeserializeObject<TemplateCollection>(jsonText);

                promptTemplates.Clear();
                foreach (var template in collection.templates)
                {
                    // Ghép các dòng mảng "prompt_lines" thành 1 chuỗi string duy nhất
                    string fullPrompt = string.Join(Environment.NewLine, template.prompt_lines);
                    promptTemplates.Add(template.key, fullPrompt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nghiêm trọng: Không thể đọc file 'PromptTemplates.json'.\nChương trình sẽ không thể tạo prompt.\n\nChi tiết: {ex.Message}", "Lỗi Tải Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // *** THAY THẾ: Hàm BuildPlcPrompt (dùng JSON) ***
        private string BuildPlcPrompt(string hangPLC, string loaiPLC, string loaiKhoi, string ngonNgu, string yeuCauLogic)
        {
            // 1. Xác định key (ví dụ: "Siemens_FC" hoặc "Siemens_FB")
            string khoiKey = loaiKhoi.StartsWith("FUNCTION (FC)") ? "FC" : "FB";

            // Tạm thời gán cứng "Siemens" vì logic JSON của bạn chỉ có Siemens
            // string key = $"{hangPLC}_{khoiKey}"; 
            string key = $"Siemens_{khoiKey}"; // Ví dụ: "Siemens_FC"

            // 2. Tra cứu template trong Dictionary
            string template;
            if (!promptTemplates.TryGetValue(key, out template))
            {
                // Nếu không tìm thấy, dùng template mặc định
                MessageBox.Show($"Không tìm thấy template cho key: '{key}'. Sử dụng template 'Siemens_FB' mặc định.");
                if (!promptTemplates.TryGetValue("Siemens_FB", out template))
                {
                    MessageBox.Show("Lỗi nghiêm trọng: Không tìm thấy template 'Siemens_FB' mặc định.");
                    return "LỖI: KHÔNG TÌM THẤY TEMPLATE";
                }
            }

            // 3. Thay thế các placeholder bằng giá trị thực
            template = template.Replace("%HANG_PLC%", hangPLC);
            template = template.Replace("%LOAI_PLC%", loaiPLC);
            template = template.Replace("%NGON_NGU%", ngonNgu);
            template = template.Replace("%LOAI_KHOI%", loaiKhoi);
            template = template.Replace("%LOGIC_HERE%", yeuCauLogic);

            // --- Cập nhật dựa trên prompt cũ của bạn (thêm quy tắc) ---
            if (hangPLC.Equals("Siemens", StringComparison.OrdinalIgnoreCase))
            {
                template = template.Replace("%SIEMENS_RULES%",
                    "2.  *Cấu hình khối (Siemens):* Luôn sử dụng S7_Optimized_Access := 'TRUE'.\n" +
                    "    * Tuân thủ cách quy định cấu hình của ba mục sau\n" +
                    "    * Mục TITLE phải đi cùng dấu \"=\". (Ví dụ TITLE = 'Simple Motor Control')\n" +
                    "    * Mục AUTHOR phải đi cùng dấu \":\" (ví dụ AUTHOR: 'Professional PLC Engineer')\n" +
                    "    * Mục VERSION phải đi cùng dấu \":\" (ví dụ VERSION: 0.1)");
            }
            else
            {
                template = template.Replace("%SIEMENS_RULES%", ""); // Xóa nếu không phải Siemens
            }

            if (loaiKhoi.StartsWith("FUNCTION_BLOCK"))
            {
                template = template.Replace("%VAR_RULES%", "    * Tất cả biến nội VAR phải có tiền tố \"stat_\" (cho biến tĩnh) hoặc \"temp_\" (cho biến tạm).");
            }
            else
            {
                template = template.Replace("%VAR_RULES%", "    * Tất cả biến nội VAR phải có tiền tố \"temp_\" (cho biến tạm).");
            }

            return template;
        }


        // *** CẬP NHẬT LỚN: Nút Generate (Sửa lỗi thứ tự tham số) ***
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            lblPerformance.ForeColor = Color.Black;
            if (string.IsNullOrWhiteSpace(txtPrompt.Text))
            {
                MessageBox.Show("Vui lòng nhập yêu cầu logic điều khiển.");
                return;
            }

            if (cbHangPLC.SelectedItem == null || cbLoaiPLC.SelectedItem == null || cbNgonNgu.SelectedItem == null || cbLoaiKhoi.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đầy đủ Hãng, Loại PLC, Ngôn ngữ và Loại khối.");
                return;
            }

            string hangPLC = cbHangPLC.SelectedItem.ToString();
            string loaiPLC = cbLoaiPLC.SelectedItem.ToString();
            string ngonNgu = cbNgonNgu.SelectedItem.ToString();
            string loaiKhoi = cbLoaiKhoi.SelectedItem.ToString();
            string yeuCauLogic = txtPrompt.Text;

            btnGenerate.Enabled = false;
            lblPerformance.Text = "Đang tạo code PLC.....";
            // Cập nhật giao diện
            // *** SỬA LỖI THỨ TỰ THAM SỐ ***
            // (Thứ tự đúng: hangPLC, loaiPLC, loaiKhoi, ngonNgu, yeuCauLogic)
            string promptReady = BuildPlcPrompt(hangPLC, loaiPLC, loaiKhoi, ngonNgu, yeuCauLogic);

            var startTime = DateTime.Now;
            var scriptContent = await GenerateScriptFromGemini(promptReady);
            var endTime = DateTime.Now;

            if (!string.IsNullOrEmpty(scriptContent))
            {
                SaveScriptToFile(scriptContent);
                lblPerformance.Text = $"API Response Time: {(endTime - startTime).TotalSeconds:F2} seconds";
                lblPerformance.ForeColor = Color.Green;
            }
            else
            {
                lblPerformance.Text = "Lỗi: Không nhận được nội dung từ AI.";
                lblPerformance.ForeColor = Color.Red;
            }

            btnGenerate.Enabled = true;
        }


        // --- (Các hàm còn lại: GenerateScriptFromGemini, ExtractCodeFromMarkdown, SaveScriptToFile... giữ nguyên) ---
        #region API Calls & File Handling
        private async Task<string> GenerateScriptFromGemini(string prompt)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var requestBody = new
                    {
                        contents = new[]
                        {
                            new
                            {
                                role = "user",
                                parts = new[]
                                {
                                    new
                                    {
                                        text = prompt
                                    }
                                }
                            }
                        },
                        generationConfig = new
                        {
                            temperature = 0.4,
                            maxOutputTokens = 8192
                        }
                    };

                    string jsonBody = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(GeminiApiUrl, content);

                    if (!response.IsSuccessStatusCode)
                    {
                        string errorDetails = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error calling Gemini API: {response.StatusCode} ({response.ReasonPhrase})\n\nDetails:\n{errorDetails}");
                        return null;
                    }

                    var responseBytes = await response.Content.ReadAsByteArrayAsync();
                    var responseBody = Encoding.UTF8.GetString(responseBytes);

                    var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                    if (jsonResponse.candidates == null || jsonResponse.candidates.Count == 0)
                    {
                        // ... (error handling)
                        return null;
                    }

                    string rawScript = jsonResponse.candidates[0].content.parts[0].text.ToString();
                    rawScript = rawScript.Replace("\\n", Environment.NewLine).Replace("\\\"", "\"");

                    return ExtractCodeFromMarkdown(rawScript);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi không mong muốn: {ex.Message}");
                return null;
            }
        }

        private string ExtractCodeFromMarkdown(string rawResponse)
        {
            try
            {
                var match = Regex.Match(rawResponse, @"```(?:[a-z]+)?\s*([\s\S]*?)\s*```");
                if (match.Success)
                {
                    string code = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        return code;
                    }
                }
                return rawResponse.Trim();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi trích xuất code: {ex.Message}");
                return rawResponse;
            }
        }

        private void SaveScriptToFile(string scriptContent)
        {
            try
            {
                File.WriteAllText(scriptPath, scriptContent);
                rtbOutput.Text = "Đã lưu code vào file: " + scriptPath + "\n\n" + scriptContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu file: {ex.Message}");
            }
        }
        #endregion

        // --- (Hàm sự kiện trống - Giữ nguyên) ---
        #region Empty Event Handlers
        private void txtPrompt_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }

    // *** THÊM 2 CLASS HỖ TRỢ JSON VÀO ĐÂY (BÊN NGOÀI CLASS FORM1) ***
    public class PromptTemplate
    {
        public string key { get; set; }
        public System.Collections.Generic.List<string> prompt_lines { get; set; }
    }

    public class TemplateCollection
    {
        public System.Collections.Generic.List<PromptTemplate> templates { get; set; }
    }
}