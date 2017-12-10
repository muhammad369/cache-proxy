using cache_proxy.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cache_proxy
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static BindingList<LogItem> list = new BindingList<LogItem>();

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != System.Windows.Forms.MouseButtons.Left)
            {
                return;
            }
            //
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                this.Show();
                try
                {
                    dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.RowCount - 1;
                }
                catch { }
            }
        }

        Timer t = new Timer();

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            //
            this.dataGridView1.DataSource = list;
            //
            this.Location = new Point(
                Screen.PrimaryScreen.Bounds.Width - this.Width - 50,
                Screen.PrimaryScreen.Bounds.Height - this.Height - 50
                );
            //
            t.Interval = 500;
            t.Tick += Form1_Tick;
            t.Start();
            
        }

        void Form1_Tick(object sender, EventArgs e)
        {
            this.Hide();
            t.Stop();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void Form1_ResizeBegin(object sender, EventArgs e)
        {
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                WindowState = FormWindowState.Normal;
            }
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "proxy server will be shut down", "confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //Application.Exit();
                //Close();
                Environment.Exit(Environment.ExitCode);
            }
        }

        private void useCacheMenuItem_Click(object sender, EventArgs e)
        {
            toggleCache();
        }

        private void toggleCache()
        {
            if (Program.useCache)
            {
                Program.useCache = false;
                useCacheMenuItem.Checked = false;
                button1.Text = "Enable Cache";
                button1.BackColor = Color.Orange;
            }
            else
            {
                Program.useCache = true;
                useCacheMenuItem.Checked = true;
                button1.Text = "Disable Cache";
                button1.BackColor = Color.GreenYellow;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //clear
            list.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //use cache
            toggleCache();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }
    }
}
