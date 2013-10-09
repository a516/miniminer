using System;

namespace MiniMiner
{
    class Program
    {
        static Pool _pool;
		private static bool logout;

        static void Main()
        {
            while (!logout)
            {
                _pool = SelectPool();
				if (_pool != null)
				{
                	_pool.StartWorkers();
                	Console.WriteLine();
                	Console.Write("Hit 'Enter' to try again...");
                	Console.ReadLine();
				}
            }
        }

        public static void ClearConsole()
        {
            Console.Clear();
			Console.WriteLine("**********************************************************");
			Console.WriteLine("***              Minimal Bitcoin Miner                 ***");
            Console.WriteLine("** press x to exit, + adds a thread, - removes a thread **");
			Console.WriteLine("**********************************************************");
			Console.WriteLine();
        }

        private static Pool SelectPool()
        {
            ClearConsole();
            Print("Chose a Mining Pool 'user:password@url:port' or leave empty to skip.");
            Console.Write("Select Pool: ");
            var login = ReadLineDefault(System.Configuration.ConfigurationManager.AppSettings["DefaultLogin"]);
			if (login == "x" || login == "X") {
				logout = true;
				return null;
			}
            return new Pool(login);
        }

        public static void Print(string msg)
        {
            Console.WriteLine(msg);
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
