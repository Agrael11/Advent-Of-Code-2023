namespace Testing_2023
{
    [TestClass]
    public class UnitTest12
    {
        private readonly string example1;

        public UnitTest12()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day12/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day12.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 21, $"Incorrect result! Expected:21, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day12.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 525152, $"Incorrect result! Expected:525152 Got:{result}");
        }
    }
}