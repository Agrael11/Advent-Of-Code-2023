namespace Testing_2023
{
    [TestClass]
    public class UnitTest21
    {
        private readonly string example1;

        public UnitTest21()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day21/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result1 = AdventOfCode.Day21.Challenge1.DoChallenge(example1);
            Assert.IsTrue(result1 == 42,  $"Incorrect result! Expected:42, Got:{result1}");
        }
    }
}