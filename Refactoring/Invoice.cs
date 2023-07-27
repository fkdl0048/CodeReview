public class Invoice
{
    public string customer;
    public List<Performance> performances;

    public Invoice(string customer, List<Performance> performances)
    {
        this.customer = customer;
        this.performances = performances;
    }
}