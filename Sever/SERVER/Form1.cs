using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace SERVER
{
    public partial class Form1 : Form
    {
        TcpListener server;
        TcpClient clientconnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            server = new TcpListener(IPAddress.Any, 8000); //Opens server port
            server.Start(); //Starts server
            listBox1.Items.Add("Server Started"); //Adds info to list
            clientconnection = server.AcceptTcpClient(); //Accepts connection request
            timer1.Start(); //Starts timer to check stream at regular intervals
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            NetworkStream DataStream = clientconnection.GetStream(); //Gets the data stream
            byte[] buffer = new byte[clientconnection.ReceiveBufferSize]; //Gets required buffer size
            int Data = DataStream.Read(buffer, 0, clientconnection.ReceiveBufferSize); //Gets message (encoded)
            string message = Encoding.ASCII.GetString(buffer, 0, Data); //Decoodes message
            listBox1.Items.Add(message); //Adds message to output window

        }
        //server.Stop();
        //this.Visible = false;
        //clientconnection.Close();
        //DataStream.Close();
        //timer1.Stop();
    }
}
