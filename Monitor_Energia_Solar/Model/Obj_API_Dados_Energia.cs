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
    public class Obj_API_Dados_Energia
    {
        public class Bandeiras
        {
            public int Id { get; set; }
            public int FlagType { get; set; }
            public string Month { get; set; }
            public double Value { get; set; }
        }

        public class Root_Bandeiras
        {
            public int total { get; set; }
            public List<Bandeiras> items { get; set; }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class TarifasArray
        {
            public string agente { get; set; }
            public string validadesde { get; set; }
            public string subgrupo { get; set; }
            public string modalidade { get; set; }
            public string acessante { get; set; }
            public string classe { get; set; }
            public string subclasse { get; set; }
            public string posto { get; set; }
            public string tarifademandatusd { get; set; }
            public string tarifaconsumotusd { get; set; }
            public string tarifaconsumote { get; set; }
        }

        public class Root_Tarifas
        {
            public List<TarifasArray> TarifasArray { get; set; }
        }


    }
}