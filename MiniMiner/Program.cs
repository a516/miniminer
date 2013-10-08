using System;

namespace MiniMiner
{
    class Program
    {
        static Pool _pool;

        static void Main()
        {
            while (true)
            {
                try
                {
                    _pool = SelectPool();
                    _pool.StartWorkers();
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.Write("ERROR: ");
                    Console.WriteLine(e.Message);
                    break;
                }
                Console.WriteLine();
                Console.Write("Hit 'Enter' to try again...");
                Console.ReadLine();
            }
        }


        public static void ClearConsole()
        {
            Console.Clear();
            Console.WriteLine("*****************************");
            Console.WriteLine("*** Minimal Bitcoin Miner ***");
            Console.WriteLine("*****************************");
            Console.WriteLine();
        }

        private static Pool SelectPool()
        {
            ClearConsole();
            Print("Chose a Mining Pool 'user:password@url:port' or leave empty to skip.");
            Console.Write("Select Pool: ");
            var login = ReadLineDefault(System.Configuration.ConfigurationManager.AppSettings["DefaultLogin"]);
            return new Pool(login);
        }

        public static void Print(string msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine();
        }

        private static string ReadLineDefault(string defaultValue)
        {
            //Allow Console.ReadLine with a default value
            var userInput = Console.ReadLine();
            Console.WriteLine();
            return string.IsNullOrEmpty(userInput)? defaultValue : userInput;
        }
    }
}
