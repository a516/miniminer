using System;
using System.Collections;
using System.ComponentModel;
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
                OnDequeue.BeginInvoke(this, null, null, null);
        }

        protected virtual void Dequeued(EventArgs e)
        {

            if (OnDequeue != null)
                OnDequeue.BeginInvoke(this, null, null, null);
        }

        public static void Invoke(ISynchronizeInvoke sync, Action action)
        {
            if (!sync.InvokeRequired)
                action();
            else
            {
                var args = new object[] { };
                sync.Invoke(action, args);
            }
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

        public new void Enqueue(object toAdd) { }
    }
}
