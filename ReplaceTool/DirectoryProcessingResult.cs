using System;

namespace ReplaceTool
{
    public class DirectoryProcessingResult
    {
        public TimeSpan Time { get; set; }

        public int TotalFileCount { get; set; }

        public int ReplacedFileCount { get; set; }
            
        public int TotalLineCount { get; set; }
            
        public int ReplacedLineCount { get; set; }
    }
}