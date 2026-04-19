namespace YogoPop.Core.JsonExtend;

public static class JsonExtension
{
    public static string ToJson(this object obj)
    {
        var serializerSettings = InjectionContext.Resolve<JsonSerializerSettings>();

        return obj.ToJson(serializerSettings);
    }

    public static string ToJson(this object obj, Newtonsoft.Json.Formatting formatting)
    {
        var serializerSettings = InjectionContext.Resolve<JsonSerializerSettings>();

        return obj.ToJson(serializerSettings, formatting);
    }

    public static string ToJson(this object obj, JsonSerializerSettings serializerSettings)
    {
        return serializerSettings == null ? JsonConvert.SerializeObject(obj) : JsonConvert.SerializeObject(obj, serializerSettings);
    }

    public static string ToJson(this object obj, JsonSerializerSettings serializerSettings, Newtonsoft.Json.Formatting formatting)
    {
        return serializerSettings == null ? JsonConvert.SerializeObject(obj, formatting) : JsonConvert.SerializeObject(obj, formatting, serializerSettings);
    }

    public static T ToObject<T>(this string jsonStr)
    {
        var serializerSettings = InjectionContext.Resolve<JsonSerializerSettings>();

        return jsonStr.ToObject<T>(serializerSettings);
    }

    public static T ToObject<T>(this string jsonStr, JsonSerializerSettings serializerSettings)
    {
        if (jsonStr.IsEmptyString())
            return default;

        if (typeof(T) == typeof(string))
            return (T)Convert.ChangeType(jsonStr, typeof(T));

        return serializerSettings == null ? JsonConvert.DeserializeObject<T>(jsonStr) : JsonConvert.DeserializeObject<T>(jsonStr, serializerSettings);
    }

    public static object ToObject(this string jsonStr, Type type)
    {
        var serializerSettings = InjectionContext.Resolve<JsonSerializerSettings>();

        return jsonStr.ToObject(type, serializerSettings);
    }

    public static object ToObject(this string jsonStr, Type type, JsonSerializerSettings serializerSettings)
    {
        return serializerSettings == null ? JsonConvert.DeserializeObject(jsonStr, type) : JsonConvert.DeserializeObject(jsonStr, type, serializerSettings);
    }

    public static JObject ToJObject(this string jsonStr)
    {
        return jsonStr == null ? JObject.Parse("{}") : JObject.Parse(jsonStr.Replace("&nbsp;", string.Empty));
    }

    public static string[] ToStringArray(this object dataArray)
    {
        string dataStr = dataArray.ToJson();
        JArray jArray = JArray.Parse(dataStr);

        List<string> dataList = new List<string>();
        foreach (var jArrayItem in jArray)
            dataList.Add(jArrayItem.Value<string>());

        return dataList.ToArray();
    }

    public static JsonResult ToJsonResult(this object obj)
    {
        return new JsonResult(obj);
    }

    public static JsonResult ToJsonResult(this object obj, JsonSerializerSettings jsonSetting)
    {
        return new JsonResult(obj, jsonSetting);
    }

    public static string FormatPropertyName(this string propertyName)
    {
        string result = propertyName;

        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add(result, string.Empty);

        var json = dic.ToJson();

        dic = json.ToObject<Dictionary<string, string>>();

        foreach (var kv in dic)
        {
            result = kv.Key;
            break;
        }

        return result;
    }
}