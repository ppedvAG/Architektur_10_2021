// See https://aka.ms/new-console-template for more information
using ppedv.Laureatus.Logic;

Console.WriteLine("Hello, World!");

var core = new Core();
foreach (var person in core.GetWinnersOfYear(2222))
{
    Console.WriteLine($"{person.Name}");
}

Console.WriteLine("Ende");
Console.ReadLine();
