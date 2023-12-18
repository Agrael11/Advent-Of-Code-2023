namespace Testing_2023
{
    [TestClass]
    public class UnitTest17
    {
        private readonly string example1;

        public UnitTest17()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day17/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day17.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 102, $"Incorrect result! Expected:102, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day17.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 94, $"Incorrect result! Expected:94 Got:{result}");
        }
    }
}