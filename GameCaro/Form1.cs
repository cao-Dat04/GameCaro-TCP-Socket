using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManager BanCo;
        #endregion
        public Form1()
        {
            InitializeComponent();
            BanCo = new ChessBoardManager(pnlBanCo, txbPlayerName,pctbMark);
            BanCo.EndedGame += BanCo_EndedGame;
            BanCo.PlayerMarked += BanCo_PlayerMarked;
            
            prcbCoolDown.Step  = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            NewGame();
        }
        #region Methods
        void EndGame()
        {
            tmCoolDown.Stop();
            pnlBanCo.Enabled = false;
            MessageBox.Show("End Game");
            
        }
        void Quit()
        {
            Application.Exit();
        }

        void NewGame()
        {
            prcbCoolDown.Value = 0;
            tmCoolDown.Stop();
            BanCo.VeBanCo();
            

        }

        void BanCo_PlayerMarked(object sender, EventArgs e)
        {
            tmCoolDown.Start();
            prcbCoolDown.Value = 0;
        }

        void BanCo_EndedGame(object sender, EventArgs e)
        {
            EndGame();
        }

       
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if(prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                
                EndGame() ;
                
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát Game!", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)

                    e.Cancel = true;
        }
        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pnlBanCo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pctbM_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
  
}
