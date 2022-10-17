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
using Monitor_Energia_Solar.Controller;
using Monitor_Energia_Solar.Service;
using Monitor_Energia_Solar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Android.Graphics;
using AlertDialog = Android.App.AlertDialog;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "", Theme = "@style/AppTheme", Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        TextView textMessage;
        TextView textView_session;
        TextView textView_ip;
        public static string IP_atual;


        public override void OnBackPressed()
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Atenção");
            alert.SetIcon(Resource.Drawable.sair);
            alert.SetMessage("Deseja realmente sair da aplicação?");

            alert.SetPositiveButton("sim", (senderAlert, args) =>
            {
                this.FinishAffinity();
            });

            alert.SetNegativeButton("Não", (senderAlert, args) =>
            {
                return;
            });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.myMenu, menu);
            return base.OnPrepareOptionsMenu(menu);


        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.about_app)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("DOMINGOS OCTAVIO DIAS FERRAN SOUZA");
                alert.SetIcon(Resource.Drawable.pessoa);
                alert.SetMessage("Aplicativo para apresentação do TCC 2022 // FAETERJ -RJ");

                alert.SetPositiveButton("OK", (senderAlert, args) =>
                {
                    alert.Dispose();
                });
                Dialog dialog = alert.Create();
                dialog.Show();
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

            var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);

            string usuario = dadosUsuario.GetString("Usuario", null);
            string token = dadosUsuario.GetString("Token", null);
            string ip = dadosUsuario.GetString("Ip", null);
            string senha = dadosUsuario.GetString("Senha", null);

            //setar ip e usuario
            TextView usuarioLog = FindViewById<TextView>(Resource.Id.session);
            TextView ipLog = FindViewById<TextView>(Resource.Id.ip);

            MainActivityFirebaseController controler = new MainActivityFirebaseController();
            if (ip == null)
            {
                IP_atual = controler.RetrieveIP(token);
                var usuarioEdit = dadosUsuario.Edit();
                usuarioEdit.PutString("Ip", IP_atual);
            }
            else
            {
                IP_atual = ip;
            }
           

            IP_atual = "http://" + IP_atual + "/";
            usuarioLog.Text = "USUÁRIO: " + usuario;
            ipLog.Text = "IP: " + IP_atual; ;
        }


        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    StartActivity(typeof(GraficoAtividade));
           
                    return true;
                case Resource.Id.navigation_dashboard:
                    StartActivity(typeof(Escolha_BussolaInclinacao));
                    return true;
                case Resource.Id.navigation_notifications:
                    StartActivity(typeof(EscolhaTarifaBandeira));
                    return true;
            }
            return false;
        }


        public delegate void TimerCallback(object state);

        public async Task<Obj_Dados_WebServer> Executar()
        {

            ProgressBar progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            Obj_Dados_WebServer obj_Dados = new Obj_Dados_WebServer();
            obj_Dados.Tensao = 0;
            obj_Dados.Corrente = 0;
            obj_Dados.Luminosidade = 0;
            obj_Dados.Horario = "00:00:00";


            try
            {
                Obj_Dados_WebServer obj_Dados_WebServer2 = new Obj_Dados_WebServer();
                ArduinoDadosService arduinoDados = new ArduinoDadosService();
                
                progress.Visibility = ViewStates.Visible;

                return await arduinoDados.GetDadosAsync(IP_atual);

            }
            #pragma warning disable CS0168 // A variável foi declarada, mas nunca foi usada
            catch (NoRouteToHostException e)
            #pragma warning restore CS0168 // A variável foi declarada, mas nunca foi usada
            {
                obj_Dados.mensagem = "Erro de conexão ao endereço remoto...";
                return obj_Dados;
            }
            #pragma warning disable CS0168 // A variável foi declarada, mas nunca foi usada
            catch (Exception e)
            #pragma warning restore CS0168 // A variável foi declarada, mas nunca foi usada
            {
                obj_Dados.mensagem = "Sem conexão com o Arduino...";
                return obj_Dados;
            }

        }

        public void TimerProc(object state)
        {

            TextView sincronizando_erro= FindViewById<TextView>(Resource.Id.sincronizando); //visivel

            TextView textView_corrente = FindViewById<TextView>(Resource.Id.corrente);
            TextView textView_luminosidade = FindViewById<TextView>(Resource.Id.lumisosidade);
            TextView textView_potencia = FindViewById<TextView>(Resource.Id.potencia);
            TextView textView_horario = FindViewById<TextView>(Resource.Id.horario);
            TextView label_tensao = FindViewById<TextView>(Resource.Id.label_tensao);

            RelativeLayout layoutMultimetro = FindViewById<RelativeLayout>(Resource.Id.rel_tensao);
            RelativeLayout relativeTextos = FindViewById<RelativeLayout>(Resource.Id.rel_anything);

            TextView textView_font = FindViewById<TextView>(Resource.Id.tensao);

            Android.Graphics.Typeface tf = Android.Graphics.Typeface.CreateFromAsset(Assets, "Digital Display.ttf");
            textView_font.SetTypeface(tf, TypefaceStyle.Normal);

            ProgressBar progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);



            RunOnUiThread(async () =>
            {

            Obj_Dados_WebServer retornoWebService = await Executar();

            progress.Indeterminate = true;

            if (String.Compare(retornoWebService.mensagem, "Erro de conexão ao endereço remoto...") == 0 || String.Compare(retornoWebService.mensagem, "Sem conexão com o Arduino...") == 0)
            {
                //textView_corrente.Visibility = Android.Views.ViewStates.Invisible;
                //textView_luminosidade.Visibility = Android.Views.ViewStates.Invisible;
                //textView_potencia.Visibility = Android.Views.ViewStates.Invisible;
                //textView_horario.Visibility = Android.Views.ViewStates.Invisible;

                relativeTextos.Visibility = Android.Views.ViewStates.Invisible;
                layoutMultimetro.Visibility = Android.Views.ViewStates.Invisible;
                label_tensao.Visibility = ViewStates.Gone;

                sincronizando_erro.Text = retornoWebService.mensagem;
                progress.Visibility = ViewStates.Visible;
            }
            else
            {
                //textView_corrente.Visibility = Android.Views.ViewStates.Visible;
                //textView_luminosidade.Visibility = Android.Views.ViewStates.Visible;
                //textView_potencia.Visibility = Android.Views.ViewStates.Visible;
                //textView_horario.Visibility = Android.Views.ViewStates.Visible;
                relativeTextos.Visibility = Android.Views.ViewStates.Visible;
                layoutMultimetro.Visibility = Android.Views.ViewStates.Visible;
                label_tensao.Visibility = ViewStates.Visible;
                sincronizando_erro.Visibility = ViewStates.Gone;


                textView_corrente.Text = "Corrente (A): " + retornoWebService.Corrente.ToString();
                textView_luminosidade.Text = "Luminosidade: " + retornoWebService.Luminosidade.ToString();

                float potencia = retornoWebService.Tensao * retornoWebService.Corrente;
                textView_potencia.Text = "Potência (W): " + String.Format("{0:0.00}", potencia);

                    //teste

                 


                    textView_horario.Text = retornoWebService.Horario.ToString();
                    
                    float texto_tensao = retornoWebService.Tensao;
                    textView_font.Text = String.Format("{0:0.00}", texto_tensao);

                    progress.Visibility = ViewStates.Invisible;
              
                  

                }
            });
        }
    }
}
