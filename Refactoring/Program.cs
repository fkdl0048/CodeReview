public class Program
{
    public static void Main()
    {
        List<Play> plays = new List<Play>();
        
        plays.Add(new Play("Hamlet", "tragedy"));
        plays.Add(new Play("As You Like It", "comedy"));
        plays.Add(new Play("Othello", "tragedy"));

        List<Performance> performances = new List<Performance>();

        performances.Add(new Performance("Hamlet", 55));
        performances.Add(new Performance("As You Like It", 35));
        performances.Add(new Performance("Othello", 40));

        Invoice invoices = new Invoice("BigCo", performances);

        Account account = new Account(plays, invoices);
        Console.WriteLine(account.Statment());
    }
}