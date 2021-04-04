using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimplePHPClientMessenger
{
    public partial class Messenger : Form
    {
        // Drag Control
        #region Drag Stuff
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        WebClient client = new WebClient();
        #endregion
        public Messenger()
        {
            InitializeComponent();
            guna2TextBox1.PlaceholderText = "Loading, Please wait...";
        }

        // Server Config
        string IP = "167.114.59.12";
        string PHP = "SendMessage.php";

        #region More Drag Stuff :3
        public static void FormDrag(IntPtr Handle, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void guna2Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            FormDrag(Handle, e);
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            FormDrag(Handle, e);
        }
        #endregion
        #region Controls
        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
            Application.Exit();
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Send Message to Server
        /* 
            On button click it sends a request to the given server providing the message and the username.
        */
        private void SendMessageButton_Click(object sender, EventArgs e)
        {
            if (guna2Button1.Visible == true)
            {
                guna2TextBox1.PlaceholderText = "Now click the reload button at the top left to load the messages!";
                if (guna2TextBox1.Text == "")
                {
                    guna2TextBox1.Text = "";
                    MessageBox.Show("Message Box is empty!", "Messenger");
                }
                else
                {
                    try
                    {
                        webBrowser1.Navigate("http://" + IP + "/" + PHP + "?username=USERNAME" + "&message=" + guna2TextBox1.Text);
                        guna2TextBox1.Text = "";
                        MessageBox.Show("Message Sent!", "Messenger");
                    }
                    catch
                    {
                        MessageBox.Show("Error!", "Messenger");
                    }
                }
            }
            else
            {
                if (guna2TextBox1.Text == "")
                {
                    guna2TextBox1.Text = "";
                    MessageBox.Show("Message Box is empty!", "Messenger");
                }
                else
                {
                    try
                    {
                        webBrowser1.Navigate("http://" + IP + "/" + PHP + "?username=USERNAME" + "&message=" + guna2TextBox1.Text);
                        guna2TextBox1.Text = "";
                        MessageBox.Show("Message Sent!", "Messenger");
                    }
                    catch
                    {
                        MessageBox.Show("Error!", "Messenger");
                    }
                }
            }
        }
        #endregion
        #region Auto Update Chat
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                var url = "http://167.114.59.12/msg.txt";
                string output = client.DownloadString(url);
                string[] lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                {
                    guna2TextBox1.PlaceholderText = "Type a message and press enter to send a message.";
                    Chat.Text = "\r\n" + output;
                }
            }
            catch
            {
                AutoUpdateChat.Stop();
                guna2Button1.Visible = true;
                Chat.Text = "Error, No message file made, Please be the first one to talk!";
                guna2TextBox1.PlaceholderText = "Type a message and press enter to be the first to talk in the chat!";
                MessageBox.Show("Error grabbing messages!", "Messenger");
            }
        }
        #endregion

        #region Key Presses
        private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                SendMessageButton.PerformClick();
            }
        }
        #endregion

        #region Reload Button
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2Button1.Visible = false;
            AutoUpdateChat.Start();
        }
        #endregion
    }
}
