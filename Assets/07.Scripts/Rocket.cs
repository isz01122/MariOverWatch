using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour 
{
    public GameObject paticle;
    public GameObject explosion;
    public float speed = 1000f;
    Rigidbody rocketRb;
    MeshRenderer rd;
	// Use this for initialization
	void Start () 
    {
        rocketRb = GetComponent<Rigidbody>();
        rd = GetComponent<MeshRenderer>();

        rocketRb.AddForce(transform.forward * speed);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.tag == "Player")
        {

            rd.enabled = false;
            paticle.SetActive(false);
            CreateExplosion(collision.transform.position);
            Destroy(gameObject, 5f);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    void CreateExplosion(Vector3 explosionpos)
    {
        GameObject newexplo = Instantiate(explosion, explosionpos, Quaternion.identity);
        Destroy(newexplo, 3.5f);

    }
}
