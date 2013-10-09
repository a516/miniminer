using System;

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
        }

		private static void ExecuteShare(Work work)
		{
			Program.ClearConsole();
			Program.Print("*** Worker Found Valid Share ***");
			Program.Print("Share: " + Utils.ToString(work.Current));
			Program.Print("Nonce: " + Utils.ToString(work.Nonce));
			Program.Print("Hash: " + Utils.ToString(work.Hash));
			Program.Print("Sending Share to Pool...");
			Program.Print(work.SendShare() ? "Server accepted the Share!" : "Server declined the Share!");
		}
		
		public static void SendShare(Work work)
		{
			WorkerQueue.Enqueue(work);
		}
	}
}

