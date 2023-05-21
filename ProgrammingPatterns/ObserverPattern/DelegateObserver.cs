namespace ObserverPattern
{
    delegate void OnEvent();

    class DelegateEntertainer
    {
        public event OnEvent myEvent;
        public void AddObserver(OnEvent sbscriber)
        {
            myEvent += sbscriber;
        }

        public void RemoveObserver(OnEvent sbscriber)
        {
            myEvent -= sbscriber;
        }

        public void Notify()
        {
            myEvent();
        }
    }
}