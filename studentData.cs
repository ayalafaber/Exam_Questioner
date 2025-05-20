using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using ClosedXML.Excel;



namespace Exam_Questioner
{
    public partial class studentData : Form
    {
        private StudentDataLogic logic;
        
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
                dataGridView1.RightToLeft = RightToLeft.Yes;
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView3.Visible = false;
                txtSearchName.Visible = true; // הפעלת אפשרות חיפוש
                btnSearch.Visible = true;
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

                dataGridView3.Columns.Clear(); // חשוב!
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
                var result = logic.SearchStudentByName(nameToSearch);

                if (result.Rows.Count > 2)
                {
                    dataGridView1.DataSource = result;
                    dataGridView1.Visible = true;
                    dataGridView2.Visible = false;
                    dataGridView3.Visible = false;
                }
                else
                {
                    MessageBox.Show("לא נמצאו ציונים עבור השם שחיפשת.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בחיפוש:\n" + ex.Message);
            }
        }
    }
}