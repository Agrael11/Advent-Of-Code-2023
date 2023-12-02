namespace AdventOfCode.Day02
{
    /// <summary>
    /// Represents single set of cubes
    /// </summary>
    class Set
    {
        public int Reds { get; private set; } = 0;
        public int Greens { get; private set; } = 0;
        public int Blues { get; private set; } = 0;

        /// <summary>
        /// Creates set from input string
        /// </summary>
        /// <param name="input"></param>
        public Set(string input)
        {
            //Split the string by commas int cube groups.
            string[] splitInput = input.Split(',');

            for (int i = 0; i < splitInput.Length; i++) //For each cube group
            {
                string[] colorInfo = splitInput[i].Trim().Split(' '); //split group by space
                int amount = int.Parse(colorInfo[0]); //Get amount of cubes
                switch (colorInfo[1]) //And assign it by color
                {
                    case "red": Reds = amount; break;
                    case "green": Greens = amount; break;
                    case "blue": Blues = amount; break;
                }
            }
        }

        public override string ToString() //Just useful for debugging :)
        {
            return $"{Reds} red, {Greens} green, {Blues} blue";
        }
    }
}