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
        // Quản lý bàn cờ và logic trò chơi
        ChessBoardManager BanCo;
        // Quản lý kết nối socket để giao tiếp mạng
        SocketManager socket;
        // Cờ để đánh dấu liệu đang trong quá trình kết nối hay không
        bool isConnecting = false;
        #endregion

        public Form1()
        {
            InitializeComponent();

            // Khởi tạo quản lý bàn cờ với các thành phần giao diện liên quan
            BanCo = new ChessBoardManager(pnlBanCo, txbPlayerName, pctbMark);
            // Đăng ký sự kiện khi trò chơi kết thúc
            BanCo.EndedGame += BanCo_EndedGame;
            // Đăng ký sự kiện khi người chơi đánh dấu một ô
            BanCo.PlayerMarked += BanCo_PlayerMarked;

            // Khởi tạo thanh tiến độ cooldown
            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;
            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            // Khởi tạo quản lý socket cho giao tiếp mạng
            socket = new SocketManager();

            // Bắt đầu một trò chơi mới
            NewGame();

            // Đăng ký sự kiện khi khách hàng kết nối
            socket.OnClientConnected += (message) =>
            {
                // Đảm bảo cập nhật giao diện người dùng trên luồng chính
                this.Invoke((MethodInvoker)(() =>
                {
                    txt_result.AppendText(message + Environment.NewLine);
                    Listen(); // Bắt đầu lắng nghe dữ liệu đến
                }));
            };
        }

        #region Methods

        /// <summary>
        /// Xử lý kết thúc trò chơi bằng cách dừng timer cooldown,
        /// vô hiệu hóa bảng cờ, và hiển thị thông báo.
        /// </summary>
        void EndGame()
        {
            tmCoolDown.Stop(); // Dừng timer cooldown
            //pnlBanCo.Enabled = false; // Vô hiệu hóa bảng cờ
            BanCo.IsMyTurn = false;
        }

        /// <summary>
        /// Thoát ứng dụng.
        /// </summary>
        void Quit()
        {
            Application.Exit();
        }

        /// <summary>
        /// Khởi tạo một trò chơi mới bằng cách đặt lại cooldown và vẽ lại bàn cờ.
        /// </summary>
        void NewGame()
        {
            BanCo.Count = 0;
            prcbCoolDown.Value = 0; // Đặt lại thanh tiến độ cooldown
            tmCoolDown.Stop(); // Dừng timer cooldown
            BanCo.VeBanCo(); // Vẽ bàn cờ
        }

        /// <summary>
        /// Xử lý sự kiện khi người chơi đánh dấu một ô trên bàn cờ.
        /// Bắt đầu timer cooldown, vô hiệu hóa bàn cờ, và gửi nước đi cho đối thủ.
        /// </summary>
        /// <param name="sender">Nguồn gốc của sự kiện.</param>
        /// <param name="e">Thông tin về sự kiện chứa điểm được nhấp.</param>
        void BanCo_PlayerMarked(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start(); // Bắt đầu timer cooldown
            //pnlBanCo.Enabled = false; // Vô hiệu hóa bảng cờ
            BanCo.IsMyTurn = false;
            prcbCoolDown.Value = 0; // Đặt lại thanh tiến độ cooldown

            // Tạo và gửi dữ liệu socket chứa nước đi
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));
            Listen(); // Bắt đầu lắng nghe phản hồi
        }

        /// <summary>
        /// Xử lý sự kiện khi trò chơi kết thúc.
        /// Kết thúc trò chơi và thông báo cho đối thủ.
        /// </summary>
        /// <param name="sender">Nguồn gốc của sự kiện.</param>
        /// <param name="e">Thông tin về sự kiện.</param>
        void BanCo_EndedGame(object sender, EventArgs e)
        {
            EndGame(); // Xử lý kết thúc trò chơi
            // Thông báo cho đối thủ rằng trò chơi đã kết thúc
            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
        }

        /// <summary>
        /// Xử lý sự kiện tick của timer cooldown.
        /// Cập nhật thanh tiến độ và kiểm tra thời gian hết hạn.
        /// </summary>
        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep(); // Tăng thanh tiến độ

            // Kiểm tra nếu cooldown đã đạt giá trị tối đa
            if (prcbCoolDown.Value >= prcbCoolDown.Maximum && BanCo.IsMyTurn)
            {
                EndGame(); // Kết thúc trò chơi do hết thời gian
                // Thông báo cho đối thủ về việc timeout
                MessageBox.Show("Bạn đã thua do hết thời gian!");
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
            }
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấp vào mục "New Game" trong menu.
        /// Bắt đầu một trò chơi mới và thông báo cho đối thủ.
        /// </summary>
        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame(); // Khởi tạo trò chơi mới
            // Thông báo cho đối thủ để bắt đầu trò chơi mới
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            //pnlBanCo.Enabled = true; // Kích hoạt lại bảng cờ
            BanCo.IsMyTurn = true;
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấp vào mục "Quit" trong menu.
        /// Thoát ứng dụng.
        /// </summary>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit(); // Thoát ứng dụng
        }

        /// <summary>
        /// Xử lý sự kiện khi đóng form.
        /// Xác nhận việc thoát và thông báo cho đối thủ nếu đã kết nối.
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            if (MessageBox.Show("Bạn có chắc muốn thoát Game!", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true; // Hủy bỏ sự kiện đóng form
            }
            else
            {
                try
                {
                    if (!socket.IsConnected) return; // Nếu chưa kết nối, không cần thông báo
                    // Thông báo cho đối thủ rằng người dùng đang thoát
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch { /* Xử lý ngoại lệ nếu cần */ }
            }
        }

        /// <summary>
        /// Xử lý sự kiện Paint cho panel2. Hiện tại chưa có gì.
        /// </summary>
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Chưa có vẽ tùy chỉnh
        }

        /// <summary>
        /// Xử lý sự kiện khi form được tải.
        /// Đăng ký sự kiện KeyDown cho textbox tin nhắn.
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            txt_message.KeyDown += new KeyEventHandler(txt_message_KeyDown);
        }

        /// <summary>
        /// Xử lý sự kiện KeyDown cho textbox tin nhắn.
        /// Gửi tin nhắn khi nhấn phím Enter.
        /// </summary>
        private void txt_message_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Ngăn không cho tiếng "ding" khi nhấn Enter
                e.SuppressKeyPress = true;

                // Gọi phương thức gửi tin nhắn
                SendMessage();
            }
        }

        /// <summary>
        /// Xử lý sự kiện Paint cho pnlBanCo. Hiện tại chưa có gì.
        /// </summary>
        private void pnlBanCo_Paint(object sender, PaintEventArgs e)
        {
            // Chưa có vẽ tùy chỉnh
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấp vào pctbM. Hiện tại chưa có gì.
        /// </summary>
        private void pctbM_Click(object sender, EventArgs e)
        {
            // Không có hành động được định nghĩa
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấp vào label1. Hiện tại chưa có gì.
        /// </summary>
        private void label1_Click(object sender, EventArgs e)
        {
            // Không có hành động được định nghĩa
        }

        /// <summary>
        /// Xử lý sự kiện Paint cho panel3. Hiện tại chưa có gì.
        /// </summary>
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            // Chưa có vẽ tùy chỉnh
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấp vào nút LAN.
        /// Xử lý kết nối như một client hoặc tạo server.
        /// </summary>
        private void btnLAN_Click(object sender, EventArgs e)
        {
            if (socket.IsConnected) // Kiểm tra xem đã kết nối chưa
            {
                MessageBox.Show("Đã kết nối rồi");
                return;
            }
            else if (isConnecting)
            {
                MessageBox.Show("Đang trong quá trình kết nối. Vui lòng đợi...");
                return;
            }
            isConnecting = true; // Đặt cờ đang kết nối
            socket.IP = txbIP.Text; // Lấy địa chỉ IP từ textbox

            if (!socket.ConnectServer()) // Cố gắng kết nối như một client
            {
                // Nếu kết nối thất bại, trở thành server
                socket.isServer = true;
                //pnlBanCo.Enabled = true; // Kích hoạt bảng cờ cho server
                BanCo.IsMyTurn = false;
                socket.CreateServer(); // Tạo socket server
                txt_result.AppendText("Server đang chờ kết nối..." + Environment.NewLine); // "Server is waiting for connection..."
            }
            else
            {
                // Kết nối thành công như một client
                socket.isServer = false;
                //pnlBanCo.Enabled = false; // Vô hiệu hóa bảng cờ cho đến khi đối thủ di chuyển
                BanCo.IsMyTurn = true;
                txt_result.AppendText("Đã kết nối với server." + Environment.NewLine); // "Connected to server."
                Listen(); // Bắt đầu lắng nghe dữ liệu đến
            }

        }

        /// <summary>
        /// Xử lý sự kiện khi form được hiển thị.
        /// Đặt textbox IP thành địa chỉ IPv4 nội bộ.
        /// </summary>
        private void Form1_Shown(object sender, EventArgs e)
        {
            // Cố gắng lấy địa chỉ IPv4 cho kết nối không dây
            txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(txbIP.Text))
            {
                // Nếu không tìm thấy, cố gắng lấy địa chỉ IPv4 cho Ethernet
                txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }

        /// <summary>
        /// Bắt đầu một luồng nền để lắng nghe dữ liệu socket đến.
        /// </summary>
        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    // Nhận dữ liệu từ socket
                    SocketData data = (SocketData)socket.Receive();
                    // Xử lý dữ liệu nhận được
                    ProcessData(data);
                }
                catch { /* Xử lý ngoại lệ nếu cần */ }
            });
            listenThread.IsBackground = true; // Đảm bảo luồng không ngăn ứng dụng thoát
            listenThread.Start(); // Bắt đầu luồng lắng nghe
        }

        /// <summary>
        /// Xử lý dữ liệu socket nhận được dựa trên lệnh.
        /// </summary>
        /// <param name="data">Dữ liệu socket nhận được.</param>
        private void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    // Hiển thị thông báo
                    MessageBox.Show(data.Message);
                    break;

                case (int)SocketCommand.NEW_GAME:
                    // Gọi phương thức NewGame trên luồng UI
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                    }));
                    break;

                case (int)SocketCommand.SEND_POINT:
                    // Xử lý nước đi của đối thủ
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prcbCoolDown.Value = 0; // Đặt lại cooldown
                        // Có thể làm mới bàn cờ hoặc cập nhật giao diện
                        // pnlBanCo.Refresh();
                        tmCoolDown.Start(); // Bắt đầu timer cooldown
                        BanCo.OtherPlayerMark(data.Point); // Đánh dấu nước đi của đối thủ trên bàn cờ
                        BanCo.IsMyTurn = true;
                    }));
                    break;

                case (int)SocketCommand.QUIT:
                    // Xử lý khi đối thủ thoát trò chơi
                    tmCoolDown.Stop(); // Dừng timer cooldown
                    MessageBox.Show("Đối thủ đã thoát trò chơi.");
                    break;

                case (int)SocketCommand.END_GAME:
                    break;

                case (int)SocketCommand.TIME_OUT:
                    EndGame();
                    MessageBox.Show("Bạn đã thắng! Đối thủ hết thời gian.");
                    break;

                case (int)SocketCommand.CHAT_MESSAGE:
                    // Hiển thị tin nhắn chat nhận được
                    this.Invoke((MethodInvoker)(() =>
                    {
                        string displayMessage = $"Khách: {data.ChatMessage}"; // "Guest: [tin nhắn]"
                        txt_result.AppendText(displayMessage + Environment.NewLine); // Thêm vào hiển thị chat
                    }));
                    break;

                default:
                    // Xử lý các lệnh không xác định nếu cần
                    break;
            }

            Listen(); // Tiếp tục lắng nghe dữ liệu tiếp theo
        }
        #endregion

        #region Event Handlers cho các Thành phần Giao diện Người Dùng

        /// <summary>
        /// Xử lý sự kiện khi tên người chơi thay đổi. Hiện tại chưa có gì.
        /// </summary>
        private void txbPlayerName_TextChanged(object sender, EventArgs e)
        {
            // Chưa có hành động được định nghĩa
        }

        /// <summary>
        /// Xử lý sự kiện khi textbox kết quả thay đổi. Hiện tại chưa có gì.
        /// </summary>
        private void txt_result_TextChanged(object sender, EventArgs e)
        {
            // Chưa có hành động được định nghĩa
        }

        /// <summary>
        /// Xử lý sự kiện khi textbox tin nhắn thay đổi. Hiện tại chưa có gì.
        /// </summary>
        private void txt_message_TextChanged(object sender, EventArgs e)
        {
            // Chưa có hành động được định nghĩa
        }

        /// <summary>
        /// Xử lý sự kiện khi nhấp vào nút "Send".
        /// Gửi tin nhắn chat.
        /// </summary>
        private void btn_Send_Click(object sender, EventArgs e)
        {
            SendMessage(); // Gọi phương thức gửi tin nhắn
        }

        /// <summary>
        /// Gửi một tin nhắn chat đến đối thủ.
        /// </summary>
        private void SendMessage()
        {
            if (string.IsNullOrWhiteSpace(txt_message.Text)) return; // Không gửi tin nhắn trống

            string message = txt_message.Text; // Lấy nội dung tin nhắn
            string senderName = txbPlayerName.Text; // Lấy tên người gửi

            // Tạo và gửi dữ liệu socket chứa tin nhắn chat
            socket.Send(new SocketData(
                (int)SocketCommand.CHAT_MESSAGE,
                "",
                new Point(),
                senderName,
                DateTime.Now,
                message));

            txt_result.AppendText("Bạn: " + message + "\n"); // "Bạn: [tin nhắn]"

            txt_message.Clear(); // Xóa nội dung ô nhập tin nhắn
        }

        #endregion
    }
}
