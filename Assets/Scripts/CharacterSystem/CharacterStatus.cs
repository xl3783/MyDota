using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.CharacterSystem
{
	/// <summary>
	/// 角色状态
	/// </summary>
	public class CharacterStatus : MonoBehaviour
	{
        public CharacterAnimationParameter chParameter = new CharacterAnimationParameter();
        public float moveSpeed = 3;
        public float walkSpeed = 1.5f;
        public int HP = 1000;
        public int MaxHP = 1000;
        public int SP = 1000;
        public int MaxSP = 1000;
        public int BaseATK = 30;
        public int PhysicalDefencce = 10;
        public int MagicDefencce = 10;
        public int AttackInterval = 1;
        public int AttackDistance = 1000;
        public void Damage(int damge)
        {
            HP -= damge;
            HP = HP > 0 ? HP : 0;
        }
    }
}