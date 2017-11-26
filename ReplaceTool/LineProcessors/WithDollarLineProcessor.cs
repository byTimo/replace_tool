using System.Linq;
using System.Text.RegularExpressions;

namespace ReplaceTool.LineProcessors
{
    public class WithDollarLineProcessor : ILineProcessor
    {
        private readonly IArgumentProcessor argumentProcessor;
        private static readonly Regex digitLogPattern = new Regex(@"(?<logger>\s*log(?:g?er)?\.[a-z]+)\(\$\""(?<message>.*{[\w\.,@:\(\)\""\?\s]+}.*)\""\);$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex argsPattern = new Regex(@"{(?<arg>[\w\.,@:\(\)\""\?\s]+)}", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        public WithDollarLineProcessor(IArgumentProcessor argumentProcessor = null)
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
            var logger = match.Groups["logger"].Value;
            var log = match.Groups["message"].Value;

            var paramsLine = string.Join(", ", argsPattern.Matches(log).Cast<Match>().Select(x => x.Groups["arg"].Value).GroupBy(x => x).Select(x => x.Key));

            var newMessage = argsPattern.Replace(log, m => $"{{{argumentProcessor.Get(m.Groups["arg"].Value)}}}");

            result.NewLine = $"{logger}{(logger.EndsWith("Format") ? "" : "Format")}(\"{newMessage}\", {paramsLine});";
            result.Replaced = true;

            return result;
        }
    }
}