namespace NullObjcetPattern
{
    public interface IAnimal
    {
        public void Say();
    }

    public class Cat : IAnimal
    {
        public void Say()
        {
            System.Console.WriteLine("냐옹이다옹~");
        }
    }

    public class NullAnimal : IAnimal
    {
        public void Say()
        {
            // 아무것도 하지 않음
        }
    }

    class NullObjcetPattern
    {
        static void Main(string[] args)
        {
            IAnimal[] animals = new IAnimal[5];

            animals[0] = new Cat();
            animals[1] = new NullAnimal();
            animals[2] = new Cat();
            animals[3] = new NullAnimal();
            animals[4] = new Cat();

            for (int i = 0; i < animals.Length; i++)
            {
                animals[i].Say();
            }
        }
    }
}