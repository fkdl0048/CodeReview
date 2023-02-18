namespace ObserverPattern
{
    public interface Observer
    {
        public void OnNotify();
    }

    public interface ISubject
    {
        void AddObserver(Observer observer);
        void RemoveObserver(Observer observer);
        void Notify();
    }

    public class SbscriberA : Observer
    {
        public void OnNotify()
        {
            System.Console.WriteLine("A입니다.");
        }
    }

    public class SbscriberB : Observer
    {
        public void OnNotify()
        {
            System.Console.WriteLine("B입니다.");
        }
    }

    public class Entertainer : ISubject
    {

        List<Observer> observers = new List<Observer>();

        public void AddObserver(Observer observer)
        {
            observers.Add(observer);    
        }

        public void RemoveObserver(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.OnNotify();
            }
        }
    }
}