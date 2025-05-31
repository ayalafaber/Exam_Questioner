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
            const string apiKey = "";
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            // בניית הפרומפט
            var prompt = $@"
השאלה: {question}
תשובה נכונה לדוגמה: {correctAnswer}
תשובת המשתמש: {userAnswer}
האם התשובה(של המשתמש) נכונה? ענה 'כן' או 'לא' והסבר בקצרה.";

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
