namespace Testing_2023
{
    [TestClass]
    public class UnitTest13
    {
        private readonly string example1;

        public UnitTest13()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day13/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day13.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 405, $"Incorrect result! Expected:405, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day13.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 400, $"Incorrect result! Expected:400 Got:{result}");
        }
    }
}