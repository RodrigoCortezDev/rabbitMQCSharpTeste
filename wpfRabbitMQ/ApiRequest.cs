using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace wpfRabbitMQ.Postgres
{
    public class ApiRequest
    {
        /// <summary>
        /// Cria um HttpWebRequest e serializa o retorno json para o tipo T informado -  Pode ser lançado uma exceção nesse método.
        /// </summary>
        public static T JsonRequest<T>(string url, int timeout = 30000) where T : class
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = timeout;

            using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(string.Format("Server error (HTTP {0}: {1}).", response.StatusCode,
                        response.StatusDescription));
                }

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(T));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());

                return objResponse as T;
            }
        }

        public static HttpStatusCode RequestStatusCode(string url, int timeout = 30000)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = timeout;

            using (HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse)
            {
                if (response == null)
                    throw new InvalidOperationException("Erro na requisição");

                return response.StatusCode;
            }
        }

        public static HttpStatusCode RequestStatusCode(string url, object dados, int timeout = 30000)
        {
            // começa criar a solicitação passando os parâmetros de URL, tipo de retorno json e timeout
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = timeout;

            // serializa o objeto que será recebido pelo controller
            string jsonRequest = JsonSerializer.Serialize(dados);

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(jsonRequest);
            request.ContentLength = bytes.Length;

            using (var requestStream = request.GetRequestStream())
            {
                // escreve/adiciona na solicitação o json que já foi convertido em bytes
                requestStream.Write(bytes, 0, bytes.Length);

                // começa fazer a leitura da resposta do servidor
                using (var responseStream = (HttpWebResponse)request.GetResponse())
                {
                    return responseStream.StatusCode;
                }
            }
        }

        public static T JsonRequest<T>(string url, object dados, int timeout = 30000)
        {

            // começa criar a solicitação passando os parâmetros de URL, tipo de retorno json e timeout
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Timeout = timeout;

            // serializa o objeto que será recebido pelo controller
            string jsonRequest = JsonSerializer.Serialize(dados);

            var encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(jsonRequest);
            request.ContentLength = bytes.Length;

            using (var requestStream = request.GetRequestStream())
            {
                // escreve/adiciona na solicitação o json que já foi convertido em bytes
                requestStream.Write(bytes, 0, bytes.Length);

                // começa fazer a leitura da resposta do servidor
                using (var responseStream = (HttpWebResponse)request.GetResponse())
                {
                    using (var responseReader = new StreamReader(responseStream.GetResponseStream()))
                    {
                        var jsonResponse = responseReader.ReadToEnd();

                        try
                        {
                            return JsonSerializer.Deserialize<T>(jsonResponse);
                        }
                        catch (Exception)
                        {
                            throw new Exception("Erro na serialização do retorno da requisição");
                        }
                    }
                }
            }
        }

        public static T XmlRequest<T>(string url, int timeout = 30000)
        {

            // começa criar a solicitação passando os parâmetros de URL, tipo de retorno json e timeout
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/xml";
            request.Timeout = timeout;

            // começa fazer a leitura da resposta do servidor
            using (var responseStream = (HttpWebResponse)request.GetResponse())
            {
                using (var responseReader = new StreamReader(responseStream.GetResponseStream()))
                {
                    var xmlString = responseReader.ReadToEnd();

                    try
                    {
                        var serializer = new XmlSerializer(typeof(T));

                        using (TextReader reader = new StringReader(xmlString))
                        {
                            var result = (T)serializer.Deserialize(reader);
                            return result;
                        }
                    }
                    catch (Exception)
                    {
                        throw new Exception("Erro na serialização do retorno da requisição");
                    }
                }
            }

        }


        public static string Request(string urlbase, object header, string metodo, bool blnEnviaPost = false, int intTimeOut = 30)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlbase);
                client.Timeout = new TimeSpan(0, 0, intTimeOut);
                if (header != null)
                {
                    foreach (var item in header.GetType().GetProperties())
                        client.DefaultRequestHeaders.Add(item.Name, (string)GetPropValue(header, item.Name));
                }
                string reposta;

                HttpResponseMessage response = null;
                if (!blnEnviaPost)
                    response = client.GetAsync(metodo).Result;
                else
                    response = client.PostAsync(metodo, null).Result;

                using (HttpContent content = response.Content)
                {
                    Task<string> result = content.ReadAsStringAsync();
                    reposta = result.Result;
                }
                return reposta;
            }
        }


        public static HttpResponseMessage RequestMessage(string urlbase, object header, string metodo, bool blnEnviaPost = false)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlbase);
                if (header != null)
                {
                    foreach (var item in header.GetType().GetProperties())
                        client.DefaultRequestHeaders.Add(item.Name, (string)GetPropValue(header, item.Name));
                }

                HttpResponseMessage response = null;
                if (!blnEnviaPost)
                    response = client.GetAsync(metodo).Result;
                else
                    response = client.PostAsync(metodo, null).Result;

                return response;
            }
        }



        private static object GetPropValue(object src, string propName)
        {
            var propertyInfo = src.GetType().GetProperty(propName);
            return propertyInfo?.GetValue(src, null);
        }
    }
}
