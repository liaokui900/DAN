using System;
using System.Diagnostics;
using DAN.CollectionQueue;
using DAN.RabbitMQ;
using DAN.Tests.Repository;

namespace DAN.Tests
{
    public class PublishTest
    {
        public const string ExchangeStr = "fanTest";
        public const string QueueStr = "fanQueueTest";
        private static string TypeUserName = typeof(User).Name;
        //这里就不引入依赖注入了。
        private Repository<User> repository = new Repository<User>();
        Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// 每次都推送
        /// </summary>
        public void RunEachPush()
        {
            stopwatch.Restart();
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
        }

        /// <summary>
        /// 只推送一次
        /// </summary>
        public void RunOncePush()
        {
            //批量测试聚合通知。
            stopwatch.Restart();
            for (var i = 0; i <= 100000; i++)
            {
                var user = new User()
                {
                    CompanyId = 13232,
                    Mobile = "11111" + i
                };
                repository.Update(user);
            }
            RabbitProvider.Publish(ExchangeStr, QueueStr, TypeUserName, DANQueue<User>.TryTakeAll());
            stopwatch.Stop();
            Console.WriteLine($"UpdateDelayPush-Time:" + stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// 延时推送
        /// </summary>
        public void RunDelayTimePush()
        {
            System.Timers.Timer timer = new System.Timers.Timer(5000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
            //批量测试大量数据
            for (var i = 0; i <= 1000000; i++)
            {
                var user = new User()
                {
                    CompanyId = 13232,
                    Mobile = "11111" + i
                };
                repository.Update(user);
            }
        }
        /// <summary>
        /// 定时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
