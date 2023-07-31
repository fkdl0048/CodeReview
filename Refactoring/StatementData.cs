public class StatementData
{
    public readonly string customer;
    public readonly List<EnrichPerformance> performances;
    public readonly int totalAmount;
    public readonly int totalVolumeCredits;

    public StatementData(Invoice invoice, List<Play> plays)
    {
        this.customer = invoice.customer;
        this.performances = new List <EnrichPerformance>();
        foreach (var perf in invoice.performances)
        {
            this.performances.Add(new EnrichPerformance(perf, plays));
        }
        this.totalAmount = TotalAmount();
        this.totalVolumeCredits = TotalVolumeCredits();
    }

    private int TotalAmount()
    {
        int result = 0;
        foreach (var perf in performances)
        {
            result += perf.amount;
        }
        return result;
    }

    private int TotalVolumeCredits()
    {
        int result = 0;
        foreach (var perf in performances)
        {
            result += perf.volumeCredits;
        }
        return result;
    }
}