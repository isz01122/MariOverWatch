using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 7f;
    public float jumpPower = 300f;
    public float rotateSpeed = 80f;
    public float rotSpeed = 200f;
    public float bulletSpeed = 2000f;
    public AudioClip jumpClip;
    public GameObject ifPlayerDieStopFire;
    private int Hp = 100;
    private int HpInit = 100;
    private bool IsDie;
    public AudioClip rocketSound;
    public AudioClip dieSound;
    public AudioClip reloadSound;
    public AudioClip hitSound;
    //public GameObject BloodEffect;
    public Image HpBar;

    public GameObject Menu;
    bool menu = false;
    bool IsFalse = true;


    Rigidbody rbody;
    Animator animator;
    Vector3 movement;
    Transform playerTransform;
    AudioSource audioSource;
    //public AudioSource jumpSource;
    float horizontalMove;
    float verticalMove;
    float rotateX;
    private bool IsLanding = false;

    bool IsPunch = false;

    void Awake()
    {
        Time.timeScale = 1;
        IsDie = false;
        Cursor.lockState = CursorLockMode.Locked;//마우스 커서 고정
        Cursor.visible = false;//마우스 커서 보이기

        HpBar.color = Color.green;

        
        rbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    public void ReloadAnimation()
    {
        animator.SetTrigger("IsReload");
        audioSource.PlayOneShot(reloadSound, 1.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateX = Input.GetAxis("Mouse X");
        playerTransform.Rotate(Vector3.up * rotateX * rotSpeed * Time.deltaTime);

        if (IsDie) return;

        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        if (horizontalMove == 0)
        {
            animator.SetBool("IsSideWalk", false);
        }
        else
        {
            animator.SetBool("IsSideWalk", true);
        }


        if (verticalMove == 0)
        {
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
        }

        Run();

        if (Input.GetButtonDown("Jump") && IsLanding)
        {
            Jump();
        }
        else
        {
            animator.SetBool("IsJump", false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu = !menu;
            Menu.SetActive(menu);
            if (IsFalse)
            {
                Time.timeScale = 0;
                IsFalse = false;
                Cursor.lockState = CursorLockMode.None;//마우스 커서 고정
                Cursor.visible = true;//마우스 커서 보이기
            }
            else
            {
                Time.timeScale = 1;
                IsFalse = true;
                Cursor.lockState = CursorLockMode.Locked;//마우스 커서 고정
                Cursor.visible = false;//마우스 커서 보이기
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Punch")
        {
            //IsPunch = true;
            Hp -= 10;
            HpBar.fillAmount = (float)Hp / (float)HpInit;
            animator.SetTrigger("IsHit");
            audioSource.PlayOneShot(hitSound, 1.0f);

            if (HpBar.fillAmount <= 0.3f)
                HpBar.color = Color.red;

            else if (HpBar.fillAmount <= 0.5f)
                HpBar.color = Color.yellow;

        }
        //if (IsPunch)
        //{
        //    IsPunch = false;
        //    Hit();
        //    HpBar.fillAmount = (float)Hp / (float)HpInit;
        //    animator.SetTrigger("IsHit");
        //    audioSource.PlayOneShot(hitSound, 1.0f);
        //}
        if (Hp <= 0)
        {
            other.GetComponent<SphereCollider>().enabled = false;
            Die();
        }


    }

    //void Hit()
    //{
    //    Hp -= 10;
    //}

    void OnCollisionEnter(Collision collision)
    {
        IsLanding = true;
        if (collision.transform.tag == "Rocket")
        {
            rbody.constraints = RigidbodyConstraints.FreezePosition;
            audioSource.PlayOneShot(rocketSound ,1.0f);
            Hp -= 100;
            HpBar.fillAmount = (float)Hp / (float)HpInit;
            animator.SetTrigger("IsHit");
            Die();
        }
    }

    void Die()
    {
        IsDie = true;
        ifPlayerDieStopFire.SetActive(false);
        horizontalMove = 0;
        verticalMove = 0;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in enemy)
        {
            item.SendMessage("StopAnimation");
        }
        animator.SetTrigger("IsDie");
        Invoke("DieSound", 3f);
        Invoke("Reload", 10f);
    }

    void Win()
    {
        IsDie = true;
        ifPlayerDieStopFire.SetActive(false);
        horizontalMove = 0;
        verticalMove = 0;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in enemy)
        {
            item.SendMessage("StopAnimation");
        }
        Invoke("Reload", 10f);
    }
    void DieSound()
    {
        audioSource.PlayOneShot(dieSound, 1.0f);
    }
    void Reload()
    {
        SceneManager.LoadScene(0);
    }
    void Run()
    {

        movement = new Vector3(horizontalMove, 0, verticalMove);
        movement = movement * speed * Time.deltaTime;

        playerTransform.Translate(Vector3.right * horizontalMove * speed * Time.deltaTime);
        playerTransform.Translate(Vector3.forward * verticalMove * speed * Time.deltaTime);
    }
    void Jump()
    {
        audioSource.PlayOneShot(jumpClip);
        animator.SetBool("IsJump", true);
        rbody.velocity = Vector3.up * 5.0f;
        IsLanding = false;
    }
}