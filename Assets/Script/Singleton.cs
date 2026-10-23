/// <summary>
/// 纯C#无Mono通用泛型单例
/// </summary>
/// <typeparam name="T">自身类，需要无参构造</typeparam>
public abstract class Singleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }

    // 手动销毁单例
    public void DestroySingleton()
    {
        _instance = null;
    }
}