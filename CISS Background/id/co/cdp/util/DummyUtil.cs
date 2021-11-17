using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CISS.id.co.cdp.vo;
using System.IO;
using CISS_Background.id.co.cdp.util;

namespace CISS.id.co.cdp.util
{
    public static class DummyUtil
    {
        public static List<SecurosDummyVo> getCurrentSecurosDummy() 
        {
            List<SecurosDummyVo> result = new List<SecurosDummyVo>();
            using (var reader = File.OpenText("dummy/securos.csv"))
            {
                string line = null;
                string container_no = null;
                string truck_no = null;
                string gate = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cols = line.Split(',');
                    if (container_no == null)
                    {
                        container_no = cols[0];
                        truck_no = cols[1];
                        gate = cols[2];
                    }
                    else
                    {
                        result.Add(new SecurosDummyVo());
                        SecurosDummyVo vo = result.Last();
                        AttributesUtil.setMemberValue(vo, container_no, cols[0]);
                        AttributesUtil.setMemberValue(vo, truck_no, cols[1]);
                        AttributesUtil.setMemberValue(vo, gate, cols[2]);
                    }                    
                }
            }
            return result;
        }

        public static List<TruckScheduleVo> getCurrentTruckScheduleDummy()
        {
            List<TruckScheduleVo> result = new List<TruckScheduleVo>();
            using (var reader = File.OpenText("dummy/schedule.csv"))
            {
                string line = null;
                string truck_no = null;
                string container_no = null;
                string rfid = null;
                string gate = null;
                string weight = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cols = line.Split(',');
                    if (truck_no == null)
                    {
                        truck_no = cols[0];
                        container_no = cols[1];
                        rfid = cols[2];
                        gate = cols[3];
                        weight = cols[4];
                    }
                    else
                    {
                        result.Add(new TruckScheduleVo());
                        TruckScheduleVo vo = result.Last();
                        AttributesUtil.setMemberValue(vo, truck_no, cols[0]);
                        AttributesUtil.setMemberValue(vo, container_no, cols[1]);
                        AttributesUtil.setMemberValue(vo, rfid, cols[2]);
                        AttributesUtil.setMemberValue(vo, gate, int.Parse(cols[3]));
                        if(!cols[4].Equals(""))
                            AttributesUtil.setMemberValue(vo, weight, int.Parse(cols[4]));
                        else
                            AttributesUtil.setMemberValue(vo, weight, 0);
                    }

                }
            }
            return result;
        }

        public static List<WeightDummyVo> getWeightDummy()
        {
            List<WeightDummyVo> result = new List<WeightDummyVo>();
            using (var reader = File.OpenText("dummy/weight.csv"))
            {
                string line = null;
                string truck_no = null;
                string weight = null;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] cols = line.Split(',');
                    if (truck_no == null)
                    {
                        truck_no = cols[0];
                        weight = cols[1];
                    }
                    else
                    {
                        result.Add(new WeightDummyVo());
                        WeightDummyVo vo = result.Last();
                        AttributesUtil.setMemberValue(vo, truck_no, cols[0]);
                        AttributesUtil.setMemberValue(vo, weight, int.Parse(cols[1]));
                    }

                }
            }
            return result;
        }
    }
}
