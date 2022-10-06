using Android.App;
using Android.Content;
using Android.OS;
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
    class Tab_Login
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public string IP_conexao { get; set; }
        public string Cod { get; set; }

        public string Email { get; set; }





    }
}