using EF_Repositories;
using EF_Infrastructure.Context;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        FleetManagementContext context = new();
        EFBestuurderRepository repo = new(context);

        var bestuurders = repo.Bestuurders;
        foreach(var b in bestuurders){
            System.Console.WriteLine($"{b.Naam} {b.Voornaam} {b.Rijksregisternummer}");
        }

    }
}