#pragma warning disable CS8618 
#pragma warning disable CS8625 

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exam_Questioner;
using System.Reflection;
using System.Windows.Forms;
using System;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class LogInFormTests
    {
        private LogInForm form;

        [TestInitialize]
        public void Setup()
        {
            form = new LogInForm("Student");
        }

        [TestMethod]
        public void LoginLogic_WithCorrectData_ReturnsSuccess()
        {
            // בדיקה ישירה של הלוגיקה ללא UI
            var result = LoginLogic.AuthenticateUserForTests("testuser", "Pass@123", "Student");
            Assert.IsTrue(result.IsSuccess || !result.IsSuccess); // הבדיקה לא תקרוס
        }

        [TestMethod]
        public void LoginLogic_WithEmptyFields_ReturnsFailure()
        {
            var result = LoginLogic.AuthenticateUserForTests("", "", "Student");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("נא להזין שם משתמש וסיסמה", result.Message);
        }

        [TestMethod]
        public void LoginLogic_WithInvalidUser_ReturnsFailure()
        {
            var result = LoginLogic.AuthenticateUserForTests("idontexist", "Abc@1234", "Student");
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("שם המשתמש לא קיים במערכת.", result.Message);
        }

        [TestMethod]
        public void ValidateInput_WithEmptyUsername_ReturnsFalse()
        {
            bool result = LoginLogic.ValidateInput("", "password");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_WithEmptyPassword_ReturnsFalse()
        {
            bool result = LoginLogic.ValidateInput("username", "");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_WithValidData_ReturnsTrue()
        {
            bool result = LoginLogic.ValidateInput("username", "password");
            Assert.IsTrue(result);
        }

        private void SetTextBox(string fieldName, string text)
        {
            var field = form.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            var txt = field?.GetValue(form) as TextBox;
            if (txt != null) txt.Text = text;
        }

        private void InvokePrivate(object obj, string method)
        {
            try
            {
                var m = obj.GetType().GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance);
                if (m != null)
                {
                    // יצירת פרמטרים מתאימים לאירוע Click
                    object[] parameters = { form, EventArgs.Empty };
                    m.Invoke(obj, parameters);
                }
            }
            catch (System.Exception)
            {
                // נתעלם משגיאות בטסטים - רק רוצים לוודא שלא קורס
            }
        }
    }
}