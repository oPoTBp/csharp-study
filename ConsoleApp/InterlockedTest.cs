using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class InterlockedTest
    {
        static int counter = 0;
        private static readonly object lockObject = new object();
        public static void test()
        {

            {
                counter = 0;
                DateTime start = DateTime.Now;

                List<Thread> threads = new List<Thread>();
                for (int i = 0; i < 1000; i++)
                {
                    threads.Add(new Thread(() =>
                    {
                        for (int j = 0; j < 100000; j++)
                        {
                            //lock(lockObject)
                            {
                                //counter++;
                            }
                            Interlocked.Increment(ref counter);
                        }
                    }));
                    //threads[i].Start();
                }

                foreach (Thread t in threads)
                {
                    t.Start();
                }
                foreach (Thread t in threads)
                {
                    t.Join();
                }
                DateTime end = DateTime.Now;
                double costTime = (end - start).TotalSeconds;
                Console.WriteLine($"In Interlocked, Final Counter: {counter}, costTime: {costTime}"); // 输出 5000
            }

            {
                counter = 0;
                DateTime start = DateTime.Now;

                List<Thread> threads = new List<Thread>();
                for (int i = 0; i < 1000; i++)
                {
                    threads.Add(new Thread(() =>
                    {
                        for (int j = 0; j < 100000; j++)
                        {
                            //lock(lockObject)
                            {
                                counter++;
                            }
                            //Interlocked.Increment(ref counter);
                        }
                    }));
                    //threads[i].Start();
                }

                foreach (Thread t in threads)
                {
                    t.Start();
                }
                foreach (Thread t in threads)
                {
                    t.Join();
                }
                DateTime end = DateTime.Now;
                double costTime = (end - start).TotalSeconds;
                Console.WriteLine($"In counter, Final Counter: {counter}, costTime: {costTime}"); // 输出 5000
            }

            {
                counter = 0;
                DateTime start = DateTime.Now;

                List<Thread> threads = new List<Thread>();
                for (int i = 0; i < 1000; i++)
                {
                    threads.Add(new Thread(() =>
                    {
                        for (int j = 0; j < 100000; j++)
                        {
                            lock (lockObject)
                            {
                                counter++;
                            }
                            //Interlocked.Increment(ref counter);
                        }
                    }));
                    //threads[i].Start();
                }

                foreach (Thread t in threads)
                {
                    t.Start();
                }
                foreach (Thread t in threads)
                {
                    t.Join();
                }
                DateTime end = DateTime.Now;
                double costTime = (end - start).TotalSeconds;
                Console.WriteLine($"In lock, Final Counter: {counter}, costTime: {costTime}"); // 输出 5000
            }
        }
    }
}
