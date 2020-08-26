using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using MyDota.Common;

namespace ns
{
	/// <summary>
	/// 将csv转换为json信息
	/// </summary>
	public class GenerateSkillConfig : Editor
    {
        [MenuItem("Tools/Resource/Generate SkillConfig")]
        public static void Generate()
        {
            var ss = CommonReader.ReadCSV("skillConfig.csv");
            int i = 1;
        }
    }
}