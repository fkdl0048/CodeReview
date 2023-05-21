using ExtensionMethods;

namespace Programs
{
    class Program
    {
        static void Main()
        {
            string s = "Hello Extension Methods";
            int i = MyExtensions.WordCount(s)
            System.Console.WriteLine(i);
        }
    }
}