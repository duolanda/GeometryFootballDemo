using UnityEngine;

public class Singleton<T> where T : class, new()
{
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
    }

    protected Singleton()
    {
        if(null != _instance)
        {
            Debug.LogError("This " + (typeof(T)).ToString() + " Singleton Instance is not null !!!");
        }
        Init();
    }

    public virtual void Init() { }
}