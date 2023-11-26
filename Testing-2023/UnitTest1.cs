namespace Testing_2023
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string example;

        public UnitTest1()
        {
            example = System.IO.File.ReadAllText("Examples/Day01/example01.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day01.Challange1.DoChallange(example);
            Assert.IsTrue(result == 24000, $"Incorrect result! Expected:24000, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day01.Challange2.DoChallange(example);
            Assert.IsTrue(result == 45000, $"Incorrect result! Expected:45000, Got:{result}");
        }
    }
}