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
using static Monitor_Energia_Solar.Obj_API_Dados_Energia;

namespace Monitor_Energia_Solar.Service
{
    public class TarifasService
    {
        public async Task <List<Obj_API_Dados_Energia.TarifasArray>> GetTarifasAPI(String url)
        {

            

            List<Obj_API_Dados_Energia.TarifasArray> ListDadosTarifas = new List<Obj_API_Dados_Energia.TarifasArray>();
            try
            {

                HttpClient client = new HttpClient();

                var response = await client.GetStringAsync(url);

                ListDadosTarifas = JsonConvert.DeserializeObject<List<Obj_API_Dados_Energia.TarifasArray>>(response);
                
                return ListDadosTarifas;

            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                string mensagem = e.Message;

            }
            catch (Exception e)
            {
                string mensagem = e.Message;

            }
            //string Urlbandeiras = StringsUrl.ulrBandeiras;

            return ListDadosTarifas;

        }
    }
}