using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlConnector;
using System.Data;

namespace Monitor_Energia_Solar
{
    class BancoLogin
    {
        int resultado;
        public Obj_Banco_Dados Consulta_login(string usuario, string senha)
        {
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();

            conection.AdicionarParametros("usuario", usuario);
            conection.AdicionarParametros("senha", senha);
            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_login");
            Obj_Banco_Dados obj_Banco_Dados = new Obj_Banco_Dados();

            foreach (DataRow dataRow in dataTable.Rows)
            {

                obj_Banco_Dados.Id =  Convert.ToInt32(dataRow["id"]);
                obj_Banco_Dados.Usuario = Convert.ToString(dataRow["usuario"]);
                obj_Banco_Dados.Senha = Convert.ToString(dataRow["senha"]);
                obj_Banco_Dados.Cod = Convert.ToString(dataRow["cod"]);
                obj_Banco_Dados.IP_conexao = Convert.ToString(dataRow["ip_conexao"]);
            }
            conectionmysql.Close();

            return obj_Banco_Dados;
        }

        public int Consulta_token(string token)
        {
           
        
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();

            conection.AdicionarParametros("token", token);
            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_Token");
    
            foreach (DataRow dataRow in dataTable.Rows)
            {
                resultado = Convert.ToInt32(dataRow["count(login.cod)"]);        
            }

   
            conectionmysql.Close();
            return resultado;

        }

        public void InserirDadosPessoais(string usuario, int senha, string email, string token)
        {
      

            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();



            conection.AdicionarParametros("usuar", usuario);
            conection.AdicionarParametros("senh", senha);
            conection.AdicionarParametros("emai", email);
            conection.AdicionarParametros("token", token);

            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Update_dados_Pessoais");
         
            conectionmysql.Close();


        }

        public string ConsultarIp(string token)
        {

            string resultado = "";
            Conexao_Banco_Dados conection = new Conexao_Banco_Dados();
            MySqlConnection conectionmysql = conection.CriarConexao();

            
            conection.AdicionarParametros("token", token);

            DataTable dataTable = conection.ExecutarConsulta(CommandType.StoredProcedure, "Select_ip");
            foreach (DataRow dataRow in dataTable.Rows)
            {
                 resultado = Convert.ToString(dataRow["ip_conexao"]);
            }

            conectionmysql.Close();

            return resultado;


        }
    }
}
