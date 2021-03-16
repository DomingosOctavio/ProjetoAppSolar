using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        EditText usuario;
        EditText senha;
        TextView txtview;

        Button btn_get;
        Button btn_sair;
        Button btn_login;



        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.login);

            usuario = FindViewById<EditText>(Resource.Id.usuario);
            //usuario.SetTextColor(Android.Graphics.Color.Yellow);
            senha = FindViewById<EditText>(Resource.Id.senha);
            //senha.SetTextColor(Android.Graphics.Color.Yellow);

            btn_login = FindViewById<Button>(Resource.Id.entrar);
            btn_get = FindViewById<Button>(Resource.Id.get);
      

            //txtview = FindViewById<TextView>(Resource.Id.texto1);

            btn_login.Click += Btnsave_Click;
            //btn_get.Click += Btnget_Click;
            //btn_sair.Click += Btnreset_Click;

            //login.Click += delegate
            //{
            //    var intent = new Intent(this, typeof(MainActivity));
            //    intent.SetFlags(ActivityFlags.NewTask);
            //    //Navigation to SecondActivity
            //    StartActivity(intent);
            //    //delete main activity from navigation
            //    Finish();
            //    Toast.MakeText(this, "Botão de Login foi clicado!", ToastLength.Short).Show();
            //};
        }
        private void Btnsave_Click(object sender, EventArgs e)
        {

            //Save Sessionvalue  

            Context mContext = Android.App.Application.Context;
            Session_classe ap = new Session_classe(mContext);
            ap.saveAccessKey(usuario.Text);

            Session_Token token = new Session_Token(mContext);
            ap.saveAccessKey(senha.Text);



            // usuario.Text = "";

            var intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.NewTask);
            //Navigation to SecondActivity
            StartActivity(intent);
            //delete main activity from navigation
            Finish();
            //Toast.MakeText(this, "Botão de Login foi clicado!", ToastLength.Short).Show();

        }
        //private void Btnget_Click(object sender, EventArgs e)
        //{

        //    //Get Sessionvalue  

        //    Context mContext = Android.App.Application.Context;
        //    Session_classe ap = new Session_classe(mContext);
        //    string key = ap.getAccessKey();
        //    txtview.Text = key.ToString();

        //}
        //private void Btnreset_Click(object sender, EventArgs e)
        //{
        //    ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
        //    ISharedPreferencesEditor editer = pref.Edit();
        //    editer.Remove("PREFERENCE_ACCESS_KEY").Commit(); ////Remove Spec key values  
        //}
    }
}