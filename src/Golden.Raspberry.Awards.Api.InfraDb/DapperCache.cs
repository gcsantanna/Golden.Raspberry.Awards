namespace Golden.Raspberry.Awards.Api.InfraDb
{
    public class DapperCache<TKey, TValue>
    {
        private readonly Dictionary<TKey, TValue> _values = new();

        public TValue Get(TValue @object, Func<TValue, TKey> keySelector)
        {
            var key = keySelector.Invoke(@object);

            if (!_values.TryGetValue(key, out var value))
            {
                value = @object;
                _values.Add(key, @object);
            }

            return value;
        }

        public IEnumerable<TValue> GetValues() => _values.Values;
    }

    public class DapperCache
    {
        private readonly Dictionary<Type, DapperCache<long, object>> _types = new();

        public TValue Get<TValue>(TValue @object, Func<TValue, long> keySelector)
        {
            var type = typeof(TValue);
            if (!_types.TryGetValue(type, out var dic))
            {
                dic = new DapperCache<long, object>();
                _types[type] = dic;
            }

            return (TValue)dic.Get(@object, x => keySelector.Invoke((TValue)x));
        }
    }
}
