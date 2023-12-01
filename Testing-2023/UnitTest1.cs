namespace Testing_2023
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string example1;
        private readonly string example2;

        public UnitTest1()
        {
            example1 = System.IO.File.ReadAllText("Examples/Day01/example01.txt");
            example2 = System.IO.File.ReadAllText("Examples/Day01/example02.txt");
        }

        [TestMethod]
        public void TestPart1()
        {
            var result = AdventOfCode.Day01.Challange1.DoChallange(example1);
            Assert.IsTrue(result == 142, $"Incorrect result! Expected:142, Got:{result}");
        }

        [TestMethod]
        public void TestPart2()
        {
            var result = AdventOfCode.Day01.Challange2.DoChallange(example2);
            Assert.IsTrue(result == 281, $"Incorrect result! Expected:281, Got:{result}");
        }
    }
}