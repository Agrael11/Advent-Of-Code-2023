namespace Testing_2023
{
    [TestClass]
    public class UnitTest9
    {
        private readonly string example1;

        public UnitTest9()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day09/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day09.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 114, $"Incorrect result! Expected:114, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day09.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 2, $"Incorrect result! Expected:2 Got:{result}");
        }
    }
}