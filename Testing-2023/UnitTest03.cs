namespace Testing_2023
{
    [TestClass]
    public class UnitTest03
    {
        private readonly string example1;

        public UnitTest03()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day03/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day03.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 4361, $"Incorrect result! Expected:4361, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day03.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 467835, $"Incorrect result! Expected:467835, Got:{result}");
        }
    }
}