using MyDota.AI.FSM;
using MyDota.SkillSystem.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.Common
{
	/// <summary>
	/// 按规则生成类名
	/// </summary>
	public class ClassTypeGenerate
	{
        /// <summary>
        /// 根据选区类型生成选区类名
        /// </summary>
		public static string GenerateSelector(SelectorType type)
        {
            // 选区对象命名规则：MyDota.SkillSystem.Selector. + 枚举名 + AttackSelector
            return string.Format("MyDota.SkillSystem.Selectors.{0}AttackSelector", type);
        }

        /// <summary>
        /// 根据选区类型生成选区类名
        /// </summary>
        public static string GenerateImpactEffect(string type)
        {
            // 影响对象 对象命名规则：MyDota.SkillSystem.ImpactEffects. + 枚举名 + ImpactEffect
            return string.Format("MyDota.SkillSystem.ImpactEffects.{0}ImpactEffect", type);
        }

        public static string GenerateFSMTrigger(FSMTriggerID triggerid)
        {
            return string.Format("Mydota.AI.FSM.{0}Trigger", triggerid);
        }
    }
}

