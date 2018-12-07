using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rbody;
    public float Speed = 1500f;


    void Start()
    {
        rbody.AddForce(transform.forward * Speed);
        //리지디바디의 함수 AddForce(로컬좌표 방향 * 스피드 ,    포스 힘의 타입  //즉시 
        Destroy(this.gameObject, 3.0f);
    }
}
