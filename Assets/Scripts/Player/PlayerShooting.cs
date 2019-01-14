using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;  //bullet damage
    public float timeBetweenBullets = 0.15f;  //shooting time / attackspeed
    public float range = 100f;  //effective range

    public GameObject bombPrefab;

    Ray shootRay = new Ray();  //raycast to find out what we hit
    RaycastHit shootHit;  //used to return what we hit
    ParticleSystem gunParticles;  //gunparticles
    LineRenderer gunLine;  //line for raycast
    AudioSource gunAudio;  //sound
    Light gunLight;  //bullet lights

    float timer;  //keey everythingin sync
    float effectsDisplayTime = 0.2f;  //life time of particles/effects
    int shootableMask;  //only click on the floor


    void Awake ()
    {

        //gets components
        shootableMask = LayerMask.GetMask ("Shootable");  //uses masks to find out which objects were marked shootable
        gunParticles = GetComponent<ParticleSystem> ();  //gets particles
        gunLine = GetComponent <LineRenderer> ();  //gets linerenderer
        gunAudio = GetComponent<AudioSource> ();  //gets audio
        gunLight = GetComponent<Light> ();  //gets light
    }


    void Update ()
    {
        timer += Time.deltaTime;  //timer used for processes and control attack speed

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)  //uses mouse left click to shoot
        {
            Shoot ();
        }

        if(Input.GetButton("Fire2"))
        {
            ShootBomb();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)  //used to disaable effects
        {
            DisableEffects ();
        }

    }

    //disables effects
    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;  //resets timer

        gunAudio.Play ();  //plays audio

        gunLight.enabled = true;  //turns on light

        //if particles are playing stop em and then start em
        gunParticles.Stop ();
        gunParticles.Play ();

        //turn on line renderer
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);  //access line positions

        //raycasting
        shootRay.origin = transform.position;  //starting position
        shootRay.direction = transform.forward;  //moves forward

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))  //uses physics to raycast out, (variable, what was hit, specified range, only hit  shootablemask)
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();  //whatever is hit, give me the enemyhealth script and store it here

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }

    void ShootBomb()
    {
        GameObject bombGO = Instantiate(bombPrefab, transform.position, transform.rotation);
    }

}
