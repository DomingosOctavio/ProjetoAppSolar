using System;
using System.Collections.Generic;
using System.Data;
using MySqlConnector;

namespace Monitor_Energia_Solar
{
    class Consulta_Dados_Banco_Dados
    {
        Conexao_Banco_Dados Conexao_Banco_Dados = new Conexao_Banco_Dados();

    public List<Obj_Dados_Grafico.Grafico> Consulta_dados_Grafico(string token)
        {
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();


            conection.AdicionarParametros("token", token);
            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_grafico_token");

            List<Obj_Dados_Grafico.Grafico> lista_objetos_grafico = new List<Obj_Dados_Grafico.Grafico>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                Obj_Dados_Grafico.Grafico objeto_grafico = new Obj_Dados_Grafico.Grafico();
                //    ID = Convert.ToInt32(dataRow["ID"]),
                objeto_grafico.tensao  = Convert.ToDouble(dataRow["tensao"]);
                objeto_grafico.corrente = Convert.ToDouble(dataRow["corrente"]);
                objeto_grafico.luminosidade = Convert.ToDouble(dataRow["luminosidade"]);
                objeto_grafico.time = Convert.ToString(dataRow["hora"]);
                objeto_grafico.data_ard = Convert.ToDateTime(dataRow["data_ard"]);

                lista_objetos_grafico.Add(objeto_grafico);
            }
            conectionmysql.Close();
         
            return lista_objetos_grafico;
        }
    }
}
