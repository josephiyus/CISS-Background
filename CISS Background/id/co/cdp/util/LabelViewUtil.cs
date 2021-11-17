using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace CISS_Background.id.co.cdp.util
{
    public class LabelViewUtil
    {
        public static void setStartingMode(Label label, string value) 
        {
            label.Invoke((MethodInvoker)delegate()
            {
                label.ForeColor = Color.Black;
                label.Text = value;
            });           
        }

        public static void setOfflineMode(Label label, string value)
        {
            label.Invoke((MethodInvoker)delegate()
            {
                label.ForeColor = Color.Red;
                label.Text = value;
            });
        }

        public static void setOnlineMode(Label label, string value)
        {
            label.Invoke((MethodInvoker)delegate()
            {
                label.ForeColor = Color.Green;
                label.Text = value;
            });
        }
    }
}
