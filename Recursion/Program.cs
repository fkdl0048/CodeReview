namespace Recursion
{
    public class Maze
    {
        public static char CIRCLE = '\u25cf';
        public static int N = 8;
        public static int [,] maze = 
        {
            {0, 0, 0, 0, 0, 0, 0, 1},
            {0, 1, 1, 0, 1, 1, 0, 1},
            {0, 0, 0, 1, 0, 0, 0, 1},
            {0, 1, 0, 0, 1, 1, 0, 0},
            {0, 1, 1, 1, 0, 0, 1, 1},
            {0, 1, 0, 0, 0, 1, 0, 1},
            {0, 0, 0, 1, 0, 0, 0, 1},
            {0, 1, 1, 1, 0, 1, 0, 0}
        };

        enum TileType
        {
            PATHWAY_COLOR,
            WALL_COLOR,
            BLOCKED_COLOR,
            PATH_COLOR
        }

        public static void PrintMaze()
        {
            for (int y = 0; y < N; y++)
            {
                for (int x = 0; x < N; x++)
                {
                    switch (maze[y, x])
                    {
                        case (int)TileType.PATHWAY_COLOR:
                            System.Console.ForegroundColor = ConsoleColor.White;
                            break;
                        case (int)TileType.WALL_COLOR:
                            System.Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        case (int)TileType.BLOCKED_COLOR:
                            System.Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case (int)TileType.PATH_COLOR:
                            System.Console.ForegroundColor = ConsoleColor.Green;
                            break;
                    }

                    System.Console.Write(CIRCLE);
                }
                System.Console.WriteLine();
            }
        }

        public static Boolean findMazePath(int x, int y)
        {
            if (x < 0 || y < 0 || x >= N || y >= N)
            {
                return false;
            }
            else if (maze[y, x] != (int)TileType.PATHWAY_COLOR)
            {
                return false;
            }
            else if (x == N-1 && y == N-1)
            {
                maze[y, x] = (int)TileType.PATH_COLOR;
                return true;
            }
            else
            {
                maze[y, x] = (int)TileType.PATH_COLOR;
                if (findMazePath(x, y - 1) || findMazePath(x + 1, y) || findMazePath(x , y + 1) || findMazePath(x - 1, y))
                {
                    return true;
                }

                maze[y, x] = (int)TileType.BLOCKED_COLOR;

                return false;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Maze.PrintMaze(); 
            Maze.findMazePath(0, 0);
            Maze.PrintMaze();
            while(true)
            {
                
            }
        }
    }
}