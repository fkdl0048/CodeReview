namespace Chapter04;

public static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1(SampleProvinceData()));
    }    

    public static Province SampleProvinceData()
    {
        Province asia = new Province("Asia", 30, 20);
        asia.AddProducer(new Producer(asia, "Byzantium", 10, 9));
        asia.AddProducer(new Producer(asia, "Attalia", 12, 10));
        asia.AddProducer(new Producer(asia, "Sinope", 10, 6));
        return asia;
    }
}