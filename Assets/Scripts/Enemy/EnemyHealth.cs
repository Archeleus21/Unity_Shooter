using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth instance = null;

    public int startingHealth = 100;  //enemy starting health
    public int currentHealth;  //stores current health
    public float sinkSpeed = 2.5f;  //used to get rid of enemy character
    public int scoreValue = 10;  //points enemies are worth
    public AudioClip deathClip;  //death sound
    public Image enemyHealthBar;  //reference to enemy healthbar

    //stores components
    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;

    //true of false variables
    private bool isInGrenadeRange = false;
    private bool isDead;
    private bool isSinking;

    //-------------------------------------------------
    //-------------------------------------------------
    public bool IsInGrenadeRange
    {
        get
        {
            return isInGrenadeRange;
        }
        set
        {
            isInGrenadeRange = !isInGrenadeRange;
        }
    }

    void Awake ()
    {
        //gets the components
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();

        //looks in the children of all gameobjects and finds the particle system
        hitParticles = GetComponentInChildren <ParticleSystem> ();

        capsuleCollider = GetComponent <CapsuleCollider> (); 

        currentHealth = startingHealth; //sets current health to starting health
    }


    void Update ()
    {

        //is enemy sinking?
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);  //moves enemy down through the floor
        }
    }

    //public to call from other scripts
    public void TakeDamage (int amount, Vector3 hitPoint)  //parameters for damage taken and where the enemy is for particles to find
    {
        if(isDead)  //if dead return and do nothing beyond this
            return;

        //if not dead
        enemyAudio.Play ();  //plays enemy's sound

        currentHealth -= amount;  //takes health away

        float updatedHealth = (float)currentHealth / startingHealth;  //converts int to float

        enemyHealthBar.fillAmount = updatedHealth;
        HealthColorTransition();


        hitParticles.transform.position = hitPoint;  //gets particles transform changes it to the vector3 hitPoint
        hitParticles.Play();  //plays particle

        if(currentHealth <= 0)  //checks if dead
        {
            Death ();
        }
    }

    private void HealthColorTransition()
    {
        if (currentHealth < 50)
        {
            enemyHealthBar.color = Color.Lerp(Color.red, Color.yellow, currentHealth * Time.deltaTime);
        }
        else
        {
            enemyHealthBar.color = Color.Lerp(Color.yellow, Color.green, (currentHealth - 50) * Time.deltaTime);
        }
    }

    void Death ()
    {
        isDead = true;  //is enemy dead?

        capsuleCollider.isTrigger = true;  //used to allow player to move through enemy after the enemy is dead

        anim.SetTrigger ("Dying");  //calls trigger to animate

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;  //turns off navmesh agent for enemy so they stop tracking player
        GetComponent <Rigidbody> ().isKinematic = true;  //makes rigidbody kinematic so that system doesnt try and adjust for colliding and bug out
        isSinking = true;  //is enemy sinking?
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);  //destroys after 2 seconds
    }
}
