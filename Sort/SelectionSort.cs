namespace Sort
{
    class SelectionSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] p = arr;
            for (int last = n - 1; last >= 1; last--)
            {
                int maxIndex = last;
                for (int k = 0; k < last; k++)
                {
                    if (p[k] > p[maxIndex])
                    {
                        maxIndex = k;
                    }
                }

                Program.Swap(ref p[last], ref p[maxIndex]);
            }

            return p;
        }
    }
}