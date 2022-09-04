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
using Monitor_Energia_Solar.Controller;

namespace Monitor_Energia_Solar
{
    [Activity(Theme = "@style/AppThemeNoAction", Icon = "@drawable/icon")]
    public class EscolhaTarifaBandeira : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private TextView textobandeira;
        private TextView textoTarifa;
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.bandeira:
                    var intent = new Intent(this, typeof(Bandeira_Atividade));
                    intent.SetFlags(ActivityFlags.NewTask);
                    //Navigation to SecondActivity
                    StartActivity(intent);
                    //delete main activity from navigation

                    return true;
                case Resource.Id.tarifa:
                    var intent2 = new Intent(this, typeof(TarifasAtividade));
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
            SetContentView(Resource.Layout.InstrucaoTarifaBandeira);

            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation3);
            navigation.SetOnNavigationItemSelectedListener(this);

            textobandeira = FindViewById<TextView>(Resource.Id.textoBandeira);
            textoTarifa = FindViewById<TextView>(Resource.Id.textoTarifa);

            TelaInstrucaoSensoresAPIController textoInstrucao = new TelaInstrucaoSensoresAPIController();

            textobandeira.Text = textoInstrucao.ConsultarTextoBandeira();
            textoTarifa.Text = textoInstrucao.ConsultarTextoTarifas();



        }
        public override void OnBackPressed()
        {

            this.Finish();
        }
    }
}