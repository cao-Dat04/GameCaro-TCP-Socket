using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameCaro
{
    public class ChessBoardManager
    {
        #region Properties
        // Panel chứa bàn cờ
        private Panel banCo;

        public Panel BanCo
        {
            get { return banCo; }
            set { banCo = value; }
        }

        // Danh sách người chơi
        private List<Player> Player;

        // Chỉ số của người chơi hiện tại (0 hoặc 1)
        private int currentPlayer;
        public int CurrentPlayer
        {
            get { return currentPlayer; }
            set { currentPlayer = value; }
        }

        // TextBox hiển thị tên người chơi
        private TextBox playerName;
        public TextBox PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }

        // PictureBox hiển thị biểu tượng của người chơi
        private PictureBox playerMark;
        public PictureBox PlayerMark
        {
            get { return playerMark; }
            set { playerMark = value; }
        }

        // Ma trận chứa các Button đại diện cho các ô trên bàn cờ
        private List<List<Button>> matrix;
        public List<List<Button>> Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }

        // Sự kiện khi người chơi đánh dấu một ô
        private event EventHandler<ButtonClickEvent> playerMarked;
        public event EventHandler<ButtonClickEvent> PlayerMarked
        {
            add
            {
                playerMarked += value;
            }
            remove
            {
                playerMarked -= value;
            }
        }

        // Sự kiện khi trò chơi kết thúc
        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }

        private bool isMyTurn;
        public bool IsMyTurn
        {
            get { return isMyTurn; }
            set { isMyTurn = value; }
        }

        private int count = 0;
        public int Count 
        { 
            get { return count; } 
            set {  count = value; }
        }

        #endregion

        #region Initialize
        // Constructor khởi tạo ChessBoardManager với các thành phần giao diện cần thiết
        public ChessBoardManager(Panel banCo, TextBox playerName, PictureBox mark)
        {
            this.BanCo = banCo; // Gán panel bàn cờ
            this.PlayerName = playerName; // Gán TextBox tên người chơi
            this.PlayerMark = mark; // Gán PictureBox biểu tượng người chơi
            this.Player = new List<Player>() // Khởi tạo danh sách người chơi
            {
                new Player("BoBo", Image.FromFile(Application.StartupPath + "\\Resources\\x.png")), // Người chơi đầu tiên
                new Player("HeHe", Image.FromFile(Application.StartupPath + "\\Resources\\o.png"))  // Người chơi thứ hai
            };
        }
        #endregion

        #region Methods
        // Vẽ bàn cờ Caro
        public void VeBanCo()
        {
            // Kích hoạt panel bàn cờ
            BanCo.Enabled = true;

            // Xóa tất cả các control hiện tại trên panel
            BanCo.Controls.Clear();

            // Đặt người chơi hiện tại là người đầu tiên
            CurrentPlayer = 0;

            // Cập nhật giao diện người chơi
            ChangePlayer();

            // Khởi tạo ma trận bàn cờ
            Matrix = new List<List<Button>>();

            // Sử dụng Cons.CHESS_BOARD_HEIGHT và Cons.CHESS_BOARD_WIDTH để tạo đúng số hàng và cột
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)  // Số hàng
            {
                // Thêm một hàng mới vào ma trận
                Matrix.Add(new List<Button>());

                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)  // Số cột
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH, // Đặt chiều rộng của button
                        Height = Cons.CHESS_HEIGHT, // Đặt chiều cao của button
                        Location = new Point(j * Cons.CHESS_WIDTH, i * Cons.CHESS_HEIGHT), // Xác định vị trí của button
                        BackgroundImageLayout = ImageLayout.Stretch, // Đặt cách hiển thị hình nền
                        Tag = i.ToString() // Gán tag để lưu hàng của button
                    };

                    // Đăng ký sự kiện Click cho button
                    btn.Click += btn_Click;

                    // Thêm button vào panel bàn cờ
                    BanCo.Controls.Add(btn);

                    // Thêm button vào ma trận
                    Matrix[i].Add(btn);
                }
            }
        }

        // Xử lý sự kiện khi một button trên bàn cờ được nhấp
        void btn_Click(object sender, EventArgs e)
        {
            if (!IsMyTurn) // Kiểm tra xem có phải lượt của người chơi không
            {
                return;
            }
            Button btn = sender as Button; // Lấy button được nhấp
            if (btn.BackgroundImage != null) // Nếu button đã có hình nền
                return; // Không làm gì

            Mark(btn); // Đánh dấu ô với hình ảnh của người chơi hiện tại
            // Sau khi đánh dấu, không còn lượt của mình

            IsMyTurn = false;
            ChangePlayer(); // Chuyển sang người chơi tiếp theo
            if (playerMarked != null) // Nếu có người đăng ký sự kiện
                playerMarked(this, new ButtonClickEvent(GetChessPoint(btn))); // Kích hoạt sự kiện đánh dấu

            if (isEndGame(btn)) // Kiểm tra xem trò chơi đã kết thúc chưa
            {
                EndGame(); // Kết thúc trò chơi
                MessageBox.Show("Bạn đã thắng!");
                return;
            }

            if (count == Cons.CHESS_BOARD_HEIGHT * Cons.CHESS_BOARD_WIDTH - 1)
            {
                count = 0;
                EndGame();
                MessageBox.Show("Bạn cờ để hết. Hòa!");
            }
            else
            {
                count++;
            }
        }

        // Đánh dấu nước đi của người chơi đối phương
        public void OtherPlayerMark(Point point)
        {
            Button btn = Matrix[point.Y][point.X]; // Lấy button tại vị trí điểm
            if (btn.BackgroundImage != null) // Nếu button đã có hình nền
                return; // Không làm gì

            //banCo.Enabled = true; // (Đã được bật ở VeBanCo)
            Mark(btn); // Đánh dấu ô với hình ảnh của người chơi đối phương

            ChangePlayer(); // Chuyển sang người chơi tiếp theo
            //if (playerMarked != null)
            //    playerMarked(this, new ButtonClickEvent(GetChessPoint(btn)));

            if (isEndGame(btn)) // Kiểm tra xem trò chơi đã kết thúc chưa
            {
                EndGame(); // Kết thúc trò chơi
                MessageBox.Show("Bạn đã thua, đừng nản lòng, làm ván mới nhé!");
                return;
            }

            if (count == Cons.CHESS_BOARD_HEIGHT * Cons.CHESS_BOARD_WIDTH - 1)
            {
                count = 0;
                EndGame();
                MessageBox.Show("Bạn cờ để hết. Hòa!");
            }
            else
            {
                count++;
            }
        }

        // Kích hoạt sự kiện kết thúc trò chơi
        public void EndGame()
        {
            if (endedGame != null) // Nếu có người đăng ký sự kiện
                endedGame(this, new EventArgs()); // Kích hoạt sự kiện kết thúc trò chơi
        }

        // Kiểm tra xem trò chơi đã kết thúc hay chưa dựa trên vị trí ô được đánh dấu
        private bool isEndGame(Button btn)
        {
            return isEndHorizontal(btn) || isEndVertical(btn) || isPrimary(btn) || isEndSub(btn); // Kiểm tra từng hướng
        }

        // Lấy vị trí của ô trên bàn cờ dựa vào button
        private Point GetChessPoint(Button btn)
        {
            int vertical = Convert.ToInt32(btn.Tag); // Lấy hàng từ Tag của button
            int horizontal = Matrix[vertical].IndexOf(btn); // Lấy cột dựa vào vị trí trong danh sách
            Point point = new Point(horizontal, vertical); // Tạo điểm
            return point; // Trả về điểm
        }

        // Kiểm tra xem có đủ 5 ô liên tiếp theo chiều ngang không
        private bool isEndHorizontal(Button btn)
        {
            Point point = GetChessPoint(btn); // Lấy điểm của button
            int countLeft = 0; // Đếm số ô liên tiếp sang trái
            for (int i = point.X; i >= 0; i--) // Lặp từ cột hiện tại sang trái
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage) // Nếu ô cùng biểu tượng
                {
                    countLeft++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }
            int countRight = 0; // Đếm số ô liên tiếp sang phải
            for (int i = point.X + 1; i < Cons.CHESS_BOARD_WIDTH; i++) // Lặp từ cột hiện tại sang phải
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage) // Nếu ô cùng biểu tượng
                {
                    countRight++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }
            return countLeft + countRight == 5; // Trả về true nếu tổng bằng 5
        }

        // Kiểm tra xem có đủ 5 ô liên tiếp theo chiều dọc không
        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn); // Lấy điểm của button
            int countTop = 0; // Đếm số ô liên tiếp phía trên
            for (int i = point.Y; i >= 0; i--) // Lặp từ hàng hiện tại lên trên
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage) // Nếu ô cùng biểu tượng
                {
                    countTop++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }
            int countBottom = 0; // Đếm số ô liên tiếp phía dưới
            for (int i = point.Y + 1; i < Cons.CHESS_BOARD_HEIGHT; i++) // Lặp từ hàng hiện tại xuống dưới
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage) // Nếu ô cùng biểu tượng
                {
                    countBottom++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }
            return countTop + countBottom == 5; // Trả về true nếu tổng bằng 5
        }

        // Kiểm tra xem có đủ 5 ô liên tiếp theo đường chéo chính không
        private bool isPrimary(Button btn)
        {
            Point point = GetChessPoint(btn); // Lấy điểm của button
            int countTop = 0; // Đếm số ô liên tiếp trên đường chéo chính
            for (int i = 0; i <= point.X; i++) // Lặp từ vị trí hiện tại đi lên trên và sang phải
            {
                if (point.X + i >= Cons.CHESS_BOARD_WIDTH || point.Y - i < 0) // Kiểm tra giới hạn
                    break;

                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage) // Nếu ô cùng biểu tượng
                {
                    countTop++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }
            int countBottom = 0; // Đếm số ô liên tiếp dưới đường chéo chính
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++) // Lặp từ vị trí hiện tại đi xuống dưới và sang trái
            {
                // Kiểm tra giới hạn hàng và cột
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i < 0)
                    break;

                // Nếu ô cùng biểu tượng
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }

            return countTop + countBottom == 5; // Trả về true nếu tổng bằng 5
        }

        // Kiểm tra xem có đủ 5 ô liên tiếp theo đường chéo phụ không
        private bool isEndSub(Button btn)
        {
            Point point = GetChessPoint(btn); // Lấy điểm của button
            int countTop = 0; // Đếm số ô liên tiếp trên đường chéo phụ
            for (int i = 0; i <= point.X; i++) // Lặp từ vị trí hiện tại đi lên trên và sang trái
            {
                if (point.X - i < 0 || point.Y - i < 0) // Kiểm tra giới hạn
                    break;

                if (Matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage) // Nếu ô cùng biểu tượng
                {
                    countTop++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }
            int countBottom = 0; // Đếm số ô liên tiếp dưới đường chéo phụ
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++) // Lặp từ vị trí hiện tại đi xuống dưới và sang phải
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH) // Kiểm tra giới hạn
                    break;
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage) // Nếu ô cùng biểu tượng
                {
                    countBottom++; // Tăng đếm
                }
                else
                    break; // Dừng nếu không giống
            }
            return countTop + countBottom == 5; // Trả về true nếu tổng bằng 5
        }

        // Đánh dấu một ô trên bàn cờ với hình ảnh của người chơi hiện tại
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark; // Đặt hình ảnh cho ô
            CurrentPlayer = CurrentPlayer == 1 ? 0 : 1; // Chuyển sang người chơi tiếp theo
        }

        // Thay đổi giao diện người chơi hiện tại (tên và hình ảnh)
        private void ChangePlayer()
        {
            PlayerName.Text = Player[CurrentPlayer].Name; // Cập nhật tên người chơi
            PlayerMark.Image = Player[CurrentPlayer].Mark; // Cập nhật hình ảnh đánh dấu
        }
        #endregion
    }

    // Lớp sự kiện khi một button trên bàn cờ được nhấp
    public class ButtonClickEvent : EventArgs
    {
        private Point clickedPoint; // Điểm được nhấp

        public Point ClickedPoint
        {
            get { return clickedPoint; }
            set { clickedPoint = value; }
        }

        // Constructor khởi tạo ButtonClickEvent với điểm được nhấp
        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point; // Gán điểm được nhấp
        }
    }
}