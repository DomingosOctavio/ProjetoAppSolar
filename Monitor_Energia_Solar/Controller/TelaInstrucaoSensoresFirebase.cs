using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Monitor_Energia_Solar.Controller
{
    public class TelaInstrucaoSensoresFirebase
    {
        private IFirebaseClient client = null;

        private FirebaseResponse response_ = null;
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = CredencialFirebase.DboAnahtar.AuthSecret,
            BasePath = CredencialFirebase.DboAnahtar.BasePath
        };

        public String RetrieveTexto(string caminho)
        {
            client = new FirebaseClient(config);
            List<Obj_Banco_Dados> loginList = new List<Obj_Banco_Dados>();
            response_ = client.Get(caminho);

            Obj_Banco_Dados obj = new Obj_Banco_Dados();
            if (!response_.Body.Equals("null"))
            {
                var result = response_.Body;
                var values = JsonConvert.DeserializeObject<string>(result);

                return values.ToString();
              
            }
            else
            {
                return "";
            }
            
        }
    }
}
