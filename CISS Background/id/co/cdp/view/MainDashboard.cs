using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.bo;
using CISS_Background.id.co.cdp.bo.impl;
using System.Net.Sockets;
using System.Net;
using CISS_Background.id.co.cdp.engine;
using CISS_Background.id.co.cdp.engine.impl;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.view;
using CISS_Background.id.co.cdp.vo;
using CISS.id.co.cdp.vo;
using CISS_Background.id.co.cdp.constant;
using CISS.id.co.cdp.view;

namespace CISS_Background
{
    public partial class MainDashboard : Form
    {
        private ILinkage trans;
        private DummyDecisionConfigVo dummyConfig;
        public SecurosDbForm frm_securos_db_config;
        public IOConfigForm frm_io_config;
        public CissDatabaseForm frm_p1_monitoring_db_config;
        public SocketConfigForm frm_socket_config;
        public APIConfigForm frm_api_config;
        public EkioskApiConfigForm frm_ekiosk_print_job;
        public MonitoringFieldVo visual;

        public MainDashboard()
        {
            InitializeComponent();
            visual = new MonitoringFieldVo();
            visual.tabel = tbl_transaction_monitoring;
            visual.server_status = label_status;
            visual.p1_connection_status = label_p1_status;
            visual.ciss_db_status = label_ciss_db;
            visual.securos_db_status = label_securos_db;
            visual.ciss_db_group = group_ciss_db;
            visual.securos_db_group = group_securos_db;
            visual.txt_csv = txt_csv;
            visual.dummy_decision = cb_dummy_decision;

            this.trans = new Transaction(visual);
            TableViewUtil.initMonitoringTable(tbl_transaction_monitoring);
            tbl_transaction_monitoring.RowHeadersVisible = false;

            this.dummyConfig = ConfigurationUtil.getConfigFromFile<DummyDecisionConfigVo>(AppConstant.DUMMY_CONFIG);
            if (this.dummyConfig.dummyDecision != null && this.dummyConfig.dummyDecision.Equals("1"))
                cb_dummy_decision.Checked = true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.trans.start();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            AppConstant.CURRENT_REFRESH_TIME = DateTime.Now;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            this.trans.stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void menu_io_configuration_Click(object sender, EventArgs e)
        {
            frm_io_config.Show();
        }

        private void menu_securos_db_Click(object sender, EventArgs e)
        {
            frm_securos_db_config.Show();
        }

        private void menu_socket_configuration_Click(object sender, EventArgs e)
        {
            frm_socket_config.Show();
        }

        private void menu_api_configuration_Click(object sender, EventArgs e)
        {
            
        }

        private void menu_p1_monitoring_db_Click(object sender, EventArgs e)
        {
            frm_p1_monitoring_db_config.Show();
        }

        private void notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notify.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void MainDashboard_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) 
            {
                this.Hide();
                notify.Visible = true;
                notify.ShowBalloonTip(1000);
            }
            else if (WindowState == FormWindowState.Normal) 
            {
                notify.Visible = false;
            }
        }

        private void cb_dummy_decision_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_dummy_decision.Checked) 
                dummyConfig.dummyDecision = "1";
            else
                dummyConfig.dummyDecision = "0";
            ConfigurationUtil.saveConfig(dummyConfig, AppConstant.DUMMY_CONFIG);
        }

        private void transactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_api_config.Show();
        }

        private void eKioskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_ekiosk_print_job.Show();
        }
    }
}
