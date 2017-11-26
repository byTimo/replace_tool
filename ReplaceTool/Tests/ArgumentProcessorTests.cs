using NUnit.Framework;

namespace ReplaceTool.Tests
{
    [TestFixture]
    public class ArgumentProcessorTests
    {
        private static readonly IArgumentProcessor argumentProcessor = new ArgumentProcessor();
        
        [TestCase("evrika", "Evrika")]
        [TestCase("evrika.Property", "Property")]
        [TestCase("evrika.ToString()", "Evrika")]
        [TestCase("evrika.Property1.Property2", "Property2")]
        [TestCase("builtPermissions.Select(x => x.ToString())", "BuiltPermissions")]
        [TestCase("string.Join(\", \", builtPermissions.Select(x => x.ToString()))", "BuiltPermissions")]
        public void Get(string input, string expected)
        {
            var actual = argumentProcessor.Get(input);
            
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}