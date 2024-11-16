namespace GameCaro
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Send = new System.Windows.Forms.Button();
            this.txt_message = new System.Windows.Forms.TextBox();
            this.txt_result = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnLAN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txbIP = new System.Windows.Forms.TextBox();
            this.pctbMark = new System.Windows.Forms.PictureBox();
            this.prcbCoolDown = new System.Windows.Forms.ProgressBar();
            this.txbPlayerName = new System.Windows.Forms.TextBox();
            this.pnlBanCo = new System.Windows.Forms.Panel();
            this.tmCoolDown = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbMark)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel2.Controls.Add(this.btn_Send);
            this.panel2.Controls.Add(this.txt_message);
            this.panel2.Controls.Add(this.txt_result);
            this.panel2.Location = new System.Drawing.Point(1064, 377);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(380, 318);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(293, 281);
            this.btn_Send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(75, 23);
            this.btn_Send.TabIndex = 2;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // txt_message
            // 
            this.txt_message.Location = new System.Drawing.Point(13, 281);
            this.txt_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_message.Name = "txt_message";
            this.txt_message.Size = new System.Drawing.Size(273, 22);
            this.txt_message.TabIndex = 1;
            this.txt_message.Text = "Hello";
            this.txt_message.TextChanged += new System.EventHandler(this.txt_message_TextChanged);
            // 
            // txt_result
            // 
            this.txt_result.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.txt_result.Location = new System.Drawing.Point(15, 12);
            this.txt_result.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_result.Name = "txt_result";
            this.txt_result.Size = new System.Drawing.Size(353, 262);
            this.txt_result.TabIndex = 0;
            this.txt_result.Text = "";
            this.txt_result.TextChanged += new System.EventHandler(this.txt_result_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnLAN);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.txbIP);
            this.panel3.Controls.Add(this.pctbMark);
            this.panel3.Controls.Add(this.prcbCoolDown);
            this.panel3.Controls.Add(this.txbPlayerName);
            this.panel3.Location = new System.Drawing.Point(1064, 64);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(380, 305);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Info;
            this.label5.Font = new System.Drawing.Font("Elephant", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(37, 5);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 43);
            this.label5.TabIndex = 9;
            this.label5.Text = "TURN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Time line";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 234);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Adress";
            // 
            // btnLAN
            // 
            this.btnLAN.BackColor = System.Drawing.SystemColors.Control;
            this.btnLAN.Location = new System.Drawing.Point(91, 258);
            this.btnLAN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLAN.Name = "btnLAN";
            this.btnLAN.Size = new System.Drawing.Size(212, 37);
            this.btnLAN.TabIndex = 4;
            this.btnLAN.Text = "LAN";
            this.btnLAN.UseVisualStyleBackColor = false;
            this.btnLAN.Click += new System.EventHandler(this.btnLAN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Font = new System.Drawing.Font("Elephant", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Menu;
            this.label1.Location = new System.Drawing.Point(29, 159);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(320, 43);
            this.label1.TabIndex = 5;
            this.label1.Text = "5 in a line to win";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txbIP
            // 
            this.txbIP.Location = new System.Drawing.Point(99, 228);
            this.txbIP.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbIP.Name = "txbIP";
            this.txbIP.Size = new System.Drawing.Size(235, 22);
            this.txbIP.TabIndex = 3;
            this.txbIP.Text = "127.0.0.1";
            // 
            // pctbMark
            // 
            this.pctbMark.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pctbMark.Cursor = System.Windows.Forms.Cursors.Default;
            this.pctbMark.Location = new System.Drawing.Point(216, 26);
            this.pctbMark.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pctbMark.Name = "pctbMark";
            this.pctbMark.Size = new System.Drawing.Size(133, 121);
            this.pctbMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctbMark.TabIndex = 2;
            this.pctbMark.TabStop = false;
            this.pctbMark.Click += new System.EventHandler(this.pctbM_Click);
            // 
            // prcbCoolDown
            // 
            this.prcbCoolDown.Location = new System.Drawing.Point(21, 123);
            this.prcbCoolDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.prcbCoolDown.Name = "prcbCoolDown";
            this.prcbCoolDown.Size = new System.Drawing.Size(177, 23);
            this.prcbCoolDown.TabIndex = 1;
            // 
            // txbPlayerName
            // 
            this.txbPlayerName.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.txbPlayerName.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.txbPlayerName.Location = new System.Drawing.Point(21, 78);
            this.txbPlayerName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txbPlayerName.Name = "txbPlayerName";
            this.txbPlayerName.ReadOnly = true;
            this.txbPlayerName.Size = new System.Drawing.Size(176, 22);
            this.txbPlayerName.TabIndex = 0;
            this.txbPlayerName.TextChanged += new System.EventHandler(this.txbPlayerName_TextChanged);
            // 
            // pnlBanCo
            // 
            this.pnlBanCo.BackColor = System.Drawing.SystemColors.Control;
            this.pnlBanCo.Location = new System.Drawing.Point(17, 64);
            this.pnlBanCo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlBanCo.Name = "pnlBanCo";
            this.pnlBanCo.Size = new System.Drawing.Size(1039, 590);
            this.pnlBanCo.TabIndex = 0;
            this.pnlBanCo.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBanCo_Paint);
            // 
            // tmCoolDown
            // 
            this.tmCoolDown.Tick += new System.EventHandler(this.tmCoolDown_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(5, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1504, 28);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.newGameToolStripMenuItem.Text = "New game";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1504, 711);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlBanCo);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Game Caro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctbMark)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ProgressBar prcbCoolDown;
        private System.Windows.Forms.TextBox txbPlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLAN;
        private System.Windows.Forms.TextBox txbIP;
        private System.Windows.Forms.PictureBox pctbMark;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Send;
        private System.Windows.Forms.TextBox txt_message;
        private System.Windows.Forms.RichTextBox txt_result;
        private System.Windows.Forms.Panel pnlBanCo;
        private System.Windows.Forms.Timer tmCoolDown;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    }
}

