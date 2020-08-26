using MyDota.CharacterSystem;
using MyDota.SkillSystem.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.SkillSystem.ImpactEffects
{
    /// <summary>
    /// 伤害生命
    /// </summary>
    public class DamageImpactEffect : IImpactEffect
    {
        private float atkInterval;
        private float durationTime;
        private SkillData skillData;

        public void Execute(SkillDeployer deployer)
        {
            //deployer.SkillData.attackTargets
            atkInterval = deployer.SkillData.atkInterval;
            durationTime = deployer.SkillData.durationTime;
            skillData = deployer.SkillData;
            deployer.StartCoroutine(RepeatDamage(deployer));
        }

        private IEnumerator RepeatDamage(SkillDeployer deployer)
        {
            float atkTime = 0;
            do
            {
                OnceDamage();
                yield return new WaitForSeconds(atkInterval);
                atkTime += atkInterval;
                deployer.ExecuteTargetSelect();
            } while (atkTime < durationTime);
        }

        private void OnceDamage()
        {
            foreach (var item in skillData.attackTargets)
            {
                var status = item.GetComponent<CharacterStatus>();
                float damage = skillData.atkRatio * skillData.owner.GetComponent<CharacterStatus>().BaseATK;
                status.Damage((int)damage);
            }
        }
    }
}

