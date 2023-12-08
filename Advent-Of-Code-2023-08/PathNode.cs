namespace AdventOfCode.Day08
{
    /// <summary>
    /// Just small node helper class
    /// </summary>
    /// <param name="leftConnection"></param>
    /// <param name="rightConnection"></param>
    public struct PathNode(string leftConnection, string rightConnection)
    {
        public string LeftConnection = leftConnection;
        public string RightConnection = rightConnection;
    }
}