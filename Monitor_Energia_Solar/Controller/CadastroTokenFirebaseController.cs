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



        protected internal string ConnectionTest(string x = null)
        {
            client = new FirebaseClient(config);

            if (client == null)
            {
                return "Erro ao inserir dados no Firebase.";
            }

            return "Erro ao inserir dados no Firebase.";
        }
        public Boolean EmptyListVerification(string token)
        {
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
        public List<Obj_Banco_Dados> ListLogin(string token)
        {
            client = new FirebaseClient(config);
            loginList = new List<Obj_Banco_Dados>();
            response_ = client.Get("Login/" + token);


            if(!response_.Body.Equals("null"))
            {
                var result = response_.Body;
                var data = JsonConvert.DeserializeObject<Dictionary<string, Obj_Banco_Dados>>(result);
               
                foreach (var log in data)
                {
                    loginList.Add((Obj_Banco_Dados)(log.Value));
                }

            }
            return loginList;
        }

        public  async void AddLogin(Obj_Banco_Dados model)
        {
            client = new FirebaseClient(config);
            if (client != null)
            {
                response_ = await client.SetAsync("Login/" + model.Token  + "/", model);
                var result = response_.ResultAs<Monitor_Energia_Solar.Obj_Banco_Dados>();
                if (result == null)
                {
                    throw new Exception("Erro ao inserir dados no Firebase.");
                }
            }
            else
            {
                throw new Exception("Erro ao inserir dados no Firebase");
            }
        }

        protected  async void UpdateStudent(Monitor_Energia_Solar.Obj_Banco_Dados model)
        {
            if (client != null)
            {
                var response = await client.UpdateAsync("login/" + model.Id, model);
                var result = response.ResultAs<Monitor_Energia_Solar.Obj_Banco_Dados>();

                if (result == null)
                {
                    throw new Exception("Erro ao inserir dados no Firebase.");
                }
            }
            else
            {
                throw new Exception("Erro ao inserir dados no Firebase.");
            }
        }

        protected internal async void DeleteStudent(string id)
        {
            if (client != null)
            {
                var response = await client.DeleteAsync("login/" + id);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Erro ao inserir dados no Firebase.");
                }
            }
        }

    }
}