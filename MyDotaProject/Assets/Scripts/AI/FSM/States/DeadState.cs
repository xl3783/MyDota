using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.AI.FSM
{
    /// <summary>
    /// 死亡状态
    /// </summary>
    public class DeadState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Dead;
        }
        // 进入状态
        public override void EnterState(FSMBase fsm) 
        {
            base.EnterState(fsm);
            fsm.enabled = false;
        }
    }
}

