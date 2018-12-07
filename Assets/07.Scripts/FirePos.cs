using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePos : MonoBehaviour
{

    public GameObject Bullet;
    public Transform thisFirePos;
    public AudioSource source; //스피커
    public AudioClip BulletSound;//소리파일 
    //public MeshRenderer MuzzleFlash;
    public GameObject Player;
    public MeshRenderer MuzzleFlash;
    int bulletInit;
    int bulletCnt;
    int usedBullet = 30;
    Animator ani;

    void Start()
    {
        //MuzzleFlash.enabled = false;
        ani = Player.GetComponent<Animator>();
        MuzzleFlash.enabled = false;
        bulletInit = 30; //UIManager.manager.canUseBullet;
        bulletCnt = 0;
    }
    void Fire()
    {
        Instantiate(Bullet, thisFirePos.position, thisFirePos.rotation);
        //프리팹 혹은 오브젝트 생성 함수 (What, Where , How rotation)
        source.PlayOneShot(BulletSound, 1.0f);
        ani.SetTrigger("IsFire");
        StartCoroutine(ShowMuzzleFlash());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bulletCnt >= bulletInit)
            {
                return;
            }
            ++bulletCnt;
            UIManager.manager.bulletScore(1);
            StartCoroutine(SpeedFire());
            //Fire();

            //코루틴 :시간 타이밍으로 반복
        }
        if(Input.GetMouseButtonDown(1))
        {
            if (bulletCnt == 30 && UIManager.manager.maxBullet != 0) 
            {
                GameObject.Find("Player").SendMessage("ReloadAnimation");
                Invoke("WaitTime", 3.0f);
            }
        }
    }
    //총을 재장전 하는동안 시간을 벌어주기 위한 함수
    //아무것도 하지 않음 1초가 지나기 전
    void WaitTime()
    {
        bulletCnt = 0;
        UIManager.manager.BulletTxt.text = bulletInit.ToString() + " / " +
            (UIManager.manager.maxBullet - usedBullet).ToString();
        UIManager.manager.canUseBullet = 30;
        UIManager.manager.maxBullet -= usedBullet;
    }
    IEnumerator ShowMuzzleFlash()
    {
        float _scale = Random.Range(1.0f, 2.0f);

        //vector3.one는 동일한 값으로!(1,1,1)
        MuzzleFlash.transform.localScale = Vector3.one * _scale;
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
        MuzzleFlash.transform.localRotation = rot;


        MuzzleFlash.enabled = true;
        yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
        MuzzleFlash.enabled = false;
    }
    IEnumerator SpeedFire()
    {

        yield return new WaitForSeconds(0.2f);
        Fire();
    }
}
