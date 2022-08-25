using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Android.Views;

namespace Monitor_Energia_Solar
{
    class Bandeiras
    {
        CancellationTokenSource _tokenSource = null;
        public async Task CallFromNonAsyncBandeiras(TextView txtID, TextView txtMes, ImageView imagem)
        {
            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

            Obj_API_Dados_Energia.Root_Bandeiras obj_bandeiras = new Obj_API_Dados_Energia.Root_Bandeiras();

            DateTime data = DateTime.Today;

            //DateTime com o primeiro dia do mês

            int mes = data.Month;

            DateTime primeiroDiaDoMes;

            if (data.Month == 1)
            {
                primeiroDiaDoMes = new DateTime(data.Year, 12, 1);
            }
            else
            {
                primeiroDiaDoMes = new DateTime(data.Year, data.Month - 1, 1);
            }

            //DateTime com o último dia do mês
            DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month - 1));



            try
            {
                string url = "https://www.apidosetoreletrico.com.br/api/energy-providers/tariff-flags?monthStart=" + primeiroDiaDoMes.ToString("yyyy/MM/dd") + "&monthEnd=" + ultimoDiaDoMes.ToString("yyyy/MM/dd");

                var api1Task2 = API_Dados_Energia.CheckNetworkErrorCallAsyncBandeiras(token, url);
                obj_bandeiras = await api1Task2;
                //if (obj_bandeiras.items.Count > 0)
                //{
                //    teste_bandeiras = true;
                //}
            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                string mensagem = e.Message;
            }
            catch (Exception e)
            {
                string mensagem = e.Message;

            }


            txtMes.Text = "Última atualização: " + obj_bandeiras.items[0].Month.ToString();

            if (obj_bandeiras.items[0].FlagType == 0) //bandeira verde
            {
            
                imagem.SetImageResource(Resource.Drawable.bandeira_verde);
                txtID.Text = "Condições favoráveis para geração de energia no Brasil.\n A tarifa não sofre nenhum acréscimo.";

            }
            else if (obj_bandeiras.items[0].FlagType == 1) //bandeira amarela
            {

                imagem.SetImageResource(Resource.Drawable.bandeira_amarela);
                txtID.Text = "Condições menos favoráveis para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + obj_bandeiras.items[0].Value.ToString() + "/KWh";
            }
            else if (obj_bandeiras.items[0].FlagType == 2) //bandeira vermelha
            {
       
                imagem.SetImageResource(Resource.Drawable.bandeira_vermelha);
                txtID.Text = "Condições mais custosas para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + obj_bandeiras.items[0].Value.ToString() + "/KWh" + "para o patamar1";
            }
            else  //bandeira vermelha 2
            {
              
                imagem.SetImageResource(Resource.Drawable.bandeira_vermelha2);
                txtID.Text = "Condições mais custosas para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + obj_bandeiras.items[0].Value.ToString() + "/KWh" + "para o patamar2";
            }
        }
    }
}