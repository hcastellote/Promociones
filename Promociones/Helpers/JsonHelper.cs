using Newtonsoft.Json;
using System.IO;

namespace Helpers
{
    public class JsonHelper
    {
        public static T GetDataFromFile<T>(string pFile)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(pFile));
        }
        public static string SerializeObject<T>(T pObject)
        {
            return JsonConvert.SerializeObject(pObject);
        }
    }
}
