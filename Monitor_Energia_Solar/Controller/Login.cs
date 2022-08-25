using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using Android.Preferences;
using SendEmail;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        EditText usuario;
        EditText senha;

        Button btn_novo_usuario;
        Button btn_login;
        Button btn_novo_recuperar;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.login);

            usuario = FindViewById<EditText>(Resource.Id.usuario);
            //usuario.SetTextColor(Android.Graphics.Color.Yellow);
            senha = FindViewById<EditText>(Resource.Id.senha);
            //senha.SetTextColor(Android.Graphics.Color.Yellow);

            btn_login = FindViewById<Button>(Resource.Id.entrar);
            btn_novo_usuario = FindViewById<Button>(Resource.Id.novo_usuario);
            btn_novo_recuperar = FindViewById<Button>(Resource.Id.recuperar);

            //txtview = FindViewById<TextView>(Resource.Id.texto1);

            btn_login.Click += Btnsave_Click;
            btn_novo_usuario.Click += Btnnovo_usuario_Click;
           
            btn_novo_recuperar.Click += delegate {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view = layoutInflater.Inflate(Resource.Layout.InputBox_Email, null);
                Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertbuilder.SetView(view);
               // var userdata = view.FindViewById<EditText>(Resource.Id.editText);
                alertbuilder.SetCancelable(false)
                .SetPositiveButton("Enviar", delegate
                {
                    Context mContext = Android.App.Application.Context;
                    Session_Usuario usuario = new Session_Usuario(mContext);
                    string UserID = usuario.getAccessKey().ToString();

                    Context mContext1 = Android.App.Application.Context;
                    Session_Token token = new Session_Token(mContext1);
                    string UserID2 = token.getAccessKey().ToString();

                    EditText etSearch = (EditText)view.FindViewById(Resource.Id.editText);
                    String userdata = etSearch.EditableText.ToString();

                    Email email = new Email();
                    email.SendEmail();

                    //select no banco procurando usuario e senha atraves do email
                    Toast.MakeText(this, "Submit Input: " + userdata, ToastLength.Short).Show();
                })
                .SetNegativeButton("Cancelar", delegate
                {
                    alertbuilder.Dispose();
                });
                Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
                dialog.Show();
            };


        }
        private void Btnsave_Click(object sender, EventArgs e)
        {

            BancoLogin obj_banco_login = new BancoLogin ();
            
            Obj_Banco_Dados obj_banco = new Obj_Banco_Dados();

            obj_banco = obj_banco_login.Consulta_login(usuario.Text, senha.Text);

            try
            {
                if (obj_banco.Usuario.Equals("") || obj_banco.Senha.Equals(""))
                {
                    Toast.MakeText(this, "Por favor, preencha os dois dos campos!", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    if (obj_banco.Usuario.Equals(usuario.Text) && obj_banco.Senha.Equals(senha.Text))
                    {
                        Context mContext = Android.App.Application.Context;
                        Session_Usuario usuario1 = new Session_Usuario(mContext);
                        usuario1.saveAccessKey(obj_banco.Usuario);

                        Context mContext1 = Android.App.Application.Context;
                        Session_Token cod = new Session_Token(mContext1);
                        cod.saveAccessKey(obj_banco.Cod);

                        Context mContext2 = Android.App.Application.Context;
                        Session_Conexao ip_conexao = new Session_Conexao(mContext2);
                        ip_conexao.saveAccessKey(obj_banco.IP_conexao);


                        var intent = new Intent(this, typeof(MainActivity));
                        intent.SetFlags(ActivityFlags.NewTask);
                        //Navigation to SecondActivity
                        StartActivity(intent);
                        //delete main activity from navigation
                        Finish();

                    }
                   
                }
               
            }
            catch(NullReferenceException ex)
            {

                Toast.MakeText(this, "Login ou Senha incorretos", ToastLength.Short).Show();
                usuario.Text = "";
                senha.Text = "";
            }




        }
        private void Btnnovo_usuario_Click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(CadastroTokenAtividade));
            intent.SetFlags(ActivityFlags.NewTask);
            //Navigation to SecondActivity
            StartActivity(intent);
            //delete main activity from navigation
            //Finish();
            //Toast.MakeText(this, "Botão de Login foi clicado!", ToastLength.Short).Show();


        }

     
        private void Btnreset_Click(object sender, EventArgs e)
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editer = pref.Edit();
            editer.Remove("PREFERENCE_ACCESS_KEY").Commit(); ////Remove Spec key values  
        }

       
    }
}