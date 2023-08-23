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

        IPerformaceCalculator calculator = CreatePerformanceCalculator(aPerformace, PlayFor(aPerformace));

        this.play = calculator.GetPlay();
        this.amount = calculator.AmountFor();
        this.volumeCredits = calculator.VolumeCreditsFor();

        audience = aPerformace.audience;
    }

    private Play PlayFor(Performance aPerformance)
    {
        return plays.FirstOrDefault(p => p.name == aPerformance.playID) ?? throw new Exception ($"연극이름:{aPerformance.playID}를 찾을 수 없습니다.");
    }

    private IPerformaceCalculator CreatePerformanceCalculator(Performance aPerformance, Play aPlay)
    {
        switch (aPlay.type)
        {
            case "tragedy":
                return new TragedyCalculator(aPerformance, aPlay);
            case "comedy":
                return new ComedyCalculator(aPerformance, aPlay);
            default:
                throw new Exception($"알 수 없는 장르:{aPlay.type}");
        }
    }
}