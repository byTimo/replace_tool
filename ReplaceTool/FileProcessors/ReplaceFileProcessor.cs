using System.IO;
using System.Text.RegularExpressions;

namespace ReplaceTool.FileProcessors
{
    public class ReplaceFileProcessor : IFileProcessor
    {
        private static readonly Regex pattern = new Regex(@"log(?:g?er)*\.[a-z]+\(\$?\""", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public ReplaceFileProcessor()
        {
            
        }
        
        public FileProcessingResult Process(string path)
        {
            var lines = File.ReadAllLines(path);
            var buffer = new string[lines.Length];

            for (var i = 0; i < buffer.Length; i++)
            {
                var newLine = ProccesLine(lines[i]);
                buffer[i] = newLine;
            }

            File.WriteAllLines(path, buffer);
            
            return new FileProcessingResult
            {
                FileName = path,
                ReplacedLineCount = lines.Length,
                TotalLineCount = lines.Length
            };
        }

        private string ProccesLine(string line)
        {
            if (!pattern.IsMatch(line))
                return line;
            return line;
        }
    }
}