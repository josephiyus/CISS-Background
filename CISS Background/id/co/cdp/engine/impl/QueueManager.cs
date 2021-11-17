using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.bo;
using CISS_Background.id.co.cdp.bo.impl;
using System.Windows.Forms;
using System.Threading;
using CISS_Background.id.co.cdp.common.persistence;
using CISS_Background.id.co.cdp.common.persistence.abstracts;
using CISS.id.co.cdp.util;

namespace CISS_Background.id.co.cdp.engine.impl
{
    public class QueueManager : IQueueManager
    {
        //private List<ITransactionQueue> queue;
        private List<object> thread_lockers;
        private MonitoringFieldVo visual;
        private ICommonDatabase cissDb;
        private ICommonDatabase securosDb;

        public QueueManager(MonitoringFieldVo visual)
        {
            // TODO: Complete member initialization
            this.visual = visual;
            this.thread_lockers = new List<object>();
            this.cissDb = MySqlCommon.getInstance();
            this.securosDb = PostgreSqlCommon.getInstance();
        }
        
        public void registerTransaction(Event gateEvent)
        {
            TextViewUtil.appendText(visual.txt_csv, "--- PREPARING PROCESS");
            gateEvent.gateID = TransactionUtil.getGateInfo(gateEvent).gateIndex;
            IEventProcessor transaction = new EventProcessorBo(visual, cissDb, securosDb);
            transaction.setLockerLine(this.thread_lockers[gateEvent.gateID - 1]);
            transaction.setProcessedEvent(gateEvent);
            transaction.process();
            TextViewUtil.endAppendText(visual.txt_csv, "--- ENDING PROCESS");
            //new Thread(transaction.process).Start();

            /*
             queue.ForEach(delegate(ITransactionQueue gateQueue)
            {
                if (gateQueue.getGateIndex() == gateEvent.gateID) 
                {
                    //object EventProcessorDummy : untuk testing alur
                    //EventProcessor : untuk running real bussiness process
                    IEventProcessor transaction = new EventProcessorBo(visual.tabel);
                    //IEventProcessor transaction = new EventProcessorBoDummy(visual.tabel); //new EventProcessorBo(visual.tabel);
                    transaction.setProcessedEvent(gateEvent);
                    gateQueue.addQueue(transaction);
                }
            }); 
             */
        }


        public void stopAllWorking()
        {
            /*
             queue.ForEach(delegate(ITransactionQueue item) {
                item.stopWorking();
            });
             */

            LabelViewUtil.setOfflineMode(visual.ciss_db_status, "NO");
            LabelViewUtil.setOfflineMode(visual.securos_db_status, "NO");

            visual.ciss_db_group.Text = string.Format("CISS DB [{0}]", "");
            visual.securos_db_group.Text = string.Format("SECUROS DB [{0}]", "");

            foreach (var item in thread_lockers)
            {
                TableViewUtil.fillDeactivationColor(visual.tabel, thread_lockers.IndexOf(item) + 1);
            }

            LabelViewUtil.setOfflineMode(visual.server_status, "OFFLINE");
        }


        public void startAllWorking()
        {
            /*
             this.queue = new List<ITransactionQueue>();
            for (int i = 0; i < 5; i++)
            {
                ITransactionQueue gateQueue = new TransactionQueue(visual.tabel);
                gateQueue.setGateIndex(i + 1);
                gateQueue.startWorking();
                this.queue.Add(gateQueue);
            }
             */
            visual.ciss_db_group.Text = string.Format("CISS DB [{0}]", cissDb.getServer());
            visual.securos_db_group.Text = string.Format("SECUROS DB [{0}]", securosDb.getServer());

            if (cissDb.isEnableConnection())
                LabelViewUtil.setOnlineMode(visual.ciss_db_status, "YES");
            else
                LabelViewUtil.setOfflineMode(visual.ciss_db_status, "NO");

            if (securosDb.isEnableConnection())
                LabelViewUtil.setOnlineMode(visual.securos_db_status, "YES");
            else
                LabelViewUtil.setOnlineMode(visual.securos_db_status, "NO");

            for (int i = 0; i < 5; i++)
            {
                TableViewUtil.fillActivationColor(visual.tabel, (i + 1));
                if (thread_lockers.Count <= 5)
                    thread_lockers.Add(new object());                    
            }    

            LabelViewUtil.setOnlineMode(visual.server_status, "ONLINE");
        }
    }
}
