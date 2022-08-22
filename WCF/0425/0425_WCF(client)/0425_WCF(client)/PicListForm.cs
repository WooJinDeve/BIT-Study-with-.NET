using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _0425_WCF_client_.localhost;

namespace _0425_WCF_client_
{
    public partial class PicListForm : Form
    {
        private WbImage wbimage = new WbImage();
        public string SelectedPic
        {
            get
            {
                return listBox1.SelectedItem.ToString();
            }
        }

        public PicListForm()
        {
            InitializeComponent();
            string[] strPicList = wbimage.GetPictureList();
            listBox1.DataSource = strPicList;

            //foreach(string strPic in strPicList)
            //{
            //    listBox1.Items.Add(strPic);
            //}
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

    }
}
