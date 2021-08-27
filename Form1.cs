using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fievel
{
    public partial class Form1 : Form
    {
        private bool IsRunning { get; set; }

        public Form1()
        {
            InitializeComponent();
            IsRunning = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsRunning = !IsRunning;
            if (IsRunning && backgroundWorker1.IsBusy != true)
            {
                backgroundWorker1.RunWorkerAsync();
                button1.Text = "Stop";
                label_status.Text = "Running";
                label_status.ForeColor = Color.DarkGreen;
                
            }
            else
            {
                backgroundWorker1.CancelAsync();
                button1.Text = "Start";
                label_status.Text = "Not Running";
                label_status.ForeColor = Color.DarkRed;
            }
        }

        private void MoveMouse(int distance = 20, bool direction = false)
        {
            var f = Form.ActiveForm;
            
            Action a = () =>
            {
                Cursor = new Cursor(Cursor.Current.Handle);

                if (direction)
                {
                    Cursor.Position = new Point(Cursor.Position.X + distance, Cursor.Position.Y);
                }
                else
                {
                    Cursor.Position = new Point(Cursor.Position.X - distance, Cursor.Position.Y);
                }
            };

            if (f.InvokeRequired)
            {
                f.Invoke(a);
            }
            else
            {
                a();
            }

        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            bool toggle = false;

            while(true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    MoveMouse(1, toggle);
                    toggle = !toggle;

                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }
    }
}
