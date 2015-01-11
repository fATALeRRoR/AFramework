using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace AFramework.Helpers
{
    public static class SerializationHelper
    {
        public static string Serialize<T>(T item)
        {
            var memStream = new MemoryStream();
            using (var textWriter = new XmlTextWriter(memStream, Encoding.Unicode))
            {
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(textWriter, item);

                memStream = textWriter.BaseStream as MemoryStream;
            }
            if (memStream != null)
                return Encoding.Unicode.GetString(memStream.ToArray());
            
            return null;
        }

        public static T Deserialize<T>(string xmlString)
        {
            if (string.IsNullOrWhiteSpace(xmlString))
                return default(T);

            using (var memStream = new MemoryStream(Encoding.Unicode.GetBytes(xmlString)))
            {
                memStream.Position = 0;
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(memStream);
            }
        }
    }
}
