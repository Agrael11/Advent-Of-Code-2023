namespace Testing_2023
{
    [TestClass]
    public class UnitTest19
    {
        private readonly string example1;

        public UnitTest19()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day19/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day19.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 19114,  $"Incorrect result! Expected:19114, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day19.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 167409079868000, $"Incorrect result! Expected:167409079868000 Got:{result}");
        }
    }
}