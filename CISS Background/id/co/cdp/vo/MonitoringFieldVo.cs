using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CISS_Background.id.co.cdp.vo
{
    public class MonitoringFieldVo
    {
        public DataGridView tabel { get; set; }
        public Label server_status { get; set; }
        public Label p1_connection_status { get; set; }
        public Label ciss_db_status { get; set; }
        public Label securos_db_status { get; set; }
        public TextBox txt_csv { get; set; }
        public GroupBox ciss_db_group { get; set; }
        public GroupBox securos_db_group { get; set; }
        public CheckBox dummy_decision { get; set; }
    }
}
