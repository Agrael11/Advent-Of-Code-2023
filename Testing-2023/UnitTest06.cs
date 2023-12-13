namespace Testing_2023
{
    [TestClass]
    public class UnitTest06
    {
        private readonly string example1;

        public UnitTest06()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day06/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day06.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 288, $"Incorrect result! Expected:288, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day06.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 71503, $"Incorrect result! Expected:71503 Got:{result}");
        }
    }
}