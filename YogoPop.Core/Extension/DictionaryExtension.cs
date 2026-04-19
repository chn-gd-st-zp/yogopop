namespace YogoPop.Core.Extension;

public static class DictionaryExtension
{
    public static bool ContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey destKey, bool ignoreCase = false)
    {
        if (!ignoreCase) return dic.ContainsKey(destKey);

        return dic.Keys.Any(o => o.ToString().ToLower() == destKey.ToString().ToLower());
    }

    public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey destKey, bool ignoreCase = false)
    {
        if (!ignoreCase) return dic.ContainsKey(destKey) ? dic[destKey] : default;

        var result = dic.Where(o => o.Key.ToString().ToLower() == destKey.ToString().ToLower()).Select(o => o.Value).SingleOrDefault();
        if (result != null && result.ToString().IsNotEmptyString()) return result;

        return default;
    }

    public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey destKey, TValue defuaultValue, bool ignoreCase = false)
    {
        if (!ignoreCase) return dic.ContainsKey(destKey) ? dic[destKey] : default;

        var result = dic.Where(o => o.Key.ToString().ToLower() == destKey.ToString().ToLower()).Select(o => o.Value).SingleOrDefault();
        if (result != null && result.ToString().IsNotEmptyString()) return result;

        return defuaultValue;
    }
}