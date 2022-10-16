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
    public class DadosPessoaisFirebaseController
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

        public Obj_Banco_Dados RetrieveLogin(string token)
        {
            client = new FirebaseClient(config);
            loginList = new List<Obj_Banco_Dados>();
            response_ = client.Get("Login/" + token);

            Obj_Banco_Dados obj = new Obj_Banco_Dados();
            if (!response_.Body.Equals("null"))
            {
                var result = response_.Body;
                var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);
         
               
                foreach (var valor in values)
                {
                    if (valor.Key.Equals("Id"))
                    {
                        obj.Id = valor.Value.ToString();
                    }
                    if (valor.Key.Equals("Usuario"))
                    {
                        obj.Usuario = valor.Value.ToString();
                    }
                    if (valor.Key.Equals("Senha"))
                    {
                        obj.Senha = valor.Value.ToString();
                    }
                    if (valor.Key.Equals("Email"))
                    {
                        obj.Email = valor.Value.ToString();
                    }
                    if (valor.Key.Equals("Token"))
                    {
                        obj.Token = valor.Value.ToString();
                    }
                    if (valor.Key.Equals("IP_conexao"))
                    {
                        obj.IP_conexao = valor.Value.ToString();
                    }


                }


            }
            return obj;
        }
        public async void AddLogin(Obj_Banco_Dados model)
        {
            client = new FirebaseClient(config);
            if (client != null)
            {
                response_ = await client.SetAsync("Login/" + model.Token + "/", model);
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
        public async void UpdateDadosPessoais(Obj_Banco_Dados model)
        {
            client = new FirebaseClient(config);
            if (client != null)
            {
                var response = await client.UpdateAsync("Login/" + model.Token, model);
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
    }
}