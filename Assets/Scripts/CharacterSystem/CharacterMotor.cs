using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.CharacterSystem
{
	/// <summary>
	/// 角色马达：负责控制角色运动
	/// </summary>
	public class CharacterMotor : MonoBehaviour
	{
        public CharacterController chControl;
        public Animator chAnim;
        public PlayerStatus playerStatus;

        // 转身速度
        public float rotateSpeed = 20;
        // 移动速度
        public float moveSpeed = 5f;
        public float jumpSpeed = 13;
        private float jumpMargin = 0.1f;
        public float transformHeight = 1.1f;
        public bool jumping = false;
        
        public float gravity = 35;
        public Vector3 moveDirection;

        public float sensitivityX = 10;
        public float sensitivityY = 10;

        private void Awake()
        {
            chControl = GetComponent<CharacterController>();
            chAnim = GetComponentInChildren<Animator>();
            playerStatus = GetComponent<PlayerStatus>();

            moveDirection = new Vector3(0, -gravity, 0);
        }
        // 旋转
        public void LookAtTarget(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            transform.rotation =  Quaternion.Lerp(transform.rotation,
                Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
        }
        // 移动
        public void Movement(Vector3 direction)
        {
            if (direction == Vector3.zero) return;
            //LookAtTarget(direction);
            chAnim.SetBool(playerStatus.chParameter.run, true);
            // 移动
            float y = moveDirection.y;
            moveDirection = transform.TransformDirection(direction) * moveSpeed;
            moveDirection.y = y;
        }
        public void StopMove()
        {
            chAnim.SetBool(playerStatus.chParameter.run, false);
            moveDirection.x = moveDirection.z = 0;
        }

        public bool IsGrounded()
        {
            //return chControl.isGrounded;
            return Physics.Raycast(transform.position,
                Vector3.down, jumpMargin + transformHeight);
        }
       
        public void Jump()
        {
            if (IsGrounded())
            {
                jumping = true;
                moveDirection.y = jumpSpeed;
            }
        }
        private void Update()
        {
            if (jumping)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            chControl.Move(moveDirection * Time.deltaTime);
            if (jumping)
            {
                if (IsGrounded())
                {
                    jumping = false;
                    moveDirection.y = -gravity;
                }
            }

        }
    }
}