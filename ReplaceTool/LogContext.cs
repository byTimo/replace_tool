using System;
using System.Collections.Generic;
using System.IO;

namespace ReplaceTool
{
    public class LogContext : IDisposable
    {
        private static LogContext context;
        
        private readonly string path;
        private readonly List<string> logLines = new List<string>();

        private LogContext(string path)
        {
            this.path = path;
        }

        public static LogContext Create(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            context = new LogContext(path);
            return context;
        }

        public static void Log(string line)
        {
            context.logLines.Add(line);
        }

        public void Dispose()
        {
            File.WriteAllLines(path, logLines);
        }
    }
}