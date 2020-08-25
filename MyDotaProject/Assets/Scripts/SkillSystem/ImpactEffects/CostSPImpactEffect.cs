using MyDota.CharacterSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.SkillSystem.ImpactEffects
{
    /// <summary>
    /// 消耗法力
    /// </summary>
    public class CostSPImpactEffect : IImpactEffect
    {
        public void Execute(SkillDeployer deployer)
        {
            var status = deployer.SkillData.owner.GetComponent<CharacterStatus>();
            status.SP -= deployer.SkillData.costSP;
        }
    }
}

