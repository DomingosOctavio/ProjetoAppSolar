using System;
using System.Collections.Generic;
using System.Data;
using Monitor_Energia_Solar.Model;
using MySqlConnector;

namespace Monitor_Energia_Solar
{
    public class Consulta_Dados_Banco_Dados
    {

        public List<Obj_Plot> Consulta_Dias(string token)
        {
           
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();


            conection.AdicionarParametros("token", token);
            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_Recuperar_Datas");

            List<Obj_Plot> lista_objetos_grafico = new List<Obj_Plot>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Obj_Plot objeto_grafico = new Obj_Plot();
                objeto_grafico.Id_dia = Convert.ToString(dataRow["Id_dia"]);
  
                lista_objetos_grafico.Add(objeto_grafico);
            }
            conectionmysql.Close();

            return lista_objetos_grafico;
        }

        public List<Obj_Plot> Consulta_dados_Grafico(string token, int procedure, string dia)
        {
            string EscolhaProcedure = "";

            if(procedure == 1) //tensao
            {
                EscolhaProcedure = "SelectTensaoPlot";
            }
            else if(procedure == 2) // corrente
            {
                EscolhaProcedure = "SelectCorrentePlot";
            }
            else if (procedure == 3) // luminosidade
            {
                EscolhaProcedure = "SelectLuminosidadePlot";
            }


            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();


            conection.AdicionarParametros("token", token);
            conection.AdicionarParametros("dia", dia);

            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, EscolhaProcedure);

            List<Obj_Plot> lista_objetos_grafico = new List<Obj_Plot>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Obj_Plot objeto_grafico = new Obj_Plot();
                //    ID = Convert.ToInt32(dataRow["ID"]),
                objeto_grafico.Id_dia = Convert.ToString(dataRow["Id_dia"]);
                objeto_grafico.Seis = Convert.ToDouble(dataRow["6h"]);
                objeto_grafico.Sete = Convert.ToDouble(dataRow["7h"]);
                objeto_grafico.Oito = Convert.ToDouble(dataRow["8h"]);
                objeto_grafico.Nove = Convert.ToDouble(dataRow["9h"]);
                objeto_grafico.Dez = Convert.ToDouble(dataRow["10h"]);
                objeto_grafico.Onze = Convert.ToDouble(dataRow["11h"]);
                objeto_grafico.Doze = Convert.ToDouble(dataRow["12h"]);
                objeto_grafico.Treze = Convert.ToDouble(dataRow["13h"]);
                objeto_grafico.Catorze = Convert.ToDouble(dataRow["14h"]);
                objeto_grafico.Quinze = Convert.ToDouble(dataRow["15h"]);
                objeto_grafico.Dezeseis = Convert.ToDouble(dataRow["16h"]);
                objeto_grafico.Dezesete = Convert.ToDouble(dataRow["17h"]);
                objeto_grafico.Dezoito = Convert.ToDouble(dataRow["18h"]);
                objeto_grafico.Token = Convert.ToString(dataRow["TOKEN"]);

             lista_objetos_grafico.Add(objeto_grafico);
            }
            conectionmysql.Close();
         
            return lista_objetos_grafico;
        }
    }
}
