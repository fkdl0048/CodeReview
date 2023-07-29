public class Account
{
    public String Statment(Invoice invoice, List<Play> plays)
    {
        int totalAmount = 0;
        int volumeCredits = 0;
        string result = $"청구 내역 (고객명:{invoice.customer})\n";

        foreach (var perf in invoice.performances)
        {
            var play = plays.FirstOrDefault(p => p.name == perf.playID);
            if (play == null)
            {
                throw new Exception($"연극이름:{perf.playID}를 찾을 수 없습니다.");
            }
            int thisAmount = AmountFor(perf, play);

            // 포인트를 적립한다.
            volumeCredits += Math.Max(perf.audience - 30, 0);
            // 희극 관객 5명마다 추가 포인트를 제공한다.
            if ("comedy" == play.type) volumeCredits += (int) Math.Floor((decimal) perf.audience / 5);

            // 청구 내역을 출력한다.
            result += $"{play.name}: ${thisAmount / 100} ({perf.audience}석)\n";
            totalAmount += thisAmount;
        }

        result += $"총액: ${totalAmount / 100}\n";
        result += $"적립 포인트: {volumeCredits}점\n";

        return result;
    }

    private int AmountFor(Performance perf, Play play)
    {
        int thisAmount = 0;

        switch (play.type)
        {
            case "tragedy": // 비극
                thisAmount = 40000;
                if (perf.audience > 30)
                {
                    thisAmount += 1000 * (perf.audience - 30);
                }

                break;
            case "comedy":
                thisAmount = 30000;
                if (perf.audience > 20)
                {
                    thisAmount += 10000 + 500 * (perf.audience - 20);
                }

                thisAmount += 300 * perf.audience;
                break;
            default:
                throw new Exception($"알 수 없는 장르:{play.type}");
        }

        return thisAmount;
    }
}