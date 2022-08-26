using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using Monitor_Energia_Solar.Controller;
using Android.Content;
using Monitor_Energia_Solar;
using Android.Widget;
using System.Net.Mail;
using Android.App;
using System.Security.Cryptography.X509Certificates;
using MimeKit;

namespace SendEmailService
{
    class Email
    {
        //smtp.Credentials = new NetworkCredential("contato@projetopainelsolar.site", "Riotcc2021");
        //more code here
        public void SendEmail(String token)
        {
            Monitor_Energia_Solar.Controller.LoginController login = new LoginController();

            Obj_Banco_Dados objBancoDados = new Obj_Banco_Dados();
            objBancoDados = login.RecuperarDadosPeloEmail(token);

            if (objBancoDados.Usuario == null || objBancoDados.Usuario == "")
            {
                Toast.MakeText(Application.Context, "Atenção, usuário não encontrado", ToastLength.Short).Show();
            }
            else
            {
                try
                {
                  
                    string url = "http://projetopainelsolar.tech/enviarEmail.php?email="+ objBancoDados.Email + "&usuario=" + objBancoDados.Usuario + "&senha=" + objBancoDados.Senha;
                    using (WebClient client = new WebClient())
                    {
                        var data = new NameValueCollection();
                      
                        var Bytecode = client.UploadValues(url, data);
                        string htmlCode = Encoding.UTF8.GetString(Bytecode, 0, Bytecode.Length);
                        // Or you can get the file content without saving it:

                    }
                  
                }

                catch (Exception ex)
                {
                    Toast.MakeText(Application.Context, ex.ToString(), ToastLength.Long);
                }

            };
        }
        }
    }

