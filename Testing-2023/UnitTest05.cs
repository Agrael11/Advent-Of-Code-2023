namespace Testing_2023
{
    [TestClass]
    public class UnitTest05
    {
        private readonly string example1;

        public UnitTest05()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day05/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day05.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 35, $"Incorrect result! Expected:35, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day05.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 46, $"Incorrect result! Expected:46 Got:{result}");
        }
    }
}