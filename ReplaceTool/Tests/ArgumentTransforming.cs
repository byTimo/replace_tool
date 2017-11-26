using System.IO;
using System.Linq;
using NUnit.Framework;

namespace ReplaceTool.Tests
{
    [TestFixture]
    public class ArgumentTransforming
    {
        [Test, Ignore("Патамушта")]
        public void METHOD()
        {
            File.WriteAllLines("D:/args.txt", File.ReadAllLines("D:/lol.txt").GroupBy(x => x).Select(x => x.Key));
        }
    }
}