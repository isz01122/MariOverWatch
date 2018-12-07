using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoLight : MonoBehaviour {

    Light inLight;
    float timer;
    public bool flag = true;
    Color startColor;
	// Use this for initialization
	void Start () {
        inLight = GetComponent<Light>();
        startColor = inLight.color;
        timer = Time.time;
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time - timer > 1f)
        {
            flag = !flag;
            timer = Time.time;
        }

        if (flag)
        {
            inLight.color = Color.Lerp(inLight.color, Color.yellow, Time.deltaTime * 3f);
        }
        else
        {
            inLight.color = Color.Lerp(inLight.color, startColor, Time.deltaTime * 3f);
        }
    }
}
