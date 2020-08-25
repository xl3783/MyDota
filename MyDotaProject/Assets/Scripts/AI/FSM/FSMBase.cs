using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.AI.FSM
{
	/// <summary>
	/// 状态机
	/// </summary>
	public class FSMBase : MonoBehaviour
	{
        [Tooltip("默认状态ID")]
        public FSMStateID defaultStateID;
        private List<FSMState> states;
        private FSMState currentState;
        // 配置状态
        // -- 创建状态对象
        // -- 设置状态映射
        private void Start()
        {
            InitDefaultState();
            ConfigFSM();
        }
        // 初始化默认状态
        private void InitDefaultState()
        {
            currentState = states.Find(x => x.StateID == defaultStateID);
            currentState.EnterState(this);
        }
        private void ConfigFSM()
        {
            states = new List<FSMState>();
            IdelState idel = new IdelState();
            idel.AddMap(FSMTriggerID.NoHealth, FSMStateID.Dead);
        }

        // 切换状态
        public void ChangeActiveState(FSMStateID stateId)
        {
            // 离开上一个状态
            currentState.ExitState(this);
            if(stateId == FSMStateID.Default)
            {
                stateId = defaultStateID;
            }
            currentState = states.Find(x => x.StateID == stateId);
            // 进入下一个状态
            currentState.EnterState(this);
        }
        public void Update()
        {
            // 判断当前状态条件
            currentState.Reason(this);
            // 执行当前状态逻辑
        }
    }
}