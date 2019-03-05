using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication2
{
    public partial class Lines : UserControl
    {
        List<Control[]> st = new List<Control[]>();
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SendToBack();
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (var item in st)
            {
                var x1 = item[0].Left + item[0].Width / 2;
                var y1 = item[0].Top + item[0].Height / 2;
                var x2 = item[1].Left + item[1].Width / 2;
                var y2 = item[1].Top + item[1].Height / 2;
                g.DrawLine(Pens.Black, x1, y1, x2, y2);
            }
        }
        public void Add(Control one, Control two)
        {
            st.Add(new Control[] { one, two });
            one.Parent = this;
            two.Parent = this;
        }
    }
}
