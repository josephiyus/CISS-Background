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
    public partial class SecurosDbForm : Form
    {
        private SecurosDbConfigVo config;

        public SecurosDbForm()
        {
            InitializeComponent();
            this.config = ConfigurationUtil.getConfigFromFile<SecurosDbConfigVo>(AppConstant.SECUROS_DB_CONFIG);
            if (this.config.server_name != null)
            {
                txt_server_name.Text = this.config.server_name;
                txt_schema.Text = this.config.schema;
                txt_port.Text = this.config.port;
                txt_database_name.Text = this.config.database_name;
                txt_username.Text = this.config.username;
                txt_password.Text = this.config.password;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            config.server_name = txt_server_name.Text;
            config.schema = txt_schema.Text;
            config.database_name = txt_database_name.Text;
            config.port = txt_port.Text;
            config.username = txt_username.Text;
            config.password = txt_password.Text;

            ConfigurationUtil.saveConfig(config, AppConstant.SECUROS_DB_CONFIG);

            ICommonDatabase db = PostgreSqlCommon.getInstance();
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
