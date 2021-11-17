using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CISS_Background.id.co.cdp.view;
using CISS_Background.id.co.cdp.util;
using CISS.id.co.cdp.view;

namespace CISS_Background
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isTrial = true;
            
            runApp(isTrial);
        }

        private static void runApp(bool isTrial)
        {            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool running = true;

            MainDashboard main = new MainDashboard();
            if (isTrial)
            {
                main.Text += " [Trial Version]";
                running = AppUtil.checkExpirationApp();
            }
            else 
            {
                main.Text += " [Registered Version]";
            }
            running = true;
            if (running)
            {
                main.frm_securos_db_config = new SecurosDbForm();
                main.frm_io_config = new IOConfigForm();
                main.frm_p1_monitoring_db_config = new CissDatabaseForm();
                main.frm_socket_config = new SocketConfigForm();
                main.frm_api_config = new APIConfigForm();
                main.frm_ekiosk_print_job = new EkioskApiConfigForm();
                Application.Run(main);
            }
            else 
            {
                MessageBox.Show("This Program Has Expired");
            }          
        }
    }
}
