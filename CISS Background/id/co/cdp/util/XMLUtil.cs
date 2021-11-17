using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace CISS_Background.id.co.cdp.util
{
    public static class XMLUtil
    {
        public static X getEventObj<X>(string xmlValues)
        {
            XmlSerializer serial = new XmlSerializer(typeof(X));
            using (StringReader sr = new StringReader(xmlValues))
            {
                return (X)serial.Deserialize(sr);
            }
        }
    }
}
