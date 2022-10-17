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
    internal class LoginFirebaseController
    {
        private IFirebaseClient client = null;

        private FirebaseResponse response_ = null;
        private List<Monitor_Energia_Solar.Obj_Banco_Dados> loginList = null;
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = CredencialFirebase.DboAnahtar.AuthSecret,
            BasePath = CredencialFirebase.DboAnahtar.BasePath
        };


        public Obj_Banco_Dados RetrieveLoginEmail(string token)
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
                        return obj;
                    }

                }
            }
            return null;
        }
        public Obj_Banco_Dados RetrieveLogin(string usuario, string senha)
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
                    if(obj.Usuario.Equals(usuario) && obj.Senha.Equals(senha))
                    {
                        return obj;
                    }
                       
                }
            }
            return null;
        }
    }
}
