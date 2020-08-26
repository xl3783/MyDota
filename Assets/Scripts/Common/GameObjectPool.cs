using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.Common
{
    /// <summary>
    /// 重置接口，用于对象池对象的重置
    /// </summary>
    public interface IResetAble
    {
        void OnReset();
    }
    /// <summary>
    /// 对象池
    /// </summary>
    public class GameObjectPool : MonoSingleton<GameObjectPool>
	{
        //对象池
        private Dictionary<string, List<GameObject>> cache;
        protected override void Init()
        {
            base.Init();
            cache = new Dictionary<string, List<GameObject>>();
        }
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="key">对象类别</param>
        /// <param name="prefab">预制件</param>
        /// <param name="pos">位置</param>
        /// <param name="rotate">旋转</param>
        /// <returns></returns>
        public GameObject CreateObject(string key, GameObject prefab, 
            Vector3 pos, Quaternion rotate)
        {
            GameObject go = null;
            if (cache.ContainsKey(key)) 
            {
                go = cache[key].Find(x => !go.activeInHierarchy);
            }
            if (go == null) 
            {
                // 创建对象
                go = Instantiate(prefab);
                // 加入池中
                if (!cache.ContainsKey(key))
                {
                    cache.Add(key, new List<GameObject> { go });
                }
            }
            // 设置属性
            go.transform.position = pos;
            go.transform.rotation = rotate;
            go.SetActive(true);
            // 每次激活对象，进行重置
            IResetAble[] resetComs = go.GetComponents<IResetAble>();
            foreach (var item in resetComs)
            {
                item.OnReset();
            }
            return go;
        }
        public void CollectObject(GameObject go, float delay = 0)
        {
            StartCoroutine(CollectObjectDelay(go, delay));
        }
        private IEnumerator CollectObjectDelay(GameObject go, float delay)
        {
            yield return new WaitForSeconds(delay);
            go.SetActive(false);
        }
    }
}