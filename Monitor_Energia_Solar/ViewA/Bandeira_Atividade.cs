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

   

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.botoes_api_energia);

          
            TextView txtID = FindViewById<TextView>(Resource.Id.txtID);
            TextView txtMes = FindViewById<TextView>(Resource.Id.txtMes);
            ImageView imagem = FindViewById<ImageView>(Resource.Id.band);

            try
            {
                Bandeiras band = new Bandeiras();
                #pragma warning disable CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
                band.CallFromNonAsyncBandeiras(txtID, txtMes, imagem);
                #pragma warning restore CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
            }
            catch (Exception ex)
            {
                Android.App.AlertDialog.Builder alertDialoger = new Android.App.AlertDialog.Builder(this);
                alertDialoger.SetTitle("Atenção");
                alertDialoger.SetMessage(ex.Message);
                alertDialoger.SetNeutralButton("OK", delegate
                {
                    alertDialoger.Dispose();
                });
            }

       

        }







    }
}
