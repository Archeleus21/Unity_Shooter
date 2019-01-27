using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private PlayerShooting player;
    private Rigidbody rb;
    private Vector3 movement = new Vector3(0f, 5f, 10f);

    private int explosionDamage = 150;
    private float explosionTimer = 3f;

    private List<EnemyHealth> EnemyList = new List<EnemyHealth>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponentInChildren<PlayerShooting>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,10, player.transform.rotation.y), ForceMode.Impulse);

        StartCoroutine(GrenadeExplostionTimer());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
            EnemyList.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
            EnemyList.Remove(enemy);
        }
    }

    //used to detonate grenade
    IEnumerator GrenadeExplostionTimer()
    {
        yield return new WaitForSecondsRealtime(explosionTimer);
        foreach (EnemyHealth enemy in EnemyList)
        {
            enemy.TakeDamage(explosionDamage, transform.position);
        }
        Destroy(gameObject);
    }
}
