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

        public async Task GetServiceBandeirasAsync(TextView txtID, TextView txtMes, ImageView imagem)
        {
            BandeiraService bandeirasService = new BandeiraService();

            Obj_API_Dados_Energia.Root_Bandeiras objBandeiras = new Obj_API_Dados_Energia.Root_Bandeiras();

            objBandeiras = await bandeirasService.GetBandeirasAPI(StringsUrl.ulrBandeiras + bandeirasService.PrimeiroDiaMes(DateTime.Today) + "&monthEnd=" +bandeirasService.UltimoDiaMes(DateTime.Today));
            bandeirasService.DefinirBandeira(txtID, txtMes, imagem, objBandeiras);
        }

   

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.botoes_api_energia);

          
            TextView txtID = FindViewById<TextView>(Resource.Id.txtID);
            TextView txtMes = FindViewById<TextView>(Resource.Id.txtMes);
            ImageView imagem = FindViewById<ImageView>(Resource.Id.band);
            //TextView txtBand = FindViewById<TextView>(Resource.Id.txtBandeira);


            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
            Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
            alertbuilder.SetView(view);
            Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
            dialog.Show();

            try
            {
                //int sleepTime = 10000; // in mills
                //Task.Delay(sleepTime).Wait();
                //// or
                //Thread.Sleep(sleepTime);
#pragma warning disable CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
                GetServiceBandeirasAsync(txtID, txtMes, imagem);
#pragma warning restore CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída

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
