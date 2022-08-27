using Android.App;
using Android.Content;
using Android.Content.Res;
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
using System.Threading;
using System.Threading.Tasks;

namespace Monitor_Energia_Solar.Service
{
    public class BandeiraService
    {
        public async Task<Obj_API_Dados_Energia.Root_Bandeiras> GetBandeirasAPI(String url)
        {
                Obj_API_Dados_Energia.Root_Bandeiras objDadosBandeiras = new Obj_API_Dados_Energia.Root_Bandeiras();
            try
            {
            
                HttpClient client = new HttpClient();

                var response = await client.GetStringAsync(url);
                
                objDadosBandeiras =JsonConvert.DeserializeObject<Obj_API_Dados_Energia.Root_Bandeiras>(response);
                return objDadosBandeiras;

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

            return objDadosBandeiras;

        }

        public String PrimeiroDiaMes(DateTime dataAtual)
        {
            if (dataAtual.Month == 1)
            {
                return new DateTime(dataAtual.Year, 12, 1).ToString("yyyy/MM/dd");
            }
            else
            {
                return new DateTime(dataAtual.Year, dataAtual.Month - 2, 1).ToString("yyyy/MM/dd");
            }
        }


        public String UltimoDiaMes(DateTime dataAtual)
        {
  
            return  new DateTime(dataAtual.Year, dataAtual.Month, DateTime.DaysInMonth(dataAtual.Year, dataAtual.Month - 1)).ToString("yyyy/MM/dd");
    
        }

        public void DefinirBandeira(TextView txtID, TextView txtMes, ImageView imagem, Obj_API_Dados_Energia.Root_Bandeiras objBandeiras)
        {
            txtMes.Text = "Última atualização: " + objBandeiras.items[0].Month.ToString();

            if (objBandeiras.items[0].FlagType == 0) //bandeira verde
            {
             
                imagem.SetImageResource(Resource.Drawable.bandeira_verde);
                txtID.Text = "Condições favoráveis para geração de energia no Brasil.\n A tarifa não sofre nenhum acréscimo.";

            }
            else if (objBandeiras.items[0].FlagType == 1) //bandeira amarela
            {
        
                imagem.SetImageResource(Resource.Drawable.bandeira_amarela);
                txtID.Text = "Condições menos favoráveis para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + objBandeiras.items[0].Value.ToString() + "/KWh";
            }
            else if (objBandeiras.items[0].FlagType == 2) //bandeira vermelha
            {
      
                imagem.SetImageResource(Resource.Drawable.bandeira_vermelha);
                txtID.Text = "Condições mais custosas para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + objBandeiras.items[0].Value.ToString() + "/KWh" + "para o patamar1";
            }
            else  //bandeira vermelha 2
            {
            
                imagem.SetImageResource(Resource.Drawable.bandeira_vermelha2);
                txtID.Text = "Condições mais custosas para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + objBandeiras.items[0].Value.ToString() + "/KWh" + "para o patamar2";
            }
        }
    }
}