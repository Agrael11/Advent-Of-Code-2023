namespace AdventOfCode.Day09
{
    public class Sequence
    {
        //Numbers in sequence.
        private readonly List<int> _numbers = [];

        public Sequence? _patternCache = null;
        public Sequence? Pattern => GeneratePattern();

        public Sequence()
        {
        }

        //Retrieve first or last element in sequence
        public int? GetFirst()
        {
            if (_numbers.Count == 0) return null;
            return _numbers[0];
        }

        public int? GetLast()
        {
            if (_numbers.Count == 0) return null;
            return _numbers[^1];
        }

        //Adds number at start or end of sequence
        public void AddNumberAtStart(int number)
        {
            _numbers.Insert(0, number);
            _patternCache = null;
        }

        public void AddNumberAtEnd(int number)
        {
            _numbers.Add(number);
            _patternCache = null;
        }

        //Get length of sequence
        public int Length()
        {
            return _numbers.Count;
        }

        //Checks if all numbers in sequence are zeroes
        public bool AllZeroes()
        {
            return _numbers.Where((number) => (number == 0)).Count() == _numbers.Count;
        }

        //Generate Pattern
        private Sequence? GeneratePattern()
        {
            if (_patternCache is null && _numbers.Count > 0)
            {
                _patternCache = new Sequence();

                if (_numbers.Count == 1)
                {
                    _patternCache.AddNumberAtEnd(_numbers[0]);
                    return _patternCache;
                }

                for (int i = 0; i < _numbers.Count - 1; i++)
                {
                    _patternCache.AddNumberAtEnd(_numbers[i + 1] - _numbers[i]);
                }
            }

            return _patternCache;
        }

        //Adds previous value (the one before first) by pattern
        public void AddPreviousByPattern()
        {
            if (Pattern is null || Pattern.Length() == 0) //If pattern is not possible to generate properly
                return; //we exit without doing anything

            if (!Pattern.AllZeroes()) //If there is non-zero number in pattern
            {
                Pattern.AddPreviousByPattern(); //We get the "previous" number in pattern
            }

            int? firstPattern = Pattern.GetFirst(); //We get the first number in pattern
            int? firstNumber = GetFirst(); //An our first number

            if (firstPattern is null || firstNumber is null) return; //If one of them is null, there is something horribly wrong and we do nothing

            AddNumberAtStart(firstNumber.Value - firstPattern.Value); //We generate previous number by pattern and add it at start.

            return;
        }

        //Adds next value by pattern
        public void AddNextByPattern()
        {
            //Same as previous function, but instead of doing stuff with previous/first number, we do it with last.

            if (Pattern is null)
                return;

            if (!Pattern.AllZeroes())
            {
                Pattern.AddNextByPattern();
            }

            int? lastPattern = Pattern.GetLast();
            int? lastNumbers = GetLast();

            if (lastPattern is null || lastNumbers is null) return;

            AddNumberAtEnd((int)(lastNumbers + lastPattern));

            return;
        }
    }
}