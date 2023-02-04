namespace Sort
{
    class BubbleSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] p = arr;
            for (int last = n - 1; last >= 1; last--)
            {
                for (int i = 0; i < last; i++)
                {
                    if (p[i] > p[i + 1])
                    {
                        Program.Swap(ref p[i], ref p[i + 1]);
                    }
                }
            }

            return p;
        }
    }
}