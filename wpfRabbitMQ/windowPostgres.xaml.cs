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
using wpfRabbitMQ.Postgres.DB;

namespace wpfRabbitMQ.Postgres
{
    public partial class windowPostgres : Window
    {
        const string queueGeradorName = "PostgresToMongo";
        const string queueConsumerName = "MongoToPostgres";

        private ConnectionFactory connectionFactory;



        public windowPostgres()
        {
            InitializeComponent();

            //Garante a criação do banco
            using (var ctx = new Context())
                ctx.Database.EnsureCreated();

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
            using (var conection = connectionFactory.CreateConnection())
            using (var channel = conection.CreateModel())
            {
                channel.QueueDeclare(queue: queueGeradorName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            }

            // Cria o worker/ Consumidor
            Task.Run(() =>
            {
                try
                {
                    using (var conection = connectionFactory.CreateConnection())
                    using (var channel = conection.CreateModel())
                    {
                        //channel.QueueDeclare(queue: queueConsumerName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += (model, ea) =>
                        {
                            try
                            {
                                var body = ea.Body.ToArray();
                                var message = Encoding.UTF8.GetString(body);
                                channel.BasicAck(ea.DeliveryTag, false);
                            }
                            catch
                            {
                                channel.BasicNack(ea.DeliveryTag, false, true);
                            }
                        };

                        channel.BasicConsume(queue: queueConsumerName, autoAck: false, consumer: consumer);

                        while (true)
                        { Thread.Sleep(1000); }
                    }
                }
                catch
                { }
            });
        }




        private void criaFila(tbUser user)
        {
            try
            {
                using (var conection = connectionFactory.CreateConnection())
                using (var channel = conection.CreateModel())
                {
                    var objSend = JsonSerializer.SerializeToUtf8Bytes(user);
                    channel.BasicPublish(exchange: string.Empty, routingKey: queueGeradorName, basicProperties: null, body: objSend);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnCriaUser_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                using (var ctx = new Context())
                {
                    var user = new tbUser();
                    user.user_name = "rodrigo" + DateTime.Now.Ticks;
                    user.user_senha = "senha" + DateTime.Now.Ticks;
                    ctx.tbUser.Add(user);
                    ctx.SaveChanges();
                    criaFila(user);
                }
            }
            catch 
            {  }
        }
    }
}
