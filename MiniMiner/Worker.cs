using System;
using System.Threading;

namespace MiniMiner
{
    public class Worker
    {
        private readonly Pool _pool;
        private const long MaxAgeTicks = 20000 * TimeSpan.TicksPerMillisecond;
        
        private const uint BatchSize = 100000;
        private readonly int _workerID;
        private uint _nonce;
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
            Work work = null;
			while (!_shouldStop && _pool != null)
			{
                if (work == null || work.Age > MaxAgeTicks)
			        work = _pool.GetWork();

				work.WorkerID = _workerID;
                if (work.FindShare(ref _nonce, BatchSize))
                {
                    work.CalculateShare(_nonce);
			        SendWorkQueue.SendShare(work);
                    work = null;
                }
			    else
			    {
                    var s = work.GetCurrentStateString(_nonce);
			        ThreadPool.QueueUserWorkItem(delegate { Program.ClearConsole(); Program.Print(s); });
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
