using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Monitor_Energia_Solar.Controller
{
    class MainActivityController
    {
        public static string IP_atual;

        public String BuscarIp(string Token)
        {
        
            BancoLogin banco = new BancoLogin();
            IP_atual = "http://" + banco.ConsultarIp(Token) + "/";

            return IP_atual;
        }

    }
}