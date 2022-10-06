using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Content;
using Android.Preferences;
using SendEmailService;
using Monitor_Energia_Solar.Controller;

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


                    EditText etSearch = (EditText)view.FindViewById(Resource.Id.editText);
                    String userdata = etSearch.EditableText.ToString();

                    if (userdata != null || userdata != "")
                    {
                        Email email = new Email();
                        email.SendEmail(userdata);
                    }
                    else
                    {
                        Toast.MakeText(Application.Context, "TOKEN Inválido", ToastLength.Short).Show();
                        return;
                    }
                  

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
        //    LayoutInflater layoutInflater = LayoutInflater.From(this);
        //    View view = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
        //    Android.Support.V7.App.AlertDialog.Builder alertbuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
        //    alertbuilder.SetView(view);
        //    Android.Support.V7.App.AlertDialog dialog = alertbuilder.Create();
        //    dialog.Show();

            Obj_Banco_Dados obj_Banco = new Obj_Banco_Dados();
            LoginController loginControler = new LoginController();
            loginControler.BuscarLogin("teste", "teste");
            try
            {
                obj_Banco = loginControler.BuscardadosLogin(usuario.Text, senha.Text);
            }
            catch(Exception ex)
            {
                Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle("Atenção");
                alert.SetIcon(Resource.Drawable.sair);
                alert.SetMessage("Ocorreu um erro ao acessar o banco de dados. Erro: " + ex.Message);

                alert.SetPositiveButton("OK", (senderAlert, args) =>
                {
                    return;
                });

              
            }
          
            try
            {
                if (obj_Banco.Usuario.Equals("") || obj_Banco.Senha.Equals(""))
                {
                    Toast.MakeText(this, "Por favor, preencha os dois dos campos!", ToastLength.Short).Show();
                    return;
                }
                else
                {
                    if (obj_Banco.Usuario.Equals(usuario.Text) && obj_Banco.Senha.Equals(senha.Text))
                    {

                        var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
                        var usuarioEdit = dadosUsuario.Edit();
                        usuarioEdit.PutString("Usuario", obj_Banco.Usuario);
                        usuarioEdit.PutString("Codigo", obj_Banco.Cod);
                        usuarioEdit.PutString("Ip", obj_Banco.IP_conexao);
                        usuarioEdit.PutString("Senha", obj_Banco.Cod);
                        usuarioEdit.Commit();




                        var intent = new Intent(this, typeof(MainActivity));
                        intent.SetFlags(ActivityFlags.NewTask);
                        //Navigation to SecondActivity
                        //alertbuilder.Dispose();
                        StartActivity(intent);
                        //delete main activity from navigation
                        Finish();

                    }

                }

            }
            catch (NullReferenceException ex)
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
            Finish();
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