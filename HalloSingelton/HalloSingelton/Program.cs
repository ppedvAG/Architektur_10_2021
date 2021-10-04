// See https://aka.ms/new-console-template for more information

using HalloSingelton;

//Console.WriteLine("Hello, World!");

//new Logger().Log("Hallo Logger");

for (int i = 0; i < 100; i++)
{
    Task.Run(() => Logger.Instance.Log("Hallo Logger"));
}


//Console.WriteLine("Ende");
Console.ReadLine();