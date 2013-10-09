using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniMiner
{
    public class WorkQueue
    {
        private readonly Pool _pool;
        private readonly int _queueCount = 1;
        private readonly ConcurrentQueue<Work> _workerQueue = new ConcurrentQueue<Work>();

        public WorkQueue(Pool pool, int queueCount)
        {
            _queueCount = queueCount;
            _pool = pool;
            Parallel.For(0, _queueCount, i=> AddWork());
            _workerQueue.OnDequeue += OnDequeue;
        }

        private void OnDequeue(object obj, EventArgs e)
        {
            AddWork();
        }

		private void AddWork()
		{
            /* use in two statement to prevent unnecessary locking */
			var w = new Work (_pool);
            _workerQueue.Enqueue(w);
		}

        public Work GetWork(Pool pool)
        {
            while (_workerQueue.Count != _queueCount)
                Thread.Sleep(20);
			return _workerQueue.Dequeue();
        }
    }
}
