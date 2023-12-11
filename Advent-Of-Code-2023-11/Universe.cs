using Helpers;

namespace AdventOfCode.Day11
{
    public class Universe(int price)
    {
        private readonly List<List<bool>> _universe = []; //This universe. easily expandable 2D array
        private readonly List<int> _rowPrices = []; //price - virtual size of each row
        private readonly List<int> _columnPrices = []; //price - virtual size of each column
        private readonly int _price = price; //prince (virtual size) of empty row or column

        public int Width
        {
            get
            {
                return _universe[0].Count;
            }
        }
        public int Height
        {
            get
            {
                return _universe.Count;
            }
        }

        /// <summary>
        /// Pushes new row on end of 2D array
        /// </summary>
        /// <param name="row"></param>
        public void PushRow(List<bool> row)
        {
            _universe.Add(row);
            _rowPrices.Add(1);
        }

        /// <summary>
        /// Checks if there's galaxy at x/y position. Doesn't use prices
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool GetAt(int x, int y)
        {
            return _universe[y][x];
        }

        /// <summary>
        /// Modifies X,y depending on prices of rows and columns before it.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Vector2l GetRealPosition(int x, int y)
        {
            int realX = 0;
            int realY = 0;
            for (int rx = 0; rx < x; rx++)
            {
                realX += _columnPrices[rx];
            }
            for (int ry = 0; ry < y; ry++)
            {
                realY += _rowPrices[ry];
            }
            return new(realX, realY);
        }

        /// <summary>
        /// Checks if row contains a galaxy
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool RowContainsGalaxy(int row)
        {
            bool contains = false;

            for (int i = 0; i < _universe[row].Count; i++)
            {
                contains |= _universe[row][i];
            }

            return contains;
        }

        /// <summary>
        /// Checks if column contains a galaxy
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public bool ColumnContainsGalaxy(int column)
        {
            bool contains = false;

            for (int i = 0; i < _universe.Count; i++)
            {
                contains |= _universe[i][column];
            }

            return contains;
        }

        /// <summary>
        /// Extends universe
        /// Every row/column that doesn't contain any galaxy has it's price set appropriately
        /// </summary>
        public void Extend()
        {
            //Just create _columnPrices if they don't exist
            if (_columnPrices.Count == 0)
            {
                for (int i = 0; i < _universe[0].Count; i++)
                {
                    _columnPrices.Add(1);
                }
            }

            for (int i = 0; i < _universe.Count; i++)
            {
                if (!RowContainsGalaxy(i))
                {
                    _rowPrices[i] = _price;
                    i++;
                }
            }
            for (int i = 0; i < _universe[0].Count; i++)
            {
                if (!ColumnContainsGalaxy(i))
                {
                    _columnPrices[i] = _price;
                    i++;
                }
            }
        }
    }
}