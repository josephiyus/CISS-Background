using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading;
using CISS_Background.id.co.cdp.vo;

namespace CISS.id.co.cdp.util
{
    public static class SocketUtil
    {
        public static void sendXMLToOtherServer(string ipAddress, int port, Event gateEvent, MonitoringFieldVo visual) 
        {
            try
            {
                TextViewUtil.appendText(visual.txt_csv, "---SEND TO DRYPORT FOR LICENSE PLATE : " + gateEvent.STAFFNAME);
                TcpClient clientSocket = new TcpClient();
                clientSocket.Connect(ipAddress, port);
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(gateEvent.xml);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                TextViewUtil.appendText(visual.txt_csv, "--- SUCCESS SEND TO DRYPORT FOR LICENSE PLATE : " + gateEvent.STAFFNAME);
            }
            catch(Exception e)
            {
                TextViewUtil.appendText(visual.txt_csv, "--- FAILED SEND TO DRYPORT CAUSE : " + e.ToString());
            }
        }
    }
}
