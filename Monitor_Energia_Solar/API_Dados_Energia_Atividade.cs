using System;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Widget;
using System.Collections;
using System.Threading;
using System.Collections.Generic;
using Android.Graphics.Drawables;
using Android.Views;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "API_Dados_Energia_Atividade")]
    public class API_Dados_Energia_Atividade : Activity
    {
      
        CancellationTokenSource _tokenSource = null;
        Spinner spinner;
        Spinner spinner_tarifas;
        ArrayAdapter adapter_companhias;
        ArrayAdapter adapter_tarifas;

        public static string Agente = "";

        int[] companhiasId = new int[] { 0, 1, 2, 3, 4, 5, 6 };
        String[] Companhias = { "Select", "CERAL ARARUAMA", "CERCI", "CERES", "ENEL - RJ", "LIGHT", "ENF"};
        String[] Tarifas = { "Select", "B1 Residencial Convencional", "B1 Residencial Tarifa Branca", "B1 Residencial Baixa Renda", "B2 Rural Convencional" };


        ArrayAdapter<String> CompanhiasAdapter;
        ArrayAdapter<String> TiposTarifasAdapter;
  
        String countryIdByPosition;
        int selectedPosition;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dados_API_Energia);

            //GetCompanhias();

            //cria a instância do spinner declarado no arquivo Main
            spinner = FindViewById<Spinner>(Resource.Id.drop_companhias_energia);
            spinner_tarifas = FindViewById<Spinner>(Resource.Id.drop_tarifas);

            spinner_tarifas.Enabled = false;

            adapter_companhias = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Companhias);
            spinner.Adapter = adapter_companhias;

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Companhia_ItemSelected);
            spinner_tarifas.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Tarifas_ItemSelected);
        

  

            TextView txtID = FindViewById<TextView>(Resource.Id.txtID);
            TextView txtMes = FindViewById<TextView>(Resource.Id.txtMes);



            // variáveis API_Tarifas

    

            try
            {
                #pragma warning disable CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
               // CallFromNonAsyncBandeiras(txtID, txtMes);
                #pragma warning restore CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
            }
            catch (Exception ex)
            {
                //Android.App.AlertDialog.Builder alertDialoger = new Android.App.AlertDialog.Builder(this);
                //alertDialoger.SetTitle("Atenção");
                //alertDialoger.SetMessage(ex.Message);
                //alertDialoger.SetNeutralButton("OK", delegate
                //{
                //    alertDialoger.Dispose();
                //});
            }

            try
            {
#pragma warning disable CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída

               // CallFromNonAsyncTarifas(Agente,txtValidadede, txtSubgrupo, txtModalidade, txtClasse, txtSubClasse, txtPosto, txtConsumoTusd, txtConsumoTe);

#pragma warning restore CS4014 // Como esta chamada não é esperada, a execução do método atual continua antes de a chamada ser concluída
            }
            catch (Exception ex)
            {
                //Android.App.AlertDialog.Builder alertDialoger = new Android.App.AlertDialog.Builder(this);
                //alertDialoger.SetTitle("Atenção");
                //alertDialoger.SetMessage(ex.Message);
                //alertDialoger.SetNeutralButton("OK", delegate
                //{
                //    alertDialoger.Dispose();
                //});
            }
        }

        private void Tarifas_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            selectedPosition = spinner.SelectedItemPosition;

    
            if  (Agente == "CERAL ARARUAMA")
            {
                //CERAL ARARUAMA
                //convencional - 17
                //branca nivel 1 - 13
                //branca nivel 2 - 14
                //branca nivel 3 - 15
                //baixa renda - 16
                //rural convencional - 30

                if (selectedPosition == 1)
                {
                    CallFromNonAsyncTarifas(17, Agente);

                }
                else if (selectedPosition == 2)
                {
                    CallFromNonAsyncTarifas(13, 14, 15, Agente);
         

                }
                else if (selectedPosition == 3)
                {
                    CallFromNonAsyncTarifas(16, Agente);

                }
                else if (selectedPosition == 4)
                {
                    CallFromNonAsyncTarifas(30, Agente);

                }

            }
            else if (Agente == "CERCI")
            {
                //CERCI
                // convencional - 17
                //branca nivel 1 - 13
                //branca nivel 2  - 14
                // branca nivel 3 - 15
                // baixa renda - 16
                // rural convencional - 30

                if (selectedPosition == 1)
                {
                    CallFromNonAsyncTarifas(17, Agente);

                }
                else if (selectedPosition == 2)
                {
                    CallFromNonAsyncTarifas(13, 14, 15, Agente);
       

                }
                else if (selectedPosition == 3)
                {
                    CallFromNonAsyncTarifas(16, Agente);

                }

                else if (selectedPosition == 4)
                {
                    CallFromNonAsyncTarifas(30, Agente);

                }
               

            }
            else if (Agente == "CERES")
            {
                //CERES
                //convencional -  17
                //branca nivel 1 - 13
                //branca nivel 2 - 14
                //branca nivel 3 - 15
                //baixa renda - 16
                //rural convencional - 30


                if (selectedPosition == 1)
                {
                    CallFromNonAsyncTarifas(17, Agente);

                }
                else if (selectedPosition == 2)
                {
                    CallFromNonAsyncTarifas(13, 14, 15, Agente);
               

                }
                else if (selectedPosition == 3)
                {
                    CallFromNonAsyncTarifas(16, Agente);

                }
                else if (selectedPosition == 4)
                {
                    CallFromNonAsyncTarifas(30, Agente);

                }

            }
            else if (Agente == "ENEL - RJ")
            {
                //ENEL RJ
                //convencional -  49
                //branca nivel 1 - 45
                //branca nivel 2 - 46
                //branca nivel 3 - 47
                //baixa renda - 48
                //rural convencional - 62

                if (selectedPosition == 1)
                {
                    CallFromNonAsyncTarifas(49, Agente);

                }
                else if (selectedPosition == 2)
                {
                    CallFromNonAsyncTarifas(45, 46, 47, Agente);
         

                }
                else if (selectedPosition == 3)
                {
                    CallFromNonAsyncTarifas(48, Agente);

                }
                else if (selectedPosition == 4)
                {
                    CallFromNonAsyncTarifas(62, Agente);

                }

            }
            else if (Agente == "LIGHT")
            {
                //    LIGHT
                //    tarifas.Add("B1 Residencial Comum"); //58
                //    tarifas.Add("B1 Residencial-Tarifa Branca");//54//55//56
                //    tarifas.Add("B1 Residencial Baixa Renda");//57
                //    tarifas.Add("B2 Rural Convencional"); //71  

                if (selectedPosition == 1)
                {
                    CallFromNonAsyncTarifas(58, Agente);

                }
                else if (selectedPosition == 2)
                {
                    CallFromNonAsyncTarifas(54, 55, 56, Agente);
                  

                }
                else if (selectedPosition == 3)
                {
                    CallFromNonAsyncTarifas(57, Agente);

                }
                else if (selectedPosition == 4)
                {
                    CallFromNonAsyncTarifas(71, Agente);

                }

            }
            else if (Agente == "ENF")
            {

                //ENF
                //convencional -  26
                //branca nivel 1 - 22
                //branca nivel 2 - 23
                //branca nivel 3 - 24
                //baixa renda - 25
                //rural convencional - 39

                if (selectedPosition == 1)
                {
                    CallFromNonAsyncTarifas(26, Agente);

                }
                else if (selectedPosition == 2)
                {
                    CallFromNonAsyncTarifas(22, 23, 24, Agente);
 

                }
                else if (selectedPosition == 3)
                {
                    CallFromNonAsyncTarifas(25, Agente);

                }
                else if (selectedPosition == 4)
                {
                    CallFromNonAsyncTarifas(39, Agente);

                }

            }


        }

        private void Companhia_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            selectedPosition = spinner.SelectedItemPosition;
            countryIdByPosition = companhiasId[selectedPosition].ToString(); //"CERAL ARARUAMA", "CERCI", "CERES", "ENEL - RJ", "LIGHT", "ENF"};

             spinner.Prompt = "Distribuidoras de Energia do RJ";
            if (selectedPosition == 1)
            {             
                Agente = "CERAL ARARUAMA";
                spinner_tarifas.Enabled = true;
                TiposTarifasAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Tarifas);
                spinner_tarifas.Adapter = TiposTarifasAdapter;
            }
            else if (selectedPosition == 2)
            {
                Agente = "CERCI";
                spinner_tarifas.Enabled = true;
                TiposTarifasAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Tarifas);
                spinner_tarifas.Adapter = TiposTarifasAdapter;
            }
            else if (selectedPosition == 3)
            {
                Agente = "CERES";
                spinner_tarifas.Enabled = true;
                TiposTarifasAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Tarifas);
                spinner_tarifas.Adapter = TiposTarifasAdapter;
            }
            else if (selectedPosition == 4)
            {
                Agente = "ENEL - RJ";
                spinner_tarifas.Enabled = true;
                TiposTarifasAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Tarifas);
                spinner_tarifas.Adapter = TiposTarifasAdapter;
            }
            else if (selectedPosition == 5)
            {
                Agente = "LIGHT";
                spinner_tarifas.Enabled = true;
                TiposTarifasAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Tarifas);
                spinner_tarifas.Adapter = TiposTarifasAdapter;
            }
            else if (selectedPosition == 6)
            {
                Agente = "ENF";
                spinner_tarifas.Enabled = true;
                TiposTarifasAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Tarifas);
                spinner_tarifas.Adapter = TiposTarifasAdapter;
            }
            else
            {
                spinner_tarifas.Enabled = false;
            }

           
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            spinner.ItemSelected -= new EventHandler<AdapterView.ItemSelectedEventArgs>(Companhia_ItemSelected);
            spinner_tarifas.ItemSelected -= new EventHandler<AdapterView.ItemSelectedEventArgs>(Tarifas_ItemSelected);    
        }

        public async void CallFromNonAsyncTarifas(int num1, int num2, int num3, string Agente)
        {
            TextView txtValidadede = FindViewById<TextView>(Resource.Id.txtValidadede);
            TextView txtSubgrupo = FindViewById<TextView>(Resource.Id.txtSubgrupo);
            TextView txtModalidade = FindViewById<TextView>(Resource.Id.txtModalidade);
            TextView txtClasse = FindViewById<TextView>(Resource.Id.txtClasse);
            TextView txtSubClasse = FindViewById<TextView>(Resource.Id.txtSubClasse);
            TextView txtPosto = FindViewById<TextView>(Resource.Id.txtPosto);
            TextView txtbranca1 = FindViewById<TextView>(Resource.Id.branca1);
            TextView txtbranca2 = FindViewById<TextView>(Resource.Id.branca2);
            TextView txtbranca3 = FindViewById<TextView>(Resource.Id.branca3);

            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

            List<Obj_API_Dados_Energia.TarifasArray> obj_api_tarifas = new List<Obj_API_Dados_Energia.TarifasArray>();

            DateTime data = DateTime.Today;

            //DateTime com o último dia do mês
            int ano = data.Year;

            string url = "https://apise.way2.com.br/v1/tarifas?apikey=ef061b3334da4721b330af853a39f73a&ano=" + "2020" + "&agente=" + Agente;

            var api1Task2 = API_Dados_Energia.API_Tarifas(token, url);
            obj_api_tarifas = await api1Task2;



            txtValidadede.Text = obj_api_tarifas[num1].validadesde.ToString();
            txtSubgrupo.Text = obj_api_tarifas[num1].subgrupo.ToString();
            txtClasse.Text = obj_api_tarifas[num1].classe.ToString();
            txtSubClasse.Text = obj_api_tarifas[num1].subclasse.ToString();
            txtPosto.Text = obj_api_tarifas[num1].posto.ToString();


            txtbranca1.Text = (obj_api_tarifas[num1].tarifaconsumotusd + obj_api_tarifas[num1].tarifaconsumote).ToString();
            txtbranca2.Text = (obj_api_tarifas[num2].tarifaconsumotusd + obj_api_tarifas[num2].tarifaconsumote).ToString();
            txtbranca3.Text = (obj_api_tarifas[num3].tarifaconsumotusd + obj_api_tarifas[num3].tarifaconsumote).ToString();



        }


        public async void CallFromNonAsyncTarifas(int num1, string Agente)
        {
            TextView txtValidadede = FindViewById<TextView>(Resource.Id.txtValidadede);
            TextView txtSubgrupo = FindViewById<TextView>(Resource.Id.txtSubgrupo);
            TextView txtModalidade = FindViewById<TextView>(Resource.Id.txtModalidade);
            TextView txtClasse = FindViewById<TextView>(Resource.Id.txtClasse);
            TextView txtSubClasse = FindViewById<TextView>(Resource.Id.txtSubClasse);
            TextView txtPosto = FindViewById<TextView>(Resource.Id.txtPosto);
            TextView txtTarifa = FindViewById<TextView>(Resource.Id.tarifa);


            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;

            List<Obj_API_Dados_Energia.TarifasArray> obj_api_tarifas = new List<Obj_API_Dados_Energia.TarifasArray>();

            DateTime data = DateTime.Today;

            //DateTime com o último dia do mês
            int ano = data.Year;

            string url = "https://apise.way2.com.br/v1/tarifas?apikey=ef061b3334da4721b330af853a39f73a&ano=" + "2020" + "&agente=" + Agente;

            var api1Task2 = API_Dados_Energia.API_Tarifas(token, url);
            obj_api_tarifas = await api1Task2;


            txtValidadede.Text = obj_api_tarifas[num1].validadesde.ToString();
            txtSubgrupo.Text = obj_api_tarifas[num1].subgrupo.ToString();
            txtClasse.Text = obj_api_tarifas[num1].classe.ToString();
            txtSubClasse.Text = obj_api_tarifas[num1].subclasse.ToString();
            txtPosto.Text = obj_api_tarifas[num1].posto.ToString();
            txtTarifa.Text = (obj_api_tarifas[num1].tarifaconsumotusd + obj_api_tarifas[num1].tarifaconsumote).ToString();





        }










        public async Task CallFromNonAsyncBandeiras(TextView txtID, TextView txtMes)
        {
            _tokenSource = new CancellationTokenSource();
            var token = _tokenSource.Token;
            ProgressBar progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);


            Obj_API_Dados_Energia.Root_Bandeiras obj_bandeiras = new Obj_API_Dados_Energia.Root_Bandeiras();

            DateTime data = DateTime.Today;

            //DateTime com o primeiro dia do mês

            int mes = data.Month;

            DateTime primeiroDiaDoMes;

            if (data.Month == 1)
            {
                primeiroDiaDoMes = new DateTime(data.Year, 12, 1);
            }
            else
            {
                primeiroDiaDoMes = new DateTime(data.Year, data.Month - 1, 1);
            }

            //DateTime com o último dia do mês
            DateTime ultimoDiaDoMes = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month - 1));

            string url = "https://www.apidosetoreletrico.com.br/api/energy-providers/tariff-flags?monthStart=" + primeiroDiaDoMes.ToString("yyyy/MM/dd") + "&monthEnd=" + ultimoDiaDoMes.ToString("yyyy/MM/dd");

            var api1Task2 = API_Dados_Energia.CheckNetworkErrorCallAsyncBandeiras(token, url);
            obj_bandeiras = await api1Task2;

            txtMes.Text = "Última atualização: " + obj_bandeiras.items[0].Month.ToString();

            if (obj_bandeiras.items[0].FlagType == 0) //bandeira verde
            {
                ImageView usuario = FindViewById<ImageView>(Resource.Id.band);
                usuario.SetImageResource(Resource.Drawable.bandeira_verde);
                txtID.Text = "Condições favoráveis para geração de energia no Brasil.\n A tarifa não sofre nenhum acréscimo.";
                progress.Visibility = ViewStates.Visible;
            }
            else if (obj_bandeiras.items[0].FlagType == 1) //bandeira amarela
            {
                ImageView usuario = FindViewById<ImageView>(Resource.Id.band);
                usuario.SetImageResource(Resource.Drawable.bandeira_amarela);
                txtID.Text = "Condições menos favoráveis para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + obj_bandeiras.items[0].Value.ToString() + "/KWh";
                progress.Visibility = ViewStates.Visible;
            }
            else if (obj_bandeiras.items[0].FlagType == 2) //bandeira vermelha
            {
                ImageView usuario = FindViewById<ImageView>(Resource.Id.band);
                usuario.SetImageResource(Resource.Drawable.bandeira_vermelha);
                txtID.Text = "Condições mais custosas para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + obj_bandeiras.items[0].Value.ToString() + "/KWh" + "para o patamar1";
                progress.Visibility = ViewStates.Visible;
            }
            else  //bandeira vermelha 2
            {
                ImageView usuario = FindViewById<ImageView>(Resource.Id.band);
                usuario.SetImageResource(Resource.Drawable.bandeira_vermelha2);
                txtID.Text = "Condições mais custosas para geração de energia no Brasil.\nA tarifa sofre acréscimo de\nR$ " + obj_bandeiras.items[0].Value.ToString() + "/KWh" + "para o patamar2";
                progress.Visibility = ViewStates.Visible;
            }
        }
    }
}