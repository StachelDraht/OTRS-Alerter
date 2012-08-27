using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Media;
using lhdAlert.Properties;

namespace lhdAlert
{
    public partial class Form1 : Form
    {
        checker chcls = new checker();
        Thread checkThread;

        public Form1()
        {
            InitializeComponent();
            objClass obj = new objClass();
            obj.lable = this.label2;
            obj.notify = this.notifyIcon1;
            object objectLable = label2;
            object objectNotify = notifyIcon1;
            //ThreadStart starter = delegate { chcls.check(label2, notifyIcon1); };
            checkThread = new Thread(chcls.check);
            checkThread.Name = "Check";
            checkThread.Start(obj);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); 
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsForm setForm = new settingsForm();
            setForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            notifyIcon1.Visible = true;
            notifyIcon1.BalloonTipTitle = "LHD Alerter";
            notifyIcon1.BalloonTipText = "I`m here!";
            notifyIcon1.ShowBalloonTip(500);
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.Visible = true;
        }

    }

    public class objClass
    {
        public Label lable;
        public NotifyIcon notify;
    }
}
