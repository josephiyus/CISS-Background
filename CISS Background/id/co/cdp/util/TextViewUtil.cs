using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CISS.id.co.cdp.util
{
    class TextViewUtil
    {
        public static void appendText(TextBox text, string value)
        {
            text.Invoke((MethodInvoker)delegate()
            {
                text.Text += DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + value + System.Environment.NewLine;
            });
        }

        public static void endAppendText(TextBox text, string value)
        {
            text.Invoke((MethodInvoker)delegate()
            {
                text.Text += DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + " : " + value + System.Environment.NewLine + System.Environment.NewLine;
            });
        }
    }
}
