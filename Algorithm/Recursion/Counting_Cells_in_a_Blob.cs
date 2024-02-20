namespace Recursion
{

    public class CCIAB
    {
        public const char CIRCLE = '\u25cf';
        private int _size;
        private int[,] cells;

        Random random = new Random();
        enum TileType
        {
            BACKGROUD,
            IMAGE,
            ALREADY
        }

        public CCIAB(int size)
        {
            _size = size;

            cells = new int[size, size];

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    cells[y, x] = random.Next(0, 2);
                }
            }
        }

        public void PrintCells()
        {
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    switch (cells[y, x])
                    {
                        case (int)TileType.BACKGROUD:
                            System.Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case (int)TileType.IMAGE:
                            System.Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case (int)TileType.ALREADY:
                            System.Console.ForegroundColor = ConsoleColor.Red;
                            break;
                    }
                    System.Console.Write(CIRCLE);
                }
                System.Console.WriteLine();
            }
        }
    
        public int CountCells(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _size || y >= _size)
            {
                return 0;
            }
            else if (cells[y, x] != (int)TileType.IMAGE)
            {
                return 0;
            }
            else
            {
                cells[y, x] = (int)TileType.ALREADY;
                return 1 + CountCells(x, y - 1) + CountCells(x + 1, y - 1)
                    + CountCells(x + 1, y) + CountCells(x + 1, y + 1)
                    + CountCells(x, y + 1) + CountCells(x - 1, y + 1)
                    + CountCells(x - 1, y) + CountCells(x - 1, y - 1);
            }
        }
    }
}