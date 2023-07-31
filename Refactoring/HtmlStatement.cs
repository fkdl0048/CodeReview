public class HtmlStatement : IStatement
{
    private readonly Invoice invoice;
    private readonly List<Play> plays;
    public HtmlStatement(Invoice invoice, List<Play> plays)
    {
        this.invoice = invoice;
        this.plays = plays;
    }
    public string Statement()
    {
        return RenderHtml(new StatementData(invoice, plays));
    }

    private string RenderHtml(StatementData data)
    {
        string result = $"<h1>청구 내역 (고객명:{data.customer})</h1>\n";

        result += "<table>\n";
        result += "<tr><th>연극</th><th>좌석 수</th><th>금액</th></tr>";
        foreach (var perf in data.performances)
        {
            result += $"<tr><td>{perf.play.name}</td><td>{perf.audience}</td>";
            result += $"<td>{Usd(perf.amount)}</td></tr>\n";
        }
        result += "</table>\n";

        result += $"<p>총액: <em>{Usd(data.totalAmount)}</em></p>\n";
        result += $"<p>적립 포인트: <em>{data.totalVolumeCredits}</em>점</p>\n";

        return result;
    }

    private string Usd(int aNumber)
    {
        return string.Format("${0:#,##0.00}", aNumber / 100);
    }
}