using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
   // public Slider healthSlider;  //reference to slider bar
    public Image healthImage;  //reference to health Image
    public Image damageImage;  //reference to damage image that was used
    public AudioClip deathClip;  //sound click for death
    public Color flashColour = new Color(1f, 0f, 0f, .1f);  //the color the damageimage will flash, (R, G, B, Opacity)
    public Text healthText;  //reference to health

    public int startingHealth = 100;  //starting health
    public int currentHealth;  //current health
    public float flashSpeed = 5f;  //speed that damageimage flashes


    Animator anim;  //reference to player animation
    AudioSource playerAudio;  //reference to plyayer audio
    PlayerMovement playerMovement;  //reference to player movement script
    PlayerShooting playerShooting;

    bool isDead;  //is player dead
    bool damaged;  //is player damaged


    void Awake ()
    {
        //gets components
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        healthText = healthText.GetComponentInChildren<Text>();

        currentHealth = startingHealth; //makes current healh same as starting health when game starts
        print("Game Starting Health: " + currentHealth);
    }


    void Update ()
    {

        //flashes damageimage indicating you are taking damage
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            //lerp alls floating values to smoothly transition from one value to another
            //Color.Lerp (color A, color B, float used for transition
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    //public so othe scripts can use it
    //used for damage when player is attacked by enemy
    public void TakeDamage (int amount)
    {
        damaged = true;  //is taking damage?

        currentHealth -= amount;  //subtracts health

        // healthSlider.value = currentHealth;  //reflects to slider

        float updatedHealth = (float)currentHealth / startingHealth;  //converts int to float
        healthImage.fillAmount = updatedHealth;  //reflects to image
        healthText.text = currentHealth.ToString();
        HealthColorTransition();

        playerAudio.Play ();  //plays sound when damaged

        if(currentHealth <= 0 && !isDead)  //checks if player is dead
        {
            Death ();  //calls death function
        }
    }

    //used to change color of health text based on health value
    private void HealthColorTransition()
    {
        if (currentHealth < 50)
        {
            healthText.color = Color.Lerp(Color.red, Color.yellow, currentHealth * Time.deltaTime);
        }
        else
        {
            healthText.color = Color.Lerp(Color.yellow, Color.green, (currentHealth - 50) * Time.deltaTime);
        }
    }

    void Death ()
    {
        isDead = true;  //is player dead?

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Dying");  //plays death animation

        playerAudio.clip = deathClip;  //store eath sound
        playerAudio.Play ();  //plays death sound

        playerMovement.enabled = false;  //stops player from moving
        playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);  //loads level
    }
}
