using System.Threading;

namespace MiniMiner
{
    public class Worker
    {
        private readonly Pool _pool;
        //private const long MaxAgeTicks = 20000 * TimeSpan.TicksPerMillisecond;
        
        private const uint BatchSize = 100000;
        private readonly int _workerID;

        private readonly object _locker = new object();
        private bool _shouldStop;

        public Worker(Pool pool, int workerID)
        {
            _workerID = workerID;
            _pool = pool;
			Program.ClearConsole();
			Program.Print("Worker" + _workerID+" Requesting Work from Pool...");
			Program.Print("Server URL: " + _pool.Url);
			Program.Print("User: " + _pool.User);
			Program.Print("Password: " + _pool.Password);
		}
		
		public void Work()
		{
			while (!_shouldStop && _pool != null)
			{
			    using (var work = _pool.GetWork())
			    {
			        if (work.FindShare(BatchSize))
			        {
			            work.CalculateShare();
			            SendWorkQueue.SendShare(work);
			        }
			        else
			        {
			            var s = work.GetCurrentStateString();
                        ThreadPool.QueueUserWorkItem(
                            delegate { Program.ClearConsole(); Program.Print(s);
                        });
			        }
			    }
			}
        }

        public void Stop()
        {
            lock (_locker)
            {
                _shouldStop = true;
            }
        }
    }
}
