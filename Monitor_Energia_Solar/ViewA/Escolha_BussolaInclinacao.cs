using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Felipecsl.GifImageViewLibrary;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Timers;
using System.IO;
using Monitor_Energia_Solar.Controller;

namespace Monitor_Energia_Solar
{
    [Activity(Theme = "@style/AppThemeNoAction", Icon = "@drawable/icon")]
    public class Escolha_BussolaInclinacao : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private GifImageView myGIFImage;
        private ProgressBar progressBar;
        private TextView textoBussola;
        private TextView textoInclinacao;
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.bussola:
                    var intent = new Intent(this, typeof(Compass_Atividade));
                    intent.SetFlags(ActivityFlags.NewTask);
                    //Navigation to SecondActivity
                    StartActivity(intent);
                    //delete main activity from navigation

                    return true;
                case Resource.Id.inclinacao:
                    var intent2 = new Intent(this, typeof(Sensor_Inclinacao_Atividade));
                    intent2.SetFlags(ActivityFlags.NewTask);
                    //Navigation to SecondActivity
                    StartActivity(intent2);
                    //delete main activity from navigation


                    return true;

            }
            return false;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.instrucao_bussolaInclinacao);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation2);
            navigation.SetOnNavigationItemSelectedListener(this);

            //myGIFImage = FindViewById<GifImageView>(Resource.Id.GIF);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar2);

            textoBussola = FindViewById<TextView>(Resource.Id.textoBussola);
            textoInclinacao = FindViewById<TextView>(Resource.Id.textoInclinacao);

            TelaInstrucaoSensoresAPIController textoInstrucao = new TelaInstrucaoSensoresAPIController();

           
            textoBussola.Text = textoInstrucao.ConsultarTextoBussola();
            textoInclinacao.Text = textoInstrucao.ConsultarTextoInclinacao(); 

        // From Drawable
        //Stream input = Resources.OpenRawResource(Resource.Drawable.GIF);

        // You should convert the "input" into Byte Array 
        //byte[] bytes = ConvertByteArray(input);

        //myGIFImage.SetBytes(bytes);
        //myGIFImage.StartAnimation();

        //Timer timer = new Timer();
        //timer.Interval = 2900;
        //timer.AutoReset = false;
        //timer.Elapsed += Timer_Elapsed;
        //timer.Start();

    }
        public override void OnBackPressed()
        {

            this.Finish();
        }
        //private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    var intent = new Intent(this, typeof(Login));
        //    intent.SetFlags(ActivityFlags.NewTask);
        //    //Navigation to SecondActivity
        //    StartActivity(intent);
        //    //delete main activity from navigation
        //    Finish();

        //}
        private byte[] ConvertByteArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                    ms.Write(buffer, 0, read);
                return ms.ToArray();
            }
        }

    }
}