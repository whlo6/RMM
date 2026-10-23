using UnityEngine;
using System;

/// <summary>
/// 可手动挂载、自动生成双模式Mono泛型单例基类
/// 使用：public class GameMgr : SingletonMono<GameMgr>
/// </summary>
/// <typeparam name="T">自身管理类</typeparam>
public abstract class Mono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lockObj = new object();

    // 全局唯一访问入口
    public static T Instance
    {
        get
        {
            lock (_lockObj)
            {
                // 已有实例直接返回
                if (_instance != null)
                    return _instance;

                // 场景查找手动挂载的实例
                _instance = FindObjectOfType<T>();

                // 场景没挂载则自动创建物体挂载脚本
                if (_instance == null)
                {
                    GameObject singletonObj = new GameObject($"[{typeof(T).Name}]");
                    _instance = singletonObj.AddComponent<T>();
                    Debug.Log($"自动创建单例物体：{singletonObj.name}");
                }

                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        lock (_lockObj)
        {
            // 已有实例且不是自己，销毁自身
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // 赋值唯一实例
            _instance = this as T;
        }
    }

    /// <summary>
    /// 切换场景不销毁该单例物体
    /// 在子类Awake/Start调用即可
    /// </summary>
    protected void DontDestroyThis()
    {
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy()
    {
        lock (_lockObj)
        {
            // 当前销毁的是唯一实例，置空防止脏引用
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }

    /// <summary>
    /// 手动销毁单例并清空引用
    /// </summary>
    public void DestroySingleton()
    {
        Destroy(gameObject);
    }
}