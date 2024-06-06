using System.Text.RegularExpressions;

namespace Front_5.Utils
{
    public class StringSanitizer
    {
        public static string SanitizeStat(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Allow only alphanumeric characters and specific symbols (e.g., space, dash, underscore)
            return Regex.Replace(input, @"[^a-zA-Z0-9\s-_]", string.Empty);
        }
    }
}
