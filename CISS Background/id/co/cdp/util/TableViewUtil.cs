using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CISS.id.co.cdp.util;
using CISS_Background.id.co.cdp.constant;
using System.Threading;
using CISS.id.co.cdp.exception;

namespace CISS_Background.id.co.cdp.util
{
    public static class TableViewUtil
    {
        internal static void initMonitoringTable(DataGridView table)
        {
            for (int i = 0; i < 5; i++)
            {
                table.Rows.Add(new DataGridViewRow());
                table.Rows[i].Cells[0].Value = "GATE " + (i + 1).ToString();

                table.Rows[i].Cells[0].Style.BackColor = Color.Red;

                if (i == 0) 
                {
                    table.Rows[i].Cells[1].Value = "L1";
                    table.Rows[i].Cells[2].Value = "OUT";
                }
                else if (i == 1)
                {
                    table.Rows[i].Cells[1].Value = "L2";
                    table.Rows[i].Cells[2].Value = "OUT";
                }
                else if (i == 2)
                {
                    table.Rows[i].Cells[1].Value = "L2";
                    table.Rows[i].Cells[2].Value = "IN";
                }
                else if (i == 3)
                {
                    table.Rows[i].Cells[1].Value = "L1";
                    table.Rows[i].Cells[2].Value = "IN";
                }
                else
                {
                    table.Rows[i].Cells[1].Value = "L5";
                    table.Rows[i].Cells[2].Value = "IN/OUT";
                }
                table.Rows[i].Height = 50;
            }            
        }

        public static void setCellValue(DataGridView table, string line, string line_type, int col, object val) 
        {
            Monitor.Enter(ThreadLocker._VIEW_STATE_SYNCH_LINE);            
            bool retry = true;
            int amountRetry = 5;
            while (retry)
            {
                try
                {
                    table.Invoke((MethodInvoker)delegate()
                    {
                        DataGridViewRow row = getRowByLineAndLineType(table, line, line_type);
                        if (val.GetType() == typeof(Image))
                        {
                            DataGridViewImageCell imageCell = new DataGridViewImageCell();
                            imageCell.Value = val;
                            row.Cells[col] = imageCell;
                        }
                        else
                        {
                            DataGridViewTextBoxCell txt = new DataGridViewTextBoxCell();
                            txt.Value = val;
                            row.Cells[col] = txt;
                        }

                    });
                    retry = false;
                    if (amountRetry == 0) 
                    {
                        throw new FailedWriteViewException();
                    }
                }
                catch
                {
                    amountRetry -= 1;
                }
            }
            Monitor.Exit(ThreadLocker._VIEW_STATE_SYNCH_LINE);                      
        }

        private static DataGridViewRow getRowByLineAndLineType(DataGridView table, string line, string line_type)
        {
            DataGridViewRow rowResult = null;
            foreach (DataGridViewRow item in table.Rows)
            {
                if (item.Cells[1].Value == line && item.Cells[2].Value.ToString().Contains(line_type)) 
                {
                    rowResult = item;
                    break;
                }
            }
            return rowResult;
        }

        internal static void fillActivationColor(DataGridView table, int gateIndex)
        {
            Monitor.Enter(ThreadLocker._VIEW_STATE_SYNCH_LINE);
            lock (ThreadLocker._VIEW_STATE_SYNCH_LINE)
            {
                table.Invoke((MethodInvoker)delegate()
                {
                    table.Rows[gateIndex - 1].Cells[0].Style.BackColor = Color.Green;
                });
            }
            Monitor.Exit(ThreadLocker._VIEW_STATE_SYNCH_LINE);      
        }

        internal static void fillDeactivationColor(DataGridView table, int gateIndex)
        {
            Monitor.Enter(ThreadLocker._VIEW_STATE_SYNCH_LINE);
            table.Invoke((MethodInvoker)delegate()
            {
                table.Rows[gateIndex - 1].Cells[0].Style.BackColor = Color.Red;
            });
            Monitor.Exit(ThreadLocker._VIEW_STATE_SYNCH_LINE);            
        }

        internal static void fillDefaultnColor(DataGridView tbl, int gateIndex)
        {
            Monitor.Enter(ThreadLocker._VIEW_STATE_SYNCH_LINE);
            tbl.Invoke((MethodInvoker)delegate()
            {
                tbl.Rows[gateIndex - 1].Cells[0].Style.BackColor = Color.White;
            });
            Monitor.Exit(ThreadLocker._VIEW_STATE_SYNCH_LINE);
        }
    }
}
