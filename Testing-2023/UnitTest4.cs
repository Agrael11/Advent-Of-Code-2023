namespace Testing_2023
{
    [TestClass]
    public class UnitTest4
    {
        private readonly string example1;

        public UnitTest4()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day04/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day04.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 13, $"Incorrect result! Expected:13, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day04.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 30, $"Incorrect result! Expected:30, Got:{result}");
        }
    }
}