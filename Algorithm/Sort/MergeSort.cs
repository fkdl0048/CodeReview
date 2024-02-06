namespace Sort
{
    class MergeSort : ISort
    {
        public int[] Sort(int[] arr, int n)
        {
            int[] newArr = new int[arr.Length];
            arr.CopyTo(newArr, 0);

            MergeAndSort(newArr, 0, newArr.Length - 1);

            return newArr;
        }

        private void MergeAndSort(int[] a, int p, int r)
        {
            if (p < r)
            {
                int q = (p + r) / 2;
                MergeAndSort(a, p, q);
                MergeAndSort(a, q + 1, r);
                Merge(a, p, q, r);
            }
        }

        private void Merge(int[] data, int p, int q, int r)
        {
            int i = p;
            int j = q + 1;
            int k = p;

            int[] temp = new int[data.Length];
            while (i <= q && j <= r)
            {
                if (data[i] <= data[j])
                {
                    temp[k++] = data[i++];
                }
                else
                {
                    temp[k++] = data[j++];
                }
            }
            while (i <= q)
            {
                temp[k++] = data[i++];
            }
            while (j <= r)
            {
                temp[k++] = data[j++];
            }
            for (int o = p; o <= r; o++)
            {
                data[o] = temp[o];
            }
        }
    }
}