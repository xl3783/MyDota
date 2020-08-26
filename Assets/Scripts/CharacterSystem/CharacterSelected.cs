using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyDota.CharacterSystem
{
	/// <summary>
	/// 
	/// </summary>
	public class CharacterSelected : MonoBehaviour
	{
        public string selectedName = "Selected";
        private GameObject selectedGo;
        private float hideTime;
        private void Start()
        {
            selectedGo = transform.Find(selectedName).gameObject;
        }
        public void SetSelectedActive(bool state)
        {
            // 设置选择器物体激活状态
            selectedGo.SetActive(state);
            // 设置当前脚本激活状态
            enabled = state;
            if (state)
            {
                hideTime = Time.time + 3;
            }
        }

        private void Update()
        {
            if(hideTime <= Time.time)
            {
                SetSelectedActive(false);
            }
        }
    }
}