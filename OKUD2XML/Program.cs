using System;
using OKUD2XML.Core;

namespace OKUD2XML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = $"{App.Name} v{App.Version}";

            Program.Run(Commands.Start);
            Program.Run(Commands.Extract);
        }

        static void Run(ICommand command)
        {
            Console.Clear();
            command.Invoke();
        }
    }
}
