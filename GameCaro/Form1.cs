using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManager BanCo;
        SocketManager socket;
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

            socket = new SocketManager();

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

        void BanCo_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            pnlBanCo.Enabled = false;
            prcbCoolDown.Value = 0;

            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));
            Listen();
        }

        void BanCo_EndedGame(object sender, EventArgs e)
        {
            EndGame();
            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
        }

       
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if(prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                
                EndGame() ;
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            pnlBanCo.Enabled = true;
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát Game!", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
            else
            {
                try
                {
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch { }
            }
        }
        

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

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        

        private void btnLAN_Click(object sender, EventArgs e)
        {
            socket.IP = txbIP.Text;
            if (!socket.ConnectServer())
            {
                socket.isServer = true;
                pnlBanCo.Enabled = true;
                socket.CreateServer();
            }
            else
            {
                socket.isServer = false;
                pnlBanCo.Enabled=false;
                Listen();
            }
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txbIP.Text))
            {
                txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    SocketData data = (SocketData)socket.Receive();
                    ProcessData(data);
                }
                catch { }
            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }
        private void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                        MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                    }));
                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prcbCoolDown.Value = 0;
                        pnlBanCo.Enabled = true;
                        tmCoolDown.Start();
                        BanCo.OtherPlayerMark(data.Point);
                    }));
                    
                    break;
                case (int)SocketCommand.QUIT:
                    tmCoolDown.Stop();
                    MessageBox.Show("Đối thủ quá gà nên đã bỏ chạy");
                    break;
                case (int)SocketCommand.END_GAME:
                    MessageBox.Show("Đã 5 con trên 1 hàng");
                    break;
                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("Hết giờ");
                    break;
                default:
                    break;
            }

            Listen();
        }
        #endregion
    }

}
