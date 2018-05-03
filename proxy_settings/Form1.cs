using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proxy_settings
{
    public partial class Form1 : Form
    {

       // [DllImport("user32.dll", CharSet = CharSet.Auto)]
        //private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);
        private const int EM_SETCUEBANNER = 0x1501;
        int panelwidth;
        bool hidden = false;
        Color Color;
        private ContextMenu m_menu = new ContextMenu();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            
            //SendMessage(address_box.Handle, EM_SETCUEBANNER, 0, "Enter the proxy address here");
            //SendMessage(port_box.Handle, EM_SETCUEBANNER, 0, "Enter the port here");
            panelwidth = panel2.Width;
            hidden = false;
            Color = button3.BackColor;

            m_menu.MenuItems.Add(0,
                new MenuItem("Show", new System.EventHandler(Show_Click)));
            m_menu.MenuItems.Add(1,
                new MenuItem("RIP", new System.EventHandler(Hide_Click)));
            m_menu.MenuItems.Add(2,
                new MenuItem("Exit", new System.EventHandler(Exit_Click)));

            notifyIcon1.ContextMenu = m_menu;
        }

        protected void Exit_Click(Object sender, System.EventArgs e)
        {
            Application.Exit();
        }
        protected void Hide_Click(Object sender, System.EventArgs e)
        {
            ///Summary
            ///Does something
            
        }
        protected void Show_Click(Object sender, System.EventArgs e)
        {
            Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (hidden)
            {
                panel2.Width = panel2.Width + 10;
                if (panel2.Width >= panelwidth)
                {
                    timer1.Stop();
                    hidden = false;
                    this.Refresh();
                }
            }
            else
            {
                panel2.Width -= 10;
                if (panel2.Width <= 0)
                {
                    timer1.Stop();
                    hidden = true;
                    this.Refresh();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Start();
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            button3.BackColor = Color.Red;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackColor = Color;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            //MessageBox.Show("gg");
            
            notifyIcon1.BalloonTipTitle = "Minimize to System Tray";
            notifyIcon1.BalloonTipText = "This will run in the background.";

            if (FormWindowState.Minimized == this.WindowState)
            {
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.02;
            }
            else
            {
                timer2.Stop();
                Application.Exit();
            }
        }
    }
    
}
