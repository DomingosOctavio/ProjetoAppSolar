using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;

namespace Monitor_Energia_Solar.Controller
{
    public class CadastroTokenFirebaseController
    {
        private IFirebaseClient client = null;
      
        private FirebaseResponse response_ = null;
        private List<Monitor_Energia_Solar.Obj_Banco_Dados> loginList = null;
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = CredencialFirebase.DboAnahtar.AuthSecret,
            BasePath = CredencialFirebase.DboAnahtar.BasePath
        };



       
        public Boolean EmptyListVerification(string token)
        {
            if (token.Equals(""))
            {

                return false;
            }

            Boolean existe = false;
            client = new FirebaseClient(config);
            if (client != null)
            {
                response_ = client.Get("Login/" + token);
                if (response_.Body.Equals("null"))
                {
                    existe = false;
                }
                else
                {
                    existe = true;
                }
            }
               return existe;
        }
    }
}