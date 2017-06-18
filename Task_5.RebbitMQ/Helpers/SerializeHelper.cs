using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using Task_5.Models;

namespace Task_5.Helpers
{
    public static class SerializeHelper
    {
        public static void ToJson(this PerformanceMould mould, Stream stream)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(PerformanceMould));
            jsonSerializer.WriteObject(stream, mould);
        }

        public static void ToJson(this IDictionary<string, PerformanceMould> cache, Stream stream)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(IDictionary<string, PerformanceMould>));
            jsonSerializer.WriteObject(stream, cache);
        }

        public static byte[] ToBytes(this PerformanceMould mould)
        {
            using (var memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, mould);
                return memoryStream.ToArray();
            }
        }

        public static PerformanceMould ToPerformanceMould(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                var bf = new BinaryFormatter();
                return bf.Deserialize(memoryStream) as PerformanceMould;
            }
        }
    }
}
