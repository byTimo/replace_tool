using System.Diagnostics;
using System.IO;
using ReplaceTool.FileProcessors;

namespace ReplaceTool
{
    public class DirectoryProcessor
    {
        private readonly IFileProcessor fileProcessor;
        
        public DirectoryProcessor(IFileProcessor fileProcessor)
        {
            this.fileProcessor = fileProcessor;
        }
            
        public DirectoryProcessingResult Process(string targetDirectory)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var files = Directory.GetFiles(targetDirectory, "*.cs", SearchOption.AllDirectories);

            var result = new DirectoryProcessingResult {TotalFileCount = files.Length};

            foreach (var path in files)
            {
                var fileProcessingResult = fileProcessor.Process(path);
                if (fileProcessingResult.ReplacedLineCount > 0)
                {
                    result.ReplacedFileCount++;
                }
                result.TotalLineCount += fileProcessingResult.TotalLineCount;
                result.ReplacedLineCount += fileProcessingResult.ReplacedLineCount;
            }
            stopwatch.Stop();

            result.Time = stopwatch.Elapsed;

            return result;
        }        
    }
}