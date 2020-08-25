using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.AI.FSM
{
    /// <summary>
    /// 血量为0
    /// </summary>
    public class NoHealthTrigger : FSMTrigger
    {
        public override bool HandleTirgger(FSMBase fsm)
        {
            throw new System.NotImplementedException();
        }

        protected override void Init()
        {
            TriggerID = FSMTriggerID.NoHealth;
        }
    }
}

