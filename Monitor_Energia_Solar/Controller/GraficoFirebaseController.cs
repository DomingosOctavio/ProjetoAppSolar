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
using Monitor_Energia_Solar.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Monitor_Energia_Solar.Controller
{
    public class GraficoFirebaseController
    {
        private IFirebaseClient client = null;

        private FirebaseResponse response_ = null;
        private List<string> plotList = null;
        private List<Obj_Plot> aplotList = null;
        private IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = CredencialFirebase.DboAnahtar.AuthSecret,
            BasePath = CredencialFirebase.DboAnahtar.BasePath
        };


        public Obj_Plot ConsultarDadosPlot2(string token, int escolhaUser, string dia)
        {
            List<Obj_Plot> lista_objetos_grafico = new List<Obj_Plot>();
            
            string nomeTabela = "";
           
            if (escolhaUser == 1) //tensao
            {
                nomeTabela = "TAB_TENSAO";
            }
            else if (escolhaUser == 2) // corrente
            {
                nomeTabela = "TAB_CORRENTE";
            }
            else if (escolhaUser == 3) // luminosidade
            {
                nomeTabela = "TAB_LUMINOSIDADE";
            }

            client = new FirebaseClient(config);
          
            response_ = client.Get(nomeTabela + "/" + token + "/" + dia);

            Obj_Plot obj = new Obj_Plot();
            if (!response_.Body.Equals("null"))
            {
                var result = response_.Body;
                obj = JsonConvert.DeserializeObject<Obj_Plot>(result);
            
          
            }

            return obj;
        }
       
       public List<string> ConsultarDatas(string token)
        {
            client = new FirebaseClient(config);
            plotList = new List<string>();
            response_ = client.Get("TAB_TENSAO/" + token);

            if (!response_.Body.Equals("null"))
            {
                var result = response_.Body;
                var values = JsonConvert.DeserializeObject<Dictionary<string, object>>(result);

                foreach (var valor in values)
                {
                    plotList.Add(valor.Key.ToString());
                }
            }
            return plotList;
        }
    }
}