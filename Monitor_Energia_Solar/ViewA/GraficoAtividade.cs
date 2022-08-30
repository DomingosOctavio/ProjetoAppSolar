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
            SetContentView(Resource.Layout.plot_geracao);

            view2 = FindViewById<PlotView>(Resource.Id.plot_view);

            btnTensao = FindViewById<Button>(Resource.Id.btnTensao);
            btnCorrente = FindViewById<Button>(Resource.Id.btnCorrente);
            btnLuz = FindViewById<Button>(Resource.Id.btnLuz);

            btnTensao.Click += BtnGraficoTensao;
            btnCorrente.Click += BtnGraficoCorrente;
            btnLuz.Click += BtnLuz;

          
         

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


        private void BtnGraficoTensao(object sender, EventArgs e)
        {
            var plotModel = new PlotModel { Title = "" };

            var colunaTensao = CreateColunaTensao(dataEscolhida, 1);
            colunaTensao.Title = "seriesString";
            colunaTensao.FillColor = OxyColor.Parse("#CC0099");
            colunaTensao.StrokeColor = OxyColor.Parse("#CCFF00");
            colunaTensao.StrokeThickness = 1;
            plotModel.Series.Add(colunaTensao);

            view2.Model = plotModel;

        }
        private void BtnGraficoCorrente(object sender, EventArgs e)
        {
            var plotModel = new PlotModel { Title = "" };

            var colunacorrente = CreateColunaCorrente(dataEscolhida, 2);
            colunacorrente.Title = "";
            colunacorrente.FillColor = OxyColor.Parse("#ffbc01");
            colunacorrente.StrokeColor = OxyColor.Parse("#0026fd");
            colunacorrente.StrokeThickness = 1;
            plotModel.Series.Add(colunacorrente);

            view2.Model = plotModel;


        }
        private void BtnLuz(object sender, EventArgs e)
        {
            var plotModel = new PlotModel { Title = "" };

            var colunaluz = CreateColunaLuz(dataEscolhida, 3);
            colunaluz.Title = "";
            colunaluz.FillColor = OxyColor.Parse("#0099FF");
            colunaluz.StrokeColor = OxyColor.Parse("#99FF99");
            colunaluz.StrokeThickness = 1;
            plotModel.Series.Add(colunaluz);

            view2.Model = plotModel;
        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Pedíodo selecionado: {0}", spinner.GetItemAtPosition(e.Position));

            var data = intervalo[e.Position];
            dataEscolhida = data.ToString();

            var plotModel = new PlotModel { Title = "" };

            var colunaTensao = CreateColunaTensao(dataEscolhida, 1);
            colunaTensao.Title = "seriesString";
            colunaTensao.FillColor = OxyColor.Parse("#CC0099");
            colunaTensao.StrokeColor = OxyColor.Parse("#CCFF00");
            colunaTensao.StrokeThickness = 1;
            plotModel.Series.Add(colunaTensao);



            var colunacorrente = CreateColunaCorrente(dataEscolhida, 2);
            colunacorrente.Title = "";
            colunacorrente.FillColor = OxyColor.Parse("#ffbc01");
            colunacorrente.StrokeColor = OxyColor.Parse("#0026fd");
            colunacorrente.StrokeThickness = 1;
            plotModel.Series.Add(colunacorrente);



            var colunaluz = CreateColunaLuz(dataEscolhida, 3);
            colunaluz.Title = "";
            colunaluz.FillColor = OxyColor.Parse("#0099FF");
            colunaluz.StrokeColor = OxyColor.Parse("#99FF99");
            colunaluz.StrokeThickness = 1;
            plotModel.Series.Add(colunaluz);

            view2.Model = plotModel;
            //view.Model = GraficoPlot(1, data.ToString());
            Toast.MakeText(this, toast, ToastLength.Long).Show();
        }

        private void GetIntervalo()
        {
            intervalo = new ArrayList();

            GraficoController consulta_data = new GraficoController();
            List<Obj_Plot> obj_Plot = new List<Obj_Plot>();

            intervalo.Add("--Selecione a data para visualizar os dados");
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
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Seis, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Sete, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Oito, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Nove, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dez, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Onze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Doze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Treze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Catorze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Quinze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezeseis, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezesete, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezoito, i));
            
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
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Seis, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Sete, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Oito, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Nove, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dez, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Onze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Doze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Treze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Catorze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Quinze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezeseis, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezesete, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezoito, i));

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
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Seis,  i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Sete, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Oito, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Nove, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dez, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Onze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Doze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Treze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Catorze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Quinze, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezeseis, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezesete, i));
                LineSeries.Items.Add(new OxyPlot.Series.ColumnItem(obj_Plot[i].Dezoito, i));
               
            }




            return LineSeries;
        }


       
    }
}
  

