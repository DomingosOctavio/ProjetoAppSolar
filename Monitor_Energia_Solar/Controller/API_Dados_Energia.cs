
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace Monitor_Energia_Solar
{
    class API_Dados_Energia
    {


        public static async Task<List<Obj_API_Dados_Energia.TarifasArray>> API_Tarifas(CancellationToken cancellationToken, string Url)
        {

            List<Obj_API_Dados_Energia.TarifasArray> datalist = new List<Obj_API_Dados_Energia.TarifasArray>();

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
            using (var response = await client.SendAsync(request, cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                List<Obj_API_Dados_Energia.TarifasArray> datalist2 = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Obj_API_Dados_Energia.TarifasArray>>(content);

                return datalist2;
            }

        }



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
        public static async Task<Obj_API_Dados_Energia.Root_Bandeiras> CheckNetworkErrorCallAsyncBandeiras(CancellationToken cancellationToken, string Url)
        {
            Obj_API_Dados_Energia.Root_Bandeiras bandeiras = new Obj_API_Dados_Energia.Root_Bandeiras();

            try
            {
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
                using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
                {
                    var stream = await response.Content.ReadAsStreamAsync();

                    if (response.IsSuccessStatusCode)
                        return DeserializeJsonFromStream<Obj_API_Dados_Energia.Root_Bandeiras>(stream);

                    var content = await StreamToStringAsync(stream);
                    throw new ApiException
                    {
                        StatusCode = (int)response.StatusCode,
                        Content = content
                    };
                }
            }
            catch (Exception e)
            {
                string msg = e.Message;
                return bandeiras;

            }

        }

        //public static async Task<Obj_API_Dados_Energia.Root_Tarifas> CheckNetworkErrorCallAsyncTarifas(CancellationToken cancellationToken, string Url)
        //{
        //    Obj_API_Dados_Energia.Root_Tarifas root_Tarifas = new Obj_API_Dados_Energia.Root_Tarifas();
        //    using (var client = new HttpClient())
        //    using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
        //    using (var response = await client.SendAsync(request, cancellationToken))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var Result = await response.Content.ReadAsStringAsync();
        //        JObject o = JObject.Parse(Result);
        //        //Obj_API_Dados_Energia.Root_Tarifas obj_dados_energia2 = JsonConvert.     <Obj_API_Dados_Energia.Root_Tarifas>(Result);
        //        ////int result = obj_dados_energia2.TarifasArray.Count;
        //    }
        //        return root_Tarifas;
        //        //using (var client = new HttpClient())
        //        //using (var request = new HttpRequestMessage(HttpMethod.Get, Url))
        //        //using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
        //        //{
        //        //    var stream = await response.Content.ReadAsStreamAsync();

        //        //    if (response.IsSuccessStatusCode)
        //        //        return DeserializeJsonFromStream<Obj_API_Dados_Energia.Root_Tarifas>(stream);

        //        //    var content = await StreamToStringAsync(stream);
        //        //    throw new ApiException
        //        //    {
        //        //        StatusCode = (int)response.StatusCode,
        //        //        Content = content
        //        //    };
        //        //}
        //    }
    }
}
