public class Account
{
    private List<Play> plays;
    private Invoice invoice;

    public Account(List<Play> plays, Invoice invoice)
    {
        this.plays = plays;
        this.invoice = invoice;
    }
    public String Statment()
    {
        Invoice statementData = new Invoice(invoice.customer , invoice.performances);
        return renderPlainText(statementData);
    }

    private string renderPlainText(Invoice statementData)
    {
        string result = $"청구 내역 (고객명:{statementData.customer})\n";

        foreach (var perf in statementData.performances)
        {
            // 청구 내역을 출력한다.
            result += $"{Playfor(perf).name}: ${USD(AmountFor(perf) / 100)} ({perf.audience}석)\n";
        }

        result += $"총액: ${USD(TotalAmount() / 100)}\n";
        result += $"적립 포인트: {totalVolumeCredits()}점\n";

        return result;
    }

    private int AmountFor(Performance aPerformance)
    {
        int result = 0;

        switch (Playfor(aPerformance).type)
        {
            case "tragedy": // 비극
                result = 40000;
                if (aPerformance.audience > 30)
                {
                    result += 1000 * (aPerformance.audience - 30);
                }

                break;
            case "comedy":
                result = 30000;
                if (aPerformance.audience > 20)
                {
                    result += 10000 + 500 * (aPerformance.audience - 20);
                }

                result += 300 * aPerformance.audience;
                break;
            default:
                throw new Exception($"알 수 없는 장르:{Playfor(aPerformance).type}");
        }

        return result;
    }

    private Play Playfor(Performance aPerformance)
    {
        // 메서드 내부 메서드 불가능하기 때문에 클래스 멤버로 처리
        return plays.FirstOrDefault(p => p.name == aPerformance.playID) ?? throw new Exception($"연극이름:{aPerformance.playID}를 찾을 수 없습니다.");
    }

    private int VolumeCreditsFor(Performance aPerformance)
    {
        int result = 0;
        result += Math.Max(aPerformance.audience - 30, 0);
        if ("comedy" == Playfor(aPerformance).type) result += (int) Math.Floor((decimal) aPerformance.audience / 5);

        return result;
    }

    private string USD(int aNumber)
    {
        return aNumber.ToString();
    }

    private int totalVolumeCredits()
    {
        int totalAmount = 0;

        foreach (var perf in invoice.performances)
        {
            totalAmount += VolumeCreditsFor(perf);
        }

        return totalAmount;
    }

    private int TotalAmount()
    {
        int result = 0;

        foreach (var perf in invoice.performances)
        {
            result += AmountFor(perf);
        }

        return result;
    }
}