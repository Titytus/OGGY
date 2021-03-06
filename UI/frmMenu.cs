﻿using System;
using System.Configuration;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Windows.Input;

namespace OGGY
{
    public partial class frmMenu : Form
    {
        #region Properties
        private SoundPlayer playerMusic = new SoundPlayer();

        //Đọc các giá trị được lưu trong App.config 
        public static int iHighScore = Convert.ToInt32(ConfigurationManager.AppSettings["iHighScore"]);
        public static bool isPlayBgMusic = Convert.ToBoolean(ConfigurationManager.AppSettings["isPlayBgMusic"]);
        public static bool isPlayFXMucsic = Convert.ToBoolean(ConfigurationManager.AppSettings["isPlayFXMucsic"]);
        #endregion

        public frmMenu()
        {
            InitializeComponent();
            lblHighScore.Text = iHighScore.ToString();
            PlayBackgroundMusic();
            PlayFXMucsic();
        }

        private void PicFXMusic_Click(object sender, EventArgs e)
        {
            isPlayFXMucsic = !isPlayFXMucsic;
            PlayFXMucsic();
        }

        private void PlayFXMucsic()
        {
            if (isPlayFXMucsic)
                picFXMusic.BackgroundImage = OGGY.Properties.Resources.opt_music;
            else
                picFXMusic.BackgroundImage = OGGY.Properties.Resources.opt_nomusic;
        }

        /// <summary>
        /// Khi nguoi choi muon thoat game
        /// </summary>
        private void Exit()
        {
            DialogResult traLoi = MessageBox.Show("Bạn muốn thoát game chứ?", "Xác nhận", MessageBoxButtons.YesNo);
            if (traLoi == DialogResult.Yes)
            {
                //Cập nhật thông tin trong App config
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["iHighScore"].Value = iHighScore.ToString();
                config.AppSettings.Settings["isPlayBgMusic"].Value = isPlayBgMusic.ToString();
                config.AppSettings.Settings["isPlayFXMucsic"].Value = isPlayFXMucsic.ToString();
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");

                Application.Exit();
            }
        }

        private void PicExit_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void PicInfo_Click(object sender, EventArgs e)
        {
            string info = "\t\t GAME OGGY \n\n\n (c) Nguyễn Huỳnh Minh Tiến & Hồ Quốc Khánh \n\n\t\t FIT-HCMUTE";
            MessageBox.Show(info, "Thông tin về game", MessageBoxButtons.OK);
        }

        private void PicBgMusic_Click(object sender, EventArgs e)
        {
            isPlayBgMusic = !isPlayBgMusic;
            PlayBackgroundMusic();
        }

        private void PlayBackgroundMusic()
        {
            if (isPlayBgMusic)
            {
                picBgMusic.BackgroundImage = OGGY.Properties.Resources.opt_sound;
                playerMusic = new SoundPlayer(Properties.Resources.Background1);
                playerMusic.PlayLooping();
            }
            else
            {
                picBgMusic.BackgroundImage = OGGY.Properties.Resources.opt_nosound;
                playerMusic.Stop();
            }
        }

        private void FrmMenu_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bitmap = Properties.Resources.img_background;
            e.Graphics.DrawImageUnscaledAndClipped(bitmap, this.DisplayRectangle);
            bitmap = Properties.Resources.logo_text_EN;
            e.Graphics.DrawImageUnscaledAndClipped(bitmap, new Rectangle(280, 80, 850, 350));
            bitmap = Properties.Resources.menu_fridge;
            e.Graphics.DrawImageUnscaledAndClipped(bitmap, new Rectangle(40, 200, 180, 450));
            bitmap = Properties.Resources.bob_sleep_00;
            e.Graphics.DrawImage(bitmap, new Rectangle(950, 540, 220, 120));
            bitmap = Properties.Resources.new_badge_en;
            e.Graphics.DrawImageUnscaledAndClipped(bitmap, new Rectangle(550, 420, 150, 110));
        }

        private void EventClick(object sender, MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(550, 420, 150, 110);
            if (rect.Left <= e.X && e.X <= rect.Right)
                if (rect.Top <= e.Y && e.Y <= rect.Bottom)
                {
                    frmMain frmMain = new frmMain();
                    playerMusic.Stop();
                    this.Hide();
                    frmMain.ShowDialog();
                    this.Show();
                    PlayBackgroundMusic();
                    PlayFXMucsic();
                    if (iHighScore < frmMain.scores)
                        iHighScore = frmMain.scores;
                    lblHighScore.Text = iHighScore.ToString();
                }
        }

        private void frmMenu_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Exit();
        }
    }
}
