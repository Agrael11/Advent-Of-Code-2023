namespace Testing_2023
{
    [TestClass]
    public class UnitTest18
    {
        private readonly string example1;

        public UnitTest18()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day18/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day18.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 62, $"Incorrect result! Expected:62, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day18.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 952408144115, $"Incorrect result! Expected:952408144115 Got:{result}");
        }
    }
}