using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.AI.FSM
{
    /// <summary>
    /// 空闲状态
    /// </summary>
    public class IdelState : FSMState
    {
        protected override void Init()
        {
            StateID = FSMStateID.Idle;
        }
        
    }
}

