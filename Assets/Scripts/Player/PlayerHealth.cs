using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100f;  //starting health
    public float currentHealth;  //current health
    public Slider healthSlider;  //reference to slider bar
    public Image damageImage;  //reference to damage image that was used
    public AudioClip deathClip;  //sound click for death
    public float flashSpeed = 5f;  //speed that damageimage flashes
    public Color flashColour = new Color(1f, 0f, 0f, .1f);  //the color the damageimage will flash, (R, G, B, Opacity)


    Animator anim;  //reference to player animation
    AudioSource playerAudio;  //reference to plyayer audio
    PlayerMovement playerMovement;  //reference to player movement script
    //PlayerShooting playerShooting;
    bool isDead;  //is player dead
    bool damaged;  //is player damaged


    void Awake ()
    {
        //gets components
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();

        currentHealth = startingHealth * .01f; //makes current healh same as starting health when game starts
    }


    void Update ()
    {

        //flashes damageimage indicating you are taking damage
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            //lerp alls floating values to smoothly transition from one value to another
            //Color.Lerp (color A, color B, float used for transition
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }

    //public so othe scripts can use it
    //used for damage when player is attacked by enemy
    public void TakeDamage (float amount)
    {
        damaged = true;  //is taking damage?

        currentHealth -= amount * .01f;  //subtracts health

        healthSlider.value = currentHealth;  //reflects to slider

        playerAudio.Play ();  //plays sound when damaged

        if(currentHealth <= 0 && !isDead)  //checks if player is dead
        {
            Death ();  //calls death function
        }
    }


    void Death ()
    {
        isDead = true;  //is player dead?

        //playerShooting.DisableEffects ();

        anim.SetTrigger ("Dying");  //plays death animation

        playerAudio.clip = deathClip;  //store eath sound
        playerAudio.Play ();  //plays death sound

        playerMovement.enabled = false;  //stops player from moving
        //playerShooting.enabled = false;
    }


    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);  //loads level
    }
}
