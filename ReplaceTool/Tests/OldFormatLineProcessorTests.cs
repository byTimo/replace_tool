using NUnit.Framework;
using ReplaceTool.LineProcessors;

namespace ReplaceTool.Tests
{
    [TestFixture]
    public class OldFormatLineProcessorTests
    {
        private readonly ILineProcessor processor = new OldFormatLineProcessor();
        
        [TestCase("logger.ErrorFormat(\"Ошибка конвертации банковского документа {Type}, {Id}.\", ex, document.Type, document.Id.ToString(\"B\"));")]
        [TestCase("logger.Error(\"Ошибка импорта\", exception);")]
        
        public void CanReplace_False(string input)
        {
            var canReplace = processor.CanReplace(input);

            Assert.That(canReplace, Is.EqualTo(false));
        }

        [TestCase(" log.InfoFormat(\"Заявки загружены для organizationId={0} productId={1}\", organizationId, productId);", " log.InfoFormat(\"Заявки загружены для organizationId={OrganizationId} productId={ProductId}\", organizationId, productId);")]
        [TestCase("log.InfoFormat(\"Загружены сценарии оплаты productId={0} organizationId={1}\", productId, organizationId);", "log.InfoFormat(\"Загружены сценарии оплаты productId={ProductId} organizationId={OrganizationId}\", productId, organizationId);")]
        [TestCase("logger.InfoFormat(\"Packet total build time: {0} ms\", timer.ElapsedMilliseconds);", "logger.InfoFormat(\"Packet total build time: {ElapsedMilliseconds} ms\", timer.ElapsedMilliseconds);")]
        public void Replace(string input, string expected)
        {
            var result = processor.Replace(input);
            
            Assert.That(result.Replaced, Is.EqualTo(true));
            Assert.That(result.NewLine, Is.EqualTo(expected));
        }
    }
}