namespace Sort
{
    class SelectionSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] newArr = new int[arr.Length];
            arr.CopyTo(newArr, 0);
            
            for (int last = n - 1; last >= 1; last--)
            {
                int maxIndex = last;
                for (int k = 0; k < last; k++)
                {
                    if (newArr[k] > newArr[maxIndex])
                    {
                        maxIndex = k;
                    }
                }

                Program.Swap(ref newArr[last], ref newArr[maxIndex]);
            }

            return newArr;
        }
    }
}