using Android.Content;
using Android.App;
using Android.OS;
using Android.Widget;
using MySqlConnector;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;
using System;
using System.Collections;
using System.Collections.Generic;
using Monitor_Energia_Solar.Controller;
using Monitor_Energia_Solar.Model;
using Android.Views;

namespace Monitor_Energia_Solar
{
    [Activity(Label = "DropdownEmpresasAtividade")]
    public class GraficoAtividade : Activity
    {
        Spinner spinner;
        ArrayAdapter adapter;
        ArrayList intervalo;
        Button btnTensao;
        Button btnCorrente;
        Button btnLuz;
        PlotView view2;
        private static string dataEscolhida;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.geracao_plot);

            view2 = FindViewById<PlotView>(Resource.Id.plot_view);

            btnTensao = FindViewById<Button>(Resource.Id.btnTensao);
            btnCorrente = FindViewById<Button>(Resource.Id.btnCorrente);
            btnLuz = FindViewById<Button>(Resource.Id.btnLuz);

            btnTensao.Click += BtnGraficoTensao;
            btnCorrente.Click += BtnGraficoCorrente;
            btnLuz.Click += BtnLuz;


            LayoutInflater layoutInflater = LayoutInflater.From(this);
            View view3 = layoutInflater.Inflate(Resource.Layout.Aguarde, null);
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("");
            builder.SetView(view3);
            Android.App.AlertDialog alerta = builder.Create();
            alerta.Show();

            //preenche o arraylist com os dados
            GetIntervalo();
        //cria a instância do spinner declarado no arquivo Main
        spinner = FindViewById<Spinner>(Resource.Id.drop_dadosDiarios);
            //cria o adapter usando o leiaute SimpleListItem e o arraylist
        adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, intervalo);
        //vincula o adaptador ao controle spinner
        spinner.Adapter = adapter;
            //define o evento ItemSelected para exibir o item selecionado
        spinner.ItemSelected += Spinner_ItemSelected;

            
        }
        public override void OnBackPressed()
        {
            this.Finish();
        }

        private void BtnGraficoTensao(object sender, EventArgs e)
        {
            if (dataEscolhida == null || dataEscolhida.Equals(""))
            {
                Toast.MakeText(this, "Por favor selecione uma data", ToastLength.Long).Show();
                return;
            }
            var plotModel = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,



                Title = "(V)"
            };

            var colunaTensao = CreateColunaTensao(dataEscolhida, 1);
            colunaTensao.Title = "Tensão (V)";
            colunaTensao.FillColor = OxyColor.Parse("#CC0099");
            colunaTensao.StrokeColor = OxyColor.Parse("#CCFF00");
            colunaTensao.StrokeThickness = 1;
            plotModel.Series.Add(colunaTensao);

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            categoryAxis.Labels.Add("6h");
            categoryAxis.Labels.Add("7h");
            categoryAxis.Labels.Add("8h");
            categoryAxis.Labels.Add("9h");
            categoryAxis.Labels.Add("10h");
            categoryAxis.Labels.Add("11h");
            categoryAxis.Labels.Add("12h");
            categoryAxis.Labels.Add("13h");
            categoryAxis.Labels.Add("14h");
            categoryAxis.Labels.Add("15h");
            categoryAxis.Labels.Add("16h");
            categoryAxis.Labels.Add("17h");
            categoryAxis.Labels.Add("18h");
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

            var categoryAxis2 = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis2.Labels.Add("1");
            categoryAxis2.Labels.Add("2");
            categoryAxis2.Labels.Add("3");
            categoryAxis2.Labels.Add("4");
            categoryAxis2.Labels.Add("5");
            categoryAxis2.Labels.Add("6");
            categoryAxis2.Labels.Add("7");
            categoryAxis2.Labels.Add("8");
            categoryAxis2.Labels.Add("9");
            categoryAxis2.Labels.Add("10");
            categoryAxis2.Labels.Add("11");
            categoryAxis2.Labels.Add("12");
            categoryAxis2.Labels.Add("13");
            categoryAxis2.Labels.Add("14");
            categoryAxis2.Labels.Add("15");
            categoryAxis2.Labels.Add("16");
            categoryAxis2.Labels.Add("17");
            categoryAxis2.Labels.Add("18");
            categoryAxis2.Labels.Add("19");
            categoryAxis2.Labels.Add("20");
           

            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

           

            view2.Model = plotModel;
     

        }
        private void BtnGraficoCorrente(object sender, EventArgs e)
        {
            if (dataEscolhida == null || dataEscolhida.Equals(""))
            {
                Toast.MakeText(this, "Por favor selecione uma data", ToastLength.Long).Show();
                return;
            }

            var plotModel = new PlotModel {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,



                Title = "(A)" };

            var colunacorrente = CreateColunaCorrente(dataEscolhida, 2);
            colunacorrente.Title = "Corrente(A): ";
            colunacorrente.FillColor = OxyColor.Parse("#ffbc01");
            colunacorrente.StrokeColor = OxyColor.Parse("#0026fd");
            colunacorrente.StrokeThickness = 1;
            plotModel.Series.Add(colunacorrente);

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            categoryAxis.Labels.Add("6h");
            categoryAxis.Labels.Add("7h");
            categoryAxis.Labels.Add("8h");
            categoryAxis.Labels.Add("9h");
            categoryAxis.Labels.Add("10h");
            categoryAxis.Labels.Add("11h");
            categoryAxis.Labels.Add("12h");
            categoryAxis.Labels.Add("13h");
            categoryAxis.Labels.Add("14h");
            categoryAxis.Labels.Add("15h");
            categoryAxis.Labels.Add("16h");
            categoryAxis.Labels.Add("17h");
            categoryAxis.Labels.Add("18h");
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

            var categoryAxis2 = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis2.Labels.Add("1");
            categoryAxis2.Labels.Add("2");
            categoryAxis2.Labels.Add("3");
            categoryAxis2.Labels.Add("4");
            categoryAxis2.Labels.Add("5");
            categoryAxis2.Labels.Add("6");
            categoryAxis2.Labels.Add("7");
            categoryAxis2.Labels.Add("8");
            categoryAxis2.Labels.Add("9");
            categoryAxis2.Labels.Add("10");
            categoryAxis2.Labels.Add("11");
            categoryAxis2.Labels.Add("12");
            categoryAxis2.Labels.Add("13");
            categoryAxis2.Labels.Add("14");
            categoryAxis2.Labels.Add("15");
            categoryAxis2.Labels.Add("16");
            categoryAxis2.Labels.Add("17");
            categoryAxis2.Labels.Add("18");
            categoryAxis2.Labels.Add("19");
            categoryAxis2.Labels.Add("20");


            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            view2.Model = plotModel;
         




        }
        private void BtnLuz(object sender, EventArgs e)
        {
            if (dataEscolhida == null || dataEscolhida.Equals(""))
            {
                Toast.MakeText(this, "Por favor selecione uma data", ToastLength.Long).Show();
                return;
            }

            var plotModel = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,



                Title = "(Leitura LDR)"
            };

            var colunaluz = CreateColunaLuz(dataEscolhida, 3);
            colunaluz.Title = "Luminosidade: ";
            colunaluz.FillColor = OxyColor.Parse("#0099FF");
            colunaluz.StrokeColor = OxyColor.Parse("#99FF99");
            colunaluz.StrokeThickness = 1;
            plotModel.Series.Add(colunaluz);

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            categoryAxis.Labels.Add("6h");
            categoryAxis.Labels.Add("7h");
            categoryAxis.Labels.Add("8h");
            categoryAxis.Labels.Add("9h");
            categoryAxis.Labels.Add("10h");
            categoryAxis.Labels.Add("11h");
            categoryAxis.Labels.Add("12h");
            categoryAxis.Labels.Add("13h");
            categoryAxis.Labels.Add("14h");
            categoryAxis.Labels.Add("15h");
            categoryAxis.Labels.Add("16h");
            categoryAxis.Labels.Add("17h");
            categoryAxis.Labels.Add("18h");
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

            var categoryAxis2 = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis2.Labels.Add("1");
            categoryAxis2.Labels.Add("2");
            categoryAxis2.Labels.Add("3");
            categoryAxis2.Labels.Add("4");
            categoryAxis2.Labels.Add("5");
            categoryAxis2.Labels.Add("6");
            categoryAxis2.Labels.Add("7");
            categoryAxis2.Labels.Add("8");
            categoryAxis2.Labels.Add("9");
            categoryAxis2.Labels.Add("10");
            categoryAxis2.Labels.Add("11");
            categoryAxis2.Labels.Add("12");
            categoryAxis2.Labels.Add("13");
            categoryAxis2.Labels.Add("14");
            categoryAxis2.Labels.Add("15");
            categoryAxis2.Labels.Add("16");
            categoryAxis2.Labels.Add("17");
            categoryAxis2.Labels.Add("18");
            categoryAxis2.Labels.Add("19");
            categoryAxis2.Labels.Add("20");


            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            view2.Model = plotModel;
          
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Data selecionada: {0}", spinner.GetItemAtPosition(e.Position));

            var data = intervalo[e.Position];
            dataEscolhida = data.ToString();

           



            var plotModel = new PlotModel {
                    
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0,

                Title = "Gráfico (V), (A), LDR" };

            var colunaTensao = CreateColunaTensao(dataEscolhida, 1);
            colunaTensao.Title = "Tensão (V)";
            colunaTensao.FillColor = OxyColor.Parse("#CC0099");
            colunaTensao.StrokeColor = OxyColor.Parse("#CCFF00");
            colunaTensao.StrokeThickness = 1;
            plotModel.Series.Add(colunaTensao);



            var colunacorrente = CreateColunaCorrente(dataEscolhida, 2);
            colunacorrente.Title = "Corrente (A): ";
            colunacorrente.FillColor = OxyColor.Parse("#ffbc01");
            colunacorrente.StrokeColor = OxyColor.Parse("#0026fd");
            colunacorrente.StrokeThickness = 1;
            plotModel.Series.Add(colunacorrente);



            var colunaluz = CreateColunaLuz(dataEscolhida, 3);
            colunaluz.Title = "LDR: ";
            colunaluz.FillColor = OxyColor.Parse("#0099FF");
            colunaluz.StrokeColor = OxyColor.Parse("#99FF99");
            colunaluz.StrokeThickness = 1;
            plotModel.Series.Add(colunaluz);

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom };
            categoryAxis.Labels.Add("6h");
            categoryAxis.Labels.Add("7h");
            categoryAxis.Labels.Add("8h");
            categoryAxis.Labels.Add("9h");
            categoryAxis.Labels.Add("10h");
            categoryAxis.Labels.Add("11h");
            categoryAxis.Labels.Add("12h");
            categoryAxis.Labels.Add("13h");
            categoryAxis.Labels.Add("14h");
            categoryAxis.Labels.Add("15h");
            categoryAxis.Labels.Add("16h");
            categoryAxis.Labels.Add("17h");
            categoryAxis.Labels.Add("18h");
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

            var categoryAxis2 = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis2.Labels.Add("1");
            categoryAxis2.Labels.Add("2");
            categoryAxis2.Labels.Add("3");
            categoryAxis2.Labels.Add("4");
            categoryAxis2.Labels.Add("5");
            categoryAxis2.Labels.Add("6");
            categoryAxis2.Labels.Add("7");
            categoryAxis2.Labels.Add("8");
            categoryAxis2.Labels.Add("9");
            categoryAxis2.Labels.Add("10");
            categoryAxis2.Labels.Add("11");
            categoryAxis2.Labels.Add("12");
            categoryAxis2.Labels.Add("13");
            categoryAxis2.Labels.Add("14");
            categoryAxis2.Labels.Add("15");
            categoryAxis2.Labels.Add("16");
            categoryAxis2.Labels.Add("17");
            categoryAxis2.Labels.Add("18");
            categoryAxis2.Labels.Add("19");
            categoryAxis2.Labels.Add("20");


            plotModel.Axes.Add(categoryAxis);
            plotModel.Axes.Add(valueAxis);

            view2.Model = plotModel;



            

            //view.Model = GraficoPlot(1, data.ToString());
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void GetIntervalo()
        {
            intervalo = new ArrayList();

            GraficoController consulta_data = new GraficoController();
            List<Obj_Plot> obj_Plot = new List<Obj_Plot>();


            var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
            string token = dadosUsuario.GetString("Codigo", null);

            obj_Plot = consulta_data.ConsultarDatas(token);

            for (int i = 0; i < obj_Plot.Count; i++)
            {
                intervalo.Add(obj_Plot[i].Id_dia);

            }
        }
        private static ColumnSeries CreateColunaTensao(string dataEscolhida, int tipo)
        {
      
            GraficoController consulta_dados = new GraficoController();
            List<Obj_Plot> obj_Plot = new List<Obj_Plot>();
            var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
            string token = dadosUsuario.GetString("Codigo", null);

            obj_Plot = consulta_dados.ConsultarDadosPlot(token, tipo, dataEscolhida);

            var LineSeries = new OxyPlot.Series.ColumnSeries();

            for (int i = 0; i < obj_Plot.Count; i++)
            {
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Seis));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Sete));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Oito));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Nove));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dez));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Onze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Doze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Treze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Catorze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Quinze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezeseis));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezesete));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezoito));

            }


            return LineSeries;



        }

        private static ColumnSeries CreateColunaLuz(string dataEscolhida, int tipo)
        {

            GraficoController consulta_dados = new GraficoController();
            List<Obj_Plot> obj_Plot = new List<Obj_Plot>();
            var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
            string token = dadosUsuario.GetString("Codigo", null);

            obj_Plot = consulta_dados.ConsultarDadosPlot(token, tipo, dataEscolhida);

            var LineSeries = new OxyPlot.Series.ColumnSeries();


            for (int i = 0; i < obj_Plot.Count; i++)
            {
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Seis));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Sete));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Oito));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Nove));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dez));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Onze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Doze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Treze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Catorze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Quinze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezeseis));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezesete));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezoito));

            }
            return LineSeries;
        }
        private static ColumnSeries CreateColunaCorrente(string dataEscolhida, int tipo)
        {

            GraficoController consulta_dados = new GraficoController();
            List<Obj_Plot> obj_Plot = new List<Obj_Plot>();
            var dadosUsuario = Application.Context.GetSharedPreferences("usuario", Android.Content.FileCreationMode.Private);
            string token = dadosUsuario.GetString("Codigo", null);

            obj_Plot =consulta_dados.ConsultarDadosPlot(token, tipo, dataEscolhida);

            var LineSeries = new OxyPlot.Series.ColumnSeries();


            for(int i=0;i< obj_Plot.Count; i++)
            {
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Seis));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Sete));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Oito));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Nove));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dez));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Onze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Doze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Treze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Catorze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Quinze));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezeseis));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezesete));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezoito));
               
            }




            return LineSeries;
        }


       
    }
}
  

