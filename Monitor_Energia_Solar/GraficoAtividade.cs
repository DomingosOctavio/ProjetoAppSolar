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
        RadioGroup rdgrp1;
        PlotView view;

        protected override void OnCreate(Bundle savedInstanceState)
        {
          
            
            base.OnCreate(savedInstanceState);           

            SetContentView(Resource.Layout.plot_geracao);

            rdgrp1 = FindViewById<RadioGroup>(Resource.Id.radioGroup1);

            view = FindViewById<PlotView>(Resource.Id.plot_view);

            RadioButton checkedRadioButton = FindViewById<RadioButton>(rdgrp1.CheckedRadioButtonId);

            RadioButton linha = FindViewById<RadioButton>(Resource.Id.linha);
            RadioButton barras = FindViewById<RadioButton>(Resource.Id.barras);

            linha.Click += RadioButtonClick1;
            barras.Click += RadioButtonClick2;


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

        private void RadioButtonClick1(object sender, EventArgs e)
        {
           
            RadioButton rb = (RadioButton)sender;
            Toast.MakeText(this, rb.Text, ToastLength.Short).Show();
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
                string msg = ex.Message;
            }
   

             
        }
        private void RadioButtonClick2(object sender, EventArgs e)
        {
           
            RadioButton rb = (RadioButton)sender;
            Toast.MakeText(this, rb.Text, ToastLength.Short).Show();

            try
            {
                view.Model = Grafico2();
            }
            catch (MySqlException ex)
            {
                string msg = ex.Message;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

           

        }

        private void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string toast = string.Format("Pedíodo selecionado: {0}", spinner.GetItemAtPosition(e.Position));
            int posicao= e.Position;

            if (posicao == 1) // Dados diarios
            {
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
            Context mContext = Android.App.Application.Context;
            Session_classe ap = new Session_classe(mContext);
            string key = ap.getAccessKey();
            string token = key.ToString();

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
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum =25, Minimum = 0 }); //hora

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
            Context mContext = Android.App.Application.Context;
            Session_Token ap = new Session_Token(mContext);
            string key = ap.getAccessKey();
            string token = key.ToString();

            Consulta_Dados_Banco_Dados consulta_grafico = new Consulta_Dados_Banco_Dados();
            List<Obj_Dados_Grafico.Grafico> lista_objetos_grafico = new List<Obj_Dados_Grafico.Grafico>();

            lista_objetos_grafico = consulta_grafico.Consulta_dados_Grafico(token);

            var model = new PlotModel
            {
                Title = "Teste 1",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0

            };

            var s1 = new BarSeries { Title = "2015", StrokeColor = OxyColors.Black, FillColor = OxyColors.BlueViolet, StrokeThickness = 1 };
            var s2 = new BarSeries { Title = "2016", StrokeColor = OxyColors.Aqua, FillColor = OxyColors.CadetBlue, StrokeThickness = 1 };
            var s3 = new BarSeries { Title = "2018", StrokeColor = OxyColors.Aquamarine, FillColor = OxyColors.Azure, StrokeThickness = 1 };

            foreach (Obj_Dados_Grafico.Grafico lista in lista_objetos_grafico)
            {
           
        
                s1.Items.Add(new BarItem { Value = lista.tensao });
                s2.Items.Add(new BarItem { Value = lista.corrente });
                s3.Items.Add(new BarItem { Value = lista.luminosidade });

        
            }


        

            var eixoCategoria = new CategoryAxis { Position = AxisPosition.Left };
            eixoCategoria.Labels.Add("Tensão");
            eixoCategoria.Labels.Add("Corrente");
            eixoCategoria.Labels.Add("Luminosidade");
 

            var valueAxis = new LinearAxis
            {
                Position = AxisPosition.Bottom,
                MinimumPadding = 0,
                MaximumPadding = 0.06,
                AbsoluteMinimum = 0
                
            };

            model.Series.Add(s1);
            model.Series.Add(s2);
            model.Series.Add(s3);

            model.Axes.Add(eixoCategoria);
            model.Axes.Add(valueAxis);

            return model;
        }

    }
}