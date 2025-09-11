using Exam_Questioner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Study_Management
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            EnvLoader.Load();
            var test = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            System.Diagnostics.Debug.WriteLine("✅ API key length: " + (test?.Length ?? 0));
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormSplash());

        }
    }
}
