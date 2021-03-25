using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        MainGame game;
        Graphics gr;
        Bitmap bmp;
        int tick = 0;

        public Form1()
        {
            InitializeComponent();

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gr = Graphics.FromImage(bmp);
            game = new MainGame();

            Game.HelpingClass.Render.SetScreen(
                Game.HelpingClass.PointOp.Div(new SizeF(pictureBox1.Size), MainGame.CellEdge*2));
            
            GameTickTimer.Start();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            tick += 1;
            game.Step();
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (tick % 100 == 0)
            {
                bmp.Dispose();
                bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }
            {
                gr = Graphics.FromImage(bmp);

                game.Redraw(gr);

                e.Graphics.DrawImage(bmp, 0, 0);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        { game.KeyDown(e); }

        public void SetTitle(string txt)
        {
            this.Text = txt;
        }
    }
}
