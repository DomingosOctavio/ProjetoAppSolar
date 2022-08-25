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

namespace Monitor_Energia_Solar
{
    [Activity(Label = "DropdownEmpresasAtividade")]
    public class GraficoAtividade : Activity
    {
        Spinner spinner;
        ArrayAdapter adapter;
        ArrayList intervalo;
   
        PlotView view;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.plot_geracao);



            view = FindViewById<PlotView>(Resource.Id.plot_view);




            // Em seguida, escreva o Método  
            try
            {
                view.Model = Grafico1();
            }
            catch (MySqlException ex)
            {
                string msg = ex.Message;
            }
            catch (Exception ex)
            {
                view.Model = Grafico2();
                string msg = ex.Message;
            }



            //preenche o arraylist com os dados
            GetIntervalo();
            //cria a instância do spinner declarado no arquivo Main
            spinner = FindViewById<Spinner>(Resource.Id.drop_companhias);
            //cria o adapter usando o leiaute SimpleListItem e o arraylist
            adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, intervalo);
            //vincula o adaptador ao controle spinner
            spinner.Adapter = adapter;
            //define o evento ItemSelected para exibir o item selecionado
            spinner.ItemSelected += Spinner_ItemSelected;
        }




        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Pedíodo selecionado: {0}", spinner.GetItemAtPosition(e.Position));
            int posicao = e.Position;

            if (posicao == 1) // Dados diarios
            {
                Grafico1();
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }
            else if (posicao == 2) // Dados mensais
            {
                Toast.MakeText(this, toast, ToastLength.Long).Show();
            }

        }

        private void GetIntervalo()
        {
            intervalo = new ArrayList();
            intervalo.Add("--Selecione o intervalo de tempo--");
            intervalo.Add("Dados Diários");
            intervalo.Add("Dados Mensais");
        }

        public PlotModel Grafico1()
        {
            Context mContext1 = Android.App.Application.Context;
            Session_Token ap2 = new Session_Token(mContext1);
            string token = ap2.getAccessKey().ToString();

            Consulta_Dados_Banco_Dados consulta_grafico = new Consulta_Dados_Banco_Dados();
            List<Obj_Dados_Grafico.Grafico> lista_objetos_grafico = new List<Obj_Dados_Grafico.Grafico>();

            try
            {
                lista_objetos_grafico = consulta_grafico.Consulta_dados_Grafico(token);
            }
            catch (MySqlException e)
            {
                string msg = e.Message;
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }


            var plotModel = new PlotModel { Title = "Gráfico teste" };
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = 23, Minimum = 0 }); //tensao
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 25, Minimum = 0 }); //hora

            var series1 = new LineSeries
            {
                Color = OxyColors.DarkBlue,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerStroke = OxyColors.DarkBlue,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 4.0
            };

            var series2 = new LineSeries
            {
                Color = OxyColors.DarkOrange,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerStroke = OxyColors.DarkRed,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 4.0
            };

            var series3 = new LineSeries
            {
                Color = OxyColors.LightGreen,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerStroke = OxyColors.YellowGreen,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 4.0
            };

            DateTime s;
            double hora;
            String hora_string;

            foreach (Obj_Dados_Grafico.Grafico lista in lista_objetos_grafico)
            {
                s = DateTime.Parse(lista.time);
                hora_string = (s.ToString("HH"));
                hora = Convert.ToDouble(hora_string);
                series1.Points.Add(new DataPoint(hora, lista.tensao));
                series2.Points.Add(new DataPoint(hora, lista.corrente));
                series3.Points.Add(new DataPoint(hora, lista.luminosidade));
            }

            plotModel.Series.Add(series1);
            plotModel.Series.Add(series2);
            plotModel.Series.Add(series3);

            return plotModel;
        }






        public PlotModel Grafico2()
        {
            Context mContext1 = Android.App.Application.Context;
            Session_Token ap2 = new Session_Token(mContext1);
            string token = ap2.getAccessKey().ToString();

            Consulta_Dados_Banco_Dados consulta_grafico = new Consulta_Dados_Banco_Dados();
            List<Obj_Dados_Grafico.Grafico> lista_objetos_grafico = new List<Obj_Dados_Grafico.Grafico>();

            try
            {
                lista_objetos_grafico = consulta_grafico.Consulta_dados_Grafico(token);
            }
            catch (MySqlException e)
            {
                string msg = e.Message;
            }
            catch (Exception e)
            {
                string msg = e.Message;
            }


            var plotModel = new PlotModel { Title = "Gráfico teste" };
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Maximum = 23, Minimum = 0 }); //tensao
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 25, Minimum = 0 }); //hora

            var series1 = new LineSeries
            {
                Color = OxyColors.DarkBlue,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerStroke = OxyColors.DarkBlue,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 4.0
            };

            var series2 = new LineSeries
            {
                Color = OxyColors.DarkOrange,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerStroke = OxyColors.DarkRed,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 4.0
            };

            var series3 = new LineSeries
            {
                Color = OxyColors.LightGreen,
                MarkerType = MarkerType.Circle,
                MarkerSize = 3,
                MarkerStroke = OxyColors.YellowGreen,
                MarkerFill = OxyColors.Black,
                MarkerStrokeThickness = 4.0
            };

            DateTime s;
            double hora;
            String hora_string;

            foreach (Obj_Dados_Grafico.Grafico lista in lista_objetos_grafico)
            {
                s = DateTime.Parse(lista.time);
                hora_string = (s.ToString("HH"));
                hora = Convert.ToDouble(hora_string);
                series1.Points.Add(new DataPoint(hora, lista.tensao));
                series2.Points.Add(new DataPoint(hora, lista.corrente));
                series3.Points.Add(new DataPoint(hora, lista.luminosidade));
            }

            plotModel.Series.Add(series1);
            plotModel.Series.Add(series2);
            plotModel.Series.Add(series3);

            return plotModel;
        }
    }
}
