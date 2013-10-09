using System;
using System.Collections.Generic;
using System.Threading;

namespace MiniMiner
{
	public class SendWorkQueue
	{
		private static bool stop;
		private const int Queuecount = 8;
		private static readonly Queue<Work> _workerQueue = new Queue<Work>();
		private static object locker;
		
		public SendWorkQueue()
		{
			locker = new object ();
		}

		public void StartThread()
		{
			while (!stop)
			{
				while (_workerQueue.Count > 0)
				{
					Work w;
					lock(locker)
					{
						w = _workerQueue.Dequeue();
					}
					if (w != null)
						ExecuteShare (w);
				}
				Thread.Sleep(100);
			}
		}

		private void ExecuteShare(Work work)
		{
			Program.ClearConsole();
			Program.Print("*** Worker Found Valid Share ***");
			Program.Print("Share: " + Utils.ToString(work.Current));
			Program.Print("Nonce: " + Utils.ToString(work.Nonce));
			Program.Print("Hash: " + Utils.ToString(work.Hash));
			Program.Print("Sending Share to Pool...");
			Program.Print(work.SendShare() ? "Server accepted the Share!" : "Server declined the Share!");
		}
		
		public void Stop()
		{
			stop = true;
		}
		
		public static void SendShare(Work work)
		{
			lock(locker)
			{
				_workerQueue.Enqueue (work);
			}
		}
	}
}

