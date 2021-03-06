﻿using System;
using DAN.CollectionQueue;
using DAN.Models;

namespace DAN.Tests.Repository
{
    /// <summary>
    /// 仓储
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
