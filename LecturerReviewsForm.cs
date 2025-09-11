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
            rbStar.BackColor = Color.FromArgb(245, 158, 11);  // Gold/Yellow
            rbHappy.BackColor = Color.FromArgb(16, 185, 129); // Green
            rbSad.BackColor = Color.FromArgb(239, 68, 68);    // Red

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

            string reviewMessage = txtReviewMessage.Text;

            // שימוש בלוגיקה החיצונית לבדיקת תקפות הביקורת
            bool isValid = LecturerReviewsLogic.IsReviewValid(
                reviewMessage,
                rbStar.Checked,
                rbHappy.Checked,
                rbSad.Checked
            );

            if (!isValid)
            {
                MessageBox.Show("יש למלא את כל שדות הביקורת כולל דירוג.", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var selectedStudent = (StudentInfo)cmbStudents.SelectedItem;

                // שימוש בלוגיקה החיצונית לקבלת הטקסט של הדירוג
                string rating = LecturerReviewsLogic.GetRating(
                    rbStar.Checked,
                    rbHappy.Checked,
                    rbSad.Checked
                );

                bool success = ExcelHelper.SaveReview(
                    _lecturerFullName,
                    selectedStudent.Username,
                    selectedStudent.FullName,
                    reviewMessage,
                    rating
                );

                if (success)
                {
                    MessageBox.Show("הביקורת נשלחה בהצלחה!", "הצלחה",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // איפוס הטופס
                    txtReviewMessage.Clear();
                    rbStar.Checked = false;
                    rbHappy.Checked = false;
                    rbSad.Checked = false;
                    SetupRatingButtons();

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

        private void headerPanel_Paint(object sender, PaintEventArgs e) { }

        private void titleLabel_Click(object sender, EventArgs e) { }
    }
}