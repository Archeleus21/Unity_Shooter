using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;  //controls enemy attack speed
    public float attackDamage = 10f;  //controls enemy da


    Animator anim;  //reference to animation
    GameObject player;  //reference to player
    PlayerHealth playerHealth;  //reference to player health script
    //EnemyHealth enemyHealth;
    bool playerInRange;  //is player in range?
    float timer;  //used to keep everything in sync


    void Awake ()
    {
        //gets components and references
        player = GameObject.FindGameObjectWithTag ("Player");  //do once in awake to store and never use again, help improve performance
        playerHealth = player.GetComponent <PlayerHealth> (); 
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    //checks if player is in range and able to be attacked
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)  //are you the player?
        {
            playerInRange = true;  //is player in range?
        }
    }

    //checks if player is in range
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)  //are you the player?
        {
            playerInRange = false;  //is player in range?
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;  //adds to timer

        if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)  //checks if attacking is within the attack speed that was given
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)  //checks if player is dead
        {
            anim.SetTrigger ("PlayerDead");  //player dead animation
        }
    }


    void Attack ()
    {
        timer = 0f;  //resets timer

        if(playerHealth.currentHealth > 0)  //if player has health, lets take some away
        {
            playerHealth.TakeDamage (attackDamage);  //calls function from other script
        }
    }
}
