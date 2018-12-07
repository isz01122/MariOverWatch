using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCamera : MonoBehaviour 
{

    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;//마우스 커서 고정
        Cursor.visible = true;//마우스 커서 보이기
    }

    void Update () 
    {
		
	}
}
