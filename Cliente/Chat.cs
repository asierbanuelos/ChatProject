using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class Chat : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private StreamReader reader;
        private StreamWriter writer;

        public Chat(TcpClient client, NetworkStream stream, StreamReader reader, StreamWriter writer)
        {
            InitializeComponent();

            this.client = client;
            this.stream = stream;
            this.reader = reader;
            this.writer = writer;


        }


    }
}
