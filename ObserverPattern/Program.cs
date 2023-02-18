namespace ObserverPattern
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Entertainer entertainer = new Entertainer();

            // entertainer.AddObserver(new SbscriberA());
            // entertainer.AddObserver(new SbscriberB());
            
            // Observer observer = new SbscriberA();
            // entertainer.AddObserver(observer);

            // entertainer.Notify();

            // entertainer.RemoveObserver(observer);
            // entertainer.Notify();

            DelegateEntertainer delegateEntertainer = new DelegateEntertainer();

            delegateEntertainer.AddObserver( () => System.Console.WriteLine("!@#"));

            delegateEntertainer.Notify();

        }
    }
}