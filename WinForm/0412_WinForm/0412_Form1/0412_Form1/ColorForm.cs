using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _0412_Form1
{
    public partial class ColorForm : Form
    {
        public string StrText { get; set; }


        public ColorForm(string text)
        {
            InitializeComponent();

            this.Text = text;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaint(PaintEventArgs pea)
        {
            Graphics grfx = pea.Graphics;
            SolidBrush br = new SolidBrush(Color.Black);
            grfx.DrawString(StrText, this.Font, br, 10, 7);
        }

      
    }
}
