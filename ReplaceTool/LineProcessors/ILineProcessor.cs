namespace ReplaceTool.LineProcessors
{
    public interface ILineProcessor
    {
        bool CanReplace(string line);

        LineProcessingResult Replace(string line);
    }
}