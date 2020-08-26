using MyDota.SkillSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.CharacterSystem
{
	/// <summary>
	/// 角色控制
	/// </summary>
	public class PlayerController : MonoBehaviour
	{
        #region 组件
        // 角色状态
        public PlayerStatus player;
        // 角色动画控制
        public Animator chAnim;
        public CharacterMotor motor;
        public CharacterSkillManager skillManager;
        public CharacterSkillSystem skillSystem;
        #endregion
        #region 变量
        public Dictionary<KeyCode, Vector3> moveDirections;
        public Dictionary<KeyCode, float> moveSpeed;
        // 右键水平旋转速度
        public float sensitivityX = 10;
        #endregion
        private void Start()
        {
            player = GetComponent<PlayerStatus>();
            chAnim = GetComponentInChildren<Animator>();
            motor = GetComponent<CharacterMotor>();
            skillManager = GetComponent<CharacterSkillManager>();
            skillSystem = GetComponent<CharacterSkillSystem>();
            moveDirections = new Dictionary<KeyCode, Vector3>
            {
                {KeyCode.W, new Vector3(0, 0, 1) },
                {KeyCode.A, new Vector3(-1, 0, 0) },
                {KeyCode.S, new Vector3(0, 0, -1) },
                {KeyCode.D, new Vector3(1, 0, 0) },
            };
            moveSpeed = new Dictionary<KeyCode, float>
            {
                { KeyCode.W, player.moveSpeed},
                { KeyCode.A, player.moveSpeed},
                { KeyCode.S, player.walkSpeed},
                { KeyCode.D, player.moveSpeed},
            };
        }

        private void Update()
        {
            // 移动
            foreach (var item in moveDirections)
            {
                if (Input.GetKey(item.Key))
                {
                    motor.Movement(item.Value);
                }
            }
            foreach (var item in moveDirections.Keys)
            {
                if (Input.GetKeyUp(item))
                {
                    motor.StopMove();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                motor.Jump();
            }
            if (Input.GetMouseButton(1))
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0, Space.World);
            }


            if (Input.GetMouseButton(0))
            {
                skillManager.ClickGetTarget();
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                skillSystem.UseSkill(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                skillSystem.UseSkill(2);
            }
        }
    }
}