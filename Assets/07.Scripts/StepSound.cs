using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSound : MonoBehaviour {

    AudioSource stepSound;
    public AudioClip step;
	// Use this for initialization
	void Start () {
        stepSound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
        {

            stepSound.PlayOneShot(step);
        }
    }
}
