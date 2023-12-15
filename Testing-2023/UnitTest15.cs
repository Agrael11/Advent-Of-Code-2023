namespace Testing_2023
{
    [TestClass]
    public class UnitTest15
    {
        private readonly string example1;

        public UnitTest15()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day15/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day15.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 1320, $"Incorrect result! Expected:1320, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day15.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 145, $"Incorrect result! Expected:145 Got:{result}");
        }
    }
}