namespace ReplaceTool
{
    public static class StringExtensions
    {
        public static string CapitalLetter(this string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}