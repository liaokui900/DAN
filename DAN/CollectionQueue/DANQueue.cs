using System.Collections.Concurrent;
using System.Collections.Generic;
using DAN.Models;

namespace DAN.CollectionQueue
{
    /// <summary>
    /// </summary>
    public class DANQueue<T> : IDANQueue<T>
    {
        private static BlockingCollection<DANMessage<T>> GlobalCollection;

        static DANQueue()
        {
            GlobalCollection = new BlockingCollection<DANMessage<T>>();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool TryAdd(DANMessage<T> item)
        {
            return GlobalCollection.TryAdd(item);
        }
        /// <summary>
        /// 获取一个
        /// </summary>
        /// <param name="item"></param>
        public static DANMessage<T> TryTake()
        {
            var msg = new DANMessage<T>();
            if (GlobalCollection.TryTake(out msg))
            {
                return msg;
            }
            return null;
        }
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public static List<DANMessage<T>> TryTakeAll()
        {
            var list = new List<DANMessage<T>>();
            while (true)
            {
                var q = TryTake();
                if (q == null)
                {
                    return list;
                }
                list.Add(q);
            }
        }
        /// <summary>
        /// 统计
        /// </summary>
        public static int Count()
        {
            return GlobalCollection.Count;
        }
    }
}
