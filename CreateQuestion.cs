using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace Exam_Questioner
{
    public partial class CreateQuestion : Form
    {
        string filePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "database.xlsx");
        //private string filePath = "database.xlsx";
        private DataTable questionsTable;
        private string selectedQuestionId = null;

        public CreateQuestion()
        {
            InitializeComponent();
            InitializeExcel();

        }

        private void CreateQuestion_Load(object sender, EventArgs e)
        {
            comboBoxType.Items.AddRange(new string[] { "שאלה פתוחה", "שאלה רב ברירה" });
            comboBoxCategory.Items.AddRange(new string[] { "מתמטיקה", "היסטוריה", "פיזיקה", "כימיה" });
            comboBoxDifficulty.Items.AddRange(new string[] { "קל", "בינוני", "קשה" });
            LoadQuestionsToGrid();
        }

        private void InitializeExcel()
        {
            if (!File.Exists(filePath))
            {
                using (var workbook = new XLWorkbook())
                {
                    var ws = workbook.Worksheets.Add("Questions");
                    ws.Cell("A1").Value = "ID";
                    ws.Cell("B1").Value = "שאלה";
                    ws.Cell("C1").Value = "סוג";
                    ws.Cell("D1").Value = "קטגוריה";
                    ws.Cell("E1").Value = "רמת קושי";
                    ws.Cell("F1").Value = "תשובה";
                    workbook.SaveAs(filePath);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsFormIncomplete())
            {
                MessageBox.Show("אנא מלאו את כל השדות");
                return;
            }


            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheet("Questions");
                foreach (var row in ws.RowsUsed().Skip(1))
                {
                    // אם התא בעמודה A (ID) ריק – תן לו GUID חדש
                    if (string.IsNullOrWhiteSpace(row.Cell(1).GetString()))
                        row.Cell(1).Value = Guid.NewGuid().ToString();
                }
                var newQuestionId = Guid.NewGuid().ToString();
                var lastRow = ws.LastRowUsed().RowNumber() + 1;

                ws.Cell(lastRow, 1).Value = newQuestionId;
                ws.Cell(lastRow, 2).Value = txtQuestion.Text;
                ws.Cell(lastRow, 3).Value = comboBoxType.Text;
                ws.Cell(lastRow, 4).Value = comboBoxCategory.Text;
                ws.Cell(lastRow, 5).Value = comboBoxDifficulty.Text;
                ws.Cell(lastRow, 6).Value = txtAnswer.Text;

                workbook.Save();
            }

            MessageBox.Show("השאלה נשמרה בהצלחה!");
            ClearForm();
            LoadQuestionsToGrid();
        }

        private void LoadQuestionsToGrid()
        {
            questionsTable = new DataTable();
            questionsTable.Columns.Add("ID");
            questionsTable.Columns.Add("שאלה");
            questionsTable.Columns.Add("סוג");
            questionsTable.Columns.Add("קטגוריה");
            questionsTable.Columns.Add("רמת קושי");
            questionsTable.Columns.Add("תשובה");

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheet("Questions");
                foreach (var row in ws.RowsUsed().Skip(1))
                {
                    questionsTable.Rows.Add(
                        row.Cell(1).GetString(),
                        row.Cell(2).GetString(),
                        row.Cell(3).GetString(),
                        row.Cell(4).GetString(),
                        row.Cell(5).GetString(),
                        row.Cell(6).GetString()
                    );
                }
            }

            questionsGrid.DataSource = questionsTable;
        }

        private void questionsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = questionsGrid.Rows[e.RowIndex];
                selectedQuestionId = row.Cells[0].Value?.ToString(); // ID
                txtQuestion.Text = row.Cells[1].Value?.ToString();
                comboBoxType.Text = row.Cells[2].Value?.ToString();
                comboBoxCategory.Text = row.Cells[3].Value?.ToString();
                comboBoxDifficulty.Text = row.Cells[4].Value?.ToString();
                txtAnswer.Text = row.Cells[5].Value?.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedQuestionId))
            {
                MessageBox.Show("לא נבחרה שאלה למחיקה.");
                return;
            }

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheet("Questions");
                var row = ws.RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == selectedQuestionId);
                if (row != null) row.Delete();
                workbook.Save();
            }

            MessageBox.Show("השאלה נמחקה בהצלחה.");
            ClearForm();
            LoadQuestionsToGrid();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(selectedQuestionId))
            {
                MessageBox.Show("לא נבחרה שאלה לעריכה.");
                return;
            }

            using (var workbook = new XLWorkbook(filePath))
            {
                var ws = workbook.Worksheet("Questions");
                var row = ws.RowsUsed().Skip(1).FirstOrDefault(r => r.Cell(1).GetString() == selectedQuestionId);
                if (row != null)
                {
                    row.Cell(2).Value = txtQuestion.Text;
                    row.Cell(3).Value = comboBoxType.Text;
                    row.Cell(4).Value = comboBoxCategory.Text;
                    row.Cell(5).Value = comboBoxDifficulty.Text;
                    row.Cell(6).Value = txtAnswer.Text;
                }
                workbook.Save();
            }

            MessageBox.Show("השאלה עודכנה בהצלחה.");
            ClearForm();
            LoadQuestionsToGrid();
        }

        private bool IsFormIncomplete()
        {
            return string.IsNullOrWhiteSpace(txtQuestion.Text) ||
                   string.IsNullOrWhiteSpace(txtAnswer.Text) ||
                   comboBoxType.SelectedIndex == -1 ||
                   comboBoxCategory.SelectedIndex == -1 ||
                   comboBoxDifficulty.SelectedIndex == -1;
        }

        private void ClearForm()
        {
            txtQuestion.Clear();
            txtAnswer.Clear();
            comboBoxType.SelectedIndex = -1;
            comboBoxCategory.SelectedIndex = -1;
            comboBoxDifficulty.SelectedIndex = -1;
            selectedQuestionId = null;
        }
    }
}
