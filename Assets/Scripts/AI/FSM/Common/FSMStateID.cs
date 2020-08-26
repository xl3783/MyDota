using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.AI.FSM
{
	/// <summary>
	/// 状态ID
	/// </summary>
	public enum FSMStateID
	{
        // 不存在该状态
		None,
        // 默认状态
        Default,
        // 闲置
        Idle,
        // 死亡
        Dead
	}
}

