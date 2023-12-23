namespace Testing_2023
{
    [TestClass]
    public class UnitTest23
    {
        private readonly string example1;

        public UnitTest23()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day23/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result1 = AdventOfCode.Day20.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result1 == 94, $"Incorrect result! Expected:94, Got:{result1}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result1 = AdventOfCode.Day20.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result1 == 154, $"Incorrect result! Expected:154, Got:{result1}");
        }
    }
}