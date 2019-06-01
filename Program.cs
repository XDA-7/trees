using System;
using System.Threading;

namespace Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Init();
            var printer = new Printer();
            var world = new World(Console.WindowWidth, Console.WindowHeight);
            while (true)
            {
                world.Run();
                printer.PrintWorld(world);
                // printer.PrintTreeIds(world);
                Thread.Sleep(1000);
            }
        }
    }
}
