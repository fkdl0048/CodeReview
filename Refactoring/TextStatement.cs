public class TextStatement : IStatement
{
    private readonly Invoice invoice;
    private readonly List<Play> plays;

    public TextStatement(Invoice invoice, List<Play> plays)
    {
        this.invoice = invoice;
        this.plays = plays;
    }
    
    public string Statement()
    {
        return RenderPlainText(new StatementData(invoice, plays));
    }

    private string RenderPlainText(StatementData data)
    {
        string result = $"청구 내역 (고객명:{data.customer})\n";

        foreach (var perf in data.performances)
        {
            result += $" {perf.play.name}: {Usd(perf.amount)} ({perf.audience}석)\n";
        }
        
        result += $"총액: {Usd(data.totalAmount)}\n";
        result += $"적립 포인트: {data.totalVolumeCredits}점\n";

        return result;
    }

    private string Usd(int aNumber)
    {
        return string.Format("${0:#,##0.00}", aNumber / 100);
    }
}