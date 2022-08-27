using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "DadosPessoaisAtividade")]
    public class DadosPessoaisAtividade : Activity
    {




        Button btn_concluir;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DadosPessoais);

         


            btn_concluir = FindViewById<Button>(Resource.Id.concluir);
            btn_concluir.Click += Btnconcluir_Click;
        }
        private void Btnconcluir_Click(object sender, EventArgs e)
        {
            EditText txt_usuario = FindViewById<EditText>(Resource.Id.usuario);
            EditText txt_senha = FindViewById<EditText>(Resource.Id.senha);
            EditText txt_email = FindViewById<EditText>(Resource.Id.email);

            Context mContext = Android.App.Application.Context;

       
            var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
            var usuarioEdit = dadosUsuario.Edit();
            usuarioEdit.PutString("Usuario", txt_usuario.Text);
            usuarioEdit.PutString("Senha", txt_senha.Text);
            usuarioEdit.Commit();

            var dadoToken = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
            string token = dadoToken.GetString("Codigo", null);
 

            var intent = new Intent(this, typeof(Login));
            intent.SetFlags(ActivityFlags.NewTask);

            BancoLogin bancoLogin = new BancoLogin();
            bancoLogin.InserirDadosPessoais(txt_usuario.Text, Convert.ToInt32(txt_senha.Text), txt_email.Text, token);

            Toast.MakeText(this, "Cadastro Realizado com sucesso!", ToastLength.Short).Show();
            //Navigation to SecondActivity
            StartActivity(intent);
            //delete main activity from navigation
            Finish();
     


        }

    }
}