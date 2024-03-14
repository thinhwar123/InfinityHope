using System;
using System.Collections.Generic;

public static class DelegateCache
{
    private static readonly Dictionary<object, Dictionary<string, Delegate>> Cache = new Dictionary<object, Dictionary<string, Delegate>>();

    private static Dictionary<string, Delegate> GetObjectCache(this object instance)
    {
        if (!Cache.TryGetValue(instance, out Dictionary<string, Delegate> delegates))
        {
            Cache[instance] = delegates = new Dictionary<string, Delegate>();
        }
        return delegates;
    }

    public static T GetDelegate<T>(this object instance, string method) where T: class
    {
        Dictionary<string, Delegate> delegates = GetObjectCache(instance);
        if (!delegates.TryGetValue(method, out Delegate del))
        {
            delegates[method] = del = Delegate.CreateDelegate(typeof(T), instance, method);
        }
        return del as T;
    }
    
    public static T AddDelegate<T>(this object instance, string method) where T: class
    {
        Dictionary<string, Delegate> delegates = GetObjectCache(instance);
        if (!delegates.TryGetValue(method, out Delegate del))
        {
            delegates[method] = del = Delegate.CreateDelegate(typeof(T), instance, method);
        }
        return del as T;
    }

    public static void ClearAllDelegate(this object instance)
    {
        Cache.Remove(instance);
    }
}