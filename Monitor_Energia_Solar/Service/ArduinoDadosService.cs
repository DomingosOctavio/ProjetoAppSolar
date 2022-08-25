using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Monitor_Energia_Solar.Service
{
    public class ArduinoDadosService
    {
        public async Task<Obj_Dados_WebServer> GetDadosAsync(String IP_atual)
        {
            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync(IP_atual);
            var dados = JsonConvert.DeserializeObject<Obj_Dados_WebServer>(response);
            return dados;
        }
    }
}