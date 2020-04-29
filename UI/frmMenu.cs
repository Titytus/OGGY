﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace OGGY
{
    public partial class frmMenu : Form
    {
        //Bạn chuyển sang vẽ màn hình theo graphics nha
        private BufferedGraphics gp;
        private BufferedGraphicsContext context;

        public static int iHighScore = 0;
        public frmMenu()
        {
            InitializeComponent();
            context = BufferedGraphicsManager.Current;
            gp = context.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            picBgMusic.Image = OGGY.Properties.Resources.opt_music;
            picFXMusic.Image = OGGY.Properties.Resources.opt_sound;
            pictureBox3.Image = OGGY.Properties.Resources.logo_text_EN;
            lblHighScore.Text = iHighScore.ToString();
            
        }

        private void DrawBackground(object sender, PaintEventArgs e)
        {
            Image img = Image.FromFile("assets/menu/background.png");
            gp.Graphics.DrawImage(img, 0, 0);
            gp.Render();
        }

        #region Events
        [DllImport("winmm.dll")]
        public static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        #endregion
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Cái này ông chuyển sang Soundplayer luôn cho dễ
            mciSendString("close MediaFile", null, 0, IntPtr.Zero);
            mciSendString("open \"" + @"assets\music\Background.mp3" + "\" type mpegvideo alias MediaFile", null, 0, IntPtr.Zero);
            mciSendString("play MediaFile REPEAT", null, 0, IntPtr.Zero);
        }

        private void picNew_Click(object sender, EventArgs e)
        {
            frmMain frmMain = new frmMain();
            this.Hide();
            frmMain.ShowDialog();
            this.Show();
            if (iHighScore < frmMain.scores)
                iHighScore = frmMain.scores;
            lblHighScore.Text = iHighScore.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult traLoi = MessageBox.Show("Bạn muốn thoát game chứ?", "Xác nhận", MessageBoxButtons.YesNo);
            if (traLoi == DialogResult.Yes)
                Application.Exit();
        }

        private void picFXMusic_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            DrawBackground(this, new PaintEventArgs(this.CreateGraphics(), this.DisplayRectangle));
        }
    }
}