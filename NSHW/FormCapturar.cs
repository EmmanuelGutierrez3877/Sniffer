using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using PcapDotNet.Analysis;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Base;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets.Http;

namespace NSHW
{
    public partial class GUI : Form
    {
        private  IList<LivePacketDevice> AdaptersList;
        private PacketDevice selectedAdapter;
        private bool first_time = true; 
        List<List<Byte>> bd = new List<List<byte>>();

        public GUI()
        {
            InitializeComponent();

            try
            {
                AdaptersList = LivePacketDevice.AllLocalMachine;
            }
            catch(Exception e)
            {
                MessageBox.Show("Please make sure to run as Adminstrator and install Winpcap");
            }

            PcapDotNetAnalysis.OptIn = true;

            if (AdaptersList.Count == 0)
            {

                MessageBox.Show("No adapters found !!");

                return;

            }

            for (int i = 0; i != AdaptersList.Count; ++i)
            {
                LivePacketDevice Adapter = AdaptersList[i];

                if (Adapter.Description != null)

                    adapters_list.Items.Add(Adapter.Description);
                else
                    adapters_list.Items.Add("Unknown");
            }

        }
       
  
        private void start_button_Click(object sender, EventArgs e)
        {
            

            if (adapters_list.SelectedIndex >= 0)
            {
                timer1.Enabled = true;
                selectedAdapter = AdaptersList[adapters_list.SelectedIndex];
                
                if (first_time)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

                start_button.Enabled = false;
                stop_button.Enabled = true;
                adapters_list.Enabled = false;
                first_time = false;
                
            }
            else
            {
                MessageBox.Show("Selecciona un adaptador");
            }
        }


        private void stop_button_Click(object sender, EventArgs e)
        {
            start_button.Enabled = true;
            stop_button.Enabled = false;
            adapters_list.Enabled = true;
            timer1.Enabled = false;
        }


        List<Byte> bf= new List<byte>();
       

        private void PacketHandler(Packet packet)
        {
            bf.Clear();
            foreach(byte b in packet.Buffer)
            {
                bf.Add(b);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            string a = "";
            foreach (Byte b in bf)
            {
                a += b.ToString();
            }
            ListViewItem item = new ListViewItem(a);

            if (! listView1.Items.Contains(item) && a != "")
            {
                
                //item.SubItems.Add(bf.ToString());
                List<Byte> auxB = new List<byte>(bf);
                
                bd.Add(auxB);
                listView1.Items.Add(item);
            }


        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            using (PacketCommunicator communicator = selectedAdapter.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1000))
            {
                
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    MessageBox.Show("This program works only on Ethernet networks!");

                    return;
                }
                communicator.ReceivePackets(0, PacketHandler);
            }
        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            List<Byte> Ob = new List<byte>();
            int i = listView1.SelectedIndices[0];
            Ob = bd[i];
            FormLector fl = new FormLector(Ob);
            fl.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FormLector f1 = new FormLector();
            f1.ShowDialog();
        }

    }
}
