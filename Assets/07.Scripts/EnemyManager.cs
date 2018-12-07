using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject koopaEnemy;
    public GameObject monkeyEnemy;
    public GameObject ghoustEnemy;
    public GameObject rocket;
    public Transform[] Points;
    public Transform[] rocketPoints;
    public Transform[] NotKoopaPoints;
    private float TimePrev;
    private float rTimePrev;
    public int koopaMaxCount = 5;
    public int NotkoopaMaxCount = 10;

    void Start()
    {
        Points = GameObject.Find("SpawnPoints").GetComponentsInChildren<Transform>();
        rocketPoints = GameObject.Find("RocketSpawnPoints").GetComponentsInChildren<Transform>();
        NotKoopaPoints = GameObject.Find("NotKoopaSpawnPoints").GetComponentsInChildren<Transform>();

        //하이라키상에 "SpawnPoints"라는 이름을 가진 오브젝트를 찾는다.
        //찾은 하위오브젝트의 트랜스폼 들을 Points라는 배열에 넣는다.
        TimePrev = Time.time; //현재시간 
        rTimePrev = Time.time;
    }

    void Update()
    {    //현재시 -과거시를 빼면 흘러간 시간이 계산 
        if (Time.time - TimePrev >= 3f)
        {
            CreateEmemy();
            TimePrev = Time.time;
        }
        if (Time.time - rTimePrev >= 3f)
        {
            CreateRocketEmemy();
            rTimePrev = Time.time;
        }
    }
    void CreateEmemy()
    {
        //에너미들의 갯수를 가져와서 정수로 형변환 하여 저장한다!
        int EnemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (EnemyCount < koopaMaxCount)
        {

            int idx = Random.Range(1, Points.Length);
            Instantiate(koopaEnemy, Points[idx].position, Points[idx].rotation);


            //int random = Random.Range(1, 5);
            //if (random == 1)
            //Instantiate(Enemy[0], Points[idx].position, Points[idx].rotation);
            //else if (random == 2)
            //    Instantiate(Enemy[1], Points[idx].position, Points[idx].rotation);
            //else if (random == 3)
            //    Instantiate(Enemy[2], Points[idx].position, Points[idx].rotation);
            //else if (random == 4)
            //Instantiate(Enemy[3], Points[idx].position, Points[idx].rotation);
        }

        if (EnemyCount < NotkoopaMaxCount)
        {

            int idx = Random.Range(1, NotKoopaPoints.Length);
            int random = Random.Range(1, 3);

            if (random == 1)
            {
                Instantiate(monkeyEnemy, NotKoopaPoints[idx].position, NotKoopaPoints[idx].rotation);
            }
            else if (random == 2)
            {
                Instantiate(ghoustEnemy, NotKoopaPoints[idx].position, NotKoopaPoints[idx].rotation);
            }
        }


    }
    void CreateRocketEmemy()
    {
        for (int idx = 1; idx < rocketPoints.Length; idx++)
        {
            Instantiate(rocket, rocketPoints[idx].position, Quaternion.LookRotation(rocketPoints[idx].right));

        }
    }
}
