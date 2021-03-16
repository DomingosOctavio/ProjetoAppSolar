using System;
using System.Data;
using System.Security.Permissions;

using MySqlConnector;

namespace Monitor_Energia_Solar
{
    class Conexao_Banco_Dados
    {
        //define o dataset


        
            //MySqlConnection connection = new MySqlConnection("Server=31.170.166.124;User Id=u232657111_arduino;Password=Astecas1508;database=u232657111_bd");
        
        public int contador = 0;

        //Criar Conexão
        public void FecharConexao(MySqlConnection connection)
        {
            connection.Close();
        }
        public MySqlConnection CriarConexao()
        {
            MySqlConnection connection = new MySqlConnection("Server=31.170.166.124;User Id=u232657111_arduino;Password=Astecas1508;database=u232657111_bd");
            //127.0.0.1:61468

            connection.Open();
            return connection;

            //  return new MySqlConnection(Settings.Default.StringConexao);

        }
        //Parâmetros que vão para o banco

        private MySqlParameterCollection mysqlParameterCollection = new MySqlCommand().Parameters;




        /*public void GuardarImagem(CommandType commandType, string nomeStoreProcedureOuTextoSql,string caminho, string parametro)
        {
            MySqlConnection mysqlConnection = CriarConexao();

            mysqlConnection.Open();

            System.IO.FileStream fs;
            Byte[] bindata;
            MySqlParameter picpara;

            MySqlCommand mysqlcommand = mysqlConnection.CreateCommand();

            //Colocando as coisas dentro do comando (dento da caixa que vai trafegar na Conexao)

            mysqlcommand.CommandType = commandType;

            mysqlcommand.CommandText = nomeStoreProcedureOuTextoSql;

            mysqlcommand.CommandTimeout = 7200; // Em segundos

           // cmd = new MySqlCommand("INSERT INTO mypic (pic) VALUES(?pic)", conn);
            picpara = mysqlcommand.Parameters.Add(parametro, MySqlDbType.MediumBlob);
            mysqlcommand.Prepare();

            //txtPicPath is the path of the image, e.g. C:\MyPic.png

            fs = new FileStream(caminho, FileMode.Open, FileAccess.Read);
            bindata = new byte[Convert.ToInt32(fs.Length)];
            fs.Read(bindata, 0, Convert.ToInt32(fs.Length));
            fs.Close();

            picpara.Value = bindata;
            mysqlcommand.ExecuteNonQuery();
        }*/



        public void LimparParametros()

        {

            mysqlParameterCollection.Clear();

        }

        public void AdicionarParametros(string nomeParametro, object valorParamero)

        {

            mysqlParameterCollection.Add(new MySqlParameter(nomeParametro, valorParamero));

        }
        public void Adicionar_Parametro(string dado, object valor)
        {

            mysqlParameterCollection.AddWithValue(dado, valor);

        }


        //Persistência de dados - Inserir, Alterar, Excluir

        public object ExecutarManipulacao(CommandType commandType, string nomeStoreProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexao

                using (MySqlConnection mysqlConnection = CriarConexao())
                {
                    //Abrir conexão

                    mysqlConnection.Open();

                    //Criar o comando que vai levar a informação para o banco.

                    MySqlCommand mysqlcommand = mysqlConnection.CreateCommand();

                    //Colocando as coisas dentro do comando (dento da caixa que vai trafegar na Conexao)

                    mysqlcommand.CommandType = commandType;

                    mysqlcommand.CommandText = nomeStoreProcedureOuTextoSql;

                    mysqlcommand.CommandTimeout = 7200; // Em segundos



                    //Adicionar os perâmetros no comando

                    foreach (MySqlParameter mysqlParameter in mysqlParameterCollection)

                    {

                        mysqlcommand.Parameters.Add(new MySqlParameter(mysqlParameter.ParameterName, mysqlParameter.Value));

                    }

                    //Executar o comando, ou seja,mandar o comando ir até o banco de dados.

                    return mysqlcommand.ExecuteScalar();
                }
            }

            catch (Exception ex)

            {

                throw new Exception(ex.Message);

            }


        }

        //Consultar registros do banco de dados
        public DataTable ExecutarConsulta(CommandType commandType, string nomeStoreProcedureOuTextoSql)
        {
            try
            {
                //Criar a conexao

                using (MySqlConnection mysqlConnection = CriarConexao())
                {
                    //Abrir conexão

                 

                    //Criar o comando que vai levar a informação para o banco.

                    MySqlCommand mysqlcommand = mysqlConnection.CreateCommand();

                    //Colocando as coisas dentro do comando (dento da caixa que vai trafegar na Conexao)

                    mysqlcommand.CommandType = commandType;

                    mysqlcommand.CommandText = nomeStoreProcedureOuTextoSql;

                    mysqlcommand.CommandTimeout = 7200; // Em segundos



                    //Adicionar os perâmetros no comando

                    foreach (MySqlParameter mysqlParameter in mysqlParameterCollection)

                    {

                        mysqlcommand.Parameters.Add(new MySqlParameter(mysqlParameter.ParameterName, mysqlParameter.Value));

                    }

                    //Criar um adaptador

                    MySqlDataAdapter mysqlDataAdapter = new MySqlDataAdapter(mysqlcommand);

                    //DataTable = Tabela de Dados vazia onde colocar os dados que vem do banco

                    DataTable dataTable = new DataTable();

                    //Mandar o comando até o banco buscar os dados eo adaptador preencher o datatable

                    mysqlDataAdapter.Fill(dataTable);

                    return dataTable;
                }
            }

            catch (Exception ex)

            {

                throw new Exception(ex.Message);

            }

        }


    }
}
