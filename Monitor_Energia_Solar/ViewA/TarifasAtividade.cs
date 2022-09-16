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


namespace Monitor_Energia_Solar
{
    [Activity(Label = "")]
    public class TarifasAtividade : AppCompatActivity
    {
     
        Spinner spinner;
        Spinner spinner_tarifas;
        ArrayAdapter adapter_companhias;
        ProgressBar progress;
        Button btn_calcular;
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
        private void Btnsave_Click(object sender, EventArgs e)
        {
            if (Agente == "" || Agente == null)
            {
                Toast.MakeText(this, "Por favor, prencha o campo Companhia de Energia", ToastLength.Short).Show();
                return;
            }
            if (selectedPosition == 0 || selectedPosition == null)
            {
                Toast.MakeText(this, "Por favor, prencha o campo Tipo de tarifa", ToastLength.Short).Show();
                return;
            }
            if (Agente == "ENEL - RJ")
            {
                //LayoutInflater layoutInflater = LayoutInflater.From(this);
                //View view3 = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
                //Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                //builder.SetTitle("");
                //builder.SetView(view3);
                //Android.App.AlertDialog alerta = builder.Create();
                //alerta.Show();

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
                    Tarifas_SyncAsync(55, 0, 0, "ENEL RJ");


                }
                //builder.Dispose();
            }
            else if (Agente == "LIGHT")
            {
                //LayoutInflater layoutInflater = LayoutInflater.From(this);
                //View view3 = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
                //Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                //builder.SetTitle("");
                //builder.SetView(view3);
                //Android.App.AlertDialog alerta = builder.Create();
                //alerta.Show();

                //    LIGHT
                //    tarifas.Add("B1 Residencial Comum"); //58
                //    tarifas.Add("B1 Residencial-Tarifa Branca");//54//55//56
                //    tarifas.Add("B1 Residencial Baixa Renda");//57
                //    tarifas.Add("B2 Rural Convencional"); //71  

                if (selectedPosition == 1)
                {
                    Tarifas_SyncAsync(52, 0, 0, "LIGHT");

                }
                else if (selectedPosition == 2)
                {
                    Tarifas_SyncAsync(48, 49, 50, "LIGHT");


                }
                else if (selectedPosition == 3)
                {
                    Tarifas_SyncAsync(51, 0, 0, "LIGHT");

                }
                else if (selectedPosition == 4)
                {
                    Tarifas_SyncAsync(58, 0, 0, "LIGHT");

                }
                //builder.Dispose();
            }
            else if (Agente == "ENF")
            {
                //LayoutInflater layoutInflater = LayoutInflater.From(this);
                //View view3 = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
                //Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                //builder.SetTitle("");
                //builder.SetView(view3);
                //Android.App.AlertDialog alerta = builder.Create();
                //alerta.Show();

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
                //builder.Dispose();
            }

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.tarifas);

            progress = FindViewById<ProgressBar>(Resource.Id.progressBar1);
      
      
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

            btn_calcular = FindViewById<Button>(Resource.Id.calcular);
            btn_calcular.Click += Btnsave_Click;

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
            TextView txttarifaTotal = FindViewById<TextView>(Resource.Id.txttarifa);
            TextView txtTotalKwh = FindViewById<TextView>(Resource.Id.txtTotalKwh);
            EditText editTexttarifa = FindViewById<EditText>(Resource.Id.editTexttarifa);


            txtbranca1.Visibility = ViewStates.Visible;
            txtbranca2.Visibility = ViewStates.Visible;
            txtbranca3.Visibility = ViewStates.Visible;

            txtValidadede.Text = "";
            txtSubgrupo.Text = "";
            txtClasse.Text = "";
            txtSubClasse.Text = "";
            txtModalidade.Text = "";
            txtPosto.Text = "";
            txttarifaTotal.Text = "";
            txtbranca1.Text = "";
            txtbranca2.Text = "";
            txtbranca3.Text = "";
            txtTotalKwh.Text = "";


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
                        string validade = obj_api_tarifas[posicao].validadesde.ToString();
                      
                        txtValidadede.Text = "Validade = " + validade.Substring(0, 10);
                        txtSubgrupo.Text = "Subgrupo = " + obj_api_tarifas[posicao].subgrupo.ToString();
                        txtClasse.Text = "Classe = " + obj_api_tarifas[posicao].classe.ToString();
                        txtSubClasse.Text = "SubClasse = " + obj_api_tarifas[posicao].subclasse.ToString();
                        txtModalidade.Text = "Modalidade = " + obj_api_tarifas[posicao].subclasse.ToString();
                        txtPosto.Text = "Posto = " + obj_api_tarifas[posicao].posto.ToString();
                        
                       


                        double total = Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumotusd) + Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumote);
                        double def = (total * 0.00001);
                        txttarifaTotal.Text = "Tarifa de energia *TE + *TUSD = " + def.ToString();

                        txtTotalKwh.Text = "Valor da conta R$: " + (def * Convert.ToDouble(editTexttarifa.Text)).ToString();
                       
                        if (num2 != 0 || num3 != 0)
                        {
                            double total1 = Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumotusd) + Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumote);
                            double def1 = (total1 * 0.00001);
                            txtbranca1.Text = "Tarifa de energia(1) *TE + *TUSD = " + def1.ToString();


                            double total2 = Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumotusd) + Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumote);
                            double def2 = (total2 * 0.00001);
                            txtbranca2.Text = "Tarifa de energia(2) *TE + *TUSD = " + def2.ToString();

                            double total3 = Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumotusd) + Convert.ToDouble(obj_api_tarifas[posicao].tarifaconsumote);
                            double def3 = (total3 * 0.00001);
                            txtbranca3.Text = "Tarifa de energia(3) *TE + *TUSD = " + def3.ToString();
                          
                           
                            progress.Visibility = ViewStates.Invisible;
                            break;
                        }
                        break;
                    }
                    validacao = false;
                  
                
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

         

         
          
        }

    }
}