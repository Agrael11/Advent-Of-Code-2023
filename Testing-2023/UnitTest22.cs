namespace Testing_2023
{
    [TestClass]
    public class UnitTest22
    {
        private readonly string example1;

        public UnitTest22()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day22/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result1 = AdventOfCode.Day22.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result1 == 5, $"Incorrect result! Expected:5, Got:{result1}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result1 = AdventOfCode.Day22.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result1 == 7, $"Incorrect result! Expected:7, Got:{result1}");
        }
    }
}