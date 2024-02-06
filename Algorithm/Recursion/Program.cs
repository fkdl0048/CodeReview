namespace Recursion
{
    class Program
    {
        static void Main(string[] args)
        {
            // 길찾기
            // Maze.PrintMaze(); 
            // Maze.findMazePath(0, 0);
            // Maze.PrintMaze();
            
            // ---------------

            // 연결된 픽셀 찾기
            // CCIAB cCIAB = new CCIAB(20);
            // cCIAB.PrintCells();
            // System.Console.WriteLine(cCIAB.CountCells(0, 0));
            // cCIAB.PrintCells();
            // System.Console.ReadLine();

            // n-queen문제
            // NQueen nQueen = new NQueen(8);
            // nQueen.queens(0);

            // 멱집합
            Powerset powerset = new Powerset();
            powerset.PrintPowerSet(0);

        }
    }
}