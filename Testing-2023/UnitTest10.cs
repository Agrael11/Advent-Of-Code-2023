namespace Testing_2023
{
    [TestClass]
    public class UnitTest10
    {
        private readonly string example1;
        private readonly string example2;

        public UnitTest10()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day10/example01.txt");
            example2 = System.IO.File.ReadAllText("Examples/Day10/example02.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day10.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 8, $"Incorrect result! Expected:8, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day10.Challenge2.DoChallenge(example2);
            Assert.IsTrue(result == 10, $"Incorrect result! Expected:10 Got:{result}");
        }
    }
}