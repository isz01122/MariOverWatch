using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Boss : MonoBehaviour 
{

    private Animator Ani; //애니메이션 동작 표현 
    private Transform ZomBieTr;  //좀비의 위치 
    private Transform playerTr;  //플레이어의 위치 
    public float attackDist = 3.0f; //공격범위
    public float traceDist = 25f; //추적 범위 
    private NavMeshAgent Navi;
    private int Hp = 100;
    private int HpInit = 100;
    private bool IsDie = false;
    public int Damage = 10;
    float prevTime;
    //public GameObject BloodEffect;
    public Image HpBar;
    private Canvas thisCanvas;
    private AudioSource Source;
    public AudioClip DieSound;
    public AudioClip StepSound;
    public AudioClip PunchSound;
    public AudioClip IdleSound;

    bool playerDie = false;
    Rigidbody rb;

    void Step()
    {
        Source.PlayOneShot(StepSound);
    }
    void Punch()
    {
        Source.PlayOneShot(PunchSound);
    }
    void Idle()
    {
        Source.PlayOneShot(IdleSound);
    }
    public void StopAnimation()
    {
        playerDie = true;
    }
    void Start()
    {
        Source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        //네트웍 게임인 경우  미리 스타트 함수에서 컴퍼넌트를 갖고 시작해야
        //속도상으로 유리하다.
        Ani = GetComponent<Animator>();
        ZomBieTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //하이라키의 게임오브젝트중 Player태그를 가진 오브젝트를 찾아서
        //그 오브젝트 컴퍼넌트 안에 트랜스폼을 찾아 온다. 
        Navi = GetComponent<NavMeshAgent>();

        HpBar.color = Color.green;
        thisCanvas = GetComponentInChildren<Canvas>();

        //자기 자신의 하위 오브젝트에서 캐싱
    }
    void OnCollisionEnter(Collision Other)
    {
        if (Other.gameObject.tag == "Bullet")
        {
            Destroy(Other.gameObject);
            Navi.isStopped = true;
            Ani.SetTrigger("IsHit");
            Hp -= Damage;
            HpBar.fillAmount = (float)Hp / (float)HpInit;

            if (Hp <= 0)
                Die();
            if (HpBar.fillAmount <= 0.3f)
                HpBar.color = Color.red;
            else if (HpBar.fillAmount <= 0.5f)
                HpBar.color = Color.yellow;

            //CreateBlood(Other.transform.position);
        }
    }
    //void CreateBlood(Vector3 bloodpos)
    //{
    //    GameObject newblood = Instantiate(BloodEffect, bloodpos, Quaternion.identity);
    //    Destroy(newblood, 0.6f);

    //}

    void Die()
    {
        UIManager.manager.KillScore(1);
        gameObject.tag = "Untagged";
        thisCanvas.enabled = false;
        Navi.isStopped = true;
        Ani.SetTrigger("IsDie");
        IsDie = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 5f);
        Source.PlayOneShot(DieSound, 1.0f);

        foreach (Collider col in GetComponentsInChildren<SphereCollider>())
        {
            col.enabled = false;
        }
        foreach (Collider col in GetComponentsInChildren<CapsuleCollider>())
        {
            col.enabled = false;
        }
    }


    void Update()
    {

        if (playerDie)
        {
            Navi.isStopped = true;
            Ani.SetBool("IsAttack", false);
            Ani.SetBool("IsWalk", false);
            return;
        }
        if (IsDie) return;


        //3차원좌표에서 거리를 구하는 함수 
        float dist = Vector3.Distance(playerTr.position, ZomBieTr.position);




        if (dist <= attackDist)
        {
            Navi.isStopped = true; //네비게이션 중지 
            Ani.SetBool("IsAttack", true);
        }
        else if (dist <= traceDist)
        {
            Navi.destination = playerTr.position;
            //플레이어를 추적한다
            Navi.isStopped = false;  //네비게이션 활성화 

            Ani.SetBool("IsWalk", true);
            Ani.SetBool("IsAttack", false);
        }
        else
        {

            Navi.isStopped = false;  //네비게이션 활성화 
            Ani.SetBool("IsWalk", false);
        }

    }
}
