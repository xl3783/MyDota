using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.CharacterSystem
{
	/// <summary>
	/// 人物主相机控制
	/// </summary>
	public class MainCameraController : MonoBehaviour
	{
        // 是否可操作
        private bool isActive = false;
        // 记录原角度
        public Quaternion cameraQueaternion;
        public float sensitivityX = 10;
        public float sensitivityY = 3;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                isActive = true;
                cameraQueaternion = transform.rotation;
            }
            if (isActive)
            {
                transform.Rotate(0, 
                    Input.GetAxis("Mouse X") * sensitivityX, 0, Space.World);
            }
            if (Input.GetMouseButtonUp(0))
            {
                gameObject.transform.rotation = cameraQueaternion;
                isActive = false;
            }
        }
    }
}