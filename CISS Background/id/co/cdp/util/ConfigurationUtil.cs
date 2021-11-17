using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CISS_Background.id.co.cdp.util
{
    public class ConfigurationUtil
    {
        private static string basic_path = Application.ExecutablePath.Replace(Application.ExecutablePath.Split('\\').Last(), "");

        public static X getConfigFromFile<X>(string fileName) 
        {
            X result = Activator.CreateInstance<X>();
            try
            {
                if (File.Exists(basic_path + fileName)) 
                {
                    using (var reader = File.OpenText(basic_path + fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string key = line.Split('=')[0];
                            string val = line.Split('=')[1];
                            AttributesUtil.setMemberValue<X>(result, key, val);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static void saveConfig<X>(X output_config, string fileName) 
        {
            if (File.Exists(basic_path + fileName))
            {
                File.Delete(basic_path + fileName);
            }
            using (StreamWriter file = new StreamWriter(@basic_path + fileName))
            {
                foreach (var key in AttributesUtil.getAllMembersName<X>())
                {
                    file.WriteLine(key + "=" + AttributesUtil.getMemberValue<X>(output_config, key));
                }                
            }
        }
    }
}
