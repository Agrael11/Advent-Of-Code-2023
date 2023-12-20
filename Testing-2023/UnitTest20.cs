namespace Testing_2023
{
    [TestClass]
    public class UnitTest20
    {
        private readonly string example1;
        private readonly string example2;

        public UnitTest20()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day20/example01.txt");
            example2 = System.IO.File.ReadAllText("Examples/Day20/example02.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result1 = AdventOfCode.Day20.Challenge1.DoChallenge(example1);
            var result2 = AdventOfCode.Day20.Challenge1.DoChallenge(example2);
            Assert.IsTrue(result1 == 32000000 && result2 == 11687500,  $"Incorrect result! Expected:32000000, Got:{result1} & Expected:11687500, Got:{result2}");
        }
    }
}