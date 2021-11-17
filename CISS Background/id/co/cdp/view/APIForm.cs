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

namespace CISS_Background.id.co.cdp.view
{
    public partial class APIConfigForm : Form
    {
        private APIConfigVo config;

        public APIConfigForm()
        {
            InitializeComponent();
            this.config = ConfigurationUtil.getConfigFromFile<APIConfigVo>(AppConstant.API_CONFIG);
            if (this.config.frontend_api_basic != null)
            {
                txt_doss.Text = this.config.doss_api_basic;
                txt_frontend.Text = this.config.frontend_api_basic;
                txt_ekiosk_in.Text = this.config.ekiosk_in;
                txt_ekiosk_out.Text = this.config.ekiosk_out;
                txt_doss_put_response.Text = this.config.put_response;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            config.doss_api_basic = txt_doss.Text;
            config.frontend_api_basic = txt_frontend.Text;
            config.ekiosk_in = txt_ekiosk_in.Text;
            config.ekiosk_out = txt_ekiosk_out.Text;
            config.put_response = txt_doss_put_response.Text;
            ConfigurationUtil.saveConfig(config, AppConstant.API_CONFIG);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
