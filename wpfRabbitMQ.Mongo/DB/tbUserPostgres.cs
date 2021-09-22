using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfRabbitMQ.Mongo.DB
{
    public class tbUserPostgres
    {
        public long user_pk { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
        public string user_senha { get; set; }
    }
}
