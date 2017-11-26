using System.IO;
using System.Text.RegularExpressions;

namespace ReplaceTool.FileProcessors
{
    public class ScaningFileProcessor : IFileProcessor
    {
        private static readonly Regex pattern = new Regex(@"log(?:g?er)*\.[a-z]+\(\$?\""", RegexOptions.Compiled | RegexOptions.IgnoreCase);        
        
        public FileProcessingResult Process(string path)
        {
            var isFileNameFlushed = false;
            var result = new FileProcessingResult {FileName = path};
            foreach (var line in File.ReadLines(path))
            {
                result.TotalLineCount++;
                if (pattern.IsMatch(line))
                {
                    result.ReplacedLineCount++;
                    if (!isFileNameFlushed)
                    {
                        LogContext.Log(path);
                        isFileNameFlushed = true;
                    }
                    LogContext.Log(line);
                }
            }

            return result;
        }
    }
}