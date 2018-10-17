using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace DAN.RabbitMQ
{
    public class RabbitProvider
    {
        public const string RABBITMQURL = "amqp://test:test@rabbitmq.login1.com:5672/test";
        private static IConnection conn;
        /// <summary>
        /// 获取连接。
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static IConnection CreateConnection(string url)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = new Uri(url);
            factory.AutomaticRecoveryEnabled = true;
            IConnection conn = factory.CreateConnection();
            return conn;
        }

        /// <summary>
        /// 单个
        /// </summary>
        /// <param name="data"></param>
        public static void Publish<T>(string exchange, string queue, string route, T data)
        {
            if (conn == null || !conn.IsOpen)
            {
                conn = CreateConnection(RABBITMQURL);

            }
            using (IModel model = conn.CreateModel())
            {
                model.ExchangeDeclare(exchange, ExchangeType.Direct);
                model.QueueDeclare(queue, false, false, false, null);
                model.QueueBind(queue, exchange, route, null);
                //IBasicProperties props = ch.CreateBasicProperties();
                //FillInHeaders(props); // or similar
                // byte[] body = ComputeBody(props); // or similar             

                model.BasicPublish(exchange, route, null, System.Text.Encoding.Default.GetBytes(data.ToString()));
            }
        }

        /// <summary>
        /// 多条数据
        /// </summary>
        /// <param name="data"></param>
        public static void Publish<T>(string exchange, string queue, string route, List<T> data)
        {
            if (conn == null || !conn.IsOpen)
            {
                conn = CreateConnection(RABBITMQURL);
            }
            using (IModel model = conn.CreateModel())
            {
                model.ExchangeDeclare(exchange, ExchangeType.Direct);
                model.QueueDeclare(queue, false, false, false, null);
                model.QueueBind(queue, exchange, route, null);
                //IBasicProperties props = ch.CreateBasicProperties();
                //FillInHeaders(props); // or similar
                // byte[] body = ComputeBody(props); // or similar
                foreach (var item in data)
                {
                    model.BasicPublish(exchange, route, null, System.Text.Encoding.Default.GetBytes(item.ToString()));
                }
            }
        }
    }
}
