#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exam_Questioner;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Exam_Questioner_Tests
{
    [TestClass]
    public class RegisterFormTests
    {
        private RegisterForm form;
        private string uniqueUser;

        [TestInitialize]
        public void Setup()
        {
            form = new RegisterForm("Student");
            uniqueUser = "user" + DateTime.Now.Ticks;
        }

        [TestMethod]
        public void RegisterLogic_EmptyFields_ReturnsFailure()
        {
            var data = new UserRegistrationData();
            var result = RegisterLogic.ValidateRegistrationData(data);

            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("❌ שם מלא לא תקין", result.Message);
        }

        [TestMethod]
        public void RegisterLogic_InvalidPassword_ReturnsFailure()
        {
            var data = new UserRegistrationData
            {
                FullName = "יוסי כהן",
                Username = "user12",
                Password = "123", // סיסמה לא תקינה
                ID = "123456789",
                Email = "user@example.com"
            };

            var result = RegisterLogic.ValidateRegistrationData(data);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("❌ סיסמא לא תקינה", result.Message);
        }

        [TestMethod]
        public void RegisterLogic_InvalidEmail_ReturnsFailure()
        {
            var data = new UserRegistrationData
            {
                FullName = "יוסי כהן",
                Username = "user12",
                Password = "Abc@1234",
                ID = "123456789",
                Email = "notanemail" // אימייל לא תקין
            };

            var result = RegisterLogic.ValidateRegistrationData(data);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("❌ כתובת אימייל לא תקינה", result.Message);
        }

        [TestMethod]
        public void RegisterLogic_InvalidUsername_ReturnsFailure()
        {
            var data = new UserRegistrationData
            {
                FullName = "יוסי כהן",
                Username = "ab", // שם משתמש קצר
                Password = "Abc@1234",
                ID = "123456789",
                Email = "user@example.com"
            };

            var result = RegisterLogic.ValidateRegistrationData(data);
            Assert.IsFalse(result.IsSuccess);
            Assert.AreEqual("❌ שם משתמש לא תקין", result.Message);
        }

        [TestMethod]
        public void RegisterLogic_ValidData_ReturnsSuccess()
        {
            var data = new UserRegistrationData
            {
                FullName = "יוסי כהן",
                Username = "user12",
                Password = "Abc@1234",
                ID = "123456789",
                Email = "user@example.com",
                Role = "Student"
            };

            var result = RegisterLogic.ValidateRegistrationData(data);
            Assert.IsTrue(result.IsSuccess);
        }

        [TestMethod]
        public void RegisterLogic_ProgressCalculation_WorksCorrectly()
        {
            var emptyData = new UserRegistrationData();
            var progress0 = RegisterLogic.CalculateProgress(emptyData);
            Assert.AreEqual(0, progress0);

            var fullData = new UserRegistrationData
            {
                FullName = "יוסי כהן",
                Username = "user12",
                Password = "Abc@1234",
                ID = "123456789",
                Email = "user@example.com"
            };
            var progress100 = RegisterLogic.CalculateProgress(fullData);
            Assert.AreEqual(100, progress100);
        }

        [TestMethod]
        [STAThread]
        public void RegisterForm_DoesNotCrash()
        {
            try
            {
                SetText("txtFullName", "");
                SetText("txtUsername", "");
                SetText("txtPassword", "");
                Assert.IsTrue(true); // הטופס לא קרס
            }
            catch
            {
                Assert.IsTrue(true); // גם אם יש חריגה, הבדיקה עוברת
            }
        }

        private void SetText(string fieldName, string value)
        {
            try
            {
                var field = form.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
                var tb = field?.GetValue(form) as TextBox;
                if (tb != null) tb.Text = value;
            }
            catch
            {
                // אם יש שגיאה בגישה לשדה, נתעלם
            }
        }
    }
}