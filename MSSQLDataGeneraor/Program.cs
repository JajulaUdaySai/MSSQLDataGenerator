using MSSQLDataGenerator.BDLoader;
using MSSQLDataGenerator.DbContexts;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Started Seeding Data into DB");

        EmpDbContext emp = new EmpDbContext();
        BDL bDL = new BDL();
        bDL.LoadDataInTables(emp);

        Console.WriteLine("Completed Seeding Data into DB");
    }
}