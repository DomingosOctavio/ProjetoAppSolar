using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;

namespace SendEmail
{
    class Email
    {
        //smtp.Credentials = new NetworkCredential("contato@projetopainelsolar.site", "Riotcc2021");
        //more code here
        public void SendEmail()
        {
            string url = "http://projetopainelsolar.site/email2.php?";
            var request = (HttpWebRequest)WebRequest.Create(url);

            var postData = "a=" + Uri.EscapeDataString("myUser");
            postData += "&b=" + Uri.EscapeDataString("myPassword");
            var data = Encoding.ASCII.GetBytes(url +postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
        }
    }
}
