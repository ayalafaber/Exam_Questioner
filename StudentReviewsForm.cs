using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Media;

namespace Exam_Questioner
{
    public partial class StudentReviewsForm : Form
    {
        private readonly string _studentUsername;
        private readonly string _studentFullName;
        private Timer confettiTimer;
        private List<ConfettiLabel> confettiLabels;
        private Random random = new Random();
        private SoundPlayer yayy;
        private SoundPlayer sad;


        public StudentReviewsForm(string studentUsername, string studentFullName)
        {
            _studentUsername = studentUsername;
            _studentFullName = studentFullName;
            InitializeComponent();
            yayy = new SoundPlayer(Properties.Resources.yayy);
            sad = new SoundPlayer(Properties.Resources.sad);

            // Initialize confetti system
            confettiLabels = new List<ConfettiLabel>();
            confettiTimer = new Timer();
            confettiTimer.Interval = 30; // Faster animation
            confettiTimer.Tick += ConfettiTimer_Tick;
        }

        private void StudentReviewsForm_Load(object sender, EventArgs e)
        {
            SetupDataGrid();
            LoadReviews();
        }

        private void SetupDataGrid()
        {
            // Clear existing columns
            dataGridReviews.Columns.Clear();

            // Add columns with better spacing
            dataGridReviews.Columns.Add("Date", "📅 תאריך");
            dataGridReviews.Columns.Add("Lecturer", "👨‍🏫 מרצה");
            dataGridReviews.Columns.Add("Message", "💬 הודעת הביקורת");
            dataGridReviews.Columns.Add("Rating", "⭐ דירוג");

            // Set column widths for better display
            dataGridReviews.Columns["Date"].Width = 140;
            dataGridReviews.Columns["Lecturer"].Width = 180;
            dataGridReviews.Columns["Message"].Width = 500;
            dataGridReviews.Columns["Rating"].Width = 210;

            // Set column alignment
            dataGridReviews.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridReviews.Columns["Lecturer"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridReviews.Columns["Message"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridReviews.Columns["Rating"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Font sizes and styles for different columns
            dataGridReviews.Columns["Date"].DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dataGridReviews.Columns["Lecturer"].DefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dataGridReviews.Columns["Message"].DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dataGridReviews.Columns["Rating"].DefaultCellStyle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);

            // Enable text wrapping for message column
            dataGridReviews.Columns["Message"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Add click event for confetti effect
            dataGridReviews.CellClick += DataGridReviews_CellClick;
        }

        private void DataGridReviews_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridReviews.Rows.Count)
            {
                var ratingCell = dataGridReviews.Rows[e.RowIndex].Cells["Rating"];
                if (ratingCell != null && ratingCell.Value != null)
                {
                    string rating = ratingCell.Value.ToString();
                    string emoji = "🎉"; // Default confetti

                    // Choose emoji based on rating
                    if (rating.Contains("מצוין"))
                        emoji = "⭐"; // Star for excellent
                    else if (rating.Contains("טוב"))
                        emoji = "😊"; // Happy face for good
                    else if (rating.Contains("שיפור"))
                        emoji = "💪"; // Muscle for improvement

                    // Start confetti effect
                    StartConfettiEffect(emoji, rating);
                }
            }
        }

        private void StartConfettiEffect(string emoji, string rating = "מצוין")
        {
            // Clear existing confetti
            ClearConfetti();

            // Determine color based on rating
            Color confettiColor = Color.Black; // Default
            if (rating.Contains("מצוין"))
            {
                yayy.Play();
                confettiColor = Color.Gold; // Gold/Yellow for excellent

            }
            else if (rating.Contains("טוב"))
            {
                yayy.Play();
                confettiColor = Color.Green; // Green for good
            }
            else if (rating.Contains("שיפור"))
            {
                sad.Play();
                confettiColor = Color.Red; // Red for improvement

            }

            // Create new confetti labels
            for (int i = 0; i < 30; i++)
            {
                Label confettiLabel = new Label();
                confettiLabel.Text = emoji;
                confettiLabel.Font = new Font("Segoe UI Emoji", 28, FontStyle.Bold);
                confettiLabel.AutoSize = true;
                confettiLabel.BackColor = Color.Transparent;
                confettiLabel.ForeColor = confettiColor;

                // Make the label transparent to parent - simple approach
                confettiLabel.Parent = this;

                int startX = random.Next(0, this.Width - 50);
                int startY = random.Next(-100, -20);

                confettiLabel.Location = new Point(startX, startY);

                ConfettiLabel confetti = new ConfettiLabel
                {
                    Label = confettiLabel,
                    VelocityX = random.Next(-3, 4),
                    VelocityY = random.Next(3, 7),
                    Life = 200 + random.Next(0, 100)
                };

                confettiLabels.Add(confetti);
                this.Controls.Add(confettiLabel);
                confettiLabel.BringToFront();
            }

            // Start animation
            confettiTimer.Start();
        }

        private void ClearConfetti()
        {
            foreach (var confetti in confettiLabels)
            {
                this.Controls.Remove(confetti.Label);
                confetti.Label.Dispose();
            }
            confettiLabels.Clear();
        }

        private void ConfettiTimer_Tick(object sender, EventArgs e)
        {
            // Update confetti labels
            for (int i = confettiLabels.Count - 1; i >= 0; i--)
            {
                var confetti = confettiLabels[i];

                // Update position
                int newX = confetti.Label.Location.X + confetti.VelocityX;
                int newY = confetti.Label.Location.Y + confetti.VelocityY;
                confetti.Label.Location = new Point(newX, newY);

                // Apply gravity
                confetti.VelocityY += 1;

                // Decrease life
                confetti.Life--;

                // Remove if dead or off screen
                if (confetti.Life <= 0 || newY > this.Height + 50)
                {
                    this.Controls.Remove(confetti.Label);
                    confetti.Label.Dispose();
                    confettiLabels.RemoveAt(i);
                }
            }

            // Stop timer when no confetti left
            if (confettiLabels.Count == 0)
            {
                confettiTimer.Stop();
            }
        }

        // Helper class for confetti labels
        public class ConfettiLabel
        {
            public Label Label { get; set; }
            public int VelocityX { get; set; }
            public int VelocityY { get; set; }
            public int Life { get; set; }
        }

        private void LoadReviews()
        {
            try
            {
                var reviews = ExcelHelper.GetReviewsForStudent(_studentUsername);

                dataGridReviews.Rows.Clear();

                if (reviews.Count > 0)
                {
                    foreach (var review in reviews)
                    {
                        int rowIndex = dataGridReviews.Rows.Add(
                            review.Date ?? "",
                            review.LecturerName ?? "",
                            review.Message ?? "",
                            review.Rating ?? ""
                        );

                        // Color rows by rating with beautiful colors
                        DataGridViewRow row = dataGridReviews.Rows[rowIndex];
                        if (!string.IsNullOrEmpty(review.Rating))
                        {
                            if (review.Rating.Contains("מצוין"))
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220); // Light yellow
                                row.DefaultCellStyle.ForeColor = Color.FromArgb(184, 134, 11); // Dark yellow
                                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(245, 158, 11);
                                row.DefaultCellStyle.SelectionForeColor = Color.White;
                            }
                            else if (review.Rating.Contains("טוב"))
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(236, 253, 245); // Light green
                                row.DefaultCellStyle.ForeColor = Color.FromArgb(5, 150, 105); // Dark green
                                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(16, 185, 129);
                                row.DefaultCellStyle.SelectionForeColor = Color.White;
                            }
                            else if (review.Rating.Contains("שיפור"))
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(254, 242, 242); // Very light red
                                row.DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                                row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(239, 68, 68);
                                row.DefaultCellStyle.SelectionForeColor = Color.White;
                            }
                        }
                    }

                    dataGridReviews.Visible = true;
                    lblNoReviews.Visible = false;

                    // Update summary panel
                    labelReviewCount.Text = $"סה\"כ ביקורות: {reviews.Count}";

                    // Calculate and display average rating
                    CalculateAverageRating(reviews);

                    // Update icon based on latest rating
                    var latestReview = reviews.FirstOrDefault();
                    if (latestReview != null && !string.IsNullOrEmpty(latestReview.Rating))
                    {
                        if (latestReview.Rating.Contains("מצוין"))
                            summaryIcon.Text = "⭐";
                        else if (latestReview.Rating.Contains("טוב"))
                            summaryIcon.Text = "😊";
                        else if (latestReview.Rating.Contains("שיפור"))
                            summaryIcon.Text = "💪";
                    }
                }
                else
                {
                    ShowNoReviewsMessage();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"שגיאה בטעינת הביקורות: {ex.Message}", "שגיאה",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowNoReviewsMessage();
            }
        }

        private void CalculateAverageRating(List<ReviewInfo> reviews)
        {
            if (reviews.Count == 0)
            {
                labelAverageRating.Text = "דירוג ממוצע: --";
                avgRatingIcon.Text = "📈";
                return;
            }

            int totalScore = 0;
            int validRatings = 0;

            foreach (var review in reviews)
            {
                if (!string.IsNullOrEmpty(review.Rating))
                {
                    if (review.Rating.Contains("מצוין"))
                    {
                        totalScore += 3;
                        validRatings++;
                    }
                    else if (review.Rating.Contains("טוב"))
                    {
                        totalScore += 2;
                        validRatings++;
                    }
                    else if (review.Rating.Contains("שיפור"))
                    {
                        totalScore += 1;
                        validRatings++;
                    }
                }
            }

            if (validRatings > 0)
            {
                double average = (double)totalScore / validRatings;

                if (average >= 2.5)
                {
                    labelAverageRating.Text = "דירוג ממוצע: מצוין";
                    avgRatingIcon.Text = "⭐";
                    averageRatingPanel.BackColor = Color.FromArgb(245, 158, 11); // Yellow/Gold
                }
                else if (average >= 1.5)
                {
                    labelAverageRating.Text = "דירוג ממוצע: טוב";
                    avgRatingIcon.Text = "😊";
                    averageRatingPanel.BackColor = Color.FromArgb(16, 185, 129); // Green
                }
                else
                {
                    labelAverageRating.Text = "דירוג ממוצע: צריך שיפור";
                    avgRatingIcon.Text = "💪";
                    averageRatingPanel.BackColor = Color.FromArgb(239, 68, 68); // Red
                }
            }
            else
            {
                labelAverageRating.Text = "דירוג ממוצע: --";
                avgRatingIcon.Text = "📈";
                averageRatingPanel.BackColor = Color.FromArgb(16, 185, 129); // Default green
            }
        }

        private void ShowNoReviewsMessage()
        {
            dataGridReviews.Visible = false;
            lblNoReviews.Visible = true;
            labelReviewCount.Text = "סה\"כ ביקורות: 0";
            labelAverageRating.Text = "דירוג ממוצע: --";
            summaryIcon.Text = "📝";
            avgRatingIcon.Text = "📈";
            averageRatingPanel.BackColor = Color.FromArgb(16, 185, 129); // Default green
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReviews();
            MessageBox.Show("הביקורות עודכנו!", "מידע",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Test button for confetti - you can add this temporarily
        private void TestConfetti()
        {
            StartConfettiEffect("🎉");
        }

        // Add this to test - you can call it from anywhere
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            // Press SPACE to test confetti
            if (e.KeyCode == Keys.Space)
            {
                StartConfettiEffect("⭐", "מצוין");
            }
            // Press C to test confetti with smile
            if (e.KeyCode == Keys.C)
            {
                StartConfettiEffect("😊", "טוב");
            }
            // Press M to test confetti with muscle
            if (e.KeyCode == Keys.M)
            {
                StartConfettiEffect("💪", "שיפור");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                confettiTimer?.Stop();
                confettiTimer?.Dispose();
                ClearConfetti();
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }

    // Helper class for confetti labels
    public class ConfettiLabel
    {
        public Label Label { get; set; }
        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public int Life { get; set; }
    }

    // Helper class for confetti particles (keeping for reference)
    public class ConfettiParticle
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float VelocityX { get; set; }
        public float VelocityY { get; set; }
        public string Emoji { get; set; }
        public int Life { get; set; }
    }
}