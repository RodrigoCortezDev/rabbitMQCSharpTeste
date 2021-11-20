using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using wpfRabbitMQ.Mongo.DB;

namespace wpfRabbitMQ.Mongo
{
    public partial class windowMongo : Window
    {
        const string queueGeradorName = "MongoToPostgres";
        const string queueConsumerName = "PostgresToMongo";
        private EventingBasicConsumer consumer;
        private ConnectionFactory connectionFactory;
        private IConnection conection;
        private IModel channel;



        public windowMongo()
        {
            InitializeComponent();


            //Cria o padrão de conexão do Rabbit
            connectionFactory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                Port = 5672,
                RequestedConnectionTimeout = new TimeSpan(0, 0, 0, 3),
            };


            //Cria a fila
            conection = connectionFactory.CreateConnection();
            channel = conection.CreateModel();
            
            channel.QueueDeclare(queue: queueGeradorName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            


            //Cria o worker/Consumidor
            consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var objDes = JsonSerializer.Deserialize<tbUserPostgres>(body);
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };
            channel.BasicConsume(queue: queueConsumerName, autoAck: false, consumer: consumer);
        }
    }
}
