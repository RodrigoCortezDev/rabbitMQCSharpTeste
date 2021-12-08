using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using wpfRabbitMQ.Postgres.DB;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;

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
            //using (var ctx = new Context())
            //    ctx.Database.EnsureCreated();

            //Cria o padrão de conexão do Rabbit
            //connectionFactory = new ConnectionFactory()
            //{
            //    HostName = "localhost",
            //    UserName = "guest",
            //    Password = "guest",
            //    Port = 5672,
            //    RequestedConnectionTimeout = new TimeSpan(0, 0, 0, 3),
            //};

            //Cria a fila
            //using (var conection = connectionFactory.CreateConnection())
            //using (var channel = conection.CreateModel())
            //{
            //    channel.QueueDeclare(queue: queueGeradorName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            //}

            // Cria o worker/ Consumidor
            //Task.Run(() =>
            //{
            //    try
            //    {
            //        using (var conection = connectionFactory.CreateConnection())
            //        using (var channel = conection.CreateModel())
            //        {
            //            //channel.QueueDeclare(queue: queueConsumerName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            //            var consumer = new EventingBasicConsumer(channel);
            //            consumer.Received += (model, ea) =>
            //            {
            //                //ea.BasicProperties.ReplyTo
            //                //ea.BasicProperties.CorrelationId

            //                try
            //                {
            //                    var body = ea.Body.ToArray();
            //                    var message = Encoding.UTF8.GetString(body);
            //                    channel.BasicAck(ea.DeliveryTag, false);

            //                    //var basic = channel.CreateBasicProperties();
            //                    //basic.CorrelationId = ea.BasicProperties.CorrelationId;

            //                    //channel.BasicPublish(exchange: string.Empty, routingKey: ea.BasicProperties.ReplyTo, basicProperties: basic, body: body, mandatory: false);
            //                }
            //                catch
            //                {
            //                    channel.BasicNack(ea.DeliveryTag, false, true);
            //                }
            //            };

            //            channel.BasicConsume(queue: queueConsumerName, autoAck: false, consumer: consumer);
                        

            //            while (true)
            //            { Thread.Sleep(1000); }
            //        }
            //    }
            //    catch
            //    { }
            //});
        }




        private void criaFila(tbUser user)
        {
            //try
            //{
            //    using (var conection = connectionFactory.CreateConnection())
            //    using (var channel = conection.CreateModel())
            //    {
            //        var objSend = JsonSerializer.SerializeToUtf8Bytes(user);
            //        channel.BasicPublish(exchange: string.Empty, routingKey: queueGeradorName, basicProperties: null, body: objSend);                    
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }



        private void btnCriaUser_Click(object sender, RoutedEventArgs e)
        {
            //Salvando novo user e criando fila
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

        private void btnConsultaCorporate_Click(object sender, RoutedEventArgs e)
        {
            List<string> cnpjs = new List<string>();
            cnpjs.Add("17988531000137");
            cnpjs.Add("75677872000198");
            cnpjs.Add("01966262000178");
            cnpjs.Add("09571000000116");
            cnpjs.Add("80854045000162");
            cnpjs.Add("81095671000185");
            cnpjs.Add("76987106000192");
            cnpjs.Add("32218112000100");
            cnpjs.Add("05109318000129");
            cnpjs.Add("28583176000132");
            cnpjs.Add("11376223000174");
            cnpjs.Add("25333036000118");
            cnpjs.Add("17296370000110");
            cnpjs.Add("33536115000154");
            cnpjs.Add("84884014000105");
            cnpjs.Add("00871495000124");
            cnpjs.Add("33489895000129");
            cnpjs.Add("08302236000194");
            cnpjs.Add("01957400000152");
            cnpjs.Add("08985712000119");
            cnpjs.Add("35901658000159");
            cnpjs.Add("76756113000183");
            cnpjs.Add("07069427000195");
            cnpjs.Add("03613889000170");
            cnpjs.Add("81679672000177");
            cnpjs.Add("42226621000138");
            cnpjs.Add("34294041000150");
            cnpjs.Add("42247064000131");
            cnpjs.Add("01487893000104");
            cnpjs.Add("01868383000187");
            cnpjs.Add("75258772000127");
            cnpjs.Add("82467911000198");
            cnpjs.Add("78921095000173");
            cnpjs.Add("03016075000159");
            cnpjs.Add("79130647000198");
            cnpjs.Add("75350587000168");
            cnpjs.Add("34838905000157");
            cnpjs.Add("09305931000172");
            cnpjs.Add("61399093000163");
            cnpjs.Add("07184749000185");
            cnpjs.Add("32680463000138");
            cnpjs.Add("08179993000112");
            cnpjs.Add("26751219000116");
            cnpjs.Add("24139028000172");
            cnpjs.Add("10480261000100");
            cnpjs.Add("12615561000184");
            cnpjs.Add("27993606000121");
            cnpjs.Add("39582329000143");
            cnpjs.Add("17154029000120");


            var dados = new
            {
                Authorization = "Bearer eyJraWQiOiI5Q3NDXC9MNk9pSW56cU5QMmtnXC9aTW80OURlSDFuYUVIUWh1dmRDNitXWlU9IiwiYWxnIjoiUlMyNTYifQ.eyJzdWIiOiI0OWRtZTdhaDlocXQ3czIzdnRpdmpic3NoYiIsInRva2VuX3VzZSI6ImFjY2VzcyIsInNjb3BlIjoicHVyY2hhc2VcL2ludm9pY2Uuc2VuZCBzYWxlc1wvaW52b2ljZSBzYWxlc1wvaW52b2ljZS5waXN0YS5zZW5kIHBkdlwvc2lnbnVwIHNhbGVzXC9pbnZvaWNlLnJlYWQgZnVlbFwvc3VwcGx5LnNlbmQgc2FsZXNcL2ludm9pY2UuZGVsZXRlIGFiYXN0ZWNlYWktcXJjb2RlXC9xcmNvZGUuZ2VuZXJhdGUgcHJvZHVjdFwvaW52ZW50b3J5LnNlbmQgcHJvZHVjdFwvaW52ZW50b3J5IHNhbGVzXC9pbnZvaWNlLmpldG9pbC5zZW5kIGVtcGxveWVlXC9yZWNvcmQgY29ycG9yYXRlXC9jb21wb25lbnQgc2FsZXNcL2ludm9pY2UuYW1wbS5zZW5kIHB1cmNoYXNlXC9pbnZvaWNlIHNhbGVzXC9pbnZvaWNlLndyaXRlIGVtcGxveWVlXC9yZWNvcmQuc2VuZCBlbXBsb3llZVwvcmVjb3JkLnVwZGF0ZSBmdWVsXC9zdXBwbHkgY29ycG9yYXRlXC9jb21wb25lbnQucmVhZCBzYWxlc1wvaW52b2ljZS5zZW5kIiwiYXV0aF90aW1lIjoxNjM3OTUxNzk0LCJpc3MiOiJodHRwczpcL1wvY29nbml0by1pZHAudXMtZWFzdC0xLmFtYXpvbmF3cy5jb21cL3VzLWVhc3QtMV9PM2hvT2kzaG4iLCJleHAiOjE2Mzc5NTUzOTQsImlhdCI6MTYzNzk1MTc5NCwidmVyc2lvbiI6MiwianRpIjoiNzNlM2FjODgtYjNkNS00OTA4LWI5YTYtNGQ1ZDI0NDhhMjZjIiwiY2xpZW50X2lkIjoiNDlkbWU3YWg5aHF0N3MyM3Z0aXZqYnNzaGIifQ.GQH_5bvzm29ONhJWH9a6_eYqy9JzwkYXJrrzPRyLTb_MuoYRfZ1-LNHOF1C-UF8ZFz7Tj_-dL_weTPyL0iIosZKSpiJaSwYMPhNWLUcAzJYIg6z1mApJ-WoO55JF5FGDO6v1XcH_xPRAwb4uKOaTHGF5F_Ock2szu4LGcgPsppxH9x8PCeRgrL-hl1y82H1eW1TtJ_6hAkQ8hQfm76C4fjUJJFb8Tit7yK_sY-_uEASoHgfMI3YXH4EIJSmOS2YWH98jSmhMHHKmc4W_5cHZhi9WKR5p55pttLskKIve872HH0dvNxEyEPbqfYA4TzyU6gWExyTv64c_Bc4inF8CsA",
                Content_Type = "application/json"
            };

            string strFile = "Razao; Cnpj; codigoComponente; codigoPontoVenda; codigoZonaVenda; filialAbastecedora; pontoEntrega; codigoPontoDeVendaAbadi; \r\n";
            string strErros = "";


            cnpjs.ForEach(cnpj =>
            {
                //try
                //{
                    var retorno = ApiRequest.Request("https://corporate.prd.sinapseapis.com/component?cnpjComponente=" + cnpj, dados, "");
                    var json = JsonConvert.DeserializeObject<Corporate>(retorno);
                    Thread.Sleep(1000);

                    var c = json.listaComponente.FirstOrDefault(f => f.codigoTipoComponente == "1");

                    strFile += c.razaoSocial + ";" + cnpj + ";" + c.codigoComponente + ";" + c.codigoPontoVenda + ";" + c.codigoZonaVenda + ";" + c.filialAbastecedora + ";" + c.pontoEntrega + ";" + c.codigoPontoDeVendaAbadi + "\r\n";
                //}
                //catch (Exception ex)
                //{
                //    strErros += cnpj + "\r\n";
                //}
            });


            using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\CNPJS.csv"))
            {
                outfile.Write(strFile);
            }

            if (strErros != "")
            {
                using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Erros.txt"))
                {
                    outfile.Write(strErros);
                }
            }
        }
    }
}
