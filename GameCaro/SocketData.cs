using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    [Serializable]
    public class SocketData
    {
        private int command;
        public int Command
        {
            get { return command; }
            set { command = value; }
        }

        private Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private string chatMessage;
        public string ChatMessage
        {
            get { return chatMessage; }
            set { chatMessage = value; }
        }

        private string sender;
        public string Sender
        {
            get { return sender; }
            set { sender = value; }
        }

        private DateTime timestamp;
        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        public SocketData(int commmand, string message, Point point, string sender = "", DateTime timestamp = default, string chatMessage = "")
        {
            this.Command = commmand;
            this.Point = point;
            this.Message = message;
            this.Timestamp = timestamp == default(DateTime) ? DateTime.Now : timestamp;
            this.sender = sender;
            this.chatMessage = chatMessage;
         }
    }

    public enum SocketCommand
    {
        SEND_POINT,
        NOTIFY,
        NEW_GAME,
        END_GAME,
        TIME_OUT,
        QUIT,
        CHAT_MESSAGE
    }
}
