#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type

using System;
using System.Text.RegularExpressions;

namespace Exam_Questioner
{
    // מייצג נתוני רישום משתמש
    public class UserRegistrationData
    {
        public string FullName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string ID { get; set; } = "";
        public string Email { get; set; } = "";
        public string Role { get; set; } = "";
    }

    // תוצאת ולידציה לשדה אחד
    public class FieldValidationResult
    {
        public bool IsValid { get; set; }
        public string Message { get; set; } = "";
        public ValidationMessageType MessageType { get; set; }
    }

    // סוג הודעת ולידציה
    public enum ValidationMessageType
    {
        Error,
        Success,
        Info
    }

    // תוצאת רישום משתמש
    public class RegistrationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public string ErrorField { get; set; } = "";

        public static RegistrationResult Success()
        {
            return new RegistrationResult
            {
                IsSuccess = true,
                Message = "🎉 ההרשמה הושלמה בהצלחה!\nכעת תוכל להתחבר למערכת."
            };
        }

        public static RegistrationResult Failure(string message, string errorField = "")
        {
            return new RegistrationResult
            {
                IsSuccess = false,
                Message = message,
                ErrorField = errorField
            };
        }
    }

    public static class RegisterLogic
    {
        // ולידציה מלאה של כל הנתונים
        public static RegistrationResult ValidateRegistrationData(UserRegistrationData data)
        {
            var fullNameValidation = ValidateFullName(data.FullName);
            if (!fullNameValidation.IsValid)
                return RegistrationResult.Failure("❌ שם מלא לא תקין", "txtFullName");

            var usernameValidation = ValidateUsername(data.Username);
            if (!usernameValidation.IsValid)
                return RegistrationResult.Failure("❌ שם משתמש לא תקין", "txtUsername");

            var passwordValidation = ValidatePassword(data.Password);
            if (!passwordValidation.IsValid)
                return RegistrationResult.Failure("❌ סיסמא לא תקינה", "txtPassword");

            var idValidation = ValidateID(data.ID);
            if (!idValidation.IsValid)
                return RegistrationResult.Failure("❌ מספר זהות לא תקין", "txtID");

            var emailValidation = ValidateEmail(data.Email);
            if (!emailValidation.IsValid)
                return RegistrationResult.Failure("❌ כתובת אימייל לא תקינה", "txtEmail");

            return RegistrationResult.Success();
        }

        // רישום משתמש (עם אפשרות להשבית שמירה אמיתית)
        public static RegistrationResult RegisterUser(UserRegistrationData data, bool saveToDatabase = true)
        {
            // בדיקת ולידציה
            var validationResult = ValidateRegistrationData(data);
            if (!validationResult.IsSuccess)
                return validationResult;

            // בדיקת קיום נתונים (רק אם שומרים למסד נתונים)
            if (saveToDatabase)
            {
                if (ExcelHelper.IDExists(data.ID))
                    return RegistrationResult.Failure("❌ מספר זהות כבר קיים במערכת", "txtID");

                if (ExcelHelper.UsernameExists(data.Username))
                    return RegistrationResult.Failure("❌ שם משתמש כבר קיים במערכת", "txtUsername");

                // שמירה למסד נתונים
                bool saved = ExcelHelper.RegisterUser(data.Username, data.Password, data.ID,
                                                    data.FullName, data.Email, data.Role);
                if (!saved)
                    return RegistrationResult.Failure("❌ אירעה שגיאה בשמירת הנתונים\nאנא נסה שוב.");
            }

            return RegistrationResult.Success();
        }

        // ולידציה של שם מלא
        public static FieldValidationResult ValidateFullName(string fullName)
        {
            fullName = fullName?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(fullName))
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ שדה חובה - הזן שם מלא",
                    MessageType = ValidationMessageType.Error
                };

            if (fullName.Length < 2)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ שם מלא חייב להכיל לפחות 2 תווים",
                    MessageType = ValidationMessageType.Error
                };

            return new FieldValidationResult
            {
                IsValid = true,
                Message = "✅ שם מלא תקין",
                MessageType = ValidationMessageType.Success
            };
        }

        // ולידציה של שם משתמש
        public static FieldValidationResult ValidateUsername(string username)
        {
            username = username?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(username))
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ שדה חובה - הזן שם משתמש",
                    MessageType = ValidationMessageType.Error
                };

            if (username.Length < 6)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = $"❌ שם משתמש קצר מדי ({username.Length}/6)",
                    MessageType = ValidationMessageType.Error
                };

            if (username.Length > 8)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = $"❌ שם משתמש ארוך מדי ({username.Length}/8)",
                    MessageType = ValidationMessageType.Error
                };

            int digitCount = 0;
            bool hasInvalidChars = false;
            foreach (char c in username)
            {
                if (char.IsDigit(c)) digitCount++;
                else if (!char.IsLetter(c)) hasInvalidChars = true;
            }

            if (hasInvalidChars)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ רק אותיות וספרות מותרים",
                    MessageType = ValidationMessageType.Error
                };

            if (digitCount > 2)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = $"❌ יותר מדי ספרות ({digitCount}/2)",
                    MessageType = ValidationMessageType.Error
                };

            return new FieldValidationResult
            {
                IsValid = true,
                Message = "✅ שם משתמש תקין",
                MessageType = ValidationMessageType.Success
            };
        }

        // ולידציה של סיסמה
        public static FieldValidationResult ValidatePassword(string password)
        {
            password = password ?? "";

            if (string.IsNullOrWhiteSpace(password))
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ שדה חובה - הזן סיסמא",
                    MessageType = ValidationMessageType.Error
                };

            if (password.Length < 8)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = $"❌ סיסמא קצרה מדי ({password.Length}/8)",
                    MessageType = ValidationMessageType.Error
                };

            if (password.Length > 10)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = $"❌ סיסמא ארוכה מדי ({password.Length}/10)",
                    MessageType = ValidationMessageType.Error
                };

            bool hasLetter = false, hasDigit = false, hasSpecial = false;
            foreach (char c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if ("!@#$%&*()_+-=".IndexOf(c) >= 0) hasSpecial = true;
            }

            if (!hasLetter)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ חסרה אות",
                    MessageType = ValidationMessageType.Error
                };

            if (!hasDigit)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ חסרה ספרה",
                    MessageType = ValidationMessageType.Error
                };

            if (!hasSpecial)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ חסר תו מיוחד (!@#$%&*)",
                    MessageType = ValidationMessageType.Error
                };

            return new FieldValidationResult
            {
                IsValid = true,
                Message = "✅ סיסמא חזקה",
                MessageType = ValidationMessageType.Success
            };
        }

        // ולידציה של מספר זהות
        public static FieldValidationResult ValidateID(string id)
        {
            id = id?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(id))
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ שדה חובה - הזן מספר זהות",
                    MessageType = ValidationMessageType.Error
                };

            if (id.Length != 9)
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = $"❌ מספר זהות חייב להכיל 9 ספרות ({id.Length}/9)",
                    MessageType = ValidationMessageType.Error
                };

            if (!long.TryParse(id, out _))
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ מספר זהות חייב להכיל ספרות בלבד",
                    MessageType = ValidationMessageType.Error
                };

            return new FieldValidationResult
            {
                IsValid = true,
                Message = "✅ מספר זהות תקין",
                MessageType = ValidationMessageType.Success
            };
        }

        // ולידציה של אימייל
        public static FieldValidationResult ValidateEmail(string email)
        {
            email = email?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(email))
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ שדה חובה - הזן כתובת אימייל",
                    MessageType = ValidationMessageType.Error
                };

            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                return new FieldValidationResult
                {
                    IsValid = false,
                    Message = "❌ כתובת אימייל לא תקינה",
                    MessageType = ValidationMessageType.Error
                };

            return new FieldValidationResult
            {
                IsValid = true,
                Message = "✅ אימייל תקין",
                MessageType = ValidationMessageType.Success
            };
        }

        // חישוב אחוז התקדמות
        public static int CalculateProgress(UserRegistrationData data)
        {
            int validFields = 0;
            int totalFields = 5;

            if (ValidateFullName(data.FullName).IsValid) validFields++;
            if (ValidateUsername(data.Username).IsValid) validFields++;
            if (ValidatePassword(data.Password).IsValid) validFields++;
            if (ValidateID(data.ID).IsValid) validFields++;
            if (ValidateEmail(data.Email).IsValid) validFields++;

            return (validFields * 100) / totalFields;
        }

        // הודעות מעודדות
        public static string GetMotivationalMessage(int progressPercentage)
        {
            switch (progressPercentage)
            {
                case 0:
                    return "בואו נתחיל! מלאו את הפרטים הבסיסיים 🚀";
                case 20:
                    return "התחלה מעולה! המשיכו כך 💪";
                case 40:
                    return "אתם באמצע הדרך - עוד קצת ואתם שם! 🌟";
                case 60:
                    return "מתקדמים יפה! כמה צעדים פשוטים ואתם בפנים 🎯";
                case 80:
                    return "כמעט סיימתם! עוד שדה אחד ואתם חלק מהמערכת 🏁";
                case 100:
                    return "מושלם! הצטרפו למערכת ולמדו דברים חדשים 🎓✨";
                default:
                    return "המשיכו כך - אתם עושים עבודה נהדרת! 👍";
            }
        }

        // דרישות לשדה (לתצוגה למשתמש)
        public static string GetFieldRequirements(string fieldType)
        {
            switch (fieldType.ToLower())
            {
                case "fullname":
                    return "💡 הזן את שמך המלא";
                case "username":
                    return "💡 6-8 תווים, מקסימום 2 ספרות, שאר האותיות באנגלית";
                case "password":
                    return "💡 8-10 תווים, לפחות אות אחת, ספרה אחת ותו מיוחד (!@#$%&*)";
                case "id":
                    return "💡 מספר זהות בן 9 ספרות בדיוק";
                case "email":
                    return "💡 כתובת אימייל תקינה (example@domain.com)";
                default:
                    return "";
            }
        }
    }
}