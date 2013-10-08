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
        private const int Queuecount = 4;
        private readonly Queue<Work> _workerQueue = new Queue<Work>();

        public WorkQueue(Pool pool)
        {
            _pool = pool;
        }

        public void StartThread()
        {
            for (var x = 0; x < Queuecount; ++x)
                _workerQueue.Enqueue(new Work(_pool.ParseData()));

            while (!stop)
            {
                if (_workerQueue.Count < Queuecount)
                    _workerQueue.Enqueue(new Work(_pool.ParseData()));
                Thread.Sleep(100);
            }
        }

        public void Stop()
        {
            stop = true;
        }

        public Work GetWork(Pool pool)
        {
            while (_workerQueue.Count == 0)
                Thread.Sleep(100);
            return _workerQueue.Dequeue();
        }
    }
}
