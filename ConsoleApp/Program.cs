
using ConsoleApp1;
using System;
using System.Diagnostics.Metrics;
using System.Net;
using System.Net.WebSockets;
using System.Reflection;
using System.Threading;

public class Program
{
    //public static void Main()
    //{
    //    Console.WriteLine("hello world");
    //    Person person = new Person();
    //    person.age = 1;
    //    person.name = "Test";
    //    int b = person.id;

    //    Person person2 = new Person();
    //    person2.age = 1;
    //    person2.name = "Test";

    //    int a = 1;
    //    if (person.Equals(person2))
    //    {
    //        Console.WriteLine("equal");
    //    }
    //    else
    //    {
    //        Console.WriteLine("not equal");
    //    }

    //    Console.WriteLine("{person}, {a}");
    //    Console.WriteLine($"{person.ToString()}, {a}");
    //}
    static int counter = 0;
    private static readonly object lockObject = new object();
    public static async Task Main()
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
                        lock(lockObject)
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