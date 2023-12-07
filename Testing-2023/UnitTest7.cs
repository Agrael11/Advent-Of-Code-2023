namespace Testing_2023
{
    [TestClass]
    public class UnitTest7
    {
        private readonly string example1;

        public UnitTest7()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day07/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day07.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 6440, $"Incorrect result! Expected:6440, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day07.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 5905, $"Incorrect result! Expected:5905 Got:{result}");
        }
    }
}