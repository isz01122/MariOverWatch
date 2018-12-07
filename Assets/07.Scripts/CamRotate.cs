using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float rotSpeed = 120f;
    float rotateY;
    float RY;
    Transform Tr;
	// Use this for initialization
	void Start () 
    {
        Tr = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update ()
    {
        rotateY = Input.GetAxis("Mouse Y");
        RY += rotateY;
        Mathf.Clamp(RY, -30f, 30f);
        Tr.localRotation = Quaternion.Euler(-RY, 0f, 0f);

    }
}
