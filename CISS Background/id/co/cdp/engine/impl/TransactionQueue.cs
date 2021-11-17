using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using CISS_Background.id.co.cdp.bo;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.util;

namespace CISS_Background.id.co.cdp.engine.impl
{
    class TransactionQueue : ITransactionQueue
    {
        private int gateIndex;
        private List<IEventProcessor> queue;
        private bool workingStatus;
        private DataGridView visual;

        public TransactionQueue() 
        {
            this.queue = new List<IEventProcessor>();
            this.workingStatus = false;
        }

        public TransactionQueue(DataGridView visual)
        {
            // TODO: Complete member initialization
            this.visual = visual;
            this.queue = new List<IEventProcessor>();
            this.workingStatus = false;
        }

        public void setGateIndex(int gateIndex)
        {
            this.gateIndex = gateIndex;
        }

        public void startWorking()
        {
            Console.WriteLine("starting Queue Gate " +  gateIndex.ToString());           
            this.workingStatus = true;
            Thread workerThread = new Thread(new ThreadStart(startActivity));
            workerThread.Start();            
        }

        public void startActivity()
        {
            TableViewUtil.fillActivationColor(visual, gateIndex);
            startActivity(1);
        }

        public void startActivity(int idx)
        {
            if (this.workingStatus)
            {
                if (this.queue.Count > 0)
                {
                    IEventProcessor trans = this.queue[0];
                    trans.process();
                    this.queue.Remove(trans);                    
                }
                Thread.Sleep(500);
                startActivity(1);
            }
            else
            {
                TableViewUtil.fillDeactivationColor(visual, gateIndex);
            }
        }

        public void stopWorking()
        {
            this.workingStatus = false;
        }

        public int getGateIndex()
        {
            return this.gateIndex;
        }

        public void addQueue(IEventProcessor transaction)
        {
            this.queue.Add(transaction);
        }        
    }
}
