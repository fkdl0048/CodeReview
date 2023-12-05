public class Program
{
    public static void Main()
    {
        Mammal cat = new Mammal(new CatNoise());

        cat.MakeNoise();
    }
}