using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyDota.SkillSystem
{
	/// <summary>
	/// 动画事件触发脚本
	/// </summary>
	public class AnimationEventBehaviour : MonoBehaviour
	{
        public event Action hitHandler;
        private Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }
        public void Hit()
        {
            if(hitHandler != null)
            {
                hitHandler();
            }
        }
        public void FootL()
        {

        }
        public void FootR()
        {

        }
        public void AnimCancel(string animName)
        {
            animator.SetBool(animName, false);
        }
    }
}