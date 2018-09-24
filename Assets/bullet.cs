using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public ParticleSystem grenadeParticle;
    public Light grenadeLight;
    public float bulletSpeed = 20f;
    

	// Use this for initialization
	void Awake ()
    {
        grenadeLight.enabled = false;

	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
