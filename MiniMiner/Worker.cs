using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniMiner
{
    public class Worker
    {
        private readonly Pool _pool;
        private const long MaxAgeTicks = 20000 * TimeSpan.TicksPerMillisecond;
        
        private const uint BatchSize = 100000;
        private readonly int _workerID;
        private readonly object _locker = new object();
        private bool _shouldStop;
        private readonly int _workThreads = Environment.ProcessorCount;

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
		    while ( _pool != null )
            {
                var work = new Work[_workThreads];

                if (work[0] == null || work[0].Age > MaxAgeTicks)
                    work[0] = _pool.GetWork();

                work[0].WorkerID = _workerID;
                /* Clone work */
                for (var y = 1; y < _workThreads; ++y)
                    work[y] = new Work(work[0]);

                /* fire off work in separate threads */
                Parallel.For((long) 0, _workThreads, x =>
                    {
                        var nonce = (uint) x;
                        while (!_shouldStop && work != null)
                        {
                            if (work[x].LookForShare(nonce, BatchSize))
                            {
                                work[x].CalculateShare(nonce);
                                SendWorkQueue.SendShare(work[x]);
                                work = null;
                            }
                            else
                            {
                                var s = work[x].GetCurrentStateString(nonce);
                                ThreadPool.QueueUserWorkItem(delegate
                                    {
                                        Program.ClearConsole();
                                        Program.Print(s);
                                    });
                            }

                            //increase nonce
                            if (uint.MaxValue - _workThreads < nonce)
                                nonce = (uint.MaxValue% (uint)x);
                            else nonce += (uint)x;
                        }
                    });
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
