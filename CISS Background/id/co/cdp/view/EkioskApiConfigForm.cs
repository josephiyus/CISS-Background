using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.util;
using CISS.id.co.cdp.vo;
using CISS_Background.id.co.cdp.constant;

namespace CISS.id.co.cdp.view
{
    public partial class EkioskApiConfigForm : Form
    {
        private EkioskJobPrintVo config;

        public EkioskApiConfigForm()
        {
            InitializeComponent();
            this.config = ConfigurationUtil.getConfigFromFile<EkioskJobPrintVo>(AppConstant.EKIOSK_JOB_PRINT);
            if (this.config.gate1 != null)
            {
                txt_gate1.Text = this.config.gate1;
                txt_gate2.Text = this.config.gate2;
                txt_gate3.Text = this.config.gate3;
                txt_gate4.Text = this.config.gate4;
                txt_gate5_in.Text = this.config.gate5In;
                txt_gate5_out.Text = this.config.gate5Out;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            config.gate1 = txt_gate1.Text;
            config.gate2 = txt_gate2.Text;
            config.gate3 = txt_gate3.Text;
            config.gate4 = txt_gate4.Text;
            config.gate5In = txt_gate5_in.Text;
            config.gate5Out = txt_gate5_out.Text;
            ConfigurationUtil.saveConfig(config, AppConstant.EKIOSK_JOB_PRINT);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
