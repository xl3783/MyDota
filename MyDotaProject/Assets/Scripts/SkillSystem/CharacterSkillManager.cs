using MyDota.SkillSystem.Common;
using MyDota.Common;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MyDota.SkillSystem
{
	/// <summary>
	/// 技能管理器
	/// </summary>
	public class CharacterSkillManager : MonoBehaviour
	{
        // 技能列表
        //public SkillData[] skills;
        public List<SkillData> skills = new List<SkillData>();
        SkillDeployer deployer;
        private void Start()
        {
            foreach (var item in skills)
            {
                InitSkill(item);
            }
            deployer = GetComponent<SkillDeployer>();
        }

        private void InitSkill(SkillData data)
        {
            data.skillPrefab = ResourceManager.LoadSkill<GameObject>(data.prefabName);
            data.owner = gameObject;
        }
        // 技能释放条件
        public SkillData PrepareSkill(int id)
        {
            float sp = 1;
            SkillData skill = skills.Find(x => x.skillID == id);
            if ( skill != null && skill.coolRemain <= 0 && skill.costSP >= sp)
            {
                return skill;
            } 
            return null;
        }

        // 生成技能
        public void GenerateSkill(SkillData data)
        {
            // 生成技能预制件
            GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
            // 释放技能
            deployer.SkillData = data;
            deployer.DeploySkill();
            // 销毁技能
            Destroy(skillGo, data.durationTime);

            // 冷却
            StartCoroutine(CoolTimeDown(data));
        }
        private IEnumerator CoolTimeDown(SkillData data)
        {
            while(data.coolRemain > 0)
            {
                yield return new WaitForSeconds(1);
                data.coolRemain--;
            }
            
        }
    }
}