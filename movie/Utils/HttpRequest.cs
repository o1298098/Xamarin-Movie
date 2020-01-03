using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace movie.Utils
{
    public class HttpRequest
    {
        string _baseUrl;
        public HttpRequest(String baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<String> Request(string prama)
        {
            string result;
           
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_baseUrl + prama);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = _baseUrl + prama;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = "GET";
                WebResponse response = await request.GetResponseAsync();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }

        public String Request(string prama,string method,Dictionary<string, object> data)
        {
            string result;
            StringBuilder postData = new StringBuilder();
            if (data != null && data.Count > 0)
            {
                foreach (var p in data)
                {
                    if (postData.Length > 0)
                    {
                        postData.Append("&");
                    }
                    postData.Append(p.Key);
                    postData.Append("=");
                    postData.Append(p.Value);
                }
            }
            byte[] byteData = Encoding.UTF8.GetBytes(postData.ToString());
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_baseUrl+prama);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Referer = _baseUrl + prama;
                request.Accept = "*/*";
                request.Timeout = 30 * 1000;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
                request.Method = method;
                request.ContentLength = byteData.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(byteData, 0, byteData.Length);
                stream.Flush();
                stream.Close();
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream backStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(backStream, Encoding.UTF8);
                result = sr.ReadToEnd();
                sr.Close();
                backStream.Close();
                response.Close();
                request.Abort();
            }
            catch (Exception ex)
            {
                return null;
            }
            return result;
        }
    }
}
