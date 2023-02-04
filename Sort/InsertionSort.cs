namespace Sort
{
    class InsertionSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] p = arr;

            for (int i = 1; i < n; i++)
            {
                int key = p[i];

                int j;
                for (j = i - 1; j >= 0; j--)
                {
                    if(key < p[j])
                    {
                        p[j + 1] = p[j];
                    }
                    else
                    {
                        break;
                    }
                }
                p[j + 1] = key;
            }

            return p;
        }
    }
}