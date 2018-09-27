using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using DAN.Models;
using DAN.CollectionQueue;
using DAN.Tests.Repository;

namespace DAN.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            //这里就不引入依赖注入了。
            Repository<User> repository = new Repository<User>();
            for (var i = 0; i <= 100000; i++)
            {

                repository.Update(new User()
                {
                    CompanyId = 13232,
                    Mobile = "11111" + i
                });
            }
            Console.WriteLine($"Count:{DANQueue<User>.Count()}");
            Console.ReadLine();
        }
    }
}
