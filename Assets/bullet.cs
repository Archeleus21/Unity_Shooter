using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float bulletSpeed = 20f;

	// Use this for initialization
	void Awake ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
        else
        {
            return;
        }
        
    }

}
