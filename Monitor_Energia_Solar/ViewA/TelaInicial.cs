using System.IO;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Felipecsl.GifImageViewLibrary;
using Monitor_Energia_Solar.Model;

namespace Monitor_Energia_Solar
{
    [Activity(Theme = "@style/AppThemeNoAction", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class Telainicial : Activity
    {
        private GifImageView myGIFImage;
        private ProgressBar progressBar;
        private AcessoFirebase connection = new AcessoFirebase();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.inicial);

            myGIFImage = FindViewById<GifImageView>(Resource.Id.myGIFImage);
            progressBar = FindViewById<ProgressBar>(Resource.Id.progressBar2);
            // From Drawable
            Stream input = Resources.OpenRawResource(Resource.Drawable.Sol_anim);

            // You should convert the "input" into Byte Array 
            byte[] bytes = ConvertByteArray(input);

            myGIFImage.SetBytes(bytes);
            myGIFImage.StartAnimation();

            Timer timer = new Timer();
            timer.Interval = 2900;
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

            Obj_Plot obj = new Obj_Plot();
            obj.Token = "100";
            obj.Id_dia = "15-08-1984";
            obj.Seis =  3;
            obj.Sete = 34;
            obj.Oito = 5;
            obj.Nove = 2;
            obj.Dez = 2;
            obj.Onze = 3;
            obj.Doze = 4;
            obj.Treze = 5;
            obj.Catorze = 6;
            obj.Quinze = 4;
            obj.Dezeseis = 3;
            obj.Dezesete = 4;
            obj.Dezoito = 4;


            connection.AddObject(obj);

            Obj_Plot obj2 = new Obj_Plot();
            obj2.Token = "100";
            obj2.Id_dia = "12-08-1984";
            obj2.Seis = 3;
            obj2.Sete = 34;
            obj2.Oito = 5;
            obj2.Nove = 2;
            obj2.Dez = 2;
            obj2.Onze = 3;
            obj2.Doze = 4;
            obj2.Treze = 5;
            obj2.Catorze = 6;
            obj2.Quinze = 4;
            obj2.Dezeseis = 3;
            obj2.Dezesete = 4;
            obj2.Dezoito = 4;


            connection.AddObject(obj2);
            //Obj_Banco_Dados obj = new Obj_Banco_Dados();

            //obj.Id = "200";
            //obj.Senha = "senha";
            //obj.Token = "200";
            //obj.Email = "tavotere@hotmail.com";
            //obj.Usuario = "uol2";
            //obj.IP_conexao = "senha";



            //obj.Id = "400";
            //obj.Senha = "senha5";
            //obj.Token = "350";
            //obj.Email = "tavotere@hotmail.com";
            //obj.Usuario = "uol4";
            //obj.IP_conexao = "senha";

            //connection.AddObject(obj);
        }


        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var intent = new Intent(this, typeof(Login));
            intent.SetFlags(ActivityFlags.NewTask);
            //Navigation to SecondActivity
            StartActivity(intent);
            //delete main activity from navigation
            Finish();

        }

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