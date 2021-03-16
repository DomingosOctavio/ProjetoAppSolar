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
    [Activity(Label = "",Theme = "@style/AppTheme", Icon ="@drawable/icon")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;
        CancellationTokenSource  _tokenSource = null;
        TextView textView_session;


        // List<string> lista_string = new List<string>();
        HttpClient client = new HttpClient();

        public override void OnBackPressed()
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editer = pref.Edit();
            editer.Remove("PREFERENCE_ACCESS_KEY").Commit(); ////Remove Spec key values  

            Finish();

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

            SetContentView(Resource.Layout.activity_main);

            textMessage = FindViewById<TextView>(Resource.Id.message);

            int seconds = 120;


            //var timer = new System.Threading.Timer(TimerProc, null, 0, seconds);
            var timer = new System.Threading.Timer(TimerProc, null, 0, seconds);




            textView_session = FindViewById<TextView>(Resource.Id.session);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);


            // session
            Context mContext = Android.App.Application.Context;
            Session_classe ap = new Session_classe(mContext);
            string key = ap.getAccessKey();
            textView_session.Text = key.ToString();
            //fim session



        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    StartActivity(typeof(GraficoAtividade));
                    return true;
                case Resource.Id.navigation_dashboard:
                      StartActivity(typeof(Escolha_Instrucao_Atividade));
                    return true;
                case Resource.Id.navigation_notifications:
                    StartActivity(typeof(API_Dados_Energia_Atividade));
                    return true;
            }
            return false;
        }
      

        public delegate void TimerCallback(object state);

        public async Task<Obj_Dados_WebServer> Executar()
        {

            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

            ProgressBar progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            Obj_Dados_WebServer obj_Dados = new Obj_Dados_WebServer();
            obj_Dados.Tensao = 0;
            obj_Dados.Corrente = 0;
            obj_Dados.Luminosidade = 0;
            obj_Dados.Horario = "00:00:00";


            try
            {
                Dados_Json_WebServer_Arduino retorno = new Dados_Json_WebServer_Arduino();
                Obj_Dados_WebServer obj_Dados_WebServer2 = new Obj_Dados_WebServer();

                //var api1Task = Dados_Json_WebServer_Arduino.PostCallAPI("http://192.168.43.254/");

                //var api1Task2 = Dados_Json_WebServer_Arduino.CheckNetworkErrorCallAsync(token, "http://192.168.43.254/");
                //var api1Task = retorno.Iniciar_API_Tensao(client, "http://192.168.43.254/");
                var api1Task2 = Dados_Json_WebServer_Arduino.DeserializeOptimizedFromStreamCallAsync(token, "http://192.168.43.254/");
               

                obj_Dados_WebServer2 = await api1Task2;
                progress.Visibility = ViewStates.Visible;
                return obj_Dados_WebServer2;


                //TextView textView = FindViewById<TextView>(Resource.Id.tensao_atual);

            }
            #pragma warning disable CS0168 // A variável foi declarada, mas nunca foi usada
            catch (NoRouteToHostException e)
            #pragma warning restore CS0168 // A variável foi declarada, mas nunca foi usada
            {
               
            
                obj_Dados.mensagem = "Tentando Reconectar...";
                return obj_Dados;


            }
            #pragma warning disable CS0168 // A variável foi declarada, mas nunca foi usada
            catch (Exception e)
            #pragma warning restore CS0168 // A variável foi declarada, mas nunca foi usada
            {
             
            
                obj_Dados.mensagem = "Erro: Verifique a Conexão da internet...";
                return obj_Dados;

               

            }
            
        }



        ///Task t2 = Task.Factory.StartNew(object state);


        public void TimerProc(object state)
        {

            TextView textView = FindViewById<TextView>(Resource.Id.tensao_atual);
            TextView textView_corrente = FindViewById<TextView>(Resource.Id.corrente);
            TextView textView_luminosidade = FindViewById<TextView>(Resource.Id.lumisosidade);
            TextView textView_potencia = FindViewById<TextView>(Resource.Id.potencia);
            TextView textView_horario = FindViewById<TextView>(Resource.Id.horario);

            ProgressBar progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);

            RelativeLayout relative = FindViewById<RelativeLayout>(Resource.Id.rel_anything);


            RunOnUiThread(async () =>
           {

               Obj_Dados_WebServer retorno_tensao = await Executar();

               progress.Indeterminate = true;

               if (String.Compare(retorno_tensao.mensagem, "Tentando Reconectar...") == 0 || String.Compare(retorno_tensao.mensagem, "Erro: Verifique a Conexão da internet...") == 0)
               {
                   textView_corrente.Visibility = Android.Views.ViewStates.Invisible;
                   textView_luminosidade.Visibility = Android.Views.ViewStates.Invisible;
                   textView_potencia.Visibility = Android.Views.ViewStates.Invisible;
                   textView_horario.Visibility = Android.Views.ViewStates.Invisible;
                   relative.Visibility = Android.Views.ViewStates.Invisible;

                   textView.Text = retorno_tensao.mensagem;
                   progress.Visibility = ViewStates.Visible;
               }
               else
               {
                   textView_corrente.Visibility     = Android.Views.ViewStates.Visible;
                   textView_luminosidade.Visibility = Android.Views.ViewStates.Visible;
                   textView_potencia.Visibility     = Android.Views.ViewStates.Visible;
                   textView_horario.Visibility      = Android.Views.ViewStates.Visible;
            

                   textView_corrente.Text = "Corrente: " + retorno_tensao.Corrente.ToString();
                   textView_luminosidade.Text = "Luminosidade" + retorno_tensao.Luminosidade.ToString();
                   textView_potencia.Text = "Potência: " + (retorno_tensao.Tensao * retorno_tensao.Corrente).ToString() + " Watts";
                   textView_horario.Text = retorno_tensao.Horario.ToString();

                   progress.Visibility = ViewStates.Invisible;
                   relative.Visibility = Android.Views.ViewStates.Visible;
                   textView.Text = retorno_tensao.Tensao.ToString();
               }
           });
        }
    }
}
