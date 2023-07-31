public class ComedyCalculator : IPerformaceCalculator
{
    private Performance aPerformance;
    private Play play;

    public ComedyCalculator(Performance aPerformance, Play play)
    {
        this.aPerformance = aPerformance;
        this.play = play;
    }

    public int AmountFor()
    {
        int result = 30000;
        if (aPerformance.audience > 20)
        {
            result += 10000 + 500 * (aPerformance.audience - 20);
        }

        result += 300 * aPerformance.audience;
        return result;
    }

    public int VolumeCreditsFor()
    {
        return (int) Math.Floor((decimal) aPerformance.audience / 5);
    }

    public Play GetPlay()
    {
        return play;
    }
}