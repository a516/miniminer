using System;
using System.Collections;
using System.Threading;

namespace MiniMiner
{
    public delegate void QueueEventHandler(object sender, EventArgs e);
    public class ConcurrentQueue<T>:Queue
    {
        public event QueueEventHandler OnEnqueue;
        public event QueueEventHandler OnDequeue;

        protected virtual void Enqueued(EventArgs e)
        {
            if (OnEnqueue != null)
                ThreadPool.QueueUserWorkItem(delegate { OnEnqueue(this, e); });
        }

        protected virtual void Dequeued(EventArgs e)
        {
            if (OnDequeue != null)
                ThreadPool.QueueUserWorkItem(delegate { OnDequeue(this, e); });
        }

        public new T Dequeue()
        {
            T t;
            lock (base.SyncRoot)
            {
                t = (T)base.Dequeue();
            }
            Dequeued(null);
            return t;
        }

        public void Enqueue(T toAdd)
        {
            lock (base.SyncRoot)
            {
                base.Enqueue(toAdd);
            }
            Enqueued(null);
        }

        public new void Enqueue(object toAdd)
        {
        }
    }
}
