namespace FactoryMethodPattern
{
    public abstract class Pizza
    {
        public abstract float GetPrice();
    }

    public class HamMushroom : Pizza
    {
        private float price = 1.2f;
        public override float GetPrice () => price;
    }

    public class Deluxe : Pizza
    {
        private float price = 1.4f;
        public override float GetPrice () => price;
    }

    public class Seafood : Pizza
    {
        private float price = 1.0f;
        public override float GetPrice () => price;
    }


    public interface IPizza
    {
        Pizza MakingPizza();
    }

    public class PizzaFactory
    {
        public IPizza pizza {get; set;}

        public PizzaFactory(IPizza pizza)
        {
            this.pizza = pizza;
        }

        public Pizza MakingPizza()
        {
            return pizza.MakingPizza();
        }
    }

    public class HamMushroomPizzaFactory : IPizza
    {
        public Pizza MakingPizza()
        {
            return new HamMushroom();
        }
    }

    public class DeluxePizzaFactory : IPizza
    {
        public Pizza MakingPizza()
        {
            return new Deluxe();
        }
    }
    
    public class SeafoodPizzaFactory : IPizza
    {
        public Pizza MakingPizza()
        {
            return new Seafood();
        }
    } 

    class Program
    {
        static void Main(string[] args)
        {
            PizzaFactory pizzaFactory = new PizzaFactory(new HamMushroomPizzaFactory());
            Pizza pizza = pizzaFactory.MakingPizza();

            System.Console.WriteLine(pizza.GetPrice());
        }
    }
}