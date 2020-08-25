using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Newtonsoft.Json;

namespace ns
{
	/// <summary>
	/// 生成资源映射表
	/// </summary>
	public class GenerateResConfig : Editor
	{
        [MenuItem("Tools/Resource/Generate ResConfig")]
		public static void Generate()
        {
            // 1. 查找Resources目录下所有预制件完整路径
            string[] resFiles = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
            for (int i = 0; i < resFiles.Length; i++)
            {
                resFiles[i] = AssetDatabase.GUIDToAssetPath(resFiles[i]);
            }
            
            Dictionary<string, string> prefabDict = new Dictionary<string, string>();
            // 2. 生成对应关系 json 名称=》路径
            for (int i = 0; i < resFiles.Length; i++)
            {
                string name = Path.GetFileNameWithoutExtension(resFiles[i]);
                string path = resFiles[i].Replace("Assets/Resources/", "").Replace(".prefab", "");
                prefabDict.Add(name, path);
            }

            // 3. 写入文件
            //TODO 写入平台的区别
            File.WriteAllText("Assets/StreamingAssets/PrefabConfig.json", JsonConvert.SerializeObject(prefabDict));
        }
	}
}