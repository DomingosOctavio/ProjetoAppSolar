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
    public class MainActivityFirebaseController
    {
        private IFirebaseClient client = null;

        private FirebaseResponse response_ = null;
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = CredencialFirebase.DboAnahtar.AuthSecret,
            BasePath = CredencialFirebase.DboAnahtar.BasePath
        };

        public String RetrieveIP(string token)
        {
            client = new FirebaseClient(config);
            List<Obj_Banco_Dados> loginList = new List<Obj_Banco_Dados>();
            response_ = client.Get("Login");

            Obj_Banco_Dados obj = new Obj_Banco_Dados();
            if (!response_.Body.Equals("null"))
            {
                var result = response_.Body;
                var values = JsonConvert.DeserializeObject<Dictionary<string, Obj_Banco_Dados>>(result);


                foreach (var valor in values)
                {

                    obj = valor.Value;
                    if (obj.Token.Equals(token))
                    {
                        return obj.IP_conexao.ToString();
                    }

                }
            }
            return null;
        }
    }
}