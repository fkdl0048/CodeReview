public class Province
{
    private string name;
    private List<Producer> producers = new List<Producer>();
    private int totalProduction;
    private int demand;
    private int price;

    public Province(string name, int demand, int price)
    {
        this.name = name;
        this.demand = demand;
        this.price = price;
        this.totalProduction = 0;
    }

    public void AddProducer(Producer arg)
    {
        producers.Add(arg);
        totalProduction += arg.GetProduction();
    }

    public string GetName()
    {
        return name;
    }

    public int GetTotalProduction()
    {
        return totalProduction;
    }

    public void SetTotalProduction(int arg)
    {
        totalProduction = arg;
    }

    public int GetDemand()
    {
        return demand;
    }

    public void SetDemand(int arg)
    {
        demand = arg;
    }

    public int GetPrice()
    {
        return price;
    }

    public void SetPrice(int arg)
    {
        price = arg;
    }

    public List<Producer> GetProducers()
    {
        return producers;
    }

    public int Shortfall()
    {
        return demand - totalProduction;
    }

    public int Profit()
    {
        return DemandValue() - DemandCost();
    }

    public int DemandValue()
    {
        return satisfiedDemand() * price;
    }

    public int DemandCost()
    {
        int remainingDemand = demand;
        int result = 0;
        producers.Sort((a, b) => a.GetCost() - b.GetCost());
        foreach (Producer p in producers)
        {
            int contribution = Math.Min(remainingDemand, p.GetProduction());
            remainingDemand -= contribution;
            result += contribution * p.GetCost();
        }
        return result;
    }

    public int satisfiedDemand()
    {
        return Math.Min(demand, totalProduction);
    }
}