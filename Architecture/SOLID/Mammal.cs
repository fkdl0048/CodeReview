public class Mammal
{
    private INoise _noiseMaker;

    public Mammal(INoise noiseMaker)
    {
        this._noiseMaker = noiseMaker;
    }

    public void MakeNoise()
    {
        _noiseMaker.MakeNoise();
    }
}