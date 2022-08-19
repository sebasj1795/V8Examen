using System.IO;
using System.Xml.Serialization;

namespace Security.Transversal.Common.Helpers
{
    public static class SerializeHelper
    {
        public static string Serialize<T>(T objectToSerialize)
        {
            var xmlSerializer = new XmlSerializer(objectToSerialize.GetType());

            using var textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, objectToSerialize);
            return textWriter.ToString();
        }

        public static T Deserialize<T>(string input) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using var sr = new StringReader(input);
            return (T)xmlSerializer.Deserialize(sr);
        }
        
    }
}
