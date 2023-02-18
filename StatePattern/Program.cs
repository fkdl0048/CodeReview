namespace StatePattern
{
    public interface State
    {
        public void Excute();
    }

    public class Attack : State
    {
        public void Excute()
        {
            System.Console.WriteLine("공격");
        }
    }

    public class Jump : State
    {
        public void Excute()
        {
            System.Console.WriteLine("점프");
        }
    }

    public class Jeonglee
    {
        private State state;

        public Jeonglee(State state)
        {
            this.state = state;
        }

        public void Update()
        {
            state.Excute();
        }
    }

    class Program
    {
        public void Main(string[] args)
        {
            
        }
    }
    
}