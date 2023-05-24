class Program
{
    delegate bool delegateCheck(int n);

    static void Main(string[] args)
    {
        List<int> numbers = Enumerable.Range(1, 200).ToList();

        var oddNumbers = numbers.Find(n => n % 2 == 1);

        var test = numbers.TrueForAll(n => n < 50);

        numbers.RemoveAll(n => n % 2 == 0);

        numbers.ForEach(item => Console.WriteLine(item));
    }
}