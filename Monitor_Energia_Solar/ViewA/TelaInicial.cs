using System.IO;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Felipecsl.GifImageViewLibrary;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;

namespace Monitor_Energia_Solar
{
    [Activity(Theme = "@style/AppThemeNoAction", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class Telainicial : Activity
    {
        private AcessoFirebase connection = new AcessoFirebase();
        private GifImageView myGIFImage;
        private ProgressBar progressBar;

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


            //Student1 student = new Student1();

            ////student.Id = "22222";
            ////student.Name = "tttt";
            ////student.Surname = "eeeeeeeee";
            ////student.Grade = "ddddddd";

            //connection.AddStudent(student);

            Timer timer = new Timer();
            timer.Interval = 2900;
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
     
        }
        

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            var intent = new Intent(this, typeof(Obj_Login));
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
