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

            sort = new MergeSort();
            int[] MergeArr = sort.Sort(arr, arr.Length);


            System.Console.Write("원본");
            foreach (var item in arr)
            {
                System.Console.Write($"{item}, ");
            }
            System.Console.WriteLine();

            System.Console.Write("버블 정렬");
            foreach (var item in bubbleSortArr)
            {
                System.Console.Write($"{item}, ");
            }
            System.Console.WriteLine();

            System.Console.Write("선택 정렬");
            foreach (var item in seletionsortArr)
            {
                System.Console.Write($"{item}, ");
            }
            System.Console.WriteLine();

            System.Console.Write("삽입 정렬");
            foreach (var item in insertionArr)
            {
                System.Console.Write($"{item}, ");
            }
            System.Console.WriteLine();

            System.Console.Write("합병 정렬");
            foreach (var item in MergeArr)
            {
                System.Console.Write($"{item}, ");
            }
            System.Console.WriteLine();
        }
    }
}