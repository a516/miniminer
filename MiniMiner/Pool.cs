using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MiniMiner
{
    public class Pool
    {
        public Uri Url;
        public string User;
        public string Password;
        private readonly WorkQueue _poolWorkQueue;

        public Pool(string login)
        {
            var urlStart = login.IndexOf('@');
            var passwordStart = login.IndexOf(':');
            var user = login.Substring(0, passwordStart);
            var password = login.Substring(passwordStart + 1, urlStart - passwordStart - 1);
            var url = "http://"+login.Substring(urlStart + 1);
            Url = new Uri(url);
            User = user;
            Password = password;
            _poolWorkQueue = new WorkQueue(this);
        }

        private string InvokeMethod(string method, string paramString = null)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(Url);
            webRequest.Credentials = new NetworkCredential(User, Password);
            webRequest.ContentType = "application/json-rpc";
            webRequest.Method = "POST";

            var jsonParam = (paramString != null) 
                ? string.Concat("\"", paramString, "\"") 
                : string.Empty;
            var request = string.Concat("{\"id\": 0, \"method\": \"", method, "\", \"params\": [", jsonParam, "]}");

            var byteArray = Encoding.UTF8.GetBytes(request);
            webRequest.ContentLength = byteArray.Length;
            using (var dataStream = webRequest.GetRequestStream())
                dataStream.Write(byteArray, 0, byteArray.Length);

            string reply = null;
            using (var webResponse = webRequest.GetResponse())
            using (var str = webResponse.GetResponseStream())
            {
                if (str != null)
                    using (var reader = new StreamReader(str))
                        reply = reader.ReadToEnd();
            }

            return reply;
        }

        public void StartWorkers()
        {
            var queueThread = new Thread(_poolWorkQueue.StartThread);
            queueThread.Start();
			var sendQueue = new SendWorkQueue ();
			var sendThread = new Thread (sendQueue.StartThread);
			sendThread.Start ();

			var threads = Environment.ProcessorCount;
			var workers = new List<Worker>();
			var tasks = new List<Thread>();

			for (var i = 0; i < threads; ++i)
			{
				workers.Add(new Worker(this, i));
				tasks.Add(new Thread(workers[i].Work));
                tasks[i].Start();
            }
            
            var input = string.Empty;

            while (!input.Equals("x", StringComparison.CurrentCultureIgnoreCase))
            {
                input = Console.ReadKey().KeyChar.ToString();
				switch(input)
				{
				case "+":
					workers.Add(new Worker(this, workers.Count));
					tasks.Add(new Thread(workers[workers.Count-1].Work) { IsBackground = true });
					tasks[tasks.Count-1].Start();
					break;
				case "-":
					if (workers.Count > 0)
					{
						var id = workers.Count-1;
						workers[id].Stop();
						tasks[id].Join();
						workers.RemoveAt(id);
						tasks.RemoveAt(id);
					}
					break;
				}
            }

            _poolWorkQueue.Stop();
            queueThread.Join();

			sendQueue.Stop ();
			sendThread.Join ();

            foreach (var w in workers)
                w.Stop();

			foreach (var t in tasks)
				t.Join();
        }

        
        public Work GetWork(bool silent = false)
        {
            return _poolWorkQueue.GetWork(this);
        }

        public byte[] ParseData()
        {
            return ParseData(InvokeMethod("getwork"));
        }

        private static byte[] ParseData(string json)
        {
            var match = Regex.Match(json, "\"data\": \"([A-Fa-f0-9]+)");
            if (match.Success)
            {
                var data = Utils.RemovePadding(match.Groups[1].Value);
                data = Utils.EndianFlip32BitChunks(data);
                return Utils.ToBytes(data);
            }
            throw new Exception("Didn't find valid 'data' in Server Response");
        }

        public bool SendShare(string paddedData)
        {
            var jsonReply = InvokeMethod("getwork", paddedData);
            var match = Regex.Match(jsonReply, "\"result\": true");
            return match.Success;
        }
    }
}