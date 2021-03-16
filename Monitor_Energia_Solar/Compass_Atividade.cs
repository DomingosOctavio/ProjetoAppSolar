using System;

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Xamarin.Essentials;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "@string/app_name", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class Compass_Atividade : Activity
    {
        SensorSpeed _sensorSpeed = SensorSpeed.UI;
        ImageView CompassRose;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Create your application here
            {

                SetContentView(Resource.Layout.bussola);
                SensorSpeed _sensorSpeed = SensorSpeed.UI;


              

                Compass.ReadingChanged += Compass_ReadingChanged;
                ToggleCompass();




            }
            void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
            {
                var data = e.Reading;
                CompassRose = FindViewById<ImageView>(Resource.Id.bussola);
                var temp = (float)(-1 * data.HeadingMagneticNorth);

                CompassRose.Rotation = (float)(-1 * data.HeadingMagneticNorth);
            }


        }

        public override void OnBackPressed()
        {
            Compass.Stop();
            this.Finish();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public void ToggleCompass()
        {
            try
            {
                if (Compass.IsMonitoring)
                    Compass.Stop();
                else
                    Compass.Start(_sensorSpeed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Some other exception has occurred
            }
        }
    }
}