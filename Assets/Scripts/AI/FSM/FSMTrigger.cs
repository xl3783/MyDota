using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.AI.FSM
{
	/// <summary>
	/// 条件基类
	/// </summary>
	public abstract class FSMTrigger
	{
        public FSMTriggerID TriggerID { get; set; }
        public FSMTrigger()
        {
            Init();
        }

        protected abstract void Init();
        // 是否满足条件
        public abstract bool HandleTirgger(FSMBase fsm);
    }
}

