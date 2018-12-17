using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Blocks.Framework.RPCProxy
{
    public class HttpWebClient
    {
        public static TResponse GetResponse<TResponse>(string url,CookieContainer cookieContainer,WebHeaderCollection webHeaderCollection, object data)
        {
           
            HttpWebRequest webrequest = (HttpWebRequest)HttpWebRequest.Create(url);
            webrequest.Method = "post";
            webrequest.CookieContainer = cookieContainer;
            webrequest.Headers = webHeaderCollection;
            webrequest.ContentType = "application/json;charset=UTF-8";
            byte[] postByte = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
            webrequest.ContentLength = postByte.Length;
            Stream stream1 = webrequest.GetRequestStream();
            stream1.Write(postByte, 0, postByte.Length);
            stream1.Close();
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)webrequest.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;//返回远程服务器报告回来的错误
            }
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string getStr = sr.ReadToEnd();


            return JsonConvert.DeserializeObject<TResponse>(getStr);
        }
    }
}
