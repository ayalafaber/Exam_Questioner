using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exam_Questioner
{
    // מייצג פרטי משתמש עבור אימות
    public class UserCredentials
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "";
        public string FullName { get; set; } = "";
    }

    // תוצאות אימות המשתמש
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public string Username { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Role { get; set; } = "";
        public string Email { get; set; } = "";

        // פקטורי מתודות ליצירת תוצאות נפוצות
        public static LoginResult Success(string username, string fullName, string role, string email)
        {
            return new LoginResult
            {
                IsSuccess = true,
                Message = $"התחברת כ{(role == "Student" ? "סטודנט" : "מרצה")}.",
                Username = username,
                FullName = fullName,
                Role = role,
                Email = email
            };
        }

        public static LoginResult Failure(string message)
        {
            return new LoginResult
            {
                IsSuccess = false,
                Message = message
            };
        }
    }

    // סוגי שגיאות לוגין
    public enum LoginErrorType
    {
        EmptyFields,
        UserNotExists,
        WrongPassword,
        WrongRole,
        UnknownError
    }

    public static class LoginLogic
    {
        // בדיקת תקינות נתוני הכניסה הבסיסיים
        public static bool ValidateInput(string username, string password)
        {
            return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password);
        }

        // פונקציה מרכזית לאימות משתמש (עם אפשרות להשבית הודעות)
        public static LoginResult AuthenticateUser(string username, string password, string role, bool suppressMessages = false)
        {
            // בדיקת תקינות נתונים
            if (!ValidateInput(username, password))
            {
                return LoginResult.Failure("נא להזין שם משתמש וסיסמה");
            }

            // בדיקה האם שם המשתמש קיים
            if (!ExcelHelper.UsernameExists(username))
            {
                return LoginResult.Failure("שם המשתמש לא קיים במערכת.");
            }

            // בדיקת התאמה מדויקת
            if (ExcelHelper.UserExists(username, password, role))
            {
                string fullName = ExcelHelper.GetFullName(username, role);
                string email = ExcelHelper.GetUserEmail(username, role);
                return LoginResult.Success(username, fullName, role, email);
            }

            // בדיקה האם המשתמש קיים עם תפקיד אחר
            LoginErrorType errorType = DetermineLoginError(username, password, role);
            string errorMessage = GetErrorMessage(errorType);

            return LoginResult.Failure(errorMessage);
        }

        // קביעת סוג השגיאה המדויק
        private static LoginErrorType DetermineLoginError(string username, string password, string currentRole)
        {
            // בדיקה האם שם משתמש וסיסמה נכונים אבל עם תפקיד אחר
            var alternativeRoles = GetAlternativeRoles(currentRole);

            foreach (string altRole in alternativeRoles)
            {
                if (ExcelHelper.UserExists(username, password, altRole))
                {
                    return LoginErrorType.WrongRole;
                }
            }

            // אם הגענו לכאן, הסיסמה שגויה
            return LoginErrorType.WrongPassword;
        }

        // קבלת תפקידים חלופיים
        private static List<string> GetAlternativeRoles(string currentRole)
        {
            var allRoles = new List<string> { "Student", "Lecturer" };
            return allRoles.Where(role => role != currentRole).ToList();
        }

        // קבלת הודעת שגיאה מתאימה
        private static string GetErrorMessage(LoginErrorType errorType)
        {
            switch (errorType)
            {
                case LoginErrorType.EmptyFields:
                    return "נא להזין שם משתמש וסיסמה";
                case LoginErrorType.UserNotExists:
                    return "שם המשתמש לא קיים במערכת.";
                case LoginErrorType.WrongPassword:
                    return "הסיסמה שגויה עבור שם המשתמש הזה.";
                case LoginErrorType.WrongRole:
                    return "שם משתמש ו/או סיסמה נכונים, אך התפקיד שגוי.";
                default:
                    return "שגיאה לא ידועה.";
            }
        }

        // בדיקה האם משתמש קיים (ללא בדיקת סיסמה)
        public static bool IsUserExists(string username)
        {
            return ExcelHelper.UsernameExists(username);
        }

        // קבלת שם מלא של משתמש
        public static string GetUserFullName(string username, string role)
        {
            return ExcelHelper.GetFullName(username, role);
        }

        // בדיקת חוזק סיסמה (לשימוש עתידי ברישום)
        public static bool IsPasswordStrong(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                return false;

            // ניתן להוסיף כללים נוספים כמו אותיות גדולות, מספרים וכו'
            return true;
        }

        // יצירת הודעת התחברות מותאמת אישית
        public static string CreateWelcomeMessage(string role, string fullName)
        {
            string roleText = role == "Student" ? "סטודנט" : "מרצה";
            return $"שלום {fullName}, התחברת כ{roleText}!";
        }

        // פונקציה מרכזית עם פרמטר suppressMessages
        public static (bool IsSuccess, string Message, string Username, string FullName, string Role)
            AuthenticateUserForTests(string username, string password, string role)
        {
            var result = AuthenticateUser(username, password, role, true);
            return (result.IsSuccess, result.Message, result.Username, result.FullName, result.Role);
        }

        // ולידציה מורחבת של נתוני כניסה
        public static (bool IsValid, string ErrorMessage) ValidateLoginData(string username, string password, string role)
        {
            if (!ValidateInput(username, password))
            {
                return (false, "נא להזין שם משתמש וסיסמה");
            }

            if (string.IsNullOrWhiteSpace(role))
            {
                return (false, "תפקיד לא נבחר");
            }

            var validRoles = new[] { "Student", "Lecturer" };
            if (!validRoles.Contains(role))
            {
                return (false, "תפקיד לא תקין");
            }

            return (true, string.Empty);
        }
    }
}