using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    PlayerHealth playerHealth;  //reference to player health script
    int currentPlayerHealth;
    public GameObject enemy;  //enemy
    public GameObject player;  //player
    public float spawnTime = 3f;  //spawn time
    public Transform[] spawnPoints;  //point where enemies spawn in


    void Start ()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        InvokeRepeating ("Spawn", spawnTime, spawnTime);  //used to repeat after a set time (function, time, rate)
    }

    private void Update()
    {
        currentPlayerHealth = playerHealth.currentHealth;  
    }

    void Spawn ()  //used to spawm enemies
    {
        if ( currentPlayerHealth <= 0f)  //stops spawning when player is dead
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);   //spawn point, gets random spawn point if more than one.

        //creates a copy or instance of an object
        //instantiate (game object to copy, position it will be, rotation it will have)
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);  
    }
}
