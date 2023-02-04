namespace Sort
{
    class BubbleSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] newArr = new int[arr.Length];
            arr.CopyTo(newArr, 0);

            for (int last = n - 1; last >= 1; last--)
            {
                for (int i = 0; i < last; i++)
                {
                    if (newArr[i] > newArr[i + 1])
                    {
                        Program.Swap(ref newArr[i], ref newArr[i + 1]);
                    }
                }
            }

            return newArr;
        }
    }
}