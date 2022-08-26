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

namespace Monitor_Energia_Solar.Controller
{
    class LoginController
    {
        public Obj_Banco_Dados BuscardadosLogin(string usuario, string senha)
        {
            BancoLogin obj_banco_login = new BancoLogin();
            Obj_Banco_Dados obj_banco = new Obj_Banco_Dados();
            obj_banco = obj_banco_login.Consulta_login(usuario, senha);

            return obj_banco;

        }
        public Obj_Banco_Dados RecuperarDadosPeloEmail(string token)
        {
           
            BancoLogin obj_banco_login = new BancoLogin();
            return  obj_banco_login.RecuperarDadosEmail(token);
        }
    }
}