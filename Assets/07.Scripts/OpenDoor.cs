using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class OpenDoor : MonoBehaviour {

    Animation ani;
    AudioSource audioDoor;
    private void Start()
    {
        ani = GetComponent<Animation>();
        audioDoor = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            ani.Play();
            audioDoor.Play();
        }
    }

}