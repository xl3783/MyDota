using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyDota.Common
{
	/// <summary>
	/// 资源管理器
	/// </summary>
	public class ResourceManager
	{
        private static Dictionary<string, string> skillPath;

        static ResourceManager()
        {
            // 获取预制件路径映射
            skillPath = JsonConvert.DeserializeObject<Dictionary<string, string>>(GetConfigFileText());
        }

        private static string GetConfigFileText()
        {
            // 读取json文件
            string content;
            // PC本地读取
            using (StreamReader sr = File.OpenText("Assets/StreamingAssets/PrefabConfig.json"))
            {
                content = sr.ReadToEnd();
            }
            // TODO 移动端
            // 使用UnityWebRequest读取
            //string url = "file://" + Application.streamingAssetsPath + "/PrefabConfig.json";
            return content;
        }

        public static T LoadSkill<T>(string skillName) where T : Object
        {
            if (skillPath.ContainsKey(skillName))
            {
                return Resources.Load<T>(skillPath[skillName]);
            }
            return null;
        }
    }
}

