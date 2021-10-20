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
        private void button1_Click(object sender, EventArgs e)
        {
            namedPipeClientStream = new NamedPipeClientStream(".", "mypipe");
            try
            {
                namedPipeClientStream.Connect(1000);
            }
            catch { }
            
            string message = textBox1.Text;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = textBox2.Text;
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            namedPipeClientStream.Write(bytes, 0, bytes.Length);
            namedPipeClientStream.Flush();
            namedPipeClientStream.Read(bytes, 0, bytes.Length);
            string s = Encoding.ASCII.GetString(bytes);
            listBox1.Items.Add(s);

        }
    }
}
