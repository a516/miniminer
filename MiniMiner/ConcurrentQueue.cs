﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace MiniMiner
{
    public delegate void QueueEventHandler(object sender, EventArgs e);
    public class ConcurrentQueue<T>:Queue<T>
    {
        private readonly object _locker = new object();

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
            lock (_locker)
            {
                t = base.Dequeue();
            }
            Dequeued(null);
            return t;
        }

        public new void Enqueue(T toAdd)
        {
            lock (_locker)
            {
                base.Enqueue(toAdd);
            }
            Enqueued(null);
        }
    }
}
