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

namespace Monitor_Energia_Solar
{
    public class AcessoFirebase
    {
          private IFirebaseClient client = null;
        private SetResponse response = null;
        private FirebaseResponse response_ = null;
        private List<Object> objectList = null;

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
                return "Erro client Firebase retornou null";
            }

            return "Sucesso em criar client Firebase";
        }
        protected internal async Task<List<Object >> ListObject(String nomeTabela)
        {
            objectList = new List<Object>();
            response_ = await client.GetAsync(nomeTabela);
            var result = response_.Body;
            var data = JsonConvert.DeserializeObject<Dictionary<string, Object>>(result);

            foreach (var Object in data)
            {
                objectList.Add((Object)(Object.Value));
            }

            return objectList;
        }
      

        protected internal async void AddObject(Object model, string nomeTabela,string chave)
        {
            client = new FirebaseClient(config);
            if (client != null)
            {
                response = await client.SetAsync(nomeTabela + "/" + chave + "/", model);
                var result = response.ResultAs<Monitor_Energia_Solar.Student1>();
                if (result == null)
                {
                    throw new Exception("Erro ao ");
                }
            }
            else
            {
                throw new Exception("Bağlantı başarısız.");
            }
        }

        protected internal async void UpdateObject(Object model, string chave)
        {
            if (client != null)
            {
                var response = await client.UpdateAsync("Students/" + chave, model);
                var result = response.ResultAs<Object>();

                if (result == null)
                {
                    throw new Exception("Güncelleme sırasında hata meydana geldi.");
                }
            }
            else
            {
                throw new Exception("Bağlantı başarısız.");
            }
        }

        protected internal async void DeleteObject(string nomeTabela, string id)
        {
            if (client != null)
            {
                var response = await client.DeleteAsync(nomeTabela + id);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("Silme sırasında hata meydana geldi.");
                }
            }
        }
    }
}
