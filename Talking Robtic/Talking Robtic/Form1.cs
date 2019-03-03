using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.IO;
using System.Web;

namespace Talking_Robtic
{
    public partial class Robtic : Form
    {
        HttpWebResponse Response = null;
        public Robtic()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        private const int NULL = 0, ERROR_SUCCESS = NULL;
        MLApp.MLApp matlab = null;
        private void Record_BT_Click(object sender, EventArgs e)

        {
            MLApp.MLApp matlab = null;
            Type matlabAppType = System.Type.GetTypeFromProgID("Matlab.Application");
            matlab = System.Activator.CreateInstance(matlabAppType) as MLApp.MLApp;
            string command;
            if (Record_BT.Text == "录音")
            {
                command = "R = audiorecorder( 16000, 8 ,1) ; record(R);";
                matlab.Visible = 0;
                matlab.Execute(command);
                Record_BT.Text = "停止";
            }
            else if (Record_BT.Text == "停止")
            {
                string outpath = Application.StartupPath  + "/speak.wav";
                command = "stop(R); myspeech = getaudiodata(R);audiowrite('"+outpath+"', myspeech, 16000);";
                matlab.Visible = 0;
                matlab.Execute(command);
                Record_BT.Text = "录音";
            }
        }
        private string Robot(string sendmessage)
        {
            string APIKEY = "9af07e6ea6d04e97875d8ab8ca2c3287";
            string message = sendmessage;
            string INFO = Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(sendmessage));
            string geturl = "http://www.tuling123.com/openapi/api?key=" + APIKEY + "&info=" + INFO+ "&userid=123";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(geturl);
            HttpWebResponse Myresponse = (HttpWebResponse)request.GetResponse();
            Response = Myresponse;
            using (Stream MyStream = Myresponse.GetResponseStream())
            {
                long ProgMaximum = Myresponse.ContentLength;
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = MyStream.Read(by, 0, by.Length);
                Encoding encoding = Encoding.UTF8;
                string result = "";
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    result += encoding.GetString(by, 0, osize);
                    long ProgValue = totalDownloadedByte;
                    osize = MyStream.Read(by, 0, by.Length);
                }
                JsonReader reader = new JsonTextReader(new StringReader(result));
                while (reader.Read())
                {
                    if (reader.Path == "text")
                    {
                        result = reader.Value.ToString();
                    }
                    Console.WriteLine(reader.TokenType + "/t/t" + reader.ValueType + "/t/t" + reader.Value);
                }
                return result;
            }
        }

        private void Talk_Box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string sendmessage = Talk_Box.Text;
                string getmessage = Robot(sendmessage);
                if (Robot_Box.Text == "")
                    Robot_Box.AppendText(getmessage);
                else
                    Robot_Box.AppendText("\r\n----------------------------\r\n" + getmessage);
                if (Record_Box.Text == "")
                    Record_Box.AppendText(Talk_Box.Text);
                else
                    Record_Box.AppendText("\r\n----------------------------\r\n" + Talk_Box.Text);
                Talk_Box.Text = "";
                Voice_Synthesis(getmessage);
            }
        }
        private void Voice_Synthesis(string rwords)
        {
            string tok = "24.8982fcf83542739c7835213a0eb573d8.2592000.1507219336.282335-10097695";
            string rest = "tex={0}&lan=zh&cuid=123&ctp=1&tok={1}";
            string update_data = string.Format(rest,rwords,tok);
            HttpWebRequest require = WebRequest.Create("http://tsn.baidu.com/text2audio?") as HttpWebRequest;
            require.Method = "post";
            require.ContentType = "application/x-www-form-urlencoded";
            require.ContentLength = Encoding.UTF8.GetByteCount(update_data);
            using (StreamWriter sw = new StreamWriter(require.GetRequestStream()))
                sw.Write(update_data);
            HttpWebResponse response = require.GetResponse() as HttpWebResponse;
            using (Stream stream = response.GetResponseStream())
            {
                string full_filename = Application.StartupPath + "/robotspeak.mp3";
                using (FileStream fs = new FileStream(full_filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    stream.CopyTo(fs);
                if (mciSendString(string.Format("open\"{0}\" alias robotspeak", full_filename), null, NULL, NULL) == ERROR_SUCCESS)
                {
                    mciSendString("play robotspeak wait", null, NULL, NULL);
                    mciSendString("close robotspeak", "", 0, 0);
                }
                else
                {
                    string Voice_Route = "open \"" + full_filename + "\" alias robotspeak";
                    mciSendString(@"close robotspeak", null, 0, 0);
                    mciSendString(Voice_Route, null, 0, 0);
                    mciSendString("play  robotspeak wait", null, 0, 0);
                    mciSendString("close robotspeak", "", 0, 0);
                }
            }
        }

        private void Voice_BT_Click(object sender, EventArgs e)
        {
            Record_Box.Text = Speech_Recognition(Application.StartupPath + "/speak.wav");
            string sendmessage = Record_Box.Text;
            string getmessage = Robot(sendmessage);
            if (Robot_Box.Text == "")
                Robot_Box.AppendText(getmessage);
            else
                Robot_Box.AppendText("\r\n----------------------------\r\n" + getmessage);
            if (Record_Box.Text == "")
                Record_Box.AppendText(Talk_Box.Text);
            else
                Record_Box.AppendText("\r\n----------------------------\r\n" + Talk_Box.Text);
            Talk_Box.Text = "";
            Voice_Synthesis(getmessage);
        }

        private string Speech_Recognition(string audio_path)
        {
            Voice_Recognition ats = new Voice_Recognition();
            string result = ats.Post(audio_path);
            return result;
        }


      }

    
}