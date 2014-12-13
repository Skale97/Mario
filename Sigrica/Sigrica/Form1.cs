using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sigrica
{
    public partial class frm : Form
    {
        int[,] bbpos;
        public static PictureBox[,] bbimg;
        Graphics g;
        Pen olovka;
        Brush cetka;
        Mario m1;
        Point pozicija;
        Image Marionormr, Marionorml, Mariojumpr, Mariojumpl;
        Image block, brick;
        public static Boolean r, l;
        public static int orientation = 0;

        public frm()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            olovka = new Pen(Color.Black, 4);
            cetka = new SolidBrush(Color.Gray);
            m1 = new Mario(400,200, 0.01); 
            pozicija = new Point();
            r = false;
            l = false;
            Marionormr = Image.FromFile(@"E:/C# Projekti/Sigrica/Marior.png");
            Marionorml = Image.FromFile(@"E:/C# Projekti/Sigrica/Mariol.png");
            Mariojumpr = Image.FromFile(@"E:/C# Projekti/Sigrica/Mariojr.png");
            Mariojumpl = Image.FromFile(@"E:/C# Projekti/Sigrica/Mariojl.png");
            block = Image.FromFile(@"E:/C# Projekti/Sigrica/block.png");
            brick = Image.FromFile(@"E:/C# Projekti/Sigrica/brick.png");
            bbpos = new int[16,12];
            bbimg = new PictureBox[16, 12];
            for (int i = 15; i >=0; i--)
            {
                for (int j = 0; j < 12; j++)
                {
                    bbimg[i, j] = new PictureBox();
                    this.Controls.Add(bbimg[i, j]);
                    bbimg[i, j].Location = new System.Drawing.Point(i * 50, j * 50);
                    bbimg[i, j].Image = null;
                    if(j==10 || i==0 || i==15) bbimg[i, j].Image = brick;
                }
            }
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            pozicija.X = m1.x;
            pozicija.Y = m1.y;
            picboxMario.Location = pozicija;
            switch (orientation)
            {
                case 0:
                    picboxMario.Image = Marionormr;
                break;
                case 1:
                    picboxMario.Image = Marionorml;
                break;
                case 2:
                    picboxMario.Image = Mariojumpr;    
                break;
                case 3:
                    picboxMario.Image = Mariojumpl;  
                break;
            }
            if (r) m1.right();
            if (l) m1.left();
            m1.count();
        }

        private void frm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                m1.jump();
                if (orientation == 0) orientation = 2;
                else if (orientation == 1) orientation = 3;
            }
            if (e.KeyCode == Keys.Right)
            {
                r = true;
                orientation = 0;
                if (m1.vy != 0) orientation = 2;
            }
            else if (e.KeyCode == Keys.Left)
            {
                l = true;
                orientation = 1;
                if (m1.vy != 0) orientation = 3;
            }
            else if (e.KeyCode == Keys.B)
            {
                bbimg[Cursor.Position.X / 50, Cursor.Position.Y / 50].Image = block;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void frm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right) r = false;
            else if (e.KeyCode == Keys.Left) l = false;
        }

    }

    public class Mario
    {
        static double g = -2000;
        public int x, y;
        int xt, yt;
        public double vx, vy, dt;
        Boolean ground = false, lefthit = false, righthit = false;

        public Mario(int x1, int y1, double dt1)
        {
            x = x1;
            y = y1;
            dt = dt1;
            vx = 0;
            vy = 0;
        }
        public void count()
        {
            xt = x + Convert.ToInt16(vx * dt);
            yt = y + Convert.ToInt16(vy * dt);
            if (!frm.r && !frm.l) vx = Math.Sign(vx) * (Math.Abs(vx) - 1000 * dt);
            if (!ground) vy -= g * dt;
            collision();
            if (vy == 0)
            {
                if (frm.orientation == 2) frm.orientation = 0;
                else if (frm.orientation == 3) frm.orientation = 1;
            }
            x = xt;
            y = yt;
        }
        public void jump()
        {
            if (vy == 0) vy -= 700;
        }
        public void right()
        {
            if (vx < 400 && !righthit)
            {
                vx += 1000 * dt;
                lefthit = false;
            }
        }
        public void left()
        {
            if (vx > -400 && !lefthit)
            {
                vx -= 1000 * dt;
                righthit = false;
            }
        }
        public void collision()
        {
            if (vy == 0)
            {
                if (frm.bbimg[xt / 50, yt / 50].Image != null || frm.bbimg[xt / 50, (yt + 99) / 50].Image != null)
                {
                    vx = 0;
                    xt = ((xt / 50)+1) * 50;
                }
                if (frm.bbimg[(xt+49) / 50, yt / 50].Image != null || frm.bbimg[(xt+49) / 50, (yt + 99) / 50].Image != null)
                {
                    vx = 0;
                    xt = xt / 50 * 50;
                }
            }
            if (vx == 0)
            {
                if (frm.bbimg[xt / 50, yt / 50].Image != null || frm.bbimg[(xt + 49) / 50, yt / 50].Image != null)
                {
                    vy = -vy;
                    yt = ((yt / 50) + 1) * 50;
                }
                if (frm.bbimg[xt / 50, (yt + 100) / 50].Image != null || frm.bbimg[(xt + 49) / 50, (yt + 100) / 50].Image != null)
                {
                    vy = 0;
                    yt = yt / 50 * 50;
                    ground = true;
                }
                else ground = false;
            }
            if (vx > 0 && vy < 0)
            {

            }
            if (vx < 0 && vy < 0)
            {

            }
            if (vx > 0 && vy > 0)
            {

            }
            if (vx < 0 && vy > 0)
            {

            }




            //if (((frm.bbimg[xt / 50, yt / 50].Image != null || frm.bbimg[xt / 50, (yt + 50) / 50].Image != null && y > yt / 50 * 50) && vy == 0)
            //    && (vy == 0 || ((Math.Abs(xt / 50 * 50 - xt) > Math.Abs(yt / 50 * 50 - yt) && vy>0) || (Math.Abs(xt / 50 * 50 - xt) > Math.Abs((yt / 50 +1)* 50 - yt) && vy<0))))
            //{
            //    vx = 0;
            //    xt = ((xt / 50) + 1) * 50;
            //    lefthit = true;
            //}
            //if (frm.bbimg[xt / 50, yt / 50].Image == null || frm.bbimg[xt / 50, (yt + 50) / 50].Image == null) lefthit = false;
            //if ((frm.bbimg[(xt + 50) / 50, yt / 50].Image != null || frm.bbimg[(xt + 50) / 50, (yt + 50) / 50].Image != null)
            //    && Math.Abs(((xt / 50)+1) * 50 - xt) > Math.Abs(yt / 50 * 50 - yt))
            //{
            //    vx = 0;
            //    xt = (xt / 50) * 50 - 1;
            //    righthit = true;
            //}
            //if (frm.bbimg[(xt + 50) / 50, yt / 50].Image == null || frm.bbimg[(xt + 50) / 50, (yt + 50) / 50].Image == null) righthit = false;
            //if ((frm.bbimg[(xt + 1) / 50, yt / 50].Image != null || frm.bbimg[(xt + 49) / 50, yt / 50].Image != null)
            //    && Math.Abs(xt / 50 * 50 - xt) < Math.Abs(yt / 50 * 50 - yt))
            //{
            //    vy = -vy;
            //    yt = (yt / 50 + 1) * 50 - 1;
            //}
            //if ((frm.bbimg[(xt + 1) / 50, (yt + 101) / 50].Image != null || frm.bbimg[(xt + 49) / 50, (yt + 101) / 50].Image != null)
            //    && y <= yt / 50 * 50)
            //{
            //    vy = 0;
            //    yt = yt / 50 * 50;
            //    ground = true;
            //}
            //else ground = false;
        }
    }
}
