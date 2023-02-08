namespace BuilderPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            TextOptions textOptions = GetDefaultTextOptions();
            
            System.Console.WriteLine(textOptions.GetFont);
            System.Console.WriteLine(textOptions.GetFontSize);
        }

        static TextOptions GetDefaultTextOptions()
        {
            return new TextOptionsBuilder("ARIAL")
            .SetFontSize(12.0f)
            .bulid();
        }
    }
}