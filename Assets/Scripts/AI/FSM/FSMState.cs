using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MyDota.Common;

namespace MyDota.AI.FSM
{
	/// <summary>
	/// 有限状态机状态类
	/// </summary>
	public abstract class FSMState
	{
        public FSMStateID StateID { get; set; }
        // 条件列表
        private List<FSMTrigger> triggers;
        // 下个状态映射
        private Dictionary<FSMTriggerID, FSMStateID> mapNextState;
        public FSMState()
        {
            Init();
        }
        // 为编号赋值
        protected abstract void Init();

        public void Reason(FSMBase fsm)
        {
            foreach (var item in triggers)
            {
                if (item.HandleTirgger(fsm))
                {
                    // 切换状态
                    fsm.ChangeActiveState(mapNextState[item.TriggerID]);
                }
            }
        }

        public void AddMap(FSMTriggerID triggerID, FSMStateID nextStateID)
        {
            if (mapNextState.ContainsKey(triggerID))
            {
                return;
            }
            mapNextState.Add(triggerID, nextStateID);
            string triggerName = ClassTypeGenerate.GenerateFSMTrigger(triggerID);
            triggers.Add(Activator.CreateInstance(Type.GetType(triggerName)) as FSMTrigger);
        }
        
        // 进入状态
        public virtual void EnterState(FSMBase fsm) { }
        // 状态中
        public virtual void ActionState(FSMBase fsm) { }
        // 离开状态
        public virtual void ExitState(FSMBase fsm) { }
    }
}

