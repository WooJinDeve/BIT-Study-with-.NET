using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace _0420_QRCODE
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        #region 기존코드
        private void button1_Click(object sender, EventArgs e)
        {
            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = ZXing.BarcodeFormat.QR_CODE;

            barcodeWriter.Options.Width = this.pictureBox1.Width;
            barcodeWriter.Options.Height = this.pictureBox1.Height;

            string strQRCode = "QRCODE TEST"; // 한글은 안되더라 ㅠ

            this.pictureBox1.Image = barcodeWriter.Write(strQRCode);
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            barcodeWriter.Write(strQRCode).Save(deskPath + @"\test.jpg", ImageFormat.Jpeg);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ZXing.BarcodeReader barcodeReader = new ZXing.BarcodeReader();
            string deskPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            var barcodeBitmap = (Bitmap)Image.FromFile(deskPath + @"\test.jpg");
            var result = barcodeReader.Decode(barcodeBitmap);

            this.textBox1.Text = result.Text;
        }
        #endregion

        public string FileName { get; set; }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Filter = "jpg files|*.jpg; *.png;";
                if (ofd.ShowDialog() != DialogResult.OK)
                    return;

                FileName = ofd.FileName;

                this.pictureBox1.Image = new Bitmap(FileName);
            }
            catch (Exception)
            {

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ZXing.BarcodeReader barcodeReader = new ZXing.BarcodeReader();

            var barcodeBitmap = (Bitmap)Image.FromFile(FileName);
            var result = barcodeReader.Decode(barcodeBitmap);

            this.textBox2.Text = result.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(textBox2.Text);
        }
    }
}
