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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "", Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;
        CancellationTokenSource _tokenSource = null;
        TextView textView_session;
        TextView textView_ip;
        public static string IP_atual;


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

            // Get User ID
            Context mContext = Android.App.Application.Context;

            Session_Usuario ap = new Session_Usuario(mContext);
            string UserID = ap.getAccessKey().ToString();
            //  objCommon = new CommonServices();

            Context mContext1 = Android.App.Application.Context;
            Session_Token ap2 = new Session_Token(mContext1);
            string UserID2 = ap2.getAccessKey().ToString();

            Context mContext2 = Android.App.Application.Context;
            Session_Conexao ap3 = new Session_Conexao(mContext2);
            string UserID3 = ap3.getAccessKey().ToString();


            Context mContext5 = Android.App.Application.Context;
            Session_Token ap5 = new Session_Token(mContext5);
            string UserID5 = ap2.getAccessKey().ToString();
            BancoLogin banco = new BancoLogin();
            IP_atual = "http://" + banco.ConsultarIp(UserID5) + "/";

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
                    StartActivity(typeof(Bandeira_Atividade));
                    return true;
            }
            return false;
        }


        public delegate void TimerCallback(object state);


        public async Task<Obj_Dados_WebServer> GetDadosAsync()
        {
            var response = await client.GetStringAsync(IP_atual);
            var dados = JsonConvert.DeserializeObject<Obj_Dados_WebServer>(response);
            return dados;
        }

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
                Obj_Dados_WebServer obj_Dados_WebServer2 = new Obj_Dados_WebServer();

                obj_Dados_WebServer2 = await GetDadosAsync();
                progress.Visibility = ViewStates.Visible;
                return obj_Dados_WebServer2;

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
                    textView_corrente.Visibility = Android.Views.ViewStates.Visible;
                    textView_luminosidade.Visibility = Android.Views.ViewStates.Visible;
                    textView_potencia.Visibility = Android.Views.ViewStates.Visible;
                    textView_horario.Visibility = Android.Views.ViewStates.Visible;


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
