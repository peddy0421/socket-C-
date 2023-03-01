using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace CLIENT
{
    public partial class Form1 : Form
    {
        // Main Variables       
        int port; //Port number of server
        string message; //Message to send
        int byteCount; //Raw bytes to send
        NetworkStream stream; //Link stream
        byte[] sendData; //Raw data to send
        TcpClient client; //TCP client variable
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            //Convert port number to text with error handling 
            if (!int.TryParse(textBox2.Text, out port))
            {
                //Adds debug to list box and shows message box
                MessageBox.Show("Port number not valied");
                listBox1.Items.Add("Port number invalied");
            }

            //Attemps to make connection
            try
            {
                client = new TcpClient(textBox1.Text, port);
                //Adds debug to list box and shows message box
                //MessageBox.Show("Connection Made");
                listBox1.Items.Add("Made connection with " + textBox1.Text);
            }
            catch (System.Net.Sockets.SocketException)
            {
                //Adds debug to list box and shows message box
                MessageBox.Show("Connection Failed");
                listBox1.Items.Add("Connection failed");
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                message = textBox3.Text; //Set message variable to input
                byteCount = Encoding.ASCII.GetByteCount(message); //Measures bytes required to send ASCII data
                sendData = new byte[byteCount]; //Prepares variable size for required data
                sendData = Encoding.ASCII.GetBytes(message); //Sets the sendData variable to the actual binary data (from the ASCII)
                stream = client.GetStream(); //Opens up the network stream
                stream.Write(sendData, 0, sendData.Length); //Transmits data onto the stream
                listBox1.Items.Add("Sent Data " + message); //Adds debug to the list box

            }
            catch (System.NullReferenceException) //Error if socket not open
            {
                //Adds debug to list box and shows message box
                MessageBox.Show("Connection not installised");
                listBox1.Items.Add("Failed to send data");
            }
        }
    }
}
