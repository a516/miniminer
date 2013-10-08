using System;

namespace MiniMiner
{
    public class Worker
    {
        private readonly Pool _pool;
        private const long MaxAgeTicks = 20000 * TimeSpan.TicksPerMillisecond;
        Work _work;
        uint _nonce;
        private const uint BatchSize = 100000;
        private readonly int _workerID;


        private readonly object _locker = new object();
        private bool _shouldStop = false;

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
			while (!_shouldStop)
            {
                if (_work == null || _work.Age > MaxAgeTicks)
                    _work = GetWork();

                if (_work.FindShare(ref _nonce, BatchSize))
                {
                    SendShare(_work.Current);
                    _work = null;
                }
                else PrintCurrentState();
            }
        }

        public void Stop()
        {
            lock (_locker)
            {
                _shouldStop = true;
            }
        }

        private static DateTime _lastPrint = DateTime.Now;
        private void PrintCurrentState()
        {
            Program.ClearConsole();
			Program.Print("Worker" + _workerID + "Data: " + Utils.ToString(_work.Data));
            Program.Print(
                string.Concat("Nonce: ", 
                Utils.ToString(_nonce), "/", 
                Utils.ToString(uint.MaxValue), " ", 
                (((double)_nonce / uint.MaxValue) * 100).ToString("F2"), "%"));
            Program.Print("Hash: " + Utils.ToString(_work.Hash));
            var span = DateTime.Now - _lastPrint;
            Program.Print("Speed: " + (int)((BatchSize / 1000) / span.TotalSeconds) + "Kh/s");
            _lastPrint = DateTime.Now;
        }

        private void SendShare(byte[] share)
        {
            Program.ClearConsole();
            Program.Print("*** Worker "+_workerID+" Found Valid Share ***");
            Program.Print("Share: " + Utils.ToString(_work.Current));
            Program.Print("Nonce: " + Utils.ToString(_nonce));
            Program.Print("Hash: " + Utils.ToString(_work.Hash));
            Program.Print("Sending Share to Pool...");
            Program.Print(_pool.SendShare(share) ? "Server accepted the Share!" : "Server declined the Share!");
        }

        private Work GetWork()
        {
            return _pool.GetWork();
        }
    }
}
