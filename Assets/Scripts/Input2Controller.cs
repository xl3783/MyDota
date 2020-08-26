using MyDota.CharacterSystem;
using MyDota.SkillSystem;
using MyDota.SkillSystem.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyDota
{
	/// <summary>
	/// 输入控制器
	/// </summary>
	public class Input2Controller : MonoBehaviour
	{
        public PlayerStatus player;
        public Animator chAnim;
        public CharacterMotor motor;
        public CharacterSkillManager skillManager;
        public CharacterSkillSystem skillSys;
        public bool isKeyDownW = false;
        public bool isKeyDownA = false;
        public bool isKeyDownS = false;
        public bool isKeyDownD = false;
        public Dictionary<KeyCode, bool> isKeyDown;
        public Dictionary<KeyCode, Vector3> moveDirections;
      
        private void Start()
        {
            player = GetComponent<PlayerStatus>();
            chAnim = GetComponentInChildren<Animator>();
            motor = GetComponent<CharacterMotor>();
            skillManager = GetComponent<CharacterSkillManager>();
            skillSys = GetComponent<CharacterSkillSystem>();
            isKeyDown = new Dictionary<KeyCode, bool>
            {
                {KeyCode.W, false },
                {KeyCode.A, false },
                {KeyCode.S, false },
                {KeyCode.D, false },
            };
            moveDirections = new Dictionary<KeyCode, Vector3>
            {
                {KeyCode.W, new Vector3(0, 0, 1) },
                {KeyCode.A, new Vector3(-1, 0, 0) },
                {KeyCode.S, new Vector3(0, 0, -1) },
                {KeyCode.D, new Vector3(1, 0, 0) },
            };
        }
        private void MotorMoveStart()
        {
            chAnim.SetBool("run", true);
        }
        private void MotorMoveStop()
        {
            chAnim.SetBool("run", false);
        }
        private void Update()
        {
            // 判断是否按下按键
            //foreach (var item in isKeyDown.Keys.ToList())
            //{
            //    print(Input.GetKeyDown(item));
            //    isKeyDown[item] = Input.GetKeyDown(item);
            //}

            //if (isKeyDown.Values.Any(x => true))
            //{
            //    MotorMoveStart();
            //}

            //foreach (var item in isKeyDown)
            //{
            //    if(item.Value)
            //    {
            //        motor.Movement(moveDirections[item.Key]);
            //    }
            //}

            //// 判断是否弹起按键
            //foreach (var item in isKeyDown.Keys.ToList())
            //{
            //    isKeyDown[item] = !Input.GetKeyUp(item);
            //    print(Input.GetKeyUp(item));
            //}
            //if (isKeyDown.Values.All(x => false))
            //{
            //    MotorMoveStop();
            //}

            //foreach (var item in isKeyDown)
            //{
            //    if (!item.Value)
            //    {
            //        motor.Movement(Vector3.zero);
            //    }
            //}
            if (isKeyDownW)
            {
                motor.Movement(moveDirections[KeyCode.W]);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    isKeyDownW = true;
                    MotorMoveStart();
                    motor.Movement(moveDirections[KeyCode.W]);
                }
            }

            if (Input.GetKeyUp(KeyCode.W))
            {
                MotorMoveStop();
                isKeyDownW = false;
            }

            if (isKeyDownS)
            {
                motor.Movement(moveDirections[KeyCode.S]);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.S))
                {
                    isKeyDownS = true;
                    MotorMoveStart();
                    motor.Movement(moveDirections[KeyCode.S]);
                }
            }

            if (Input.GetKeyUp(KeyCode.S))
            {
                MotorMoveStop();
                isKeyDownS = false;
            }

            if (Input.GetKeyDown(KeyCode.F1))
            {
                skillSys.UseSkill(1001);
            }
        }
    }
}