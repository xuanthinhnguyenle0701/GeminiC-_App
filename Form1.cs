using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace GeminiC__App
{
    public partial class Form1 : Form
    {
        // --- (API Keys và Path giữ nguyên) ---
        private const string ApiKey = "AIzaSyBNQonrLz5eqNjwJsDqz8WCsQRrHxtjxZ0";
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=" + ApiKey;
        private string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneratedPLC_Code.scl"); // Sẽ đổi tên file nếu là LAD/FBD

        // --- (Khai báo control giữ nguyên) ---
        private Size originalFormSize;
        private Rectangle originalbtnGenerate;
        private Rectangle originalrtbOutput;
        private Rectangle originaltxtPrompt;
        private Rectangle originalLblPerformance;
        private float originalGenerateFont;
        private float originalLabelFont;
        private Rectangle originalCbHangPLC;
        private Rectangle originalCbLoaiPLC;
        private Rectangle originalCbNgonNgu;
        private Rectangle originalLblHangPLC;
        private Rectangle originalLblLoaiPLC;
        private Rectangle originalLblNgonNgu;
        private Rectangle originalCbLoaiKhoi;
        private Rectangle originalLblLoaiKhoi;

        // --- (Dictionaries giữ nguyên) ---
        private Dictionary<string, string> promptTemplates = new Dictionary<string, string>();
        private Dictionary<string, List<string>> plcModelData = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent();

            // === THÊM DÒNG NÀY ĐỂ KẾT NỐI SỰ KIỆN ===
            this.cbNgonNgu.SelectedIndexChanged += new System.EventHandler(this.CbNgonNgu_SelectedIndexChanged);
            // (Bạn cũng nên làm tương tự cho cbHangPLC)
            this.cbHangPLC.SelectedIndexChanged += new System.EventHandler(this.CbHangPLC_SelectedIndexChanged);
            // =======================================

            this.Text = "AI PLC Code-generated Assistant";
            InitializePlcData();
            LoadPromptTemplates();

            // 1. Nạp dữ liệu cho Hãng PLC
            cbHangPLC.Items.Clear();
            cbHangPLC.Items.AddRange(plcModelData.Keys.ToArray());
            cbHangPLC.SelectedIndex = 0;

            // 2. Nạp dữ liệu cho Ngôn ngữ
            cbNgonNgu.Items.Clear();
            cbNgonNgu.Items.AddRange(new string[] { "SCL (Structured Text)", "STL (Statement List)", "Ladder (LAD)", "FBD (Function Block Diagram)" });
            cbNgonNgu.SelectedIndex = 0;

            // (Phần code chào mừng và lưu vị trí control giữ nguyên)
            lblPerformance.Text = "";
            rtbOutput.Text = $" Chào mừng đến với đồ án AI-PLC!\n" +
                             "------------------------------------------------------------------------------------------------\n" +
                             "1. Chọn Hãng, Loại PLC, Ngôn ngữ và Loại khối (FB/FC).\n" +
                             "2. Nhập yêu cầu logic điều khiển bên dưới.\n" +
                             "3. Nhấn 'Generate' để AI tạo code.\n";
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

        // --- (Hàm InitializePlcData giữ nguyên) ---
        private void InitializePlcData()
        {
            plcModelData = new Dictionary<string, List<string>>
            {
                { "Siemens", new List<string> { "SIMATIC S7-1500", "SIMATIC S7-1200", "SIMATIC S7-300", "SIMATIC S7-400" }},
                { "Schneider", new List<string> { "Modicon M340", "Modicon M580", "Modicon M221", "Modicon M251" }},
                { "Allen-Bradley", new List<string> { "ControlLogix", "CompactLogix", "MicroLogix 800", "MicroLogix 1400" }}
            };
        }

        // --- (Sự kiện CbHangPLC_SelectedIndexChanged giữ nguyên) ---
        private void CbHangPLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHangPLC.SelectedItem == null) return;
            string selectedHang = cbHangPLC.SelectedItem.ToString();

            if (plcModelData.TryGetValue(selectedHang, out List<string> models))
            {
                cbLoaiPLC.Items.Clear();
                cbLoaiPLC.Items.AddRange(models.ToArray());
                cbLoaiPLC.SelectedIndex = 0;
            }
        }

        // *** THÊM MỚI: Sự kiện khi người dùng thay đổi Ngôn ngữ ***
        private void CbNgonNgu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNgonNgu.SelectedItem == null) return;
            string selectedLang = cbNgonNgu.SelectedItem.ToString();

            if (selectedLang.Equals("Ladder (LAD)") || selectedLang.Equals("FBD (Function Block Diagram)"))
            {
                // Nếu là Ladder hoặc FBD, TẮT ô chọn Loại khối
                cbLoaiKhoi.Items.Clear();
                cbLoaiKhoi.Items.Add("Không (None)");
                cbLoaiKhoi.SelectedIndex = 0;
                cbLoaiKhoi.Enabled = false;
            }
            else // SCL (hoặc ST)
            {
                // Nếu là SCL, BẬT lại ô chọn
                cbLoaiKhoi.Enabled = true;
                cbLoaiKhoi.Items.Clear();
                cbLoaiKhoi.Items.AddRange(new string[] { "FUNCTION_BLOCK (FB)", "FUNCTION (FC)" });
                cbLoaiKhoi.SelectedIndex = 0;
            }
        }


        // --- (Hàm LoadPromptTemplates giữ nguyên) ---
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
                    string fullPrompt = string.Join(Environment.NewLine, template.prompt_lines);
                    promptTemplates.Add(template.key, fullPrompt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nghiêm trọng: Không thể đọc file 'PromptTemplates.json'.\nChi tiết: {ex.Message}", "Lỗi Tải Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // *** CẬP NHẬT: Hàm BuildPlcPrompt (xử lý 'None') ***
        private string BuildPlcPrompt(string hangPLC, string loaiPLC, string loaiKhoi, string ngonNgu, string yeuCauLogic)
        {
            // 1. Xác định key
            string khoiNgonNgu;
            string khoiKey;

            if (loaiKhoi.StartsWith("FUNCTION (FC)"))
                khoiKey = "FC";
            else if (loaiKhoi.StartsWith("FUNCTION_BLOCK (FB)"))
                khoiKey = "FB";
            else // "Không (None)"
            {
                // Tạo key dựa trên ngôn ngữ, ví dụ: Siemens_LAD
                if (ngonNgu.StartsWith("Ladder"))
                    khoiKey = "LAD"; // Sẽ tìm "Siemens_LAD"
                else if (ngonNgu.StartsWith("FBD"))
                {
                    khoiKey = "FBD"; // Sẽ tìm "Siemens_FBD"
                    khoiNgonNgu = ""; // LAD/FBD không có suffix ngôn ngữ
                }
                else
                {
                    khoiKey = "FB"; // Fallback: Nếu lỡ chọn SCL + None, cứ dùng FB
                    khoiNgonNgu = "_SCL"; // Fallback: Nếu lỡ chọn SCL + None, cứ dùng FB

                }
            } 

            if (ngonNgu.StartsWith("SCL (Structured Text)"))
                khoiNgonNgu = "_SCL";
            else if (ngonNgu.StartsWith("STL (Statement List)"))
                khoiNgonNgu = "_STL";
            else // FBD, LAD
                khoiNgonNgu = "";

            string key = $"{hangPLC}_{khoiKey}{khoiNgonNgu}";

            // 2. Tra cứu template
            string template;
            if (!promptTemplates.TryGetValue(key, out template))
            {
                // Xử lý lỗi (Fallback)
                string fallbackKey = $"Siemens_{khoiKey}{khoiNgonNgu}"; // Thử fallback về Siemens
                if (khoiKey == "LAD" || khoiKey == "FBD") fallbackKey = "Siemens_LAD"; // Fallback chung cho LAD/FBD

                MessageBox.Show($"Không tìm thấy template cho key: '{key}'.\nSử dụng template mặc định '{fallbackKey}'.");

                if (!promptTemplates.TryGetValue(fallbackKey, out template))
                {
                    // Fallback cuối cùng
                    if (!promptTemplates.TryGetValue("Siemens_FB", out template))
                    {
                        MessageBox.Show($"Lỗi nghiêm trọng: Không tìm thấy template mặc định '{fallbackKey}' hoặc 'Siemens_FB'.");
                        return "LỖI: KHÔNG TÌM THẤY TEMPLATE";
                    }
                }
            }

            // 3. Thay thế placeholder
            template = template.Replace("%HANG_PLC%", hangPLC);
            template = template.Replace("%LOAI_PLC%", loaiPLC);
            template = template.Replace("%NGON_NGU%", ngonNgu);
            template = template.Replace("%LOAI_KHOI%", loaiKhoi);
            template = template.Replace("%LOGIC_HERE%", yeuCauLogic);

            return template;
        }


        // --- (Nút Generate giữ nguyên) ---
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

            string promptReady = BuildPlcPrompt(hangPLC, loaiPLC, loaiKhoi, ngonNgu, yeuCauLogic);

            if (promptReady.StartsWith("LỖI:"))
            {
                MessageBox.Show(promptReady, "Lỗi Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnGenerate.Enabled = true;
                return;
            }

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
                // Sửa Regex: Cho phép trả về code không có ``` (cho Ladder)
                var match = Regex.Match(rawResponse, @"```(?:[a-z]+)?\s*([\s\S]*?)\s*```");
                if (match.Success)
                {
                    string code = match.Groups[1].Value.Trim();
                    if (!string.IsNullOrWhiteSpace(code))
                    {
                        return code;
                    }
                }
                // Nếu không có khối ```, trả về nguyên văn (quan trọng cho LAD/FBD)
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

    // --- (Class JSON giữ nguyên) ---
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