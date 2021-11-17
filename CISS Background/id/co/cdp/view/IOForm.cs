using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.vo;
using CISS_Background.id.co.cdp.constant;
using CISS_Background.id.co.cdp.util;

namespace CISS_Background.id.co.cdp.view
{
    public partial class IOConfigForm : Form
    {
        private IOConfigVo config;

        public IOConfigForm()
        {
            InitializeComponent();
            this.config = ConfigurationUtil.getConfigFromFile<IOConfigVo>(AppConstant.P1_FILE_CONFIG);
            if (this.config.txtPath != null)
            {
                txt_doss.Text = this.config.txtPath;
            }
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            config.txtPath = txt_doss.Text;
            config.csvPath = txt_csv.Text;
            ConfigurationUtil.saveConfig(config, AppConstant.P1_FILE_CONFIG);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
