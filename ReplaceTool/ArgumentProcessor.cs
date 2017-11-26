using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ReplaceTool
{
    public class ArgumentProcessor : IArgumentProcessor
    {
        private static readonly Regex stringJoinPattern = new Regex(@".*string\.Join\(\"".+\"",\s*(?<arg>.+)\)", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        private static readonly Regex argumentPattern = new Regex(@"\.?(?<arg>\w+)(?:\.\w+\(.*\))*$", RegexOptions.Compiled | RegexOptions.IgnorePatternWhitespace);

        private static readonly IDictionary<string, string> customMapping = new Dictionary<string, string>
        {
            {"organization.ВидОрганизации", "Format"},
            {"StatisticsType.Recognition", "StatisticsType"},
            {"worker.Id", "WorkerId"},
            {"lastEmployment.Id", "EmploymentId"},
            {"context.EvrikaUser.Id", "EvrikaUserId"},
            {"transaction.Id", "TransactionId"},
            {"transaction.UserId", "TransactionUserId"},
            {"transaction.Type", "TransactionType"},
            {"contractor.Id", "ContractorId"},
            {"document.Id", "DocumentId"},
            {"orgId", "OrganizationId"},
            {"product.Id", "ProductId"},
            {"evrikaUser.Id", "EvrikaUserId"},
            {"keBinding.Id", "KeBindingId"},
            {"mergedUser.Id", "MergedUserId"},
            {"update.Message.Chat.Id", "ChatId"},
            {"portalUser?.Id", "ProtalUserId"}
        };
        
        public string Get(string input)
        {
            if(customMapping.TryGetValue(input, out var custom))
            {
                return custom;
            }
            var stringJoinMatch = stringJoinPattern.Match(input);
            if (stringJoinMatch.Success)
            {
                return Get(stringJoinMatch.Groups["arg"].Value);
            }

            var match = argumentPattern.Match(input);
            return match.Success ? match.Groups["arg"].Value.CapitalLetter() : null;
        }
    }
}