namespace Chapter04;

public partial class Form1 : Form
{
    private Province province;

    public Form1()
    {
        InitializeData();
        InitializeComponent();
    }

    private void InitializeData()
    {
        province = new Province("Asia", 30, 20);
        province.AddProducer(new Producer(province, "Byzantium", 10, 9));
        province.AddProducer(new Producer(province, "Attalia", 12, 10));
        province.AddProducer(new Producer(province, "Sinope", 10, 6));
    }
}
