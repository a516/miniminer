using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiniMiner
{
    public class WorkQueue
    {
        private bool stop;
        private Pool _pool;
        private const int Queuecount = 8;
		private object locker;
        private readonly Queue<Work> _workerQueue = new Queue<Work>();

        public WorkQueue(Pool pool)
        {
			locker = new object ();
            _pool = pool;
        }

        public void StartThread()
        {
            for (var x = 0; x < Queuecount; ++x)
				AddWork();

            while (!stop)
            {
                if (_workerQueue.Count < Queuecount)
					AddWork();
				
                Thread.Sleep(10);
            }
        }

		private void AddWork()
		{
			var w = new Work (_pool);
			lock (locker){
				_workerQueue.Enqueue (w);
			}
		}

        public void Stop()
        {
            stop = true;
        }

        public Work GetWork(Pool pool)
        {
            while (_workerQueue.Count != Queuecount)
                Thread.Sleep(20);
			Work w;
			lock (locker) {
				w = _workerQueue.Dequeue ();
			}
			return w;
        }
    }
}
