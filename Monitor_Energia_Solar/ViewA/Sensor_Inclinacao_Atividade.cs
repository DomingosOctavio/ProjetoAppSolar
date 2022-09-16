﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Hardware;
using System;
using Android.Content.PM;
using Android.Views;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "@string/sensor_inclinacao", Theme = "@style/Theme.AppCompat.Light.NoActionBar", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]

  //  [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]



    public class Sensor_Inclinacao_Atividade : AppCompatActivity, ISensorEventListener
    {
        public static int incl = 0;
        public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy) { }
        public void OnSensorChanged(SensorEvent e)
        {
            lock (_syncLock)
            {
                double xPart = e.Values[0];
                double yPart = e.Values[1];
                double zPart = e.Values[2];

                double magnitude = Math.Sqrt(xPart * xPart + yPart * yPart + zPart * zPart);

                double cosTheta = yPart / magnitude; // <== variável de nosso interesse

                double thetaGraus = Math.Asin(cosTheta) * 180 / Math.PI;

                incl = (int)thetaGraus;

                // _sensorTextView.Text = string.Format("x={0:f}, y={1:f}, y={2:f}", e.Values[0], e.Values[1], e.Values[2]);

                _sensorTextView.Text = incl.ToString() + " graus";


                progressBarIndeterminado.Progress = 0;
                progressBarIndeterminado.Max = 27;


                if (incl <= 12 && incl >= 0)
                {
                    if (incl == 12)
                    {
                        textView_estacao.Text = "Posição ideal para o verão";
                        checkOK.Checked = true;
                    }
                    else
                    {
                        checkOK.Checked = false;
                        textView_estacao.Text = "";
                    }
                    progressBarIndeterminado.ProgressDrawable.
                  SetColorFilter(Android.Graphics.Color.DarkOrange, Android.Graphics.PorterDuff.Mode.Multiply);

                }
                if (incl > 12 && incl <= 27)
                {

                    if (incl == 27)
                    {
                        textView_estacao.Text = "Posição ideal para o inverno";
                        checkOK.Checked = true;
                    }
                    else
                    {
                        textView_estacao.Text = "";
                        checkOK.Checked = false;
                    }
                    progressBarIndeterminado.ProgressDrawable.
                    SetColorFilter(Android.Graphics.Color.LightSkyBlue, Android.Graphics.PorterDuff.Mode.Multiply);
                }
               
                else if (incl > 27 || incl < 0)
                {
                    checkOK.Checked = false;
                    textView_estacao.Text = "";
                    progressBarIndeterminado.ProgressDrawable.
                    SetColorFilter(Android.Graphics.Color.Gray, Android.Graphics.PorterDuff.Mode.Multiply);
                    incl = 0;
                }

                progressBarIndeterminado.IncrementProgressBy(incl);



            }
        }
        static readonly object _syncLock = new object();
        ProgressBar progressBarIndeterminado;
        SensorManager _sensorManager;
        TextView _sensorTextView;
        TextView textView_estacao;
        CheckBox checkOK;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
       
            SetContentView(Resource.Layout.inclinacao_tela);

            _sensorManager = (SensorManager)GetSystemService(SensorService);
            _sensorTextView = FindViewById<TextView>(Resource.Id.inclinacao_t);
            progressBarIndeterminado = FindViewById<ProgressBar>(Resource.Id.determinateBar);
            checkOK = FindViewById<CheckBox>(Resource.Id.checkBox1);
            textView_estacao = FindViewById<TextView>(Resource.Id.estacao);


        }
        protected override void OnResume()
        {
            base.OnResume();
            _sensorManager.RegisterListener(this,
                _sensorManager.GetDefaultSensor(SensorType.Accelerometer),
                SensorDelay.Ui);
        }
        public override void OnBackPressed()
        { 

            this.Finish();
        }
        protected override void OnPause()
        {
            base.OnPause();
            _sensorManager.UnregisterListener(this);
        }
      
       
    }
}