using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Pipes;


namespace Chat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        NamedPipeClientStream namedPipeClientStream = null;
        IAsyncResult asyncResult;
        byte[] bytes = new byte[256];
        private void button1_Click(object sender, EventArgs e)
        {
            namedPipeClientStream = new NamedPipeClientStream(".", "mypipe",PipeDirection.InOut,PipeOptions.Asynchronous);
            try
            {
                namedPipeClientStream.Connect(1000);
            }
            catch { }
            
            string message = textBox1.Text;
            asyncResult = namedPipeClientStream.BeginRead(bytes, 0, bytes.Length, null, null);
            timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = textBox2.Text;
            byte[] bs = Encoding.ASCII.GetBytes(message);
            namedPipeClientStream.Write(bs, 0, bs.Length);
            namedPipeClientStream.Flush();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (asyncResult.IsCompleted)
            {
                Console.WriteLine(bytes.Length);
                int a=namedPipeClientStream.EndRead(asyncResult);
                string s = Encoding.ASCII.GetString(bytes,0,a);
                listBox1.Items.Add(s);
                asyncResult = namedPipeClientStream.BeginRead(bytes, 0, bytes.Length, null, null);
            }
        }
    }
}
