using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.CharacterSystem
{
	/// <summary>
	/// 敌人状态
	/// </summary>
	public class EnemyStatus : CharacterStatus
    {
        public EnemyStatus()
        {
            print("构造函数");
            chParameter = new CharacterAnimationParameter();
            chParameter.run = "Moving";
            chParameter.attack = "Attack1Trigger";
            print(chParameter.run);
        }
        private void Start()
        {
            print(chParameter.run);
        }
    }
}