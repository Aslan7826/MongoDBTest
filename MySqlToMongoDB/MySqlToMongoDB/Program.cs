using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MySqlToMongoDB
{
    public class Program
    {
        private static SemaphoreSlim concurrencySemaphore;
        private static int pagenum = 10000;

        public static void Main(string[] args)
        {
            Console.WriteLine("-----------------------start");
            int EventCount = 0, numOfThreads = 0;
            MySqlRepository mySql = new MySqlRepository();
            EventCount = mySql.GetTabelCount();
            numOfThreads = EventCount / pagenum + 1;
            Console.WriteLine($"{numOfThreads},{EventCount}");

            int maxConcurrency = 10;
            using (concurrencySemaphore = new SemaphoreSlim(maxConcurrency))
            {
                List<Task> ts = new List<Task>();
                for (int i = 0; i < EventCount; i += pagenum)
                {
                    concurrencySemaphore.Wait();
                    Task t = Task.Factory.StartNew(TaskRun, i);
                    ts.Add(t);
                }
                Task.WaitAll(ts.ToArray());
            }

            Console.WriteLine("\n-----------------------end");
            Console.Read();
        }

        private static void TaskRun(object i)
        {
            try
            {
                MySqlRepository mySql = new MySqlRepository();
                Console.WriteLine($"({i}),{Thread.CurrentThread.ManagedThreadId}/{Process.GetCurrentProcess().Threads.Count} ");
                var evetb = mySql.GetEvents((int)i, pagenum);
                //MongoDBRepository mongo = new MongoDBRepository();
                //mongo.EntryEvent(evetb.ToList());
            }
            finally
            {
                concurrencySemaphore.Release();
            }
        }

        //private static async Task TaskRun_async(object i)
        //{
        //    await Task.Run(() =>
        //    {
        //        using (MySqlRepository mySql = new MySqlRepository())
        //        {
        //            Console.WriteLine($"({i}){Thread.CurrentThread.ManagedThreadId},{Process.GetCurrentProcess().Threads.Count}  ");
        //            var x = mySql.GetEvents_async((int)i,pagenum);
        //            x.Result.ToList();
        //        }
        //    });
        //}
    }
}