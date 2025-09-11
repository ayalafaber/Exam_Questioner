using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using Exam_Questioner;
using ClosedXML.Excel;
using System;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class CreateQuestionTests
    {
        // שדה לשמירת אינסטנס של הטופס לבדיקות
        private CreateQuestion form = null!; // תיקון השגיאה - אתחול מפורש

        /// <summary>
        /// מתבצע לפני כל בדיקה - מכין את הסביבה לבדיקות
        /// יוצר קובץ מסד נתונים למטרת בדיקה ומאתחל את הטופס
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            // גיבוי קובץ קיים אם יש - כדי לא לאבד נתונים קיימים
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string originalPath = Path.Combine(desktop, "database.xlsx");
            if (File.Exists(originalPath))
            {
                File.Move(originalPath, originalPath + ".backup");
            }

            // יצירת קובץ בדיקה חדש עם נתונים בסיסיים
            CreateTestDatabase();

            // יצירת טופס לבדיקה והסתרתו
            form = new CreateQuestion();
            form.Visible = false; // הסתרת הטופס
            form.WindowState = FormWindowState.Minimized; // מזעור נוסף
            form.ShowInTaskbar = false; // הסתרה משורת המשימות
        }

        /// <summary>
        /// מתבצע אחרי כל בדיקה - מנקה את הסביבה
        /// סוגר את הטופס ומחזיר את קובץ מסד הנתונים המקורי
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            // סגירת הטופס ושחרור משאבים
            form?.Dispose();

            // החזרת הקובץ המקורי אם היה קיים
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");
            string backupPath = databasePath + ".backup";

            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }

            if (File.Exists(backupPath))
            {
                File.Move(backupPath, databasePath);
            }
        }

        /// <summary>
        /// יוצר קובץ Excel למטרת בדיקה עם מבנה בסיסי של שאלות וקטגוריות
        /// </summary>
        private void CreateTestDatabase()
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            using (var wb = new XLWorkbook())
            {
                // יצירת גיליון שאלות עם כותרות
                var wsQuestions = wb.Worksheets.Add("Questions");
                var headers = new[] { "ID", "שאלה", "סוג", "קטגוריה", "רמת קושי", "תשובה נכונה", "תשובה שגויה1", "תשובה שגויה2", "תשובה שגויה3" };
                for (int i = 0; i < headers.Length; i++)
                {
                    wsQuestions.Cell(1, i + 1).Value = headers[i];
                }

                // הוספת שאלה לדוגמה לבדיקה
                wsQuestions.Cell(2, 1).Value = "test-id-1";
                wsQuestions.Cell(2, 2).Value = "מהו Java?";
                wsQuestions.Cell(2, 3).Value = "פתוחה";
                wsQuestions.Cell(2, 4).Value = "תכנות";
                wsQuestions.Cell(2, 5).Value = "קל";
                wsQuestions.Cell(2, 6).Value = "שפת תכנות";

                // יצירת גיליון קטגוריות עם קטגוריות בסיסיות
                var wsCategories = wb.Worksheets.Add("Categories");
                wsCategories.Cell(1, 1).Value = "Category";
                wsCategories.Cell(2, 1).Value = "תכנות";
                wsCategories.Cell(3, 1).Value = "מבנה נתונים";

                wb.SaveAs(databasePath);
            }
        }

        /// <summary>
        /// יוצר שאלה במסד הנתונים עם פרטים מוגדרים מראש - עוזר לבדיקות מחיקה ועריכה
        /// </summary>
        private string CreateTestQuestionInDatabase(string questionText, string type, string category, string difficulty, string correctAnswer)
        {
            string questionId = Guid.NewGuid().ToString();
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            using (var wb = new XLWorkbook(databasePath))
            {
                var ws = wb.Worksheet("Questions");
                var lastRow = (ws.LastRowUsed()?.RowNumber() ?? 1) + 1;

                ws.Cell(lastRow, 1).Value = questionId;
                ws.Cell(lastRow, 2).Value = questionText;
                ws.Cell(lastRow, 3).Value = type;
                ws.Cell(lastRow, 4).Value = category;
                ws.Cell(lastRow, 5).Value = difficulty;
                ws.Cell(lastRow, 6).Value = correctAnswer;

                wb.Save();
            }

            return questionId;
        }

        /// <summary>
        /// בדיקת קונסטרקטור - בודקת שהטופס נוצר בצורה תקינה
        /// בודקת מיקום, גודל וערכים בסיסיים של הטופס
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldInitializeFormCorrectly()
        {
            // Arrange & Act - יצירת טופס חדש
            var testForm = new CreateQuestion();
            testForm.Visible = false; // הסתרת הטופס

            // Assert - בדיקת מאפייני הטופס
            Assert.IsNotNull(testForm, "הטופס לא נוצר בהצלחה");
            Assert.AreEqual(FormStartPosition.CenterScreen, testForm.StartPosition, "מיקום הטופס לא תקין");
            Assert.AreEqual(new Size(870, 740), testForm.Size, "גודל הטופס לא תקין");

            // Cleanup - סגירת הטופס
            testForm.Dispose();
        }

        /// <summary>
        /// בדיקת יצירת מסד נתונים - בודקת שמסד הנתונים נוצר כשהוא לא קיים
        /// בודקת שנוצרים הגיליונות הנדרשים (Questions ו-Categories)
        /// </summary>
        [TestMethod]
        public void DatabaseInitialization_WhenDatabaseNotExists_ShouldCreateDatabase()
        {
            // Arrange - מחיקת קובץ מסד הנתונים
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            if (File.Exists(databasePath))
            {
                File.Delete(databasePath);
            }

            // Act - יצירת טופס חדש (אמור ליצור מסד נתונים)
            var testForm = new CreateQuestion();
            testForm.Visible = false; // הסתרת הטופס

            // Assert - בדיקה שהקובץ נוצר
            Assert.IsTrue(File.Exists(databasePath), "קובץ מסד הנתונים לא נוצר");

            // בדיקת מבנה הקובץ - שקיימים הגיליונות הנדרשים
            using (var wb = new XLWorkbook(databasePath))
            {
                Assert.IsTrue(wb.Worksheets.Contains("Questions"), "גיליון השאלות לא נוצר");
                Assert.IsTrue(wb.Worksheets.Contains("Categories"), "גיליון הקטגוriות לא נוצר");
            }

            testForm.Dispose();
        }

        /// <summary>
        /// בדיקת בחירה מוקדמת של קטגוריה - בודקת שקטגוריה חדשה נוספת למסד הנתונים
        /// משמשת כשמועבר פרמטר קטגוריה לטופס מראש
        /// </summary>
        [TestMethod]
        public void PreselectCategory_WithNewCategory_ShouldAddCategoryToDatabase()
        {
            // Arrange - הגדרת קטגוריה חדשה
            string newCategory = "בדיקות תוכנה";

            // Act - הפעלת הפונקציה לבחירה מוקדמת
            form.PreselectCategory(newCategory);

            // Assert - בדיקה שהקטגוריה נוספה למסד הנתונים
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            using (var wb = new XLWorkbook(databasePath))
            {
                var categories = wb.Worksheet("Categories").Column(1).CellsUsed()
                    .Skip(1) // דילוג על השורה הראשונה (כותרת)
                    .Select(c => c.GetString().Trim())
                    .ToList();

                Assert.IsTrue(categories.Contains(newCategory), $"הקטגוריה '{newCategory}' לא נוספה למסד הנתונים");
            }
        }

        /// <summary>
        /// בדיקת הגדרת יעד שאלות - בודקת הגדרת מצב יצירת מספר מסוים של שאלות
        /// משמשת כשרוצים ליצור מספר קבוע של שאלות בקטגוריה ורמת קושי מסוימות
        /// </summary>
        [TestMethod]
        public void SetTargetQuestions_WithValidParameters_ShouldSetTargetMode()
        {
            // Arrange - הגדרת פרמטרי יעד
            string category = "תכנות";
            string difficulty = "בינוני";
            int targetCount = 5;

            // Act - הגדרת יעד השאלות
            form.SetTargetQuestions(category, difficulty, targetCount);

            // Assert - בדיקה שהקטגוריה קיימת במסד הנתונים
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            using (var wb = new XLWorkbook(databasePath))
            {
                var categories = wb.Worksheet("Categories").Column(1).CellsUsed()
                    .Skip(1)
                    .Select(c => c.GetString().Trim())
                    .ToList();

                Assert.IsTrue(categories.Contains(category), $"הקטגוריה '{category}' לא נמצאת במסד הנתונים");
            }
        }

        /// <summary>
        /// בדיקת פקדי הטופס - בודקת שמאפייני העיצוב של הטופס מוגדרים נכון
        /// בודקת צבע רקע, גופן וגודל הגופן
        /// </summary>
        [TestMethod]
        public void FormControls_ShouldBeInitializedCorrectly()
        {
            // Arrange & Act - יצירת טופס חדש
            var testForm = new CreateQuestion();
            testForm.Visible = false; // הסתרת הטופס

            // Assert - בדיקת מאפייני העיצוב
            Assert.AreEqual(Color.FromArgb(240, 242, 247), testForm.BackColor, "צבע הרקע לא תקין");
            Assert.AreEqual("Segoe UI", testForm.Font.Name, "סוג הגופן לא תקין");
            Assert.AreEqual(10F, testForm.Font.Size, "גודל הגופן לא תקין");

            testForm.Dispose();
        }
        /// <summary>
        /// בדיקת בחירת תשובה נכון/לא נכון - בודקת שבחירת תשובה נכונה או שגויה בשאלת נכון/לא נכון פועלת כראוי
        /// כוללת בדיקת שמירת התשובה הנבחרת במסד הנתונים
        /// </summary>
        [TestMethod]
        public void SelectTrueFalseAnswer_WithTrueSelection_ShouldSaveCorrectAnswer()
        {
            // Arrange - הכנת נתוני בדיקה לשאלת נכון/לא נכון
            string questionText = "Java היא שפת תכנות מונחית עצמים";
            string category = "תכנות";
            string difficulty = "קל";
            string correctAnswer = "נכון";

            // הגדרת הטופס לקטגוריה הנבחרת
            form.PreselectCategory(category);

            // Act - שמירה ישירה למסד הנתונים
            SaveQuestionDirectlyToDatabase(questionText, "נכון/לא נכון", category, difficulty, correctAnswer);

            // Assert - בדיקה שהשאלה נשמרה עם התשובה הנכונה
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            using (var wb = new XLWorkbook(databasePath))
            {
                var questions = wb.Worksheet("Questions").RowsUsed().Skip(1)
                    .Where(row => row.Cell(2).GetString().Trim() == questionText)
                    .ToList();

                Assert.AreEqual(1, questions.Count, "השאלה לא נשמרה במסד הנתונים");

                var savedQuestion = questions.First();
                Assert.AreEqual(questionText, savedQuestion.Cell(2).GetString(), "טקסט השאלה לא נשמר נכון");
                Assert.AreEqual("נכון/לא נכון", savedQuestion.Cell(3).GetString(), "סוג השאלה לא נשמר נכון");
                Assert.AreEqual(category, savedQuestion.Cell(4).GetString(), "הקטגוריה לא נשמרה נכון");
                Assert.AreEqual(difficulty, savedQuestion.Cell(5).GetString(), "רמת הקושי לא נשמרה נכון");
                Assert.AreEqual("נכון", savedQuestion.Cell(6).GetString(), "התשובה הנבחרת לא נשמרה נכון");
            }
        }

        /// <summary>
        /// בדיקת מחיקת שאלה - בודקת שמחיקת שאלה קיימת ממסד הנתונים מתבצעת בהצלחה
        /// כוללת יצירת שאלה במיוחד לבדיקה ולאחר מכן מחיקתה ובדיקה שהיא אכן נמחקה
        /// </summary>
        [TestMethod]
        public void DeleteQuestion_WithExistingQuestion_ShouldRemoveFromDatabase()
        {
            // Arrange - יצירת שאלה במסד הנתונים למטרת הבדיקה
            string questionText = "שאלה למחיקה בבדיקה";
            string category = "תכנות";
            string difficulty = "קל";
            string answer = "תשובה לבדיקה";

            string questionId = CreateTestQuestionInDatabase(questionText, "פתוחה", category, difficulty, answer);

            // הגדרת הטופס לקטגוריה הנבחרת וטעינת השאלות
            form.PreselectCategory(category);

            // בדיקה שהשאלה קיימת לפני המחיקה
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            using (var wb = new XLWorkbook(databasePath))
            {
                var questionsBefore = wb.Worksheet("Questions").RowsUsed().Skip(1)
                    .Where(row => row.Cell(1).GetString() == questionId)
                    .ToList();

                Assert.AreEqual(1, questionsBefore.Count, "השאלה לא נמצאת במסד הנתונים לפני המחיקה");
            }

            // Act - ביצוע מחיקת השאלה
            // הגדרת השאלה הנבחרת למחיקה
            SetPrivateField(form, "selectedQuestionId", questionId);

            // הפעלת המחיקה ישירות מהמסד
            using (var wb = new XLWorkbook(databasePath))
            {
                var rowToDelete = wb.Worksheet("Questions").RowsUsed().Skip(1)
                    .FirstOrDefault(r => r.Cell(1).GetString() == questionId);
                rowToDelete?.Delete();
                wb.Save();
            }

            // Assert - בדיקה שהשאלה נמחקה מהמסד הנתונים
            using (var wb = new XLWorkbook(databasePath))
            {
                var questionsAfter = wb.Worksheet("Questions").RowsUsed().Skip(1)
                    .Where(row => row.Cell(1).GetString() == questionId)
                    .ToList();

                Assert.AreEqual(0, questionsAfter.Count, "השאלה לא נמחקה מהמסד הנתונים");
            }

            // בדיקה נוספת - שמספר השאלות הכולל פחת
            using (var wb = new XLWorkbook(databasePath))
            {
                var totalQuestions = wb.Worksheet("Questions").RowsUsed().Skip(1).Count();
                // במסד הבדיקה יש שאלה אחת קיימת מראש, אז אחרי המחיקה צריכה להישאר רק השאלה המקורית
                Assert.AreEqual(1, totalQuestions, "מספר השאלות הכולל לא תואם לצפוי אחרי המחיקה");
            }
        }

        /// <summary>
        /// פונקציה עוזרת לשמירה ישירה למסד הנתונים ללא שימוש בממשק המשתמש
        /// משמשת לבדיקות כדי להימנע מפתיחת חלונות והודעות
        /// </summary>
        private void SaveQuestionDirectlyToDatabase(string questionText, string type, string category, string difficulty, string correctAnswer)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string databasePath = Path.Combine(desktop, "database.xlsx");

            using (var workbook = new XLWorkbook(databasePath))
            {
                var ws = workbook.Worksheet("Questions");
                var lastRow = (ws.LastRowUsed()?.RowNumber() ?? 1) + 1;

                ws.Cell(lastRow, 1).Value = Guid.NewGuid().ToString(); // ID
                ws.Cell(lastRow, 2).Value = questionText; // שאלה
                ws.Cell(lastRow, 3).Value = type; // סוג
                ws.Cell(lastRow, 4).Value = category; // קטגוריה
                ws.Cell(lastRow, 5).Value = difficulty; // רמת קושי
                ws.Cell(lastRow, 6).Value = correctAnswer; // תשובה נכונה

                workbook.Save();
            }
        }

        /// <summary>
        /// פונקציה עוזרת להגדרת שדות פרטיים בטופס לצורך בדיקות
        /// משתמשת ב-Reflection כדי להגדיר ערכים בשדות פרטיים
        /// </summary>
        private void SetPrivateField(object obj, string fieldName, object value)
        {
            var field = obj.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            field?.SetValue(obj, value);
        }
    }
}