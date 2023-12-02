namespace AdventOfCode.Day02
{

    /// <summary>
    /// Represents one game
    /// </summary>
    class Game
    {
        public int Id { get; private set; }
        public readonly List<Set> Sets = [];
        
        public Game(string input)
        {
            string[] inputSplit = input.Split(':'); //Split by colon - on left side is Game id on right sets
            Id = int.Parse(inputSplit[0].Split(' ')[1]); //Get game Id=x (Game x) - split by space and get only right side
            inputSplit = inputSplit[1].Split(';'); //Now split the sets by semicolon
            foreach (string setDefinition in inputSplit)
            {
                Sets.Add(new(setDefinition)); //And add each new set
            }
        }

        public override string ToString() //Just useful for debugging :)
        {
            return $"Game {Id}: {string.Join("; ", Sets)}";
        }
    }
}