using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RelodeGameScene : MonoBehaviour 
{
    float prevTime;
	void Start () 
    {
        prevTime = Time.time;
	}
	
	void Update () 
    {
		if(Time.time-prevTime > 3f)
            SceneManager.LoadScene(0);
    }
}
