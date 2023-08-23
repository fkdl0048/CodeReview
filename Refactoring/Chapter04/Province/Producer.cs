public class Producer
{
    private Province province;
    private string name;
    private int cost;
    private int production;

    public Producer(Province province, string name, int cost, int production)
    {
        this.province = province;
        this.name = name;
        this.cost = cost;
        this.production = production;
    }

    public string GetName()
    {
        return name;
    }

    public int GetCost()
    {
        return cost;
    }

    public void SetCost(int arg)
    {
        cost = arg;
    }

    public int GetProduction()
    {
        return production;
    }

    public void SetProduction(int arg)
    {
        production = arg;
        // 수정
    }
}