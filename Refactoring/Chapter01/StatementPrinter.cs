public class StatementPrinter
{
    private IStatement statement;
    public StatementPrinter(IStatement statement)
    {
        this.statement = statement;
    }

    public void Print()
    {
        Console.WriteLine(statement.Statement());
    }
}