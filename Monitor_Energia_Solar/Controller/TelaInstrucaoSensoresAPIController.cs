using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Monitor_Energia_Solar.Controller
{
    public class TelaInstrucaoSensoresAPIController
    {
        public string ConsultarTextoBussola()
        {

            string texto = "";
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();


            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_texto_bussola");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                texto = Convert.ToString(dataRow["Texto_bussola"]);
            }

            conectionmysql.Close();

            return texto;


        }
        public string ConsultarTextoInclinacao()
        {

            string texto = "";
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();


            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_texto_inclinacao");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                texto = Convert.ToString(dataRow["Texto_Inclinacao"]);
            }

            conectionmysql.Close();

            return texto;


        }
        public string ConsultarTextoBandeira()
        {

            string texto = "";
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();


            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_texto_bandeira");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                texto = Convert.ToString(dataRow["Texto_bandeira"]);
            }

            conectionmysql.Close();

            return texto;


        }
        public string ConsultarTextoTarifas()
        {

            string texto = "";
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();


            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_texto_tarifa");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                texto = Convert.ToString(dataRow["Texto_Tarifas"]);
            }

            conectionmysql.Close();

            return texto;


        }
    }
}