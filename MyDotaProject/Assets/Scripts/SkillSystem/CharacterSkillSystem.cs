using MyDota.CharacterSystem;
using MyDota.SkillSystem.Common;
using MyDota.SkillSystem.Selectors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.SkillSystem
{
	/// <summary>
	/// 封装技能系统，提供简单的技能释放功能
	/// </summary>
	public class CharacterSkillSystem : MonoBehaviour
    {
        private CharacterSkillManager skillManager;
        private Animator anim;
        private SkillData currentSkill;


        private void Start()
        {
            skillManager = GetComponent<CharacterSkillManager>();
            anim = GetComponentInChildren<Animator>();

            GetComponentInChildren<AnimationEventBehaviour>().hitHandler += DeploySkill;
        }
        private void DeploySkill()
        {
            skillManager.GenerateSkill(currentSkill);
        }
        public void UseSkill(int skillID)
        {
            // 准备技能
            currentSkill = skillManager.PrepareSkill(skillID);
            if(currentSkill == null)
            {
                return;
            }
            // 播放动画
            anim.SetBool(currentSkill.animationName, true);
            // 生成技能
            // 如果单体攻击
            if(currentSkill.attackType == SkillAttackType.Single)
            {
                Transform target = SelectTarget();
                if(target != null)
                {
                    // -- 转向目标
                    transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
                    //-- 选中目标
                    var selected = target.GetComponent<CharacterSelected>();
                    print(selected);
                    if(selected != null)
                    {
                        selected.SetSelectedActive(true);
                    }
                }
            }

        }

        private Transform SelectTarget()
        {
            Transform[] target = new SectorAttackSelector().SelectTarget(currentSkill, transform);
            return target.Length > 0 ? target[0] : null;
        }

        /// <summary>
        /// NPC使用技能
        /// </summary>
        public void UseRandomSkill()
        {
            // 从管理器中挑选出可以释放的技能
            // 产生随机数
            // 释放技能
        }
    }
}