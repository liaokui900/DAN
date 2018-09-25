using System;
using System.Collections.Generic;
using System.Text;

namespace DAN.Models
{
    public class DANMessage<T> 
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        public long TimeStamp { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public T Body { get; set; }
    }
}
