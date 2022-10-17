using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Monitor_Energia_Solar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor_Energia_Solar.Controller
{
    public class GraficoController
    {
        public List<Obj_Plot> ConsultarDadosPlot(string token, int Cod_procedure, string dia)
        {

            Consulta_Dados_Banco_Dados consulta_grafico = new Consulta_Dados_Banco_Dados();
            return consulta_grafico.Consulta_dados_Grafico(token, Cod_procedure, dia);
        }

        public List<Obj_Plot> ConsultarDatas(string token)
        {
            Consulta_Dados_Banco_Dados consulta_grafico = new Consulta_Dados_Banco_Dados();
            return consulta_grafico.Consulta_Dias(token);
        }

    }
}