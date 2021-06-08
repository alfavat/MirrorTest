using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ServerService.Helper
{
    public class XmlHelper
    {
        public static T GetModelFromXml<T>(string link)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(link);
            using (TextReader reader = new StringReader(xmlDoc.InnerXml))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
