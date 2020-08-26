using MyDota.SkillSystem.Common;
using MyDota.SkillSystem.Selectors;
using MyDota.SkillSystem.ImpactEffects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.SkillSystem
{
	/// <summary>
	/// 技能释放器
	/// </summary>
	public class SkillDeployer : MonoBehaviour
	{
        private SkillData skillData;
        private IAttackSelector attackSelector;
        private List<IImpactEffect> impactEffects;

        public SkillData SkillData
        {
            get
            {
                return skillData;
            }
            set
            {
                skillData = value;
                print(skillData.selectorType);
                InitDeployer();
            }
        }

        // 创建算法对象
        private void InitDeployer()
        {
            // 选区
            attackSelector = DeployerConfigFactory.CreateAttackSelector(skillData);

            // 影响
            impactEffects = DeployerConfigFactory.CreateImpactEffects(skillData);
        }
        // 执行算法对象
        // 选区
        public void ExecuteTargetSelect()
        {
            if(skillData.attackType == SkillAttackType.Single)
            {
                skillData.attackTargets = new Transform[] { skillData.singleAttackTarget};
            }
            else
            {
                skillData.attackTargets = attackSelector.SelectTarget(skillData, this.transform);
            }
        }
        // 影响
        public void ExecuteImpactEffects()
        {
            foreach (var item in impactEffects)
            {
                item.Execute(this);
            }
        }
        // 释放
        public virtual void DeploySkill()
        {
            ExecuteTargetSelect();
            ExecuteImpactEffects();
        }
    }
}