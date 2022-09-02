using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Monitor_Energia_Solar.Service;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "Tarifas_Atividade")]
    public class TarifasAtividade : AppCompatActivity
    {
        CancellationTokenSource _tokenSource = null;
        Spinner spinner;
        Spinner spinner_tarifas;
        ArrayAdapter adapter_companhias;
        ProgressBar progress;
        public static string Agente = "";

        String[] Urls = { "https://apise.way2.com.br/v1/tarifas?apikey=ad55f064ab884c6d8157fe4de92bd1ef&ano=", "https://apise.way2.com.br/v1/tarifas?apikey=9c544b41cf934fbd8d8657a4dcd997bd&ano=", "https://apise.way2.com.br/v1/tarifas?apikey=ea6ffddc84024d269cc660121835a91a&ano=", "https://apise.way2.com.br/v1/tarifas?apikey=ef061b3334da4721b330af853a39f73a&ano=" };
        String[] Companhias = { "","ENEL - RJ", "LIGHT", "ENF" };
        String[] Tarifas = {  "","B1 Residencial Convencional", "B1 Residencial Tarifa Branca", "B1 Residencial Baixa Renda", "B2 Rural Convencional" };
        ArrayAdapter<String> TiposTarifasAdapter;

        int selectedPosition;


        public override void OnBackPressed()
        {

            Finish();

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.tarifas);

         



            progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
      
            progress.Visibility = ViewStates.Visible;

            spinner = FindViewById<Spinner>(Resource.Id.drop_companhias_energia);
            spinner_tarifas = FindViewById<Spinner>(Resource.Id.drop_tarifas);

            spinner_tarifas.Enabled = true;
            adapter_companhias = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Companhias);
            spinner.Adapter = adapter_companhias;

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Companhia_ItemSelected);
            spinner_tarifas.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Tarifas_ItemSelected);

            spinner_tarifas.Enabled = false;
            TiposTarifasAdapter = new ArrayAdapter<string>(this, Resource.Layout.support_simple_spinner_dropdown_item, Tarifas);
            spinner_tarifas.Adapter = TiposTarifasAdapter;


            progress.Visibility = ViewStates.Invisible;


        


        }

        public void Companhia_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            
            var spinner = (Spinner)sender;
            selectedPosition = spinner.SelectedItemPosition;
            // countryIdByPosition = companhiasId[selectedPosition].ToString(); //"CERAL ARARUAMA", "CERCI", "CERES", "ENEL - RJ", "LIGHT", "ENF"};

            spinner.Prompt = "Distribuidoras de Energia do RJ";

            Agente = "";



            if (selectedPosition == 1)
            {
                Agente = "ENEL - RJ";
            }
            else if (selectedPosition == 2)
            {
                Agente = "LIGHT";
            }
            else if (selectedPosition == 3)
            {
                Agente = "ENF";
            }

            if (Agente == "")
            {
                spinner_tarifas.Enabled = false;
               
            }
            else
            {
                spinner_tarifas.Enabled = true;
            }
        }


        private async System.Threading.Tasks.Task Tarifas_SyncAsync(int posicao, int num2, int num3, string Agente)
        {
            TextView txtValidadede = FindViewById<TextView>(Resource.Id.txtValidade);
            TextView txtSubgrupo = FindViewById<TextView>(Resource.Id.txtSubgrupo);
            TextView txtModalidade = FindViewById<TextView>(Resource.Id.txtModalidade);
            TextView txtClasse = FindViewById<TextView>(Resource.Id.txtClasse);
            TextView txtSubClasse = FindViewById<TextView>(Resource.Id.txtSubClasse);
            TextView txtPosto = FindViewById<TextView>(Resource.Id.txtPosto);
            TextView txtbranca1 = FindViewById<TextView>(Resource.Id.branca1);
            TextView txtbranca2 = FindViewById<TextView>(Resource.Id.branca2);
            TextView txtbranca3 = FindViewById<TextView>(Resource.Id.branca3);
            TextView txtTarifa = FindViewById<TextView>(Resource.Id.tarifa);

            txtbranca1.Visibility = ViewStates.Visible;
            txtbranca2.Visibility = ViewStates.Visible;
            txtbranca3.Visibility = ViewStates.Visible;

            txtValidadede.Text = "";
            txtSubgrupo.Text = "";
            txtClasse.Text = "";
            txtSubClasse.Text = "";
            txtModalidade.Text = "";
            txtPosto.Text = "";
            txtTarifa.Text = "";
            txtbranca1.Text = "";
            txtbranca2.Text = "";
            txtbranca3.Text = "";


            TarifasService tarifasService = new TarifasService();

            List<Obj_API_Dados_Energia.TarifasArray> obj_api_tarifas = new List<Obj_API_Dados_Energia.TarifasArray>();

            DateTime data = DateTime.Today;
            //DateTime com o último dia do mês
            int ano = data.Year;
            Boolean validacao = false;


           
            try
            {
                for (int i = 0; i < Urls.Length; i++)
                {

                    string url1 = Urls[i] + ano + "&agente=" + Agente;
                    var apiTask = tarifasService.GetTarifasAPI(url1);
                    obj_api_tarifas = await apiTask;// traz

                    if (obj_api_tarifas.Count > 0)
                    {
                        validacao = true;
                    }
                    else if(obj_api_tarifas.Count == 0)
                    {
                        url1 = Urls[i] + (ano - 1) + "&agente=" + Agente;
                        var apiTask2 = tarifasService.GetTarifasAPI(url1);
                        obj_api_tarifas = await apiTask2;// tr
                    
                        if (obj_api_tarifas.Count > 0)
                        {
                            validacao = true;
                        }
                    }
                    else if (obj_api_tarifas.Count == 0)
                    {
                        url1 = Urls[i] + (ano - 2) + "&agente=" + Agente;
                        var apiTask2 = tarifasService.GetTarifasAPI(url1);
                        obj_api_tarifas = await apiTask2;// tr

                        if (obj_api_tarifas.Count > 0)
                        {
                            validacao = true;
                        }
                    }

                    if (validacao == true)
                    {
                        txtValidadede.Text = "Validade = " + obj_api_tarifas[posicao].validadesde.ToString();
                        txtSubgrupo.Text = "Subgrupo = " + obj_api_tarifas[posicao].subgrupo.ToString();
                        txtClasse.Text = "Classe = " + obj_api_tarifas[posicao].classe.ToString();
                        txtSubClasse.Text = "SubClasse = " + obj_api_tarifas[posicao].subclasse.ToString();
                        txtModalidade.Text = "Modalidade = " + obj_api_tarifas[posicao].subclasse.ToString();
                        txtPosto.Text = "Posto = " + obj_api_tarifas[posicao].posto.ToString();
                        txtTarifa.Text = "Tarifa = " + (Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumotusd) + Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumote));


                        if (num2 != 0 || num3 != 0)
                        {
                            decimal branca1 = Convert.ToDecimal(obj_api_tarifas[posicao].tarifaconsumotusd) + Convert.ToDecimal(obj_api_tarifas[posicao].tarifaconsumote);
                            txtbranca1.Text = branca1.ToString();

                            decimal branca2 = Convert.ToDecimal(obj_api_tarifas[num2].tarifaconsumotusd) + Convert.ToDecimal(obj_api_tarifas[num2].tarifaconsumote);
                            txtbranca2.Text = branca2.ToString();

                            decimal branca3 = Convert.ToDecimal(obj_api_tarifas[num3].tarifaconsumotusd) + Convert.ToDecimal(obj_api_tarifas[num3].tarifaconsumote);
                            txtbranca3.Text = branca3.ToString();
                            progress.Visibility = ViewStates.Invisible;
                            break;

                        }
                        break;
                    }
                    validacao = false;
                    Thread.Sleep(100);
                
                }



            }
            catch (Newtonsoft.Json.JsonSerializationException e)
            {
                string mensagem = e.Message;

            }
            catch (Exception e)
            {
                string mensagem = e.Message;
            }

        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            spinner_tarifas.ItemSelected -= new EventHandler<AdapterView.ItemSelectedEventArgs>(Tarifas_ItemSelected);
        }
        private void Tarifas_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var spinner = (Spinner)sender;
            selectedPosition = spinner.SelectedItemPosition;

         

            if (Agente == "ENEL - RJ")
            {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view3 = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("");
                builder.SetView(view3);
                Android.App.AlertDialog alerta = builder.Create();
                alerta.Show();

                //ENEL RJ
                //convencional -  49
                //branca nivel 1 - 45
                //branca nivel 2 - 46
                //branca nivel 3 - 47
                //baixa renda - 48
                //rural convencional - 62

                if (selectedPosition == 1)
                {
                    Tarifas_SyncAsync(49, 0, 0, "ENEL RJ");
                
                }
                else if (selectedPosition == 2)
                {
                    Tarifas_SyncAsync(45, 46, 47, "ENEL RJ");
                 
                }
                else if (selectedPosition == 3)
                {
                    Tarifas_SyncAsync(48, 0, 0, "ENEL RJ");
                   

                }
                else if (selectedPosition == 4)
                {
                    Tarifas_SyncAsync(62, 0, 0, "ENEL RJ");
                    

                }
                builder.Dispose();
            }
            else if (Agente == "LIGHT")
            {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view3 = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("");
                builder.SetView(view3);
                Android.App.AlertDialog alerta = builder.Create();
                alerta.Show();

                //    LIGHT
                //    tarifas.Add("B1 Residencial Comum"); //58
                //    tarifas.Add("B1 Residencial-Tarifa Branca");//54//55//56
                //    tarifas.Add("B1 Residencial Baixa Renda");//57
                //    tarifas.Add("B2 Rural Convencional"); //71  

                if (selectedPosition == 1)
                {
                    Tarifas_SyncAsync(58, 0, 0, "LIGHT");

                }
                else if (selectedPosition == 2)
                {
                    Tarifas_SyncAsync(54, 55, 56, "LIGHT");


                }
                else if (selectedPosition == 3)
                {
                    Tarifas_SyncAsync(57, 0, 0, "LIGHT");

                }
                else if (selectedPosition == 4)
                {
                    Tarifas_SyncAsync(71, 0, 0, "LIGHT");

                }
                builder.Dispose();
            }
            else if (Agente == "ENF")
            {
                LayoutInflater layoutInflater = LayoutInflater.From(this);
                View view3 = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("");
                builder.SetView(view3);
                Android.App.AlertDialog alerta = builder.Create();
                alerta.Show();

                //ENF
                //convencional -  26
                //branca nivel 1 - 22
                //branca nivel 2 - 23
                //branca nivel 3 - 24
                //baixa renda - 25
                //rural convencional - 39

                if (selectedPosition == 1)
                {
                    Tarifas_SyncAsync(26, 0, 0, "ENF");

                }
                else if (selectedPosition == 2)
                {
                    Tarifas_SyncAsync(22, 23, 24, "ENF");


                }
                else if (selectedPosition == 3)
                {
                    Tarifas_SyncAsync(25, 0, 0, "ENF");

                }
                else if (selectedPosition == 4)
                {
                    Tarifas_SyncAsync(39, 0, 0, "ENF");

                }
                builder.Dispose();
            }
          
        }

    }
}