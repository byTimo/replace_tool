using NUnit.Framework;
using ReplaceTool.LineProcessors;

namespace ReplaceTool.Tests
{
    [TestFixture]
    public class WithDollarLineProcessorTests
    {
        private readonly ILineProcessor processor = new WithDollarLineProcessor();
        
        [TestCase("  Logger.InfoFormat($\"DismissalPaymentTask deletion not needed for WorkerId={worker.Id}, EmploymentId={lastEmployment.Id}\");", "  Logger.InfoFormat(\"DismissalPaymentTask deletion not needed for WorkerId={WorkerId}, EmploymentId={EmploymentId}\", worker.Id, lastEmployment.Id);")]
        [TestCase("  log.Info($\"Начинаем отправку нотификаций для userId:{userId} organizationId:{organizationId} now:{DateTime.UtcNow} lastReaded:{lastReadedMessageTimeEntity?.Time} notificationEmail: {notification?.Email} notificationTelegram: {notification?.Telegram}\");","  log.InfoFormat(\"Начинаем отправку нотификаций для userId:{UserId} organizationId:{OrganizationId} now:{UtcNow} lastReaded:{Time} notificationEmail: {Email} notificationTelegram: {Telegram}\", userId, organizationId, DateTime.UtcNow, lastReadedMessageTimeEntity?.Time, notification?.Email, notification?.Telegram);")]
        [TestCase("   logger.Info($\"Пользователь {evrikaUser.Id} (портальный пользователь {evrikaUser.PortalUserId}) привязан к UA {keBinding.KEUserId}@{keBinding.KEAbonentId}\");","   logger.InfoFormat(\"Пользователь {EvrikaUserId} (портальный пользователь {PortalUserId}) привязан к UA {KEUserId}@{KEAbonentId}\", evrikaUser.Id, evrikaUser.PortalUserId, keBinding.KEUserId, keBinding.KEAbonentId);")]
        [TestCase("logger.Warn($\"Elastic разошёлся с Mongo. Товары, не найденные в Mongo: {string.Join(\", \", lostProducts)}\");","logger.WarnFormat(\"Elastic разошёлся с Mongo. Товары, не найденные в Mongo: {LostProducts}\", string.Join(\", \", lostProducts));")]
        public void Replace(string input, string expected)
        {
            var actual = processor.Replace(input);
            
            Assert.That(actual.NewLine, Is.EqualTo(expected));
        }
    }
}