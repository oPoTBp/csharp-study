
using ConsoleApp;
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
    public static async Task Main()
    {
        //InterlockedTest.test();
        //ServerCache<string>.Test();
        var r = HttpClientPress.GetRequest("https://localhost:7115/weatherforecast");
        Console.WriteLine(r);
    }
}