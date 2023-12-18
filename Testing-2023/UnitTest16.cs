namespace Testing_2023
{
    [TestClass]
    public class UnitTest16
    {
        private readonly string example1;

        public UnitTest16()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day16/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day16.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 46, $"Incorrect result! Expected:46, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day16.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 51, $"Incorrect result! Expected:51 Got:{result}");
        }
    }
}