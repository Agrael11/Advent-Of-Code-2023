namespace Testing_2023
{
    [TestClass]
    public class UnitTest02
    {
        private readonly string example1;

        public UnitTest02()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day02/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day02.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 8, $"Incorrect result! Expected:8, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day02.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 2286, $"Incorrect result! Expected:2286, Got:{result}");
        }
    }
}