namespace ReplaceTool.FileProcessors
{
    public class FileProcessingResult
    {
        public string FileName { get; set; }

        public int TotalLineCount { get; set; }
        
        public int ReplacedLineCount { get; set; } 
    }
}