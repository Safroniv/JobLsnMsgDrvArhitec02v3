﻿using System.Text;
using RabbitMQ.Client;

namespace Messaging
{
    public class Producer
    {
        private readonly string _queueName;
        private readonly string _hostName;

        public Producer(string queueName, string hostName)
        {
            _queueName = queueName;
            _hostName = "shark.rmq.cloudamqp.com"; //hostName;
        }

        public void Send(string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostName,
                Port = 5672,
                UserName = "DBiadscs",
                Password = "NSfgbfsefgegsk8733=dm46+@3",
                VirtualHost = "qkeydfmz"

            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(
                "direct_exchange",
                "direct",
                false,
                false,
                null
            );

            var body = Encoding.UTF8.GetBytes(message); // формируем тело сообщения для отправки

            channel.BasicPublish(exchange: "direct_exchange",
                routingKey: _queueName,
                basicProperties: null,
                body: body); //отправляем сообщение
        }
    }
}