using DAN.Models;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace DAN.CollectionQueue
{
    /// <summary>
    /// </summary>
    public class DANQueue<T> : IDANQueue<T>
    {
        //private static ConcurrentQueue<DANMessage<T>> GloabalQueue => new ConcurrentQueue<DANMessage<T>>();


        private static BlockingCollection<DANMessage<T>> GlobalCollection ;
        static DANQueue() {
            GlobalCollection = new BlockingCollection<DANMessage<T>>();
        }
        public static bool TryAdd(DANMessage<T> item)
        {
           return GlobalCollection.TryAdd(item);
        }
        /// <summary>
        /// 
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
        /// 
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
        /// Count
        /// </summary>
        public static int Count()
        {
            return GlobalCollection.Count;
        }
    }
}
