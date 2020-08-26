using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.Common
{
	/// <summary>
	/// 脚本单例基类
	/// </summary>
	public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        public static T Instance 
        {
            get
            {
                if(instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if(instance == null)
                    {
                        // 脚本创建完成后会自行调用awake
                        new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                    }
                    else
                    {
                        instance.Init();
                    }                    
                }
                return instance;
            }
        }

        protected void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
        }

        protected virtual void Init()
        {
            // 允许子类自行进行初始化
        }
    }
}