using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_Questioner
{
    public partial class LecturerReviewsForm : Form
    {
        private readonly string _lecturerUsername;
        private readonly string _lecturerFullName;

        public LecturerReviewsForm(string lecturerUsername, string lecturerFullName)
        {
            _lecturerUsername = lecturerUsername;
            _lecturerFullName = lecturerFullName;
            InitializeComponent();
        }

        private void LecturerReviewsForm_Load(object sender, EventArgs e)
        {
            LoadStudents();
            SetupRatingButtons();
        }

        private void SetupRatingButtons()
        {
            // Updated colors to match the student form
            rbStar.BackColor = Color.FromArgb(245, 158, 11);  // Gold/Yellow for excellent
            rbHappy.BackColor = Color.FromArgb(16, 185, 129); // Green for good
            rbSad.BackColor = Color.FromArgb(239, 68, 68);    // Red for improvement

            // Set initial state
            rbStar.Checked = false;
            rbHappy.Checked = false;
            rbSad.Checked = false;
        }

        private void LoadStudents()
        {
            try
            {
                var students = ExcelHelper.GetAllStudents();

                if (students.Count > 0)
                {
                    cmbStudents.DisplayMember = "FullName";
                    cmbStudents.ValueMember = "Username";
                    cmbStudents.DataSource = students;
                }
                else
                {
                    MessageBox.Show("לא נמצאו סטודנטים במערכת!", "מידע",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת הסטודנטים: {ex.Message}", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbStudents.SelectedItem == null)
            {
                MessageBox.Show("אנא בחר סטודנט!", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReviewMessage.Text))
            {
                MessageBox.Show("אנא כתב הודעת ביקורת!", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!rbStar.Checked && !rbHappy.Checked && !rbSad.Checked)
            {
                MessageBox.Show("אנא בחר דירוג!", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedStudent = (StudentInfo)cmbStudents.SelectedItem;

                string rating = "";
                if (rbStar.Checked) rating = "⭐ מצוין";
                else if (rbHappy.Checked) rating = "😊 טוב";
                else if (rbSad.Checked) rating = "😞 צריך שיפור";

                bool success = ExcelHelper.SaveReview(
                    _lecturerFullName,
                    selectedStudent.Username,
                    selectedStudent.FullName,
                    txtReviewMessage.Text,
                    rating
                );

                if (success)
                {
                    MessageBox.Show("הביקורת נשלחה בהצלחה!", "הצלחה",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear form
                    txtReviewMessage.Clear();
                    rbStar.Checked = false;
                    rbHappy.Checked = false;
                    rbSad.Checked = false;
                    SetupRatingButtons(); // Reset button colors

                    if (cmbStudents.Items.Count > 0)
                        cmbStudents.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("שגיאה בשמירת הביקורת!", "שגיאה",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בשמירת הביקורת: {ex.Message}", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}