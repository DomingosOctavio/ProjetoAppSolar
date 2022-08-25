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
    public class Bandeira_Atividade : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
   
   
        // List<string> lista_string = new List<string>();
        HttpClient client = new HttpClient();

        public override void OnBackPressed()
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editer = pref.Edit();
            editer.Remove("PREFERENCE_ACCESS_KEY").Commit(); ////Remove Spec key values  

            this.Finish();

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.myMenu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.file_settings)
            {
                // do something here... 
                return true;
            }
            return base.OnOptionsItemSelected(item);
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

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);

       

        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.bandeira:
                    StartActivity(typeof(Bandeira_Atividade));
                    return true;
                case Resource.Id.tarifa:
                    StartActivity(typeof(Tarifas_Atividade));
                    return true;

            }
            return false;
        }






    }
}
