using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NSHW
{
    public partial class FormLector : Form
    {
        OpenFileDialog arc = new OpenFileDialog();
        List<Byte> contenido = new List<byte>();

        private void FormLector_Load(object sender, EventArgs e)
        {
            listViewInformacion.View = View.Details;
            listViewInformacion.FullRowSelect = true;
            listViewInformacion.GridLines = true;

            listViewInformacion.Columns.Add("Descripcion", 240);
            listViewInformacion.Columns.Add("Dato", 320);
        }

        public FormLector()
        {
            InitializeComponent();
            buttonAbrir.Enabled = true;
            buttonGuardar.Enabled = false;
        }

        ////////////////////////Practica 7
        public FormLector(List<Byte> dat)
        {
            contenido = dat;
            InitializeComponent();

            buttonAbrir.Enabled = false;
            buttonGuardar.Enabled = true;

            Leer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.ShowDialog();
            if (SFD.FileName != "")
            {
                SFD.FileName += ".bin";
                System.IO.FileStream fs = (System.IO.FileStream)SFD.OpenFile();

                fs.Write(contenido.ToArray(), 0, contenido.Count);

                fs.Close();
            }

        }
        //////////////////////////////////////////////////////////////////////////////////

        private void buttonAbrir_Click(object sender, EventArgs e)
        {


            textBoxDatos.Text = "";
            textBoxNombre.Text = "";
            contenido.Clear();

            arc.FileName = "";
            arc.ShowDialog();
            listViewInformacion.Clear();
            listViewInformacion.Columns.Add("Descripcion", 240);
            listViewInformacion.Columns.Add("Dato", 340);

            if (arc.FileName != "")
            {
                System.IO.FileStream fichero = System.IO.File.OpenRead(arc.FileName);
                System.IO.BinaryReader sr = new System.IO.BinaryReader(arc.OpenFile());
                textBoxNombre.Text = arc.FileName;

                Byte g;

                for (int i = 0; i < fichero.Length; i++)
                {
                    g = sr.ReadByte();
                    contenido.Add(g);
                }

                fichero.Close();
                sr.Close();

                Leer();


            }

        }

        private void Leer()
        {
            String dirDes = "", dirOri = "", tipo = "", resto = "";
            int res = 0;

            dirDes = ObtenBytesHex(0, 6, contenido, ":");
            dirOri = ObtenBytesHex(6, 12, contenido, ":");
            tipo = ObtenBytesHex(12, 14, contenido, "");
            res = 14;

            listViewInformacion.Items.Add("Direccion Destino:");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(dirDes);

            listViewInformacion.Items.Add("Direccion Origen:");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(dirOri);

            listViewInformacion.Items.Add("Tipo:");
            switch (tipo)
            {
                //////////IPv4
                case "0800":

                    String pre = "", tos = "", mbz = "", band = "", desp = "", pro = "";
                    int lon, lonT, ttl;

                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("08_00 : IPv4");
                    int.TryParse(ObtenBytesHex(14, 15, contenido, "")[1].ToString(), out lon);
                    lon = lon * 4;
                    //
                    listViewInformacion.Items.Add("Longitud:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(lon.ToString());
                    //
                    String bits = contenido[15].ToString("X2");
                    bits = HexStringToBinary(bits);
                    Char[] arr = bits.ToCharArray();

                    for (int i = 0; i < 8; i++)
                    {
                        if (i < 3)
                        {
                            pre += arr[i];
                        }
                        else if (i < 7)
                        {
                            tos += arr[i];
                        }
                        else
                        {
                            mbz += arr[i];
                        }
                    }

                    listViewInformacion.Items.Add("Precedencia:");
                    switch (pre)
                    {
                        case "000":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("000 : Rutina");
                            break;

                        case "001":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("001 : Prioridad");
                            break;

                        case "010":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("010 : Inmediato");
                            break;

                        case "011":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("011 : Flash");
                            break;

                        case "100":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("100 : Flash override");
                            break;

                        case "101":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("101 : Critico");
                            break;

                        case "110":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("110 : Internetwork control");
                            break;

                        case "111":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("111 : Network control");
                            break;
                    }
                    //
                    listViewInformacion.Items.Add("Tipo de servicio:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(TypeOfService(tos));

                    //
                    listViewInformacion.Items.Add("MBZ:");
                    switch (mbz)
                    {
                        case "0":
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("0 : Predeterminado");
                            break;

                        default:
                            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(mbz + " : Experimento");
                            break;
                    }
                    //
                    lonT = int.Parse(ObtenBytesHex(16, 18, contenido, ""), System.Globalization.NumberStyles.HexNumber);
                    listViewInformacion.Items.Add("Longitud total del paquete:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(lonT.ToString());
                    //
                    listViewInformacion.Items.Add("Identificacion:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(18, 20, contenido, " "));
                    //
                    listViewInformacion.Items.Add("Banderas:");
                    bits = ObtenBytesHex(20, 22, contenido, "");
                    bits = HexStringToBinary(bits);
                    arr = bits.ToCharArray();
                    if (arr[1] == '0')
                    {
                        band = band + "0   0:Divisible      ";
                    }
                    else if (arr[1] == '1')
                    {
                        band = band + "0   1:No Divisible   ";
                    }
                    if (arr[2] == '0')
                    {
                        band = band + "0:Ultimo Fracmento";
                    }
                    else if (arr[2] == '1')
                    {
                        band = band + "1:Fracmento Intermedio";
                    }
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(band);
                    //
                    for (int i = 3; i < 16; i++)
                    {
                        desp = desp + arr[i];
                    }
                    listViewInformacion.Items.Add("Des. Fracmentacion:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(desp);
                    //
                    ttl = contenido[22];
                    listViewInformacion.Items.Add("tiempo de vida:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ttl + " saltos");
                    //
                    listViewInformacion.Items.Add("Protocolo:");
                    switch (contenido[23])
                    {
                        case 0:
                            pro = "0: Reservado";
                            break;

                        case 1:
                            pro = "1: ICMP (Internet Control Message Protocol) ";
                            break;

                        case 2:
                            pro = "2: IGMP (Internet Group Management Protocol) ";
                            break;

                        case 3:
                            pro = "3: GGP (Gateway - to - Gateway Protocol) ";
                            break;

                        case 4:
                            pro = "4:IP (IP encapsulation) ";
                            break;

                        case 5:
                            pro = "5: Flujo (Stream) ";
                            break;

                        case 6:
                            pro = "6: TCP (Transmission Control) ";
                            break;

                        case 8:
                            pro = "8: EGP (Exterior Gateway Protocol) ";
                            break;

                        case 9:
                            pro = "9: PIRP (Private Interior Routing Protocol) ";
                            break;

                        case 17:
                            pro = "17: UDP (User Datagram) ";
                            break;

                        case 89:
                            pro = "89: OSPF (Open Shortest Path First) ";
                            break;

                    }
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(pro);
                    
                    //
                    listViewInformacion.Items.Add("Checksum:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(24, 26, contenido, " "));
                    //
                    listViewInformacion.Items.Add("IP Origen:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytes(26, 30, contenido, ":"));
                    //
                    listViewInformacion.Items.Add("IP Destino:");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytes(30, 34, contenido, ":"));
                    //
                    res = 34;

                    //////////////Practica 3
                    if (pro == "1: ICMP (Internet Control Message Protocol) ")
                    {
                        listViewInformacion.Items.Add("ICMP Type: ");
                        listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(contenido[34] + ": " + ICMPType(contenido[34]));


                        listViewInformacion.Items.Add("ICMP Code: ");
                        listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(contenido[35] + ": " + ICMPCode(contenido[35]));

                        listViewInformacion.Items.Add("ICMP Checksum:");
                        listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(36, 38, contenido, " "));

                        res = 38;

                    }
                    //////////////////////
                    //////////////////Practica 8
                    if (pro == "17: UDP (User Datagram) ")
                    {
                        res = UDP(res);
                    }
                    else if (pro == "6: TCP (Transmission Control) ")
                    {
                        res = TCP(res);
                    }

                    ////////////////////////////////////////////////////

                    ////////DNS////////////////////////////////////////////////////
                    if (pro== "17: UDP (User Datagram) ")
                    {
                        foreach(ListViewItem i in listViewInformacion.Items)
                        {
                            if(i.SubItems[0].Text== "Puerto de destino: " && i.SubItems[1].Text=="53")
                            {
                                listViewInformacion.Items.Add("DNS: ");
                                String dns="";

                                listViewInformacion.Items.Add("ID: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(res, res+2, contenido, ""));

                                String buffer = "";
                                foreach (Char c in ObtenBytesHex(res+2, res + 4, contenido, ""))
                                {
                                    buffer = buffer + HexStringToBinary(c.ToString());
                                }

                                listViewInformacion.Items.Add("QR: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(0,1,buffer));

                                listViewInformacion.Items.Add("Opcode: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(1, 5, buffer));

                                listViewInformacion.Items.Add("AA: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(5, 6, buffer));

                                listViewInformacion.Items.Add("TC: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(6, 7, buffer));

                                listViewInformacion.Items.Add("RD: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(7, 8, buffer));

                                listViewInformacion.Items.Add("RA: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(8, 9, buffer));

                                listViewInformacion.Items.Add("Z: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(9, 12, buffer));

                                listViewInformacion.Items.Add("RCODE: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenChars(12, 16, buffer));


                                listViewInformacion.Items.Add("QDCOUNT: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(res+4, res + 6, contenido, ""));

                                listViewInformacion.Items.Add("ANCOUNT: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(res + 6, res + 8, contenido, ""));

                                listViewInformacion.Items.Add("NSCOUNT: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(res + 8, res + 10, contenido, ""));

                                listViewInformacion.Items.Add("ARCOUNT: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(res + 10, res + 12, contenido, ""));

                                res = res + 12;

                                for (int j = res; j < contenido.Count-8; j++)
                                {
                                    int c;
                                    Int32.TryParse( contenido[j].ToString() ,out c);
                                    if (c < 33)
                                    {
                                        dns = dns + ".";
                                    }
                                    else
                                    {
                                        dns = dns + (char)c;
                                    }
                                    res++;
                                }

                                listViewInformacion.Items.Add("Dominio: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(dns);

                                listViewInformacion.Items.Add("Tipo: ");
                                String dnst = ObtenBytesHex(res, res+2, contenido, " ");
                                switch (dnst)
                                {
                                    case "00 01":
                                        dnst = "A";
                                        break;

                                    case "00 05":
                                        dnst = "CNAME";
                                        break;

                                    case "00 0D":
                                        dnst = "HINFO";
                                        break;

                                    case "00 0F":
                                        dnst = "MX";
                                        break;
                                }
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(dnst);

                                listViewInformacion.Items.Add("Clase: ");
                                String dnsc = ObtenBytesHex(res+2, res+4, contenido, " ");
                                switch (dnsc)
                                {
                                    case "00 01":
                                        dnsc = "IN";
                                        break;

                                    case "00 03":
                                        dnsc = "CH";
                                        break;

                                }
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(dnsc);


                                listViewInformacion.Items.Add("IP: ");
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(res+4,res+8,contenido,"."));
                                res = res + 8;
                            }
                        }
                    }
                    ///////////////////////

                    break;

                case "0806":
                    //////////////////////////Practica 4
                    int x, y, act, hd, op;

                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("08_06 : ARP");

                    listViewInformacion.Items.Add("Tipo de hardware: ");
                    int.TryParse(ObtenBytesHex(14, 16, contenido, ""), out hd);
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(hd + ": " + ARPHardware(hd));

                    listViewInformacion.Items.Add("Tipo de Protocolo: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(16, 18, contenido, ""));

                    int.TryParse(ObtenBytes(18, 19, contenido, ""), out x);
                    int.TryParse(ObtenBytes(19, 20, contenido, ""), out y);

                    listViewInformacion.Items.Add("Longitud de direccion de hardware: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(x.ToString());

                    listViewInformacion.Items.Add("Longitud de direccion de protocolo: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(y.ToString());

                    listViewInformacion.Items.Add("Codigo de operacion: ");
                    int.TryParse(ObtenBytesHex(20, 22, contenido, ""), out op);
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(op + ": " + ARPOperation(op));

                    listViewInformacion.Items.Add("Direccion de hardware del emisor: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(22, 22 + x, contenido, ":"));
                    act = 22 + x;

                    listViewInformacion.Items.Add("Direccion IP del emisor: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytes(act, act + y, contenido, "."));
                    act = act + y;

                    listViewInformacion.Items.Add("Direccion de hardware del receptor: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(act, act + x, contenido, ":"));
                    act = act + x;

                    listViewInformacion.Items.Add("Direccion IP del receptor: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytes(act, act + y, contenido, "."));
                    act = act + y;

                    res = act;
                    break;
                /////////////////////////////////////////

                case "0835":
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("08_35 : RARP");
                    break;

                case "86DD":
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("86_DD : IPv6");

                    //////////////Practica 5
                    String bytes = ObtenBytesHex(14, 54, contenido, "");

                    listViewInformacion.Items.Add("Version: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(bytes[0].ToString());

                    listViewInformacion.Items.Add("Clase de trafico: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(TypeOfService(HexStringToBinary(bytes[1].ToString())));

                    //listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add();

                    listViewInformacion.Items.Add("Etiqueta de flujo: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(bytes[2].ToString() + bytes[3].ToString() + bytes[4].ToString() + bytes[5].ToString() + bytes[6].ToString());

                    listViewInformacion.Items.Add("Tamaño de carga util: ");
                    int tcu = int.Parse(ObtenBytesHex(18, 20, contenido, ""), System.Globalization.NumberStyles.HexNumber);
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(tcu.ToString());

                    listViewInformacion.Items.Add("Encabezado siguiente: ");
                    pro = "";
                    switch (contenido[20])
                    {
                        case 0:
                            pro = "0: Reservado";
                            break;

                        case 1:
                            pro = "1: ICMP (Internet Control Message Protocol) ";
                            break;

                        case 2:
                            pro = "2: IGMP (Internet Group Management Protocol) ";
                            break;

                        case 3:
                            pro = "3: GGP (Gateway - to - Gateway Protocol) ";
                            break;

                        case 4:
                            pro = "4:IP (IP encapsulation) ";
                            break;

                        case 5:
                            pro = "5: Flujo (Stream) ";
                            break;

                        case 6:
                            pro = "6: TCP (Transmission Control) ";
                            break;

                        case 8:
                            pro = "8: EGP (Exterior Gateway Protocol) ";
                            break;

                        case 9:
                            pro = "9: PIRP (Private Interior Routing Protocol) ";
                            break;

                        case 17:
                            pro = "17: UDP (User Datagram) ";
                            break;

                        case 89:
                            pro = "89: OSPF (Open Shortest Path First) ";
                            break;

                        case 58:
                            pro = "58: ICMPv6 (Internet Control Message Protocol) ";
                            break;

                    }
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(pro);


                    listViewInformacion.Items.Add("limite de salto: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(contenido[21] + " saltos ");

                    listViewInformacion.Items.Add("Direccion de origen: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(22, 38, contenido, ":"));

                    listViewInformacion.Items.Add("Direccion de destino: ");
                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ObtenBytesHex(38, 54, contenido, ":"));

                    res = 54;


                    /////////////Practica 6
                    if (contenido[20] == 58)
                    {
                        listViewInformacion.Items.Add("Tipo ICMPv6: ");


                        String tipoICMPv6 = contenido[54].ToString();
                        String codigoICMPv6 = contenido[55].ToString();
                        String checkICMPv6 = ObtenBytesHex(56, 58, contenido, " ");

                        switch (tipoICMPv6)
                        {
                            case "1":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("1: Destination Unreachable");

                                listViewInformacion.Items.Add("Codigo ICMPv6: ");

                                if (codigoICMPv6 == "0")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("0: no route to destination");
                                }
                                else if (codigoICMPv6 == "1")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("1: communication with destination administratively prohibited");
                                }
                                else if (codigoICMPv6 == "2")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("2: beyond scope of source address");
                                }
                                else if (codigoICMPv6 == "3")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("3: address unreachable");
                                }
                                else if (codigoICMPv6 == "4")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("4: port unreachable");
                                }
                                else if (codigoICMPv6 == "5")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("5: source address failed ingress/egress policy");
                                }
                                else if (codigoICMPv6 == "6")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("6: reject route to destination");
                                }
                                else if (codigoICMPv6 == "7")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("7: Error in Source Routing Header");
                                }



                                break;

                            case "2":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("2: Packet Too Big");
                                break;

                            case "3":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("3: Time Exceeded");

                                listViewInformacion.Items.Add("Codigo ICMPv6: ");

                                if (codigoICMPv6 == "0")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("0: hop limit exceeded in transit");
                                }
                                else if (codigoICMPv6 == "1")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("1: fragment reassembly time exceeded");
                                }

                                break;

                            case "4":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("4: Parameter Problem");

                                listViewInformacion.Items.Add("Codigo ICMPv6: ");

                                if (codigoICMPv6 == "0")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("0: erroneous header field encountered");
                                }
                                else if (codigoICMPv6 == "1")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("1: unrecognized Next Header type encountered");
                                }
                                else if (codigoICMPv6 == "2")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("2: unrecognized IPv6 option encountered");
                                }

                                break;

                            case "100":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("100: Private experimentation");
                                break;

                            case "101":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("101: Private experimentation");
                                break;

                            case "127":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("127: Reserved for expansion of ICMPv6 error messages");
                                break;

                            case "128":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("128: Echo Request");
                                break;

                            case "129":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("129: Echo Reply");
                                break;

                            case "133":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("133: Router Solicitation (NDP)");
                                break;

                            case "134":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("134: Router Advertisement (NDP)");
                                break;

                            case "135":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("135: Neighbor Solicitation (NDP)");
                                break;

                            case "136":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("136: Neighbor Advertisement (NDP)");
                                break;

                            case "137":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("137: Redirect Message (NDP)");
                                break;

                            case "138":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("138: Router Renumbering");

                                listViewInformacion.Items.Add("Codigo ICMPv6: ");

                                if (codigoICMPv6 == "0")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("0: Router Renumbering Command");
                                }
                                else if (codigoICMPv6 == "1")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("1: Router Renumbering Result");
                                }
                                else if (codigoICMPv6 == "255")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("255: Sequence Number Reset");
                                }

                                break;

                            case "139":
                                listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("139: ICMP Node Information Query");

                                listViewInformacion.Items.Add("Codigo ICMPv6: ");

                                if (codigoICMPv6 == "0")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("0: The Data field contains an IPv6 address which is the Subject of this Query.");
                                }
                                else if (codigoICMPv6 == "1")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("1: The Data field contains a name which is the Subject of this Query, or is empty, as in the case of a NOOP.");
                                }
                                else if (codigoICMPv6 == "2")
                                {
                                    listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add("2: The Data field contains an IPv4 address which is the Subject of this Query.");
                                }

                                break;

                        }

                        listViewInformacion.Items.Add("Checksum ICMPv6: ");
                        listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(checkICMPv6);

                        res = 58;

                        
                    }

                    break;
            }

            resto = ObtenBytesHex(res, contenido.Count, contenido, " ");
            textBoxDatos.Text = resto;
        }

        String ObtenChars(int inicio, int fin, String dat)
        {
            String trama = "";
            for (int i = inicio; i < fin; i++)
            {
                trama = trama + dat[i].ToString();
            }
            return trama;
        }

        String ObtenBytesHex(int inicio, int fin, List<Byte> datos, String car)
        {
            String trama = "";
            for (int i = inicio; i < fin; i++)
            {
                trama = trama + datos[i].ToString("X2");
                if (i < fin - 1)
                {
                    trama = trama + car;
                }

            }
            return trama;
        }

        String ObtenBytes(int inicio, int fin, List<Byte> datos, String car)
        {
            String trama = "";
            for (int i = inicio; i < fin; i++)
            {
                trama = trama + datos[i].ToString();
                if (i < fin - 1)
                {
                    trama = trama + car;
                }

            }
            return trama;
        }

        ////////Practica 2
        String HexStringToBinary(string hexString)
        {
            var lup = new Dictionary<char, string>{
            { '0', "0000"},
            { '1', "0001"},
            { '2', "0010"},
            { '3', "0011"},

            { '4', "0100"},
            { '5', "0101"},
            { '6', "0110"},
            { '7', "0111"},

            { '8', "1000"},
            { '9', "1001"},
            { 'A', "1010"},
            { 'B', "1011"},

            { 'C', "1100"},
            { 'D', "1101"},
            { 'E', "1110"},
            { 'F', "1111"}
        };

            var ret = string.Join("", from character in hexString select lup[character]);
            return ret;
        }
        ////////////////////

        ///////////////////Practica 3
        String ICMPType(int i)
        {

            var lup = new Dictionary<string, string>
            {
            { "0", "Echo Reply (respuesta de eco)"},
            { "3", "Destination Unreacheable (destino inaccesible)"},
            { "4", "Source Quench (disminución del tráfico desde el origen)"},
            { "5", "Redirect (redireccionar - cambio de ruta)"},
            { "8", "Echo (solicitud de eco)"},
            { "11", "Time Exceeded (tiempo excedido para un datagrama)"},
            { "12", "Parameter Problem (problema de parámetros"},
            { "13", "Timestamp (solicitud de marca de tiempo)"},
            { "14", "Timestamp Reply (respuesta de marca de tiempo)"},
            { "15", "Information Request (solicitud de información) - obsoleto-"},
            { "16", "Information Reply (respuesta de información) - obsoleto-"},
            { "17", "Addressmask (solicitud de máscara de dirección)"},
            { "18", "Addressmask Reply (respuesta de máscara de dirección" }

        };

            var ret = lup[i.ToString()];
            return ret;
        }

        String ICMPCode(int i)
        {

            var lup = new Dictionary<String, string>
            {
            { "0", "no se puede llegar a la red"},
            { "1", "no se puede llegar al host o aplicación de destino "},
            { "2", "el destino no dispone del protocolo solicitado"},
            { "3", "no se puede llegar al puerto destino o la aplicación destino no está libre"},
            { "4", "se necesita aplicar fragmentación, pero el flag correspondiente indica lo contrario"},
            { "5", "la ruta de origen no es correcta"},
            { "6", "no se conoce la red destino"},
            { "7", "no se conoce el host destino"},
            { "8", "el host origen está aislado"},
            { "9", "la comunicación con la red destino está prohibida por razones administrativas"},
            { "10", "la comunicación con el host destino está prohibida por razones administrativas"},
            { "11", "no se puede llegar a la red destino debido al Tipo de servicio"},
            { "12", "no se puede llegar al host destino debido al Tipo de servicio" }

        };

            var ret = lup[i.ToString()];
            return ret;
        }
        /////////////////

        ////////Practica 4
        String ARPHardware(int i)
        {

            var lup = new Dictionary<String, string>
            {
                { "0" ,  "Reserved" },
                { "1" ,  "Ethernet (10Mb)" },
                { "2" ,  "Experimental Ethernet (3Mb)" },
                { "3" ,  "Amateur Radio AX.25" },
                { "4" ,  "Proteon ProNET Token Ring" },
                { "5" ,  "Chaos" },
                { "6" ,  "IEEE 802 Networks" },
                { "7" ,  "ARCNET" },
                { "8" ,  "Hyperchannel" },
                { "9" ,  "Lanstar" },
                { "10" , "Autonet Short Address" },
                { "11" , "LocalTalk" },
                { "12" , "LocalNet (IBM PCNet or SYTEK LocalNET)" },
                { "13" , "Ultra link" },
                { "14" , "SMDS" },
                { "15" , "Frame Relay" },
                { "16" , "Asynchronous Transmission Mode (ATM)" },
                { "17" , "HDLC" },
                { "18" , "Fibre Channel" },
                { "19" , "Asynchronous Transmission Mode (ATM)" },
                { "20" , "Serial Line" },
                { "21" , "Asynchronous Transmission Mode (ATM)" },
                { "22" , "MIL-STD-188-220" },
                { "23" , "Metricom" },
                { "24" , "IEEE 1394.1995" },
                { "25" , "MAPOS" },
                { "26" , "Twinaxial" },
                { "27" , "EUI-64" },
                { "28" , "HIPARP" },
                { "29" , "IP and ARP over ISO 7816-3" },
                { "30" , "ARPSec" },
                { "31" , "IPsec tunnel" },
                { "32" , "InfiniBand (TM)" },
                { "33" , "TIA-102 Project 25 Common Air Interface (CAI)" },
                { "34" , "Wiegand Interface" },
                { "35" , "Pure IP" },
                { "36" , "HW_EXP1" },
                { "37" , "HFI" },
                { "256", "HW_EXP2" },
                { "257", "AEthernet" },
                { "65535" , "Reserved" },

        };
            if ((i >= 38 && i <= 255) || (i >= 258 && i <= 65534))
            {
                return "Unassigned";

            }
            else
            {
                var ret = lup[i.ToString()];
                return ret;
            }
        }

        String ARPOperation(int i)
        {

            var lup = new Dictionary<String, string>
            {

                { "0" ,  "Reserved" },
                { "1" ,  "REQUEST" },
                { "2" ,  "REPLY" },
                { "3" ,  "request Reverse" },
                { "4" ,  "reply Reverse" },
                { "5" ,  "DRARP-Request" },
                { "6" ,  "DRARP-Reply" },
                { "7" ,  "DRARP-Error" },
                { "8" ,  "InARP-Request" },
                { "9" ,  "InARP-Reply" },
                { "10" , "ARP-NAK" },
                { "11" , "MARS-Request" },
                { "12" , "MARS-Multi" },
                { "13" , "MARS-MServ" },
                { "14" , "MARS-Join" },
                { "15" , "MARS-Leave" },
                { "16" , "MARS-NAK" },
                { "17" , "MARS-Unserv" },
                { "18" , "MARS-SJoin" },
                { "19" , "MARS-SLeave" },
                { "20" , "MARS-Grouplist-Request" },
                { "21" , "MARS-Grouplist-Reply" },
                { "22" , "MARS-Redirect-Map" },
                { "23" , "MAPOS-UNARP" },
                { "24" , "OP_EXP1" },
                { "25" , "OP_EXP2" },
                { "65535" , "Reserved" }


            };



            if (i >= 26 && i <= 65534)
            {
                return "Unassigned";
            }
            else
            {
                var ret = lup[i.ToString()];
                return ret;
            }

        }

        ////////////////Practica 5
        String TypeOfService(String i)
        {
            var lup = new Dictionary<String, string>
            {
                { "1000","1000 : Minimizar retardo" },
                { "0100","0100 : Maximizar la densidad de flujo" },
                { "0010","0010 : Maximizar la fiabilidad" },
                { "0001","0001 : Minimizar el coste monetario" },
                { "0000","0000 : Servicio normal" }


            };

            var ret = "";

            try
            {
                ret = lup[i];
            }
            catch (Exception)
            {
                ret = i + ": valor no encontrado";
                throw;
            }


            return ret;
        }

        //////Practica 8
        int TCP(int res)
        {
            String tcp = ObtenBytesHex(res, res + 24, contenido, "");

            listViewInformacion.Items.Add("TCP: ");

            long po = Int64.Parse(tcp[0].ToString() + tcp[1].ToString(), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Puerto de origen: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(po.ToString());

            long pd = Int64.Parse(tcp[2].ToString() + tcp[3].ToString(), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Puerto de destino: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(pd.ToString());


            long ns = Int64.Parse((ObtenChars(4, 8, tcp)), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Numero de secuencia: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ns.ToString());

            long na = Int64.Parse((ObtenChars(8, 12, tcp)), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Numero de acuse de recibo: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(na.ToString());

            long longitud = Int64.Parse((ObtenChars(12, 13, tcp)), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Longitud de cabecera: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(longitud.ToString());

            String aux = HexStringToBinary(ObtenChars(13, 16, tcp));

            listViewInformacion.Items.Add("reservado: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[0].ToString() + aux[1].ToString() + aux[2].ToString());

            listViewInformacion.Items.Add("NS: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[3].ToString());

            listViewInformacion.Items.Add("CWR: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[4].ToString());

            listViewInformacion.Items.Add("ECE: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[5].ToString());

            listViewInformacion.Items.Add("Urgent: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[6].ToString());

            listViewInformacion.Items.Add("Acknowledgement: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[7].ToString());

            listViewInformacion.Items.Add("Push: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[8].ToString());

            listViewInformacion.Items.Add("Reset: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[9].ToString());

            listViewInformacion.Items.Add("Synchronize: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[10].ToString());

            listViewInformacion.Items.Add("Finish: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(aux[11].ToString());

            long tv = Int64.Parse((ObtenChars(16, 18, tcp)), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Tamaño de ventana: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(tv.ToString());

            long ck = Int64.Parse((ObtenChars(18, 20, tcp)), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Checksum: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(ck.ToString());

            long pu = Int64.Parse((ObtenChars(20, 22, tcp)), System.Globalization.NumberStyles.HexNumber);
            listViewInformacion.Items.Add("Puntero urgente: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(pu.ToString());

            String opc = ObtenChars(22, 22 + (int)longitud, tcp);
            listViewInformacion.Items.Add("Opciones: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(opc.ToString());

            return res + 22;
        }
        ///////////////

        ////// Practica 9
        int UDP(int res)
        {
            String udp = ObtenBytesHex(res, res + 8, contenido, "");
            listViewInformacion.Items.Add("UDP: ");

            long po;
            long.TryParse(ObtenChars(0, 4, udp), System.Globalization.NumberStyles.HexNumber, null,out po);
            
            listViewInformacion.Items.Add("Puerto de origen: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(po.ToString());

            long pd;
            long.TryParse(ObtenChars(4, 8, udp), System.Globalization.NumberStyles.HexNumber, null, out pd);
            listViewInformacion.Items.Add("Puerto de destino: ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(pd.ToString());

            long longitud; 
            long.TryParse(ObtenChars(8, 12, udp), System.Globalization.NumberStyles.HexNumber, null, out longitud);
            listViewInformacion.Items.Add("Longitud total (UDP): ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(longitud.ToString());

            String check = ObtenChars(12, 16, udp);
            listViewInformacion.Items.Add("Checksum (UDP): ");
            listViewInformacion.Items[listViewInformacion.Items.Count - 1].SubItems.Add(check);

            return res+8;
        }
        /////////////////
    }
}