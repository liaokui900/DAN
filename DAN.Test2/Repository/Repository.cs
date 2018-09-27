using DAN.CollectionQueue;
using DAN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAN.Test2.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class Repository<TDocument> : IRepository<TDocument>
    {
        public bool Update(User user)
        {
            DANQueue<User>.TryAdd(new DANMessage<User>() { Body = user, Key = user.CompanyId + user.Mobile, Type = typeof(User).Name, TimeStamp = DateTime.Now.Ticks });
            return true;
        }
    }
}
