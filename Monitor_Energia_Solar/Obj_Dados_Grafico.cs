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
    public class Obj_Dados_Grafico
    {
        public class Grafico
        {
            public double tensao { get; set; }
            public double corrente { get; set; }
            public double luminosidade { get; set; }
            public string time { get; set; }
            public DateTime data_ard { get; set; }

        }

        public class Root_Grafico
        {
            public int total { get; set; }
            public List<Grafico> items { get; set; }
        }
    }
}