using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Exam_Questioner
{
    public static class GptAnswerChecker
    {
        // HttpClient לשימוש חוזר
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> CheckAnswerAsync(string question, string correctAnswer, string userAnswer)
        {
            // מפתח ה-API
            const string apiKey = "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"; // הכנס כאן את המפתח שלך
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // בניית הפרומפט
            var prompt = $@"
השאלה: {question}
תשובה נכונה לדוגמה: {correctAnswer}
תשובת המשתמש: {userAnswer}
האם תשובת המשתמש נכונה? ענה 'כן' או 'לא' והסבר בקצרה(תשובתך מוצגת למשתמש אז אין צורך לדבר עיו בגוף שלישי).";

            // גוף הבקשה
            var body = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.0
            };

            var json = JsonSerializer.Serialize(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // שליחת הבקשה
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"OpenAI API returned {(int)response.StatusCode} {response.ReasonPhrase}: {responseString}");
            }

            // ניתוח JSON ושליפת התשובה
            var doc = JsonDocument.Parse(responseString);
            var message = doc.RootElement
                             .GetProperty("choices")[0]
                             .GetProperty("message")
                             .GetProperty("content")
                             .GetString()
                             .Trim();

            return message;
        }
    }
}
