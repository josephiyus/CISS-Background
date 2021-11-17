using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.util;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.constant;
using CISS.id.co.cdp.util;
using System.IO;

namespace CISS_Background.id.co.cdp.engine.impl
{
    public class Transaction : ILinkage
    {
        private Socket serverSocket;
        private Socket clientSocket;
        private const int BUFFER_SIZE = 4096;
        private byte[] buffer = new byte[BUFFER_SIZE];
        private Socket currentCalledClient = null;
        private SocketConfigVo config;
        private IQueueManager queueManager;
        private MonitoringFieldVo visual;

        public Transaction(MonitoringFieldVo visual)
        {
            // TODO: Complete member initialization
            this.visual = visual;
        }

        public void start()
        {
            if (serverSocket == null)
            {
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                config = ConfigurationUtil.getConfigFromFile<SocketConfigVo>(AppConstant.SOCKET_CONFIG);
                serverSocket.Bind(new IPEndPoint(IPAddress.Parse(config.ip_address), int.Parse(config.port)));
                serverSocket.Listen(5);
            } 
            
            //the maximum pending client, define as you wish
            serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
            this.queueManager = new QueueManager(visual);
            this.queueManager.startAllWorking();
        }

        public void stop()
        {
            if (clientSocket!=null) clientSocket.Close();
            queueManager.stopAllWorking();
        }

        private void acceptCallback(IAsyncResult result)
        {
            try
            {
                clientSocket = serverSocket.EndAccept(result);
                Console.WriteLine("a client join");
                LabelViewUtil.setOnlineMode(visual.p1_connection_status, "CONNECTED");
                clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), clientSocket);
                serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
            }
            catch (Exception e)
            {
                
            }
        }

        private void receiveCallback(IAsyncResult result) 
        {            
            try
            {
                TextViewUtil.appendText(visual.txt_csv, "START RECEIVE DATA");
                currentCalledClient = (Socket)result.AsyncState;
                if (currentCalledClient.Connected)
                {
                    int received = currentCalledClient.EndReceive(result);
                    if (received > 0)
                    {
                        byte[] data = new byte[received];
                        Buffer.BlockCopy(buffer, 0, data, 0, data.Length); 
                        string xmlMessage = Encoding.UTF8.GetString(data);
                        
                        //double rangeTimeRunning = TimeSpan.FromMilliseconds((DateTime.Now - AppConstant.CURRENT_REFRESH_TIME).TotalMilliseconds).TotalMinutes;
                        if (xmlMessage.ToLower().Equals("ciss background restart!"))
                        {
                            TextViewUtil.appendText(visual.txt_csv, "Restarted Socket Connection");
                            currentCalledClient.Shutdown(SocketShutdown.Receive);
                            serverSocket.Close();
                            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                            serverSocket.Bind(new IPEndPoint(IPAddress.Parse(config.ip_address), int.Parse(config.port)));
                            serverSocket.Listen(5); //the maximum pending client, define as you wish
                            serverSocket.BeginAccept(new AsyncCallback(acceptCallback), null);
                        }
                        else if (xmlMessage.ToLower().Equals("save log!"))
                        {
                            if (visual.txt_csv.Text.Length > 0)
                            {
                                string myfile = string.Format("{0}_log.txt", DateTime.Now.ToString("yyyy-MM-dd"));

                                using (StreamWriter sw = File.AppendText(myfile))
                                {
                                    sw.WriteLine(visual.txt_csv.Text);
                                }

                                visual.txt_csv.Text = "";
                            } 
                        }
                        else
                        {
                            Event gateEvent = XMLUtil.getEventObj<Event>(xmlMessage);
                            if (gateEvent.STAFFNAME != null
                                && (gateEvent.TRCODE != null && gateEvent.TRCODE.ToUpper() != "CA")
                                && (gateEvent.TRCODE != null && gateEvent.TRCODE.ToUpper() != "CB"))
                            {
                                TextViewUtil.appendText(visual.txt_csv, "ENABLE PROCESSING INVALID TRANSACTION");
                                gateEvent.xml = xmlMessage;
                                TextViewUtil.appendText(visual.txt_csv, xmlMessage);
                                //Console.WriteLine(xmlMessage);
                                this.queueManager.registerTransaction(gateEvent);
                                //if (!visual.dummy_decision.Checked)
                                //    SocketUtil.sendXMLToOtherServer("10.0.3.116", 3002, gateEvent, visual);
                            }
                            else
                            {
                                TextViewUtil.endAppendText(visual.txt_csv, "CAN'T PROCESS VALID TRANSACTION FOR LICENSE PLATE " + (gateEvent.STAFFNAME ?? "") + ", TR CODE = " + (gateEvent.TRCODE ?? ""));
                            }
                            TextViewUtil.appendText(visual.txt_csv, "LISTENING FOR THE NEXT BATCH");
                            currentCalledClient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(receiveCallback), currentCalledClient); //repeat beginReceive
                        }
                    }
                }
            }
            catch (Exception e) 
            {
                TextViewUtil.endAppendText(visual.txt_csv, "ERROR UNHANDLED TRANSACTION = " + e.ToString());
            }
        }
    }    
}
