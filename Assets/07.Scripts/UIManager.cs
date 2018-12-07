using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour 
{

    public static UIManager manager;
    public Text KillTxt;
    public Text BulletTxt;
    public Text TimerTxt;
    public int Total = 0;
    public int canUseBullet = 30;
    public int maxBullet = 150;
    public float timer = 60f;
    int min = 5;

    bool flage = false;
    bool winflage = false;


    // Use this for initialization
    void Start()
    {
        manager = this;

        StartCoroutine(CallDie());
    }

    void Update()
    {
        if (winflage) return;
        if (flage) return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            if (min == 0)
            {
                min = 0;
            }
            else
                min--;

            timer += 60;
        }

        TimerTxt.text = string.Format("{0:D2} : {1:D2}", min, (int)timer);


    }

    IEnumerator CallDie()
    {
        yield return new WaitForSeconds(300f);
        flage = true;
        GameObject.Find("Player").SendMessage("Die");
        TimerTxt.text = "TimeOver";
    }

    public void KillScore(int Score)
    {
        Total += Score;
        KillTxt.text = "KILL : " + Total.ToString() +" / 30";
        if (Total == 30) 
        {
            winflage = true;
        }
        if (winflage)
        {
            TimerTxt.text = "Win!!!";
            TimerTxt.fontSize = 150;
            TimerTxt.color = Color.yellow;
            TimerTxt.rectTransform.anchoredPosition3D = new Vector3(0, -113f, 0);
            GameObject.Find("Player").SendMessage("Win");
        }
    }

    public void bulletScore(int Score)
    {
        canUseBullet -= Score;
        BulletTxt.text = canUseBullet.ToString() + " / " + maxBullet.ToString();
    }

    public void PlayScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitScene()
    {
        Application.Quit();
    }

}

