using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lhdAlert.Properties;

namespace lhdAlert
{
    public partial class settingsForm : Form
    {
        public settingsForm()
        {
            InitializeComponent();
            textBox1.Text = Settings.Default.dataAddr;
            textBox2.Text = Settings.Default.sound;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Settings.Default.dataAddr = textBox1.Text;
            Settings.Default.sound = textBox2.Text;
            Settings.Default.Save();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Settings.Default.dataAddr = textBox1.Text;
            Settings.Default.sound = textBox2.Text;
            Settings.Default.Save();
        }
    }
}
