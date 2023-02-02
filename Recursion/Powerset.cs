namespace Recursion
{
    public class Powerset
    {
        private char[] data;
        private bool[] include;
        private int n;
        public Powerset()
        {
            data = new char[]{'a', 'b', 'c', 'd', 'e'};
            n = data.Length;
            include = new bool[n];
        }

        public void PrintPowerSet(int k)
        {
            if (k == n)
            {
                for (int i = 0; i < n; i++)
                {
                    if (include[i])
                    {
                        System.Console.Write(data[i] + " ");
                    }
                    
                }
                System.Console.WriteLine();
                return;
            }

            include[k] = false;
            PrintPowerSet(k + 1);
            include[k] = true;
            PrintPowerSet(k + 1);
        }
    }
}