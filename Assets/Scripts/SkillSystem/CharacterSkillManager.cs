using MyDota.SkillSystem.Common;
using MyDota.Common;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Data;
using System;
using MyDota.CharacterSystem;

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
        private string[] targetTags = { "Enemy", "Player"};
        SkillDeployer deployer;
        PlayerStatus player;
        // 目标
        private GameObject target;

        private void GenerateSkillFromCSV()
        {
            DataTable skillTable = CommonReader.ReadCSV(Path.Combine("SRC", "csv", "skillConfig.csv"));
            foreach (DataRow row in skillTable.Rows)
            {
                SkillData skill = new SkillData();
                skill.skillID = Convert.ToInt32(row[0].ToString());
                skill.name = row["name"].ToString();
                skill.description = row["description"].ToString(); 
                skill.coolTime = Convert.ToSingle(row["coolTime"].ToString());
                skill.attackDistance = Convert.ToSingle(row["attackDistance"].ToString());
                skill.impactType = row["impactType"].ToString().Split(',');
                skill.atkRatio = Convert.ToSingle(row["atkRatio"].ToString());
                skill.durationTime = Convert.ToSingle(row["durationTime"].ToString());
                skill.atkInterval = Convert.ToSingle(row["atkInterval"].ToString());
                skill.prefabName = row["prefabName"].ToString();
                skill.hitFxName = row["hitFxName"].ToString();
                skill.attackType = (SkillAttackType)Enum.Parse(typeof(SkillAttackType), row["attackType"].ToString());
                skill.selectorType = (SelectorType)Enum.Parse(typeof(SelectorType), row["selectorType"].ToString());
                skills.Add(skill);
            }
        }
        private void Start()
        {
            player = GetComponent<PlayerStatus>();
            GenerateSkillFromCSV();
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
            SkillData skill = skills.Find(x => x.skillID == id);
            if(skill == null)
            {
                return null;
            }
            // 单体攻击技能没有目标，无法攻击
            if(skill.attackType == SkillAttackType.Single)
            {
                // 目标tag不满足要求，无法攻击
                // 目标超出攻击距离，无法攻击
                if (target == null)
                {
                    print("没有目标，无法攻击");
                    return null;
                }
                if (Array.IndexOf<string>(skill.attackTargetTags, target.tag) == -1)
                {
                    print("目标tag不满足要求，无法攻击");
                    return null;
                }
                if (Vector3.Distance(transform.position, target.transform.position) > skill.attackDistance)
                {
                    print("目标超出攻击距离，无法攻击");
                    return null;
                }
            }
            
            if ( skill.coolRemain <= 0 && skill.costSP <= player.SP)
            {
                skill.singleAttackTarget = target.transform;
                return skill;
            } 
            return null;
        }

        // 生成技能
        public void GenerateSkill(SkillData data)
        {
            // 生成技能特效及销毁
            if(data.skillPrefab != null)
            {
                GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);

                // 销毁技能
                Destroy(skillGo, data.durationTime);
            }
            
            // 释放技能
            deployer.SkillData = data;
            deployer.DeploySkill();
            

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
        public void ClickGetTarget()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  //camare2D.ScreenPointToRay (Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(target != null)
                {
                    if(target.transform.Find("Selected") != null)
                    {
                        target.transform.Find("Selected").gameObject.SetActive(false);
                    }
                }
                if(Array.IndexOf(targetTags, hit.collider.gameObject.tag) != -1)
                {
                    target = hit.collider.gameObject;
                    if (target.transform.Find("Selected") != null)
                    {
                        target.transform.Find("Selected").gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}