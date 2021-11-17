using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.util;
using CISS_Background.id.co.cdp.constant;
using CISS_Background.id.co.cdp.common.persistence;
using CISS_Background.id.co.cdp.common.persistence.abstracts;

namespace CISS_Background.id.co.cdp.view
{
    public partial class CissDatabaseForm : Form
    {
        private P1MonitoringDbVo config;

        public CissDatabaseForm()
        {
            InitializeComponent();
            this.config = ConfigurationUtil.getConfigFromFile<P1MonitoringDbVo>(AppConstant.P1_MONITORING_DB_CONFIG);
            if (this.config.data_source != null)
            {
                txt_data_source.Text = this.config.data_source;
                txt_schema.Text = this.config.schema;
                txt_port.Text = this.config.port;
                txt_username.Text = this.config.username;
                txt_password.Text = this.config.password;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            config.data_source = txt_data_source.Text;
            config.schema = txt_schema.Text;
            config.port = txt_port.Text;
            config.username = txt_username.Text;
            config.password = txt_password.Text;
            ConfigurationUtil.saveConfig(config, AppConstant.P1_MONITORING_DB_CONFIG);

            ICommonDatabase db = MySqlCommon.getInstance();
            if (db.isEnableConnection())
            {
                txt_status.Text = "Enable";
            }
            else
            {
                txt_status.Text = "Disable";
            } 
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
