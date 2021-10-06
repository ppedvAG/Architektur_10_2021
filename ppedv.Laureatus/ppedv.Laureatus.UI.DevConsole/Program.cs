// See https://aka.ms/new-console-template for more information
using Autofac;
using ppedv.Laureatus.Logic;
using ppedv.Laureatus.Model.Contracts;
using System.Reflection;

Console.WriteLine("Hello, World!");

//DI per referenz zur abhängigkeit
//var core = new Core(new ppedv.Laureatus.Data.EfCore.EfRepository());

//DI per Hand mit Reflection
//var efCorePath = @"C:\Users\Fred\source\repos\Architektur_10_2021\ppedv.Laureatus\ppedv.Laureatus.Data.EfCore\bin\Debug\net6.0\ppedv.Laureatus.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(efCorePath); 
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//IRepository repo = (IRepository)Activator.CreateInstance(typeMitRepo);
//var core = new Core(repo);

//DI per AutoFac ohne Referenz 
//var efCorePath = @"C:\Users\Fred\source\repos\Architektur_10_2021\ppedv.Laureatus\ppedv.Laureatus.Data.EfCore\bin\Debug\net6.0\ppedv.Laureatus.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(efCorePath);
//var builder = new ContainerBuilder();
//builder.RegisterAssemblyTypes(ass).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();
//var container = builder.Build();

//DI per AutoFac
var builder = new ContainerBuilder();
builder.RegisterType<ppedv.Laureatus.Data.EfCore.EfRepository>().AsImplementedInterfaces();
var container = builder.Build();

var core = new Core(container.Resolve<IRepository>());


foreach (var person in core.GetWinnersOfYear(238).ToList())
{
    Console.WriteLine($"{person.Name}");
    foreach (var p in person.Laureates)
    {
        Console.WriteLine($"\t{p.Price?.Year}");
    }
}

Console.WriteLine($"The winner is {core.GetPersonWithMostWins().Name}");

Console.WriteLine("Ende");
Console.ReadLine();
