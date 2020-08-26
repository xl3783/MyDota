using MyDota.SkillSystem.Common;
using MyDota.SkillSystem.ImpactEffects;
using MyDota.SkillSystem.Selectors;
using MyDota.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.SkillSystem
{
	/// <summary>
	/// 释放器配置工厂
	/// </summary>
	public class DeployerConfigFactory
	{
		public static IAttackSelector CreateAttackSelector(SkillData skillData)
        {
            // 选区对象命名规则：MyDota.SkillSystem.Selector. + 枚举名 + AttackSelector
            string selectorName = ClassTypeGenerate.GenerateSelector(skillData.selectorType);
            return CreateClassInstance<IAttackSelector>(selectorName);
        }
        public static List<IImpactEffect> CreateImpactEffects(SkillData skillData)
        {
            // 影响对象 对象命名规则：MyDota.SkillSystem.ImpactEffects. + 枚举名 + ImpactEffect
            List<IImpactEffect> impactEffects = new List<IImpactEffect>();
            foreach (var item in skillData.impactType)
            {
                string impactName = ClassTypeGenerate.GenerateImpactEffect(item);
                impactEffects.Add(CreateClassInstance<IImpactEffect>(impactName));
            }
            return impactEffects;
        }
        /// <summary>
        /// 动态创建类实例
        /// </summary>
        /// <typeparam name="T">类类型</typeparam>
        /// <param name="className">类名</param>
        /// <returns>实例</returns>
        private static T CreateClassInstance<T>(string className) where T : class
        {
            return Activator.CreateInstance(Type.GetType(className)) as T;
        }
    }
}

