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
    public partial class SocketConfigForm : Form
    {
        private SocketConfigVo socketConfigVo;

        public SocketConfigForm()
        {
            InitializeComponent();
            this.socketConfigVo = ConfigurationUtil.getConfigFromFile<SocketConfigVo>(AppConstant.SOCKET_CONFIG);
            if (this.socketConfigVo.ip_address != null) 
            {
                txt_ip_address.Text = this.socketConfigVo.ip_address;
                txt_port.Text = this.socketConfigVo.port;
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            socketConfigVo.ip_address = txt_ip_address.Text;
            socketConfigVo.port = txt_port.Text;
            ConfigurationUtil.saveConfig(socketConfigVo, AppConstant.SOCKET_CONFIG);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
