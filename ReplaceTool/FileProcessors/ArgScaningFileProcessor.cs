using System.IO;
using System.Text.RegularExpressions;

namespace ReplaceTool.FileProcessors
{
    public class ArgScaningFileProcessor : IFileProcessor
    {
        private static readonly Regex linePattern = new Regex(@"log(?:g?er)*\.[a-z]+\(\$?\"".*(?:{(?<arg>[\w\.\?]+)}.*).*\);\s*$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex argPattern = new Regex(@"{(?<arg>[\w\.]+)}", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
            
        public FileProcessingResult Process(string path)
        {
            var isFileNameFlushed = false;
            var result = new FileProcessingResult {FileName = path};
            foreach (var line in File.ReadLines(path))
            {
                result.TotalLineCount++;
                if (linePattern.IsMatch(line))
                {
                    if (!isFileNameFlushed)
                    {
                        LogContext.Log(path);
                        isFileNameFlushed = true;
                    }
                    LogContext.Log($" ---> {line.Trim()}");
                    foreach (Match match in argPattern.Matches(line))
                    {
                        result.ReplacedLineCount++;
                        LogContext.Log(match.Groups["arg"].Value);
                    }
                    
                }
            }

            return result;
        }
    }
}