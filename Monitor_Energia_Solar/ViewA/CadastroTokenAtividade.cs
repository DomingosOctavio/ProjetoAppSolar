using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Monitor_Energia_Solar.Controller;
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
        private CadastroTokenFirebaseController connection = new CadastroTokenFirebaseController();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.cadastro);
            btn_token = FindViewById<EditText>(Resource.Id.token);

            btn_proximo = FindViewById<Button>(Resource.Id.proximo);
            btn_proximo.Click += BtnProximo_Click;  
        }
      
  
        private void BtnProximo_Click(object sender, EventArgs e)
        {
            if (connection.EmptyListVerification(btn_token.Text))
            {
                var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
                var usuarioEdit = dadosUsuario.Edit();
                usuarioEdit.PutString("Codigo", btn_token.Text);
                usuarioEdit.Commit();

                Toast.MakeText(this, "Token encontrado!", ToastLength.Short).Show();
                var intent = new Intent(this, typeof(DadosPessoaisAtividade));
                intent.SetFlags(ActivityFlags.NewTask);
                //Navigation to SecondActivity
                StartActivity(intent);
                //delete main activity from navigation
                Finish();

            }
            else
            {
                 Toast.MakeText(this, "Token não encontrado!", ToastLength.Short).Show();
            }

        }
    }
}