using System.Linq;
using System.Text.RegularExpressions;

namespace ReplaceTool.LineProcessors
{
    public class OldFormatLineProcessor : ILineProcessor
    {
        private readonly IArgumentProcessor argumentProcessor;
        private static readonly Regex digitLogPattern = new Regex(@"log(?:g?er)*\.[a-z]+Format\(\""(?<log>.*{\d+}.*)\"",(?<args>.*)\);$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex argsPattern = new Regex(@"\s?(?<args>(?:\w+\.)*\w+)", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);
        private static readonly Regex digitPattern = new Regex(@"{(?<digit>\d+)}", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        public OldFormatLineProcessor(IArgumentProcessor argumentProcessor = null)
        {
            this.argumentProcessor = argumentProcessor ?? new ArgumentProcessor();
        }

        public bool CanReplace(string line)
        {
            return digitLogPattern.Match(line).Success;
        }

        public LineProcessingResult Replace(string input)
        {
            var result = new LineProcessingResult {NewLine = input};
            
            var match = digitLogPattern.Match(input);
            var log = match.Groups["log"].Value;
            var args = match.Groups["args"].Value;

            var stringArgs = argsPattern.Matches(args).Cast<Match>().Select(x => argumentProcessor.Get(x.Value)).ToArray();

            var replaced = input;
            foreach (Match m in digitPattern.Matches(log))
            {
                var digit = m.Groups["digit"].Value;
                var index = int.Parse(digit);
                var word = stringArgs[index];
                replaced = replaced.Replace(m.Value, $"{{{word}}}");
            }
            result.Replaced = true;
            result.NewLine = replaced;

            return result;
        }
    }
}