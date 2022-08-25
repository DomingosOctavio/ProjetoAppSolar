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

        public String BuscarIp()
        {
            Context mContext = Android.App.Application.Context;

            Session_Token ap5 = new Session_Token(mContext);
            string UserID5 = ap5.getAccessKey().ToString();

            BancoLogin banco = new BancoLogin();
            IP_atual = "http://" + banco.ConsultarIp(UserID5) + "/";

            return IP_atual;
        }

    }
}