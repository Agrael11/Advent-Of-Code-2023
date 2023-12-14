namespace Testing_2023
{
    [TestClass]
    public class UnitTest14
    {
        private readonly string example1;

        public UnitTest14()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day14/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day14.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 136, $"Incorrect result! Expected:136, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day14.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 64, $"Incorrect result! Expected:64 Got:{result}");
        }
    }
}