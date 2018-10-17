using Newtonsoft.Json;

namespace BreakingNews.Crosscutting.Util.Serializers
{
    public static class JsonSerializer
    {
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
