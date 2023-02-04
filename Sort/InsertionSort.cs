namespace Sort
{
    class InsertionSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] newArr = new int[arr.Length];
            arr.CopyTo(newArr, 0);

            for (int i = 1; i < n; i++)
            {
                int key = newArr[i];

                int j;
                for (j = i - 1; j >= 0; j--)
                {
                    if(key < newArr[j])
                    {
                        newArr[j + 1] = newArr[j];
                    }
                    else
                    {
                        break;
                    }
                }
                newArr[j + 1] = key;
            }

            return newArr;
        }
    }
}