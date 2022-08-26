using System.IO;
using System.Timers;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Felipecsl.GifImageViewLibrary;

namespace Monitor_Energia_Solar
{
    [Activity(Theme = "@style/AppThemeNoAction", MainLauncher = true, Icon = "@drawable/icon", NoHistory = true)]
    public class Telainicial : Activity
    {
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

            Timer timer = new Timer();
            timer.Interval = 2900;
            timer.AutoReset = false;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();

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
