namespace Sort
{
    class QuickSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] newArr = new int[arr.Length];
            arr.CopyTo(newArr, 0);

            QuickSortLogic(newArr, 0, newArr.Length - 1);

            return newArr;
        }

        private void QuickSortLogic(int[] a, int p, int r)
        {
            if (p < r)
            {
                int q = Partition(a, p, r);
                QuickSortLogic(a, p, q - 1);
                QuickSortLogic(a, q + 1, r);
            }
        }

        private int Partition(int[] a, int p, int r)
        {
            // pivot
            int x = a[r];

            int i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (a[j] <= x)
                {
                    i++;
                    Program.Swap(ref a[i], ref a[j]);
                }
            }
            Program.Swap(ref a[i + 1], ref a[r]);
            
            return i + 1;
        }
    }
}