namespace ReplaceTool.FileProcessors
{
    public interface IFileProcessor
    {
        FileProcessingResult Process(string path);
    }
}