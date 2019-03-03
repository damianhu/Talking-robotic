using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Talking_Robtic
{
    class Voice_Recognition
    {
        string cuid = "666";
        string serverURL = "http://vop.baidu.com/server_api";
        string token = "24.8982fcf83542739c7835213a0eb573d8.2592000.1507219336.282335-10097695";
        public string Post(string audioPath)
        {
            serverURL += "?lan=zh&cuid="+cuid+"&token=" + token;
            FileStream fs = new FileStream(audioPath, FileMode.Open);
            byte[] voice = new byte[fs.Length];
            fs.Read(voice, 0, voice.Length);
            fs.Close();
            fs.Dispose();
            HttpWebRequest request = null;

            Uri uri = new Uri(serverURL);
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Timeout = 30000;
            request.Method = "POST";
            request.ContentType = "audio/wav; rate=16000";
            request.ContentLength = voice.Length;
            try
            {
                using (Stream writeStream = request.GetRequestStream())
                {
                    writeStream.Write(voice, 0, voice.Length);
                    writeStream.Close();
                    writeStream.Dispose();
                }
            }
            catch
            {
                return null;
            }
            string result = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string line = string.Empty;
                        StringBuilder sb = new StringBuilder();
                        while (!readStream.EndOfStream)
                        {
                            line = readStream.ReadLine();
                            sb.Append(line);
                            sb.Append("\r");
                        }
                        result = sb.ToString();
                        readStream.Close();
                        readStream.Dispose();
                     }
                    responseStream.Close();
                    responseStream.Dispose();
                }
                response.Close();
            }
            string re = string.Empty;
            JObject jsonObj = JObject.Parse(result);
            string err_msg = (string)jsonObj["err_msg"];
            string err_no = (string)jsonObj["err_no"];
            if (err_no == "0" && err_msg == "success.")
            {
                JArray jlist = JArray.Parse(jsonObj["result"].ToString());
                return jlist[0].ToString();
            }
            else
            {
                return null;
            }


        }

    }
}
