using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SendGrid;
using SendGrid.Helpers.Mail;



namespace Exam_Questioner
{
    public partial class studentData : Form
    {
        private StudentDataLogic logic;
        private string currentStudentUsername;
        private string currentUserUsername;
        private string currentUserEmail;
        private string currentUserRole;



        public studentData()
        {
            InitializeComponent();

            

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "database.xlsx");
            logic = new StudentDataLogic(filePath);
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            dataGridView3.Visible = false;
            txtSearchName.Visible = false;
            btnSearch.Visible = false;
            button4.Visible = false; // הסתרת כפתור חיפוש לפי ת"ז
        }

        public void SetCurrentUser(string username, string email, string role)
        {
            currentUserUsername = username;
            currentUserEmail = email;
            currentUserRole = role;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();
                var data = logic.GetAllStudentGrades();
                dataGridView1.DataSource = data;
                dataGridView1.ColumnHeadersVisible = false;
                dataGridView1.RightToLeft = RightToLeft.Yes;
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                txtSearchName.Visible = true; // הפעלת אפשרות חיפוש
                btnSearch.Visible = true;
                button4.Visible = true; // הצגת כפתור חיפוש לפי ת"ז

                groupBoxEmailExport.Visible = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה:\n" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var data = logic.GetStudentAveragePerName();
                dataGridView2.DataSource = data;
                dataGridView2.RightToLeft = RightToLeft.Yes;
                dataGridView2.Visible = true;
                dataGridView1.Visible = false;
                dataGridView3.Visible = false;
                txtSearchName.Visible = false;
                btnSearch.Visible = false;
                button4.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה:\n" + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var data = logic.GetStatistics();

                if (data == null || data.Rows.Count == 0)
                {
                    MessageBox.Show("לא נמצאו נתונים להצגה.");
                    return;
                }

                dataGridView3.RightToLeft = RightToLeft.Yes;
                dataGridView3.Visible = true;
                dataGridView1.Visible = false;
                dataGridView2.Visible = false;
                txtSearchName.Visible = false;
                btnSearch.Visible = false;
                button4.Visible = false;

                dataGridView3.Columns.Clear();
                dataGridView3.AutoGenerateColumns = true; // הבטחת עמודות
                dataGridView3.DataSource = data;
                dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // יפרוס את העמודות
            }
            catch (Exception ex)
            {
                MessageBox.Show("אירעה שגיאה:\n" + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void studentData_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string nameToSearch = txtSearchName.Text.Trim();

            if (string.IsNullOrEmpty(nameToSearch))
            {
                MessageBox.Show("אנא הזן שם סטודנט לחיפוש.");
                return;
            }

            try
            {
                var result = logic.SearchStudentByNameViaUsersSheet(nameToSearch);

                if (result.Rows.Count > 2) // יש תוצאות (מעבר לשורות כותרות)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();

                    dataGridView1.DataSource = result;
                    dataGridView1.ColumnHeadersVisible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    dataGridView3.Visible = false;

                    currentStudentUsername = nameToSearch;

                    // הצגת מספר התוצאות
                    int studentsFound = result.Rows.Count - 2; // מחסירים את שורות הכותרות
                    MessageBox.Show($"נמצאו {studentsFound} תלמידים עם השם '{nameToSearch}'");
                }
                else
                {
                    MessageBox.Show($"לא נמצאו תלמידים עם השם '{nameToSearch}'.\nוודא שהשם נכתב נכון ושהסטודנט קיים במערכת.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בחיפוש:\n" + ex.Message + "\n\nפרטים נוספים נשמרו בלוג הדיבוג.");

                // הצגת פרטי דיבוג במקרה של שגיאה
                System.Diagnostics.Debug.WriteLine($"שגיאה בחיפוש עבור '{nameToSearch}': {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string idToSearch = txtSearchName.Text.Trim();

            if (string.IsNullOrEmpty(idToSearch))
            {
                MessageBox.Show("אנא הזן מספר תעודת זהות לחיפוש.");
                return;
            }

            try
            {
                var result = logic.SearchStudentByID(idToSearch);

                if (result.Rows.Count > 2) // יש תוצאות (מעבר לשורות כותרות)
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Columns.Clear();
                    dataGridView1.Rows.Clear();

                    dataGridView1.DataSource = result;
                    dataGridView1.ColumnHeadersVisible = false;
                    dataGridView1.RightToLeft = RightToLeft.Yes;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    dataGridView3.Visible = false;

                    currentStudentUsername = result.Rows[2][0].ToString().Trim();

                    // הצגת מספר התוצאות
                    int studentsFound = result.Rows.Count - 2; // מחסירים את שורות הכותרות
                    MessageBox.Show($"נמצאו {studentsFound} תלמידים עם ת\"ז שמכילה '{idToSearch}'");
                }
                else
                {
                    MessageBox.Show($"לא נמצאו תלמידים עם מספר תעודת זהות שמכיל את הספרות '{idToSearch}'.\nוודא שמספר הת\"ז נכתב נכון.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בחיפוש:\n" + ex.Message + "\n\nפרטים נוספים נשמרו בלוג הדיבוג.");
                System.Diagnostics.Debug.WriteLine($"שגיאה בחיפוש ת\"ז עבור '{idToSearch}': {ex.Message}");
            }
        }

        private async void btnSendEmail_Click(object sender, EventArgs e)
        {
            string emailTo = "";
            string tempPath = Path.Combine(Path.GetTempPath(), $"גליון_נתונים_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

            try
            {
                // יצירת קובץ זמני מהגריד הפעיל
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Data");
                    DataGridView activeGrid = GetActiveGrid();

                    if (activeGrid == null)
                    {
                        MessageBox.Show("אין נתונים לשלוח.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // כתיבת כותרות
                    for (int i = 0; i < activeGrid.Columns.Count; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = activeGrid.Columns[i].HeaderText;
                    }

                    // כתיבת תוכן
                    for (int i = 0; i < activeGrid.Rows.Count; i++)
                    {
                        for (int j = 0; j < activeGrid.Columns.Count; j++)
                        {
                            worksheet.Cell(i + 2, j + 1).Value = activeGrid.Rows[i].Cells[j].Value?.ToString();
                        }
                    }

                    worksheet.Columns().AdjustToContents();
                    workbook.SaveAs(tempPath);
                }

                // לבדוק מה נבחר ברדיובטן
                System.Diagnostics.Debug.WriteLine($"rdoSendToStudent.Checked = {rdoSendToStudent.Checked}");
                System.Diagnostics.Debug.WriteLine($"rdoSendToLecturer.Checked = {rdoSendToLecturer.Checked}");

                // קביעת למי לשלוח:
                if (rdoSendToStudent.Checked)
                {
                    if (string.IsNullOrEmpty(currentStudentUsername))
                    {
                        MessageBox.Show("יש לבחור תחילה תלמיד (באמצעות חיפוש שם או ת\"ז).", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    emailTo = GetStudentEmail(currentStudentUsername);
                }
                else // rdoSendToLecturer.Checked
                {
                    emailTo = GetLecturerEmail();
                }

                // בדיקה אם נמצא מייל
                if (string.IsNullOrEmpty(emailTo))
                {
                    MessageBox.Show("לא נבחר מייל לשליחה.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // שליחת המייל
                await SendEmailWithSendGridAsync(tempPath, emailTo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בשליחת המייל: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataGridView GetActiveGrid()
        {
            if (dataGridView1.Visible && dataGridView1.DataSource != null)
                return dataGridView1;
            else if (dataGridView2.Visible && dataGridView2.DataSource != null)
                return dataGridView2;
            else if (dataGridView3.Visible && dataGridView3.DataSource != null)
                return dataGridView3;
            else
                return null;
        }






        private async Task SendEmailWithSendGridAsync(string filePath, string recipientEmail)
        {
            try
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                if (string.IsNullOrWhiteSpace(apiKey))
                    throw new InvalidOperationException("Missing SENDGRID_API_KEY");

                var client = new SendGridClient(apiKey);

                var from = new EmailAddress("ayalafaber@gmail.com", "מערכת מבחנים");
                var to = new EmailAddress(recipientEmail);

                var subject = "📄 גיליון ציונים ממערכת המבחנים";
                var plainTextContent = "שלום, מצורף קובץ הציונים.";
                var htmlContent = "<strong>שלום, מצורף קובץ הציונים.</strong>";

                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

                byte[] fileBytes = File.ReadAllBytes(filePath);
                string fileBase64 = Convert.ToBase64String(fileBytes);

                msg.AddAttachment(Path.GetFileName(filePath), fileBase64);

                var response = await client.SendEmailAsync(msg);

                if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
                    response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("המייל נשלח בהצלחה ✅", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"שגיאה בשליחת המייל: {response.StatusCode}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בשליחת המייל: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetStudentEmail(string username)
        {
            using (var wb = new XLWorkbook(logic.FilePath))
            {
                var ws = wb.Worksheet("Users");

                foreach (var row in ws.RowsUsed().Skip(1))
                {
                    string role = row.Cell(6).GetString().Trim();

                    if (row.Cell(4).GetString().Trim().Equals(username, StringComparison.OrdinalIgnoreCase)
                        && role.Equals("Student", StringComparison.OrdinalIgnoreCase))
                    {
                        return row.Cell(5).GetString().Trim();
                    }
                }
            }

            return "";
        }

        private string GetLecturerEmail()
        {
            if (currentUserRole == "Lecturer")
            {
                return currentUserEmail;
            }
            else
            {
                MessageBox.Show("המשתמש הנוכחי אינו מרצה!", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }


        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView activeGrid = null;

                if (dataGridView1.Visible && dataGridView1.DataSource != null)
                    activeGrid = dataGridView1;
                else if (dataGridView2.Visible && dataGridView2.DataSource != null)
                    activeGrid = dataGridView2;
                else if (dataGridView3.Visible && dataGridView3.DataSource != null)
                    activeGrid = dataGridView3;

                if (activeGrid == null)
                {
                    MessageBox.Show("אין נתונים לייצא.", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // פתיחת חלון בחירת מקום שמירה
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "בחר מיקום לשמירת הגיליון";
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.FileName = $"גליון_ציונים_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string savePath = saveFileDialog.FileName;

                        // יצירת הקובץ Excel
                        using (var workbook = new ClosedXML.Excel.XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Data");

                            // כתיבת כותרות
                            for (int i = 0; i < activeGrid.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = activeGrid.Columns[i].HeaderText;
                            }

                            // כתיבת תוכן
                            for (int i = 0; i < activeGrid.Rows.Count; i++)
                            {
                                for (int j = 0; j < activeGrid.Columns.Count; j++)
                                {
                                    worksheet.Cell(i + 2, j + 1).Value = activeGrid.Rows[i].Cells[j].Value?.ToString();
                                }
                            }

                            worksheet.Columns().AdjustToContents();

                            workbook.SaveAs(savePath);

                            MessageBox.Show($"הקובץ נשמר בהצלחה:\n{savePath}", "הצלחה", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בייצוא לקובץ: {ex.Message}", "שגיאה", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void averageGroupBox_Enter(object sender, EventArgs e)
        {
        }

       
    }
}







