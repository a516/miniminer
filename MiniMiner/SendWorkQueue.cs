using System;
using System.Text;
using System.Threading;

namespace MiniMiner
{
	public static class SendWorkQueue
	{
        private static readonly ConcurrentQueue<Work> WorkerQueue = new ConcurrentQueue<Work>();
		
		static SendWorkQueue()
		{
		    WorkerQueue.OnEnqueue += OnEnqueue;
		}

        private static void OnEnqueue(object obj, EventArgs e)
        {
            /* use in two statement to prevent unnecessary locking */
            var w = WorkerQueue.Dequeue();
            if (w != null)
                ExecuteShare(w);
            Console.WriteLine("Work sent, items left to send: {0}", WorkerQueue.Count);
        }

		private static void ExecuteShare(Work work)
		{
		    var result = work.SendShare();

		    var sb = new StringBuilder();
			Program.ClearConsole();
            sb.Append("*** Worker Found Valid Share ***");
			sb.Append("Share: " + Utils.ToString(work.Current));
            sb.Append("Nonce: " + Utils.ToString(work.Nonce));
            sb.Append("Hash: " + Utils.ToString(work.Hash));
            sb.Append("Sending Share to Pool...");
            sb.Append(result ? "Server accepted the Share!" : "Server declined the Share!");

            ThreadPool.QueueUserWorkItem(
                delegate { Program.ClearConsole(); Program.Print(sb.ToString()); });
		}
		
		public static void SendShare(Work work)
		{
			WorkerQueue.Enqueue(work);
		}
	}
}

