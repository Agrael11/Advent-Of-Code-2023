namespace AdventOfCode.Day14
{
    /// <summary>
    /// Keeps map for easy access. Just reference to variable
    /// But it creates "Hash" for easy comparison so I can check if state existed before
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public class MirrorMap(int width, int height)
    {
        public int Width { get; private set; } = width;
        public int Height { get; private set; } = height;
        private BlockState[] _map = new BlockState[width * height];
        public int HashCache { get; set; } = -1;

        public BlockState this[int index]
        {
            get
            {
                return _map[index];
            }
            set
            {
                HashCache = -1;
                _map[index] = value;
            }
        }

        public void Reset(int width, int height)
        {
            HashCache = -1;
            Width = width;
            Height = height;
            _map = new BlockState[width * height];
        }

        /// <summary>
        /// Generates "Hash" of this. It's also caching the hash so it's faster
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (HashCache != -1) return HashCache;

            int hash = 0;
            for (int i = 0; i < _map.Length; i++)
            {
                hash += ((int)_map[i])*(i%Width)*(i/Width);
            }
            HashCache = hash;

            return hash;
        }
    }
}