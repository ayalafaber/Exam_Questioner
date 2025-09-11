using System;
using System.IO;

namespace Exam_Questioner
{
    public static class EnvLoader
    {
        public static void Load(string path = ".env")
        {
            if (!File.Exists(path)) return;

            foreach (var line in File.ReadAllLines(path))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith("#")) continue;

                var idx = trimmed.IndexOf('=');
                if (idx <= 0) continue;

                var key = trimmed.Substring(0, idx).Trim();
                var val = trimmed.Substring(idx + 1).Trim();

                Environment.SetEnvironmentVariable(key, val);
            }
        }
    }
}
