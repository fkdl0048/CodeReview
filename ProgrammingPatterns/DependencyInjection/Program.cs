namespace DepenencyInjection
{
    public interface IFunction
    {
        void Function();
    }

    public class AFunction : IFunction
    {
        public void Function()
        {
            System.Console.WriteLine("새로운 A기술을 도입합니다.");
        }
    }

    public class BFunction : IFunction
    {
        public void Function()
        {
            System.Console.WriteLine("새로운 B기술을 도입합니다.");
        }
    }

    public class MyService
    {
        public IFunction Func {get; set;}

        public MyService(IFunction function)
        {
            Func = function;
        }

        public void FunctionAction()
        {
            Func.Function();
        }
    }

    class Program
    {
    
        static void Main(string[] args)
        {
            MyService service = new MyService(new AFunction());

            service.FunctionAction();

            service = new MyService(new BFunction());

            service.FunctionAction();
        }
    }
}