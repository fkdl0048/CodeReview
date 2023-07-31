public class EnrichPerformance
{
    private List<Play> plays;
    public Play play;
    public int amount;
    public int volumeCredits;
    public int audience;

    public EnrichPerformance(Performance aPerformace, List<Play> plays)
    {
        this.plays = plays;
        this.play = PlayFor(aPerformace);
        this.amount = AmountFor(aPerformace);
        this.volumeCredits = VolumeCreditsFor(aPerformace);

        audience = aPerformace.audience;
    }

    private Play PlayFor(Performance aPerformance)
    {
        return plays.FirstOrDefault(p => p.name == aPerformance.playID) ?? throw new Exception ($"연극이름:{aPerformance.playID}를 찾을 수 없습니다.");
    }

    private int AmountFor(Performance aPerformance)
    {
        int result = 0;
        switch (play.type)
        {
            case "tragedy":
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
                throw new Exception($"알 수 없는 장르:{play.type}");
        }
        return result;
    }

    private int VolumeCreditsFor(Performance aPerformance)
    {
        int result = 0;
        result += Math.Max(aPerformance.audience - 30, 0);
        if ("comedy" == PlayFor(aPerformance).type) result += (int) Math.Floor((decimal) aPerformance.audience / 5);

        return result;
    }

}