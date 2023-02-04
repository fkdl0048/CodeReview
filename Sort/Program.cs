using System;

namespace Sort
{

    class Program
    {
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        static void Main(string[] args)
        {
            int[] arr = new int[]{1, 3, 2, 4,3, 4, 6, 12, 5, 199, 15};

            ISort sort = new SelectionSort();
            int[] seletionsortArr = sort.Sort(arr, arr.Length);

            sort = new BubbleSort();
            int[] bubbleSortArr = sort.Sort(arr, arr.Length);

            sort = new InsertionSort();
            int[] insertionArr = sort.Sort(arr, arr.Length);


            foreach (var item in insertionArr)
            {
                System.Console.WriteLine(item);
            }
        }
    }
}