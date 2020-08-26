using MyDota.SkillSystem.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.SkillSystem.Selectors
{
	/// <summary>
	/// 选区接口
	/// </summary>
	public interface IAttackSelector
	{
        /// <summary>
        /// 搜索目标
        /// </summary>
        /// <param name="skillData">技能参数</param>
        /// <param name="skillData">技能释放者的位置</param>
        /// <returns></returns>
        Transform[] SelectTarget(SkillData skillDatd, Transform skillTF);
	}
}

