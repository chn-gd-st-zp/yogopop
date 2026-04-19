namespace YogoPop.Core.Tool;

public class FixedSortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
{
    private List<TKey> _keys;
    private Dictionary<TKey, TValue> _dictionary;

    public FixedSortedDictionary()
    {
        _keys = new List<TKey>();
        _dictionary = new Dictionary<TKey, TValue>();
    }

    public void Add(TKey key, TValue value)
    {
        if (!_dictionary.ContainsKey(key))
        {
            _keys.Add(key);
            _dictionary.Add(key, value);
        }
    }

    public bool Remove(TKey key)
    {
        if (_dictionary.ContainsKey(key))
        {
            _keys.Remove(key);
            _dictionary.Remove(key);
            return true;
        }
        return false;
    }

    public TValue GetValue(TKey key)
    {
        if (_dictionary.ContainsKey(key))
        {
            return _dictionary[key];
        }
        return default(TValue);
    }


    public int Count => _keys.Count;

    public bool IsReadOnly => false;

    public TValue this[TKey key] { get { return GetValue(key); } set { Add(key, value); } }

    public IEnumerable<TKey> Keys => _keys;

    ICollection<TKey> IDictionary<TKey, TValue>.Keys => _keys;

    public ICollection<TValue> Values
    {
        get
        {
            var result = new List<TValue>();
            foreach (var key in _keys)
                result.Add(GetValue(key));
            return result;
        }
    }

    public bool Contains(KeyValuePair<TKey, TValue> item) => _dictionary.Contains(item);

    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => _dictionary.TryGetValue(key, out value);

    public void Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);

    public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

    public void Clear() { _keys.Clear(); _dictionary.Clear(); }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
}