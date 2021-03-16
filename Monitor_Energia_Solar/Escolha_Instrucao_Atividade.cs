using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Monitor_Energia_Solar
{
    [Activity(Theme = "@style/AppThemeNoAction", Icon = "@drawable/icon")]
    public class Escolha_Instrucao_Atividade : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
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
                    //Finish();
                    return true;
                case Resource.Id.inclinacao:
                    var intent2 = new Intent(this, typeof(Sensor_Inclinacao_Atividade));
                    intent2.SetFlags(ActivityFlags.NewTask);
                    //Navigation to SecondActivity
                    StartActivity(intent2);
                    //delete main activity from navigation
                    //Finish();
            
                    return true;
                
            }
            return false;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.instrucao_tela);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation2);
            navigation.SetOnNavigationItemSelectedListener(this);

        }
       
    }
}