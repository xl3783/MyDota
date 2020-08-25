using MyDota.SkillSystem.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.SkillSystem.ImpactEffects
{
	/// <summary>
	/// 影响效果接口
	/// </summary>
	public interface IImpactEffect
	{
        void Execute(SkillDeployer deployer);
	}
}

