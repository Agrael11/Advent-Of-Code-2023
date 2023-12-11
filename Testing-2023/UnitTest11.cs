namespace Testing_2023
{
    [TestClass]
    public class UnitTest11
    {
        private readonly string example1;

        public UnitTest11()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day11/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day11.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result == 374, $"Incorrect result! Expected:374, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day11.Challenge2.DoChallenge(example1);
            Assert.IsTrue(result == 82000210, $"Incorrect result! Expected:82000210 Got:{result}");
        }
    }
}