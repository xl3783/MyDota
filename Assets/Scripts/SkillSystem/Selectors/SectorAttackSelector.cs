using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyDota.CharacterSystem;
using MyDota.SkillSystem.Common;
using UnityEngine;

namespace MyDota.SkillSystem.Selectors
{
    /// <summary>
    /// 扇形/圆形选区
    /// </summary>
    public class SectorAttackSelector : IAttackSelector
    {
        public Transform[] SelectTarget(SkillData skillData, Transform skillTF)
        {
            List<Transform> tfsFind = new List<Transform>();
            // 获取所有的目标transform
            foreach (var item in skillData.attackTargetTags)
            {
                GameObject[] gosFind = GameObject.FindGameObjectsWithTag(item);
                foreach (var go in gosFind)
                {
                    tfsFind.Add(go.transform);
                }
            }
            // 范围筛选：角度、距离
            tfsFind = tfsFind.FindAll(x =>
                   Vector3.Distance(x.position, skillTF.position) <= skillData.attackDistance
                   && Vector3.Angle(skillTF.forward, x.position - skillTF.position) <= skillData.attackAngle / 2
               );
            // 找到活的角色
            tfsFind = tfsFind.FindAll(x => x.GetComponent<CharacterStatus>().HP > 0);
            if(tfsFind.Count == 0)
            {
                return tfsFind.ToArray();
            }
            if(skillData.attackType == SkillAttackType.Group)
            {
                return tfsFind.ToArray();
            }
            else
            {
                // TODO 暂时将获取最小值的写在这，后续统一封装
                int minDisIndex = 0;
                for (int i = 1; i < tfsFind.Count; i++)
                {
                    if(Vector3.Distance(tfsFind[i].position, skillTF.position) < 
                        Vector3.Distance(tfsFind[minDisIndex].position, skillTF.position))
                    {
                        minDisIndex = i;
                    }
                }
                return new Transform[] { tfsFind[minDisIndex] };
            }
        }
    }
}

