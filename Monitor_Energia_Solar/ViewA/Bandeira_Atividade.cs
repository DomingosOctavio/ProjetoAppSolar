using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Net;
using Monitor_Energia_Solar.Service;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "", Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    public class Bandeira_Atividade : AppCompatActivity
    {
   
   
        // List<string> lista_string = new List<string>();
        HttpClient client = new HttpClient();

        public override void OnBackPressed()
        {
            this.Finish();
        }

        public async Task GetServiceBandeirasAsync(TextView txtID, TextView txtMes, ImageView imagem, string dataInicial, string dataFinal)
        {
            BandeiraService bandeirasService = new BandeiraService();

            Obj_API_Dados_Energia.Root_Bandeiras objBandeiras = new Obj_API_Dados_Energia.Root_Bandeiras();


            string finalurl = bandeirasService.DefinirData();
            string url = "https://apise.way2.com.br/v1/bandeiras?apikey=ad55f064ab884c6d8157fe4de92bd1ef" + finalurl;
            objBandeiras = await bandeirasService.GetBandeirasAPI(url);

            if (objBandeiras.items.Count == 0)
            {
                string finalurl2 = bandeirasService.DefinirData();
                string url2 = "https://apise.way2.com.br/v1/tarifas?apikey=ef061b3334da4721b330af853a39f73a" + finalurl2;
                objBandeiras = await bandeirasService.GetBandeirasAPI(url2);
            }
            if(objBandeiras.items.Count == 0)
            {
               
                Android.App.AlertDialog.Builder alertDialoger = new Android.App.AlertDialog.Builder(this);
                alertDialoger.SetTitle("Atenção");
                alertDialoger.SetMessage("Ocorreu um erro ao consultar o serviço");
                alertDialoger.SetNeutralButton("OK", delegate
                {
                    return;
                });
            }
            bandeirasService.DefinirBandeira(txtID, txtMes, imagem, objBandeiras);
        }


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.botoes_api_energia);

          
            TextView txtID = FindViewById<TextView>(Resource.Id.txtID);
            TextView txtMes = FindViewById<TextView>(Resource.Id.txtMes);
            ImageView imagem = FindViewById<ImageView>(Resource.Id.band);
        
            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
            Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertbuilder.SetView(view);
            Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
            dialog.Show();

            try
            { 
                
                string datainicial = "";
                string datafinal = "";



                #pragma warning disable CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
                GetServiceBandeirasAsync(txtID, txtMes, imagem, datainicial, datafinal);
#pragma warning restore CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
                alertbuilder.Dispose();
            }
            catch (Exception ex)
            {
                alertbuilder.Dispose();
                Android.App.AlertDialog.Builder alertDialoger = new Android.App.AlertDialog.Builder(this);
                alertDialoger.SetTitle("Atenção");
                alertDialoger.SetMessage(ex.Message);
                alertDialoger.SetNeutralButton("OK", delegate
                {
                    //dialog.Cancel();
                });
            }

            alertbuilder.Dispose();

        }
    }
}
