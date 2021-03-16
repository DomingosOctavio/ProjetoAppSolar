using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.IO;

namespace Monitor_Energia_Solar
{
    class Dados_Json_WebServer_Arduino
    {

        //public static async Task<Obj_Dados_WebServer> PostCallAPI(string url)
        //{
        //    Obj_Dados_WebServer obj_Dados_WebServer2 = new Obj_Dados_WebServer();
        //    try
        //    {
        //        HttpClient client = new HttpClient();
                
        //            var content = new StringContent(obj_Dados_WebServer2.ToString(), Encoding.UTF8, "application/json");
        //            var response = await client.PostAsync(url, content);
        //            if (response != null)
        //            {
        //                var jsonString = await response.Content.ReadAsStringAsync();
        //                return JsonConvert.DeserializeObject<Obj_Dados_WebServer>(jsonString);
        //            }
                
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
               
        //    }
        //    return obj_Dados_WebServer2;

        //}











        ////List<string> lista_tensao_tempo_real = new List<string>();
        //public async Task<Obj_Dados_WebServer> Iniciar_API_Tensao(HttpClient client, string url)
        //{
        //    #pragma warning disable IDE0017 // Simplificar a inicialização de objeto
        //    //var client = new HttpClient();
        //    #pragma warning restore IDE0017 // Simplificar a inicialização de objeto

        //    client.BaseAddress = new Uri(url);

        //    HttpResponseMessage response = await client.GetAsync(url);        
        //    Obj_Dados_WebServer obj_Dados_WebServer = new Obj_Dados_WebServer();

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var Result = await response.Content.ReadAsStringAsync();
        //        Obj_Dados_WebServer datalist = JsonConvert.DeserializeObject<Obj_Dados_WebServer>(Result);

        //        obj_Dados_WebServer.Tensao = datalist.Tensao;
        //        obj_Dados_WebServer.Corrente = datalist.Corrente;
        //        obj_Dados_WebServer.Luminosidade = datalist.Luminosidade;
        //        obj_Dados_WebServer.Horario = datalist.Horario;

        //    }
        //    await Task.Delay(100);
        //    return obj_Dados_WebServer;
        //}


        //public static async Task<Obj_Dados_WebServer> CheckNetworkErrorCallAsync(CancellationToken cancellationToken, string Url)
        //{
        //    using (var client = new HttpClient())
        //    using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
        //    using (var response = await client.SendAsync(request, cancellationToken))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var content = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<Obj_Dados_WebServer>(content);
        //    }
        //}

        public class ApiException : Exception
        {
            public int StatusCode { get; set; }

            public string Content { get; set; }
        }

        public static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }

        public static T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new Newtonsoft.Json.JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }
        public static async Task<Obj_Dados_WebServer> DeserializeOptimizedFromStreamCallAsync(CancellationToken cancellationToken, string Url)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                    return DeserializeJsonFromStream<Obj_Dados_WebServer>(stream);

                var content = await StreamToStringAsync(stream);
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }
        }



    }
}


