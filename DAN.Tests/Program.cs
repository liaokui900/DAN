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
        public const string ExchangeStr = "fanTest";
        public const string QueueStr = "fanQueueTest";
        private static string TypeUserName = typeof(User).Name;
        static void Main(string[] args)
        {
            //这里就不引入依赖注入了。
            Repository<User> repository = new Repository<User>();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //批量测试循环实时通知
            for (var i = 0; i <= 5000; i++)
            {
                var user = new User()
                {
                    CompanyId = 13232,
                    Mobile = "11111" + i
                };
                repository.Update(user);
                RabbitProvider.Publish(ExchangeStr, QueueStr, TypeUserName, DANQueue<User>.TryTake());
            }
            stopwatch.Stop();
            Console.WriteLine($"UpdateWithPush-Time:" + stopwatch.ElapsedMilliseconds);


            //批量测试聚合通知。
            //stopwatch.Restart();
            //for (var i = 0; i <= 100000; i++)
            //{
            //    var user = new User()
            //    {
            //        CompanyId = 13232,
            //        Mobile = "11111" + i
            //    };
            //    repository.Update(user);
            //}
            //RabbitProvider.Publish(ExchangeStr, QueueStr, TypeUserName, DANQueue<User>.TryTakeAll());
            //stopwatch.Stop();
            //Console.WriteLine($"UpdateDelayPush-Time:" + stopwatch.ElapsedMilliseconds);

            //System.Timers.Timer timer = new System.Timers.Timer(5000);
            //timer.Elapsed += Timer_Elapsed;
            //timer.Start();
            ////批量测试大量数据
            //for (var i = 0; i <= 1000000; i++)
            //{
            //    var user = new User()
            //    {
            //        CompanyId = 13232,
            //        Mobile = "11111" + i
            //    };
            //    repository.Update(user);

            //}
            Console.ReadLine();
        }

        private static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var list = DANQueue<User>.TryTakeAll();
            if (list.Count > 0)
            {
                RabbitProvider.Publish(ExchangeStr, QueueStr, TypeUserName, list);
            }
        }
    }
}
