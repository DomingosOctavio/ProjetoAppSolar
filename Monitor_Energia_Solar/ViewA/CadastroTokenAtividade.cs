using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "CadastroTokenAtividade")]
    public class CadastroTokenAtividade : Activity
    {
        Button btn_proximo;
        EditText btn_token;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.cadastro);
            btn_token = FindViewById<EditText>(Resource.Id.token);
          

            btn_proximo = FindViewById<Button>(Resource.Id.proximo);
            btn_proximo.Click += BtnProximo_Click;
       
      
        }
        private int VerificarToken(string token)
        {
            BancoLogin bancoLogin =  new BancoLogin();
            return bancoLogin.Consulta_token(token);

        }

       

        private void BtnProximo_Click(object sender, EventArgs e)
        {
            if (VerificarToken(btn_token.Text) > 0)
            {


                //Context mContext1 = Android.App.Application.Context;
                //Session_Token cod = new Session_Token(mContext1);
                //cod.saveAccessKey(btn_token.Text);

                //Toast.MakeText(this, "Token encontrado!", ToastLength.Short).Show();
                //var intent = new Intent(this, typeof(DadosPessoaisAtividade));
                //intent.SetFlags(ActivityFlags.NewTask);
                ////Navigation to SecondActivity
                //StartActivity(intent);
                ////delete main activity from navigation
                ////Finish();

            }
            else
            {
                 Toast.MakeText(this, "Token não encontrado!", ToastLength.Short).Show();
            }




        }
    }
}