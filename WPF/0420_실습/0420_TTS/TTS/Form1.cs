using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Net;
using WMPLib;

namespace TTS
{
    public partial class Form1 : Form
    {
        WMPLib.WindowsMediaPlayer wplayer = new WMPLib.WindowsMediaPlayer();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string strPATH = Application.StartupPath;
            txt_PATH_MP3.Text = strPATH + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".mp3";
        }


        private void BTN_SPK_Click(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            lfn_Speak(txt, "SPK");
        }


        int intSpeak = 0;
        private void lfn_Speak(string pTXT, string pDIV1)
        {
            try
            {
                if (intSpeak.Equals(1)) { txtLOG.AppendText("실행중\r\n"); return; }
                intSpeak = 1;
                txtLOG.Text = "";
                txtLOG.AppendText("실행시작\r\n");

                string url = "https://kakaoi-newtone-openapi.kakao.com/v1/synthesize";
                string saveFileFullPath = txt_PATH_MP3.Text; //"D:/tts.mp3";
                string API_KEY = "0fcc5b9efe9b2d60a373cc2db59b1506";
                string VoiceName = "WOMAN_READ_CALM";

                if (System.IO.File.Exists(saveFileFullPath)) //파일이 있을시 삭제처리
                {
                    txtLOG.AppendText("파일삭제처리 : " + saveFileFullPath + "\r\n");
                    System.IO.File.Delete(saveFileFullPath);
                }


                if (pTXT.Equals("")) { return; }

                string SELVoiceNM = comboBox1.Text;
                switch (SELVoiceNM)
                {
                    case "여자_밝은":
                        VoiceName = "WOMAN_DIALOG_BRIGHT";
                        break;
                    case "남자_차분":
                        VoiceName = "MAN_READ_CALM";
                        break;
                    case "남자_밝은":
                        VoiceName = "MAN_DIALOG_BRIGHT";
                        break;
                }


                wplayer.close();


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.ContentType = "application/xml";
                request.Headers.Add("Authorization", API_KEY);
                byte[] byteDataParams = Encoding.UTF8.GetBytes("<speak><voice name='" + VoiceName + "'>" + pTXT + "</voice></speak>");
                request.ContentLength = byteDataParams.Length;
                Stream st = request.GetRequestStream();
                st.Write(byteDataParams, 0, byteDataParams.Length);
                st.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string status = response.StatusCode.ToString();
                Console.WriteLine("Status :" + status);
                File.Delete(saveFileFullPath);
                using (Stream output = File.OpenWrite(saveFileFullPath))
                using (Stream input = response.GetResponseStream())
                {
                    input.CopyTo(output);
                }

                wplayer.URL = saveFileFullPath;

                if (intSpeak.Equals(1)) { txtLOG.AppendText("실행완료\r\n"); return; }
            }
            catch (Exception ex)
            {
                txtLOG.AppendText(ex.ToString());
                Console.Write(ex);
            }
            finally
            {
                intSpeak = 0;
            }
        }
    }


}