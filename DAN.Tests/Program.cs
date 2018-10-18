using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DAN.Models;
using DAN.CollectionQueue;
using DAN.Tests.Repository;
using DAN.RabbitMQ;
using System.IO;
using System.Diagnostics;

namespace DAN.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            PublishTest publishTest = new PublishTest();
           // publishTest.RunEachPush();
           // publishTest.RunOncePush();
           // publishTest.RunDelayTimePush();
            Console.ReadLine();
        }


    }
}
