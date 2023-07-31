public class TragedyCalculator : IPerformaceCalculator
{
    private Performance aPerformance;
    private Play play;

    public TragedyCalculator(Performance aPerformance, Play play)
    {
        this.aPerformance = aPerformance;
        this.play = play;
    }

    public int AmountFor()
    {
        int result = 40000;
        if (aPerformance.audience > 30)
        {
            result += 1000 * (aPerformance.audience - 30);
        }

        return result;
    }

    public int VolumeCreditsFor()
    {
        return Math.Max(aPerformance.audience - 30, 0);
    }

    public Play GetPlay()
    {
        return play;
    }
}