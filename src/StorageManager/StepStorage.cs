namespace Stepwise.StorageManager;

public class StepStorage
{
    private static Dictionary<string, object> _storage = [];

    public static void Set<T>(string key, T value) where T : class
    {
        _storage.Remove(key);
        _storage.Add(key, value);
    }

    public static T Get<T>(string key) where T : class
    {
        return _storage.ContainsKey(key) ? _storage[key] as T : null;
    }

    public static void Remove(string key)
    {
        _storage.Remove(key);
    }
}