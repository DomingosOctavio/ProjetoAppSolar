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

namespace Monitor_Energia_Solar
{
    public class Obj_Dados_WebServer
    {

        public float Tensao { get; set; }
        public float Corrente { get; set; }
        public float Luminosidade { get; set; }
        public string Horario { get; set; }
        public string mensagem { get; set; }

    }
}