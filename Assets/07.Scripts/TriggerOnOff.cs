using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnOff : MonoBehaviour 
{
    SphereCollider sphereCollider;
	// Use this for initialization
	void Start () 
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
    }
	
	void TurnOn()
    {
        sphereCollider.enabled = true;
    }

    void TurnOff()
    {
        sphereCollider.enabled = false;
    }
}
