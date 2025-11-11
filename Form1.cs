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
    // LƯU Ý: Code này YÊU CẦU bạn đã kéo thả các control 
    // (cbChuyenMuc, panelPLC, cbHangPLC, v.v...) VÀO TRÌNH DESIGNER
    public partial class Form1 : Form
    {
        // --- API & PATH ---
        // THAY KEY CỦA BẠN VÀO ĐÂY (VÀ ĐẢM BẢO KEY ĐÃ BẬT BILLING NẾU DÙNG PRO)
        private const string ApiKey = "AIzaSyBNQonrLz5eqNjwJsDqz8WCsQRrHxtjxZ0";
        private const string GeminiApiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=" + ApiKey;

        private string scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GeneratedCode.scl");

        // --- (Dictionaries giữ nguyên) ---
        private Dictionary<string, string> promptTemplates = new Dictionary<string, string>();
        private Dictionary<string, List<string>> plcModelData = new Dictionary<string, List<string>>();

        public Form1()
        {
            InitializeComponent(); // BẮT BUỘC có dòng này đầu tiên

            // --- Thiết lập giao diện và nạp dữ liệu ---
            this.Text = "AI PLC & SCADA Code Generator";
            InitializePlcData();
            LoadPromptTemplates();

            // 1. Nạp dữ liệu Chuyên mục
            cbChuyenMuc.Items.Clear();
            cbChuyenMuc.Items.AddRange(new string[] { "Lập trình PLC", "Lập trình SCADA" });
            cbChuyenMuc.SelectedIndex = 0;

            // 2. Nạp dữ liệu cho Hãng PLC (trong panelPLC)
            cbHangPLC.Items.Clear();
            cbHangPLC.Items.AddRange(plcModelData.Keys.ToArray());
            cbHangPLC.SelectedIndex = 0;

            // 3. Nạp dữ liệu cho Ngôn ngữ (trong panelPLC)
            cbNgonNgu.Items.Clear();
            cbNgonNgu.Items.AddRange(new string[] { "SCL (Structured Text)", "STL (Statement List)", "Ladder (LAD)", "FBD (Function Block Diagram)" });
            cbNgonNgu.SelectedIndex = 0;

            // 4. Gán sự kiện (Nếu bạn chưa gán trong Designer)
            // (Đảm bảo các dòng này tồn tại)
            this.cbChuyenMuc.SelectedIndexChanged += new System.EventHandler(this.CbChuyenMuc_SelectedIndexChanged);
            this.cbHangPLC.SelectedIndexChanged += new System.EventHandler(this.CbHangPLC_SelectedIndexChanged);
            this.cbNgonNgu.SelectedIndexChanged += new System.EventHandler(this.CbNgonNgu_SelectedIndexChanged);

            // 5. Kích hoạt sự kiện lần đầu để UI đúng trạng thái
            CbChuyenMuc_SelectedIndexChanged(null, null);
            CbHangPLC_SelectedIndexChanged(null, null);
            CbNgonNgu_SelectedIndexChanged(null, null);

            // 6. Chỉnh lại lời chào
            lblPerformance.Text = "";
            rtbOutput.Text = $" Chào mừng đến với đồ án AI-PLC!\n (Đã tải thành công {promptTemplates.Count} templates từ file JSON)\n" +
                             "------------------------------------------------------------------------------------------------\n" +
                             "1. Chọn Chuyên mục (PLC/SCADA).\n" +
                             "2. Cấu hình các lựa chọn bên dưới.\n" +
                             "3. Nhập yêu cầu logic và nhấn 'Generate'.\n";

            // 7. XÓA CODE RESIZE: 
            // Xóa các biến original... và hàm Form1_Resize, ResizeControl.
            // Hãy dùng thuộc tính Anchor và Dock trong Designer, nó tốt hơn.
        }

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

        // *** THÊM MỚI: Sự kiện Ẩn/Hiện Panel ***
        private void CbChuyenMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbChuyenMuc.SelectedItem == null) return;
            string selectedChuyenMuc = cbChuyenMuc.SelectedItem.ToString();

            if (selectedChuyenMuc == "Lập trình PLC")
            {
                panelPLC.Visible = true;
                // (Sau này thêm panelSCADA.Visible = false;)
            }
            else // "Lập trình SCADA"
            {
                panelPLC.Visible = false;
                // (Sau này thêm panelSCADA.Visible = true;)
            }
        }


        // --- (Sự kiện CbHangPLC_SelectedIndexChanged giữ nguyên) ---
        private void CbHangPLC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbHangPLC.SelectedItem == null) return;
            string selectedHang = cbHangPLC.SelectedItem.ToString();

            if (plcModelData.TryGetValue(selectedHang, out List<string> models))
            {
                // Giữ lại lựa chọn hiện tại nếu có thể
                string currentModel = cbLoaiPLC.SelectedItem as string;

                cbLoaiPLC.Items.Clear();
                cbLoaiPLC.Items.AddRange(models.ToArray());

                if (models.Contains(currentModel))
                    cbLoaiPLC.SelectedItem = currentModel;
                else
                    cbLoaiPLC.SelectedIndex = 0;
            }
        }

        // --- (Sự kiện CbNgonNgu_SelectedIndexChanged giữ nguyên) ---
        private void CbNgonNgu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNgonNgu.SelectedItem == null) return;
            string selectedLang = cbNgonNgu.SelectedItem.ToString();

            if (selectedLang.StartsWith("Ladder") || selectedLang.StartsWith("FBD"))
            {
                cbLoaiKhoi.Enabled = false;
                cbLoaiKhoi.Items.Clear();
                cbLoaiKhoi.Items.Add("Không (None)");
                cbLoaiKhoi.SelectedIndex = 0;
            }
            else // SCL, STL
            {
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

        // *** HÀM QUAN TRỌNG: SỬA LẠI LOGIC TẠO KEY ***
        private string BuildPlcPrompt(string chuyenMuc, string hangPLC, string loaiPLC, string loaiKhoi, string ngonNgu, string yeuCauLogic)
        {
            string key = "";

            if (chuyenMuc == "Lập trình PLC")
            {
                string blockKey = "";
                string langKey = "";

                // 1. Xác định Loại Khối (FB, FC, LAD, FBD)
                if (loaiKhoi.StartsWith("FUNCTION (FC)")) blockKey = "FC";
                else if (loaiKhoi.StartsWith("FUNCTION_BLOCK (FB)")) blockKey = "FB";
                else // "Không (None)"
                {
                    if (ngonNgu.StartsWith("Ladder")) blockKey = "LAD";
                    else if (ngonNgu.StartsWith("FBD")) blockKey = "FBD";
                    else blockKey = "FB"; // Fallback (SCL/STL + None -> default to FB)
                }

                // 2. Xác định Ngôn ngữ (chỉ áp dụng cho FB/FC)
                if (blockKey == "FB" || blockKey == "FC")
                {
                    if (ngonNgu.StartsWith("SCL")) langKey = "_SCL";
                    else if (ngonNgu.StartsWith("STL")) langKey = "_STL";
                    else langKey = "_SCL"; // Mặc định là SCL nếu chọn LAD/FBD + FB/FC
                }

                key = $"{hangPLC}_{blockKey}{langKey}"; // e.g., "Siemens_FC_SCL", "Siemens_LAD"
            }
            else // "Lập trình SCADA"
            {
                // (Sau này sẽ đọc từ cbScadaPlatform...)
                key = "WinCC_Unified_JavaScript"; // Hardcoded
            }

            // 3. Tra cứu template
            string template;
            if (!promptTemplates.TryGetValue(key, out template))
            {
                // Logic Fallback (Tìm template dự phòng)
                string fallbackKey = "Siemens_FB_SCL"; // Dự phòng an toàn nhất
                if (chuyenMuc == "Lập trình PLC")
                {
                    if (key.Contains("LAD")) fallbackKey = "Siemens_LAD";
                    else if (key.Contains("FBD")) fallbackKey = "Siemens_FBD";
                    else if (key.Contains("_FC_")) fallbackKey = "Siemens_FC_SCL";
                    else if (key.Contains("_FB_")) fallbackKey = "Siemens_FB_SCL";
                }

                MessageBox.Show($"Không tìm thấy template cho key: '{key}'.\nSử dụng template mặc định '{fallbackKey}'.");
                if (!promptTemplates.TryGetValue(fallbackKey, out template))
                {
                    MessageBox.Show($"Lỗi nghiêm trọng: Không tìm thấy template mặc định '{fallbackKey}'.");
                    return "LỖI: KHÔNG TÌM THẤY TEMPLATE";
                }
            }

            // 4. Thay thế placeholder
            template = template.Replace("%HANG_PLC%", hangPLC);
            template = template.Replace("%LOAI_PLC%", loaiPLC);
            template = template.Replace("%NGON_NGU%", ngonNgu);
            template = template.Replace("%LOAI_KHOI%", loaiKhoi);
            template = template.Replace("%LOGIC_HERE%", yeuCauLogic);

            return template;
        }


        // *** Nút Generate (SỬA LỖI NULL) ***
        private async void btnGenerate_Click(object sender, EventArgs e)
        {
            lblPerformance.ForeColor = Color.Black;
            if (string.IsNullOrWhiteSpace(txtPrompt.Text))
            {
                MessageBox.Show("Vui lòng nhập yêu cầu logic điều khiển.");
                return;
            }

            // --- SỬA LỖI NULL BẮT ĐẦU TỪ ĐÂY ---
            if (cbChuyenMuc.SelectedItem == null)
            {
                MessageBox.Show("Lỗi: Vui lòng chọn một 'Chuyên mục'.");
                return;
            }

            string chuyenMuc = cbChuyenMuc.SelectedItem.ToString();
            string hangPLC = "";
            string loaiPLC = "";
            string ngonNgu = "";
            string loaiKhoi = "";
            string yeuCauLogic = txtPrompt.Text;

            if (chuyenMuc == "Lập trình PLC")
            {
                // Kiểm tra null cho các control trong panel PLC
                if (cbHangPLC.SelectedItem == null || cbLoaiPLC.SelectedItem == null || cbNgonNgu.SelectedItem == null || cbLoaiKhoi.SelectedItem == null)
                {
                    MessageBox.Show("Lỗi: Vui lòng chọn đầy đủ Hãng, Loại PLC, Ngôn ngữ và Loại khối.");
                    return;
                }

                hangPLC = cbHangPLC.SelectedItem.ToString();
                loaiPLC = cbLoaiPLC.SelectedItem.ToString();
                ngonNgu = cbNgonNgu.SelectedItem.ToString();
                loaiKhoi = cbLoaiKhoi.SelectedItem.ToString();
            }
            // (Sau này thêm else if (chuyenMuc == "Lập trình SCADA") ở đây)
            // --- KẾT THÚC SỬA LỖI NULL ---

            btnGenerate.Enabled = false;
            lblPerformance.Text = "Đang tạo code.....";

            string promptReady = BuildPlcPrompt(chuyenMuc, hangPLC, loaiPLC, loaiKhoi, ngonNgu, yeuCauLogic);

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


        // --- (Các hàm còn lại: GenerateScriptFromGemini, ExtractCodeFromMarkdown) ---
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
                        // Thêm kiểm tra lỗi (ví dụ: API Key hết hạn, Billing)
                        if (jsonResponse.error != null)
                        {
                            MessageBox.Show($"API Error: {jsonResponse.error.message}", "Lỗi API", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (jsonResponse.promptFeedback != null)
                        {
                            MessageBox.Show($"Prompt bị chặn: {jsonResponse.promptFeedback.blockReason}", "Lỗi Prompt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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

        // *** CẬP NHẬT: Hàm SaveScriptToFile (đổi tên file) ***
        private void SaveScriptToFile(string scriptContent)
        {
            try
            {
                string fileExtension = ".txt";
                string selectedLang = (cbNgonNgu.SelectedItem != null) ? cbNgonNgu.SelectedItem.ToString() : "";
                string selectedChuyenMuc = (cbChuyenMuc.SelectedItem != null) ? cbChuyenMuc.SelectedItem.ToString() : "";

                if (selectedChuyenMuc == "Lập trình SCADA")
                    fileExtension = ".js";
                else if (selectedLang.StartsWith("SCL"))
                    fileExtension = ".scl";
                else if (selectedLang.StartsWith("STL"))
                    fileExtension = ".stl";
                else if (selectedLang.StartsWith("Ladder"))
                    fileExtension = ".lad.txt"; // (Mô tả text)
                else if (selectedLang.StartsWith("FBD"))
                    fileExtension = ".fbd.txt"; // (Mô tả text)

                scriptPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"GeneratedCode{fileExtension}");

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
        private void lblPerformance_Click(object sender, EventArgs e)
        {
        }
        #endregion

        private void lblPerformance_Click(object sender, EventArgs e)
        {

        }
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