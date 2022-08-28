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

        PlotView view2;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.plot_geracao);

            view2 = FindViewById<PlotView>(Resource.Id.plot_view);

            view2.Model = GraficoPlotNovo();
            // Em seguida, escreva o Método  
            //try
            //{
            //    view.Model = GraficoTensao(1);
            //}
            //catch (MySqlException ex)
            //{
            //    string msg = ex.Message;
            //}
            //catch (Exception ex)
            //{

            //    string msg = ex.Message;
            //}


        }
        //    //preenche o arraylist com os dados
        //    //GetIntervalo();
        //    //cria a instância do spinner declarado no arquivo Main
        //    spinner = FindViewById<Spinner>(Resource.Id.drop_dadosDiarios);
        //    //cria o adapter usando o leiaute SimpleListItem e o arraylist
        //    adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, intervalo);
        //    //vincula o adaptador ao controle spinner
        //    spinner.Adapter = adapter;
        //    //define o evento ItemSelected para exibir o item selecionado
        //    spinner.ItemSelected += Spinner_ItemSelected;
        //}




        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Pedíodo selecionado: {0}", spinner.GetItemAtPosition(e.Position));

            var data = intervalo[e.Position];

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
        private static LinearBarSeries CreateExampleLinearBarSeries()
        {
            var linearBarSeries = new LinearBarSeries();


            linearBarSeries.Points.Add(new DataPoint(0, 1));
            linearBarSeries.Points.Add(new DataPoint(1, 2));
            linearBarSeries.Points.Add(new DataPoint(2, 3));
            linearBarSeries.Points.Add(new DataPoint(3, 4));
            linearBarSeries.Points.Add(new DataPoint(4, 5));
            linearBarSeries.Points.Add(new DataPoint(5, 6));
            linearBarSeries.Points.Add(new DataPoint(6, 7));
            linearBarSeries.Points.Add(new DataPoint(7, 8));

            return linearBarSeries;
        }
        public PlotModel GraficoPlotNovo()
        {

            var model = new PlotModel { Title = "LinearBarSeries with stroke" };
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left });
            var linearBarSeries = CreateExampleLinearBarSeries();
            linearBarSeries.Title = "LinearBarSeries";
            linearBarSeries.FillColor = OxyColor.Parse("#454CAF50");
            linearBarSeries.StrokeColor = OxyColor.Parse("#4CAF50");
            linearBarSeries.StrokeThickness = 1;
            model.Series.Add(linearBarSeries);

            return model;
        }
    }



}
  

