namespace Chapter04;

public partial class Form1 : Form
{
    private Province province;

    public Form1(Province province)
    {
        InitializeData(province);
        InitializeComponent();
    }

    private void InitializeData(Province province)
    {
        this.province = province;
    }
}
