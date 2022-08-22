using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using _0427_File_WPF_Client_.ServiceReference1;
using Microsoft.Win32;

namespace _0427_File_WPF_Client_
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window, IFileCallback
    {
        public string Name  { get; set; }
        public int Idx { get; set; }

        OpenFileDialog fileDlg;
        TextBox[] textbox;
        public IFile file;

        public MainWindow()
        {
            InitializeComponent();

            textbox = new TextBox[] { textbox01, textbox02, textbox03, textbox04, textbox05, textbox06,
            textbox07,textbox08,textbox09,textbox10,textbox11,textbox12,textbox13,textbox14,textbox15,
            textbox16,textbox17,textbox18,textbox19,textbox20,textbox21,textbox22};
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InstanceContext site = new InstanceContext(this);
            file = new FileClient(site);
        }

        #region Connect 
        private void Connect()
        {
            try
            {
                string temp = string.Format("이름 : {0}\r\n좌석 : {1}\r\n맞습니까?", namebox.Text, seatbox.Text);
                MessageBox.Show(temp, "확인", MessageBoxButton.OKCancel);

                Name = namebox.Text;
                Idx = int.Parse(seatbox.Text);

                StudentData[] data = file.Join(Name, Idx);
                if(data == null)
                {
                    MessageBox.Show("로그인 에러");
                    return;
                }

                foreach(StudentData dataItem in data)
                {
                    textbox[dataItem.SeatNum - 1].Text = dataItem.Name;
                    textbox[dataItem.SeatNum - 1].Foreground = Brushes.White;
                    textbox[dataItem.SeatNum - 1].Background = Brushes.DeepPink;
                }

                btnJoin.Content = "로그아웃";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("접속 오류 :{0}", ex.Message);
            }
        }

        private void DisConnect()
        {
            try
            {
                file.Leave(Name, Idx);

                btnJoin.Content = "로그인";

                for(int i = 0; i< 19; i++)
                {
                    textbox[i].Clear();
                    textbox[i].Foreground = Brushes.White;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("나가기 오류 :{0}", ex.Message);
            }
        }
        #endregion

        #region 클릭 메서드
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            seatbox.Text = (string)((Label)sender).Content;
        }

        private void namebox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Clear();
        }
        #endregion

        private void btnJoin_Click(object sender, RoutedEventArgs e)
        {
            if ((string)btnJoin.Content == "로그인")
                this.Connect();
            else
                this.DisConnect();
        }


        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            fileDlg = new OpenFileDialog();

            if(fileDlg.ShowDialog() == true)
                txtUpLoad1.Text = fileDlg.FileName;
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            FileStream dataFileStream = new FileStream(txtUpLoad1.Text,FileMode.Open,FileAccess.Read,FileShare.Read);

            BinaryReader datayReader = new BinaryReader(dataFileStream);

            byte[] byteFile = { 0 };
            byteFile = datayReader.ReadBytes(Convert.ToInt32(dataFileStream.Length));
            datayReader.Close();

            string filename = fileDlg.SafeFileName;
            file.UpLoadFile(Name, Idx, filename, byteFile);
            txtUpLoad1.Clear();

            MessageBox.Show(filename + "파일이 업로드 되었습니다.");
        }

        private void btnAllSend_Click(object sender, RoutedEventArgs e)
        {
            FileStream dataFileStream = new FileStream(txtUpLoad1.Text, FileMode.Open, FileAccess.Read, FileShare.Read);

            BinaryReader datayReader = new BinaryReader(dataFileStream);

            byte[] byteFile = { 0 };
            byteFile = datayReader.ReadBytes(Convert.ToInt32(dataFileStream.Length));
            datayReader.Close();

            string filename = fileDlg.SafeFileName;
            file.UpLoadFile(Name, Idx, filename, byteFile);
            txtUpLoad1.Clear();

            MessageBox.Show(filename + "파일이 업로드 되었습니다.");
        }

        #region  IFileCallbock 인터페이스

        public void FileRecive(string name, int idx, string msg, byte[] filedata)
        {
            try
            {
                string path = @"C:\Users\User\Desktop\정우진\비트고급\WCF\실습\0427_File(WPF_Client)\0427_File(WPF_Client)\bin\Debug\File\" + msg;
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Write);
                fs.Write(filedata, 0, filedata.Length);
                fs.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void UserEnter(string name, int idx)
        {
            textbox[idx -1].Text = name;
            textbox[idx - 1].Foreground = Brushes.White;
            textbox[idx - 1].Background = Brushes.DeepPink;
        }

        public void UserLeave(string name, int idx)
        {
            textbox[(idx - 1)].Text = "";
            textbox[idx - 1].Foreground = Brushes.White;
            textbox[idx - 1].Background = Brushes.White;
        }
        #region  X
        public IAsyncResult BeginFileRecive(string name, int idx, string msg, byte[] filedata, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndFileRecive(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginUserEnter(string name, int idx, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndUserEnter(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginUserLeave(string name, int idx, AsyncCallback callback, object asyncState)
        {
            throw new NotImplementedException();
        }

        public void EndUserLeave(IAsyncResult result)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion
    }
}
