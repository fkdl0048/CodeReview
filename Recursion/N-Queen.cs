namespace Recursion
{
    public class NQueen
    {
        int[] cols;
        int _size;
        public NQueen(int size)
        {
            _size = size;
            cols = new int[_size + 1];
        }

        public bool queens(int level)
        {
            if (!Promising(level))
                return false;
            else if (level == _size)
            {
                for (int i = 1; i < _size; i++)
                {
                    System.Console.WriteLine($"{i}, {cols[i]} ");
                }
                return true;
            }
            for (int i = 1; i <= _size; i++)
            {
                cols[level + 1] = i;
                if (queens(level + 1))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Promising(int level)
        {
            for (int i = 1; i < level; i++)
            {
                if (cols[i] == cols[level])
                {
                    return false;
                }
                else if (level - i == Math.Abs(cols[level] - cols[i]))
                {
                    return false;
                }
            }

            return true;        
        }
    }
}