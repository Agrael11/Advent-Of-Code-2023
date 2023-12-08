namespace Testing_2023
{
    [TestClass]
    public class UnitTest8
    {
        private readonly string example1;
        private readonly string example2;
        private readonly string example3;

        public UnitTest8()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day08/example01.txt");
            example2 = System.IO.File.ReadAllText("Examples/Day08/example02.txt");
            example3 = System.IO.File.ReadAllText("Examples/Day08/example03.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result1 = AdventOfCode.Day08.Challenge1.DoChallenge(example1);
            var result2 = AdventOfCode.Day08.Challenge1.DoChallenge(example2);
            Assert.IsTrue(result1 == 2 && result2 == 6, $"Incorrect result! Expected:2, Got:{result1} & Expected 6, Got:{result2}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day08.Challenge2.DoChallenge(example3);
            Assert.IsTrue(result == 6, $"Incorrect result! Expected:6 Got:{result}");
        }
    }
}