using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBGM : MonoBehaviour 
{
    AudioSource sound;

	// Use this for initialization
	void Start () 
    {
        sound = GetComponent<AudioSource>();
        sound.Play();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
