using System;

using Android.App;
using Android.Hardware;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using System.Runtime.CompilerServices;
using Android.Views.Animations;
using Android.Views;
using Android.Content;
using Android.Preferences;

namespace Monitor_Energia_Solar
{
    // [Activity(Label = "Bussola_Atividade", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light.NoActionBar")]
     [Activity(Label = "Bussola_Atividade", MainLauncher = false, Icon = "@drawable/icon")]

    public class Bussola_Atividade : AppCompatActivity, ISensorEventListener
    {
        private ImageView imageView;
        private float[] mGravity = new float[3], mGeomagnetic = new float[3];
        private float azimuth = 0f;
        private float currectAzimuth = 0f;
        private SensorManager mSensorManager;

        public void OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy)
        {

        }

        public void OnSensorChanged(SensorEvent e)
        {
            ProcessCompass(e);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public override void OnBackPressed()
        {
            ISharedPreferences pref = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editer = pref.Edit();
            editer.Remove("PREFERENCE_ACCESS_KEY").Commit(); ////Remove Spec key values  

            Finish();
        }

        private void ProcessCompass(SensorEvent e)
        {
            float alpha = 0.97f;

            if (e.Sensor.Type == SensorType.Accelerometer)
            {
                mGravity[0] = alpha * mGravity[0] + (1 - alpha) + e.Values[0];
                mGravity[1] = alpha * mGravity[0] + (1 - alpha) + e.Values[1];
                mGravity[2] = alpha * mGravity[0] + (1 - alpha) + e.Values[2];
            }
            if (e.Sensor.Type == SensorType.MagneticField)
            {
                mGeomagnetic[0] = alpha * mGeomagnetic[0] + (1 - alpha) + e.Values[0];
                mGeomagnetic[1] = alpha * mGeomagnetic[0] + (1 - alpha) + e.Values[1];
                mGeomagnetic[2] = alpha * mGeomagnetic[0] + (1 - alpha) + e.Values[2];
            }

            float[] R = new float[9], I = new float[9];
            bool sucess = SensorManager.GetRotationMatrix(R, I, mGravity, mGeomagnetic);
            if (sucess)
            {
                float[] orientation = new float[3];
                SensorManager.GetOrientation(R, orientation);
                azimuth = (float)(orientation[0] * (180 / Math.PI));
                azimuth = (azimuth + 360) % 360;

                Android.Views.Animations.Animation anim = new RotateAnimation(-currectAzimuth, -azimuth, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f, Android.Views.Animations.Dimension.RelativeToSelf, 0.5f);

                currectAzimuth = azimuth;
                anim.Duration = 500;
                anim.RepeatCount = 0;
                anim.FillAfter = true;

                imageView.StartAnimation(anim);
            }

        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.bussola);
            // Create your application here
            imageView = FindViewById<ImageView>(Resource.Id.bussola);
            mSensorManager = (SensorManager)GetSystemService(SensorService);
        }





        protected override void OnPause()
        {
            base.OnPause();
            mSensorManager.UnregisterListener(this);

        }

        protected override void OnResume()
        {
            base.OnResume();
            mSensorManager.RegisterListener(this, mSensorManager.GetDefaultSensor(SensorType.MagneticField), SensorDelay.Game);
            mSensorManager.RegisterListener(this, mSensorManager.GetDefaultSensor(SensorType.Accelerometer), SensorDelay.Game);
        }

      
    }
}