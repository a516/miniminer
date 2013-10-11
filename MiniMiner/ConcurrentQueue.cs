using System;
using System.Collections;

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
                OnEnqueue.BeginInvoke(this, null, null, null);
        }

        protected virtual void Dequeued(EventArgs e)
        {
            if (OnDequeue != null)
                OnDequeue.BeginInvoke(this, null, null, null);
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

        /// <summary>
        /// Function is disabled to prevent unsafe type usage.
        /// </summary>
        /// <param name="toAdd"></param>
        public override void Enqueue(object toAdd) { }
    }
}
