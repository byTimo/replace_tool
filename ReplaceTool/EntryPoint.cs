using System;
using System.Linq;
using ReplaceTool.FileProcessors;

namespace ReplaceTool
{
    internal static class EntryPoint
    {
        private static readonly DirectoryProcessor directoryProcessor = new DirectoryProcessor(new ReplaceFileProcessor());
        
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException("Не указан каталог для замены");
            }

            var targetDirectory = args.First();

            using (LogContext.Create(@"D:\lol.txt"))
            {
                var result = directoryProcessor.Process(targetDirectory);
                Console.WriteLine($"{result.ReplacedFileCount} / {result.TotalFileCount} for {result.Time}");
                Console.WriteLine($"{result.ReplacedLineCount} / {result.TotalLineCount}");
            }
        }
    }
}