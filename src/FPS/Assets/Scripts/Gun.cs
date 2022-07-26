using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Gun : MonoBehaviour
{


//All of the variables are declared here
    public float damage = 10f;
    public float range = 100f;
    public float impactForce = 30f;
    public float fireRate = 15f;


    public Text ammoDisplay;
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;


    public Camera fpsCam;
    public ParticleSystem muzzleFlash; 
    public GameObject impactEffect;
    public float bulletSpeed = 1f;
    private GameObject clone;
    private float nextTimeToFire = 0.1f;
    public GameObject bullet;
    public Animator animator;

    AudioSource bang;
 


//When the game starts it sets the players current ammo to the maximum amount and also gives the gun permission to play the attached audio source
    void Start()
    {
        currentAmmo = maxAmmo;
        bang = GetComponent<AudioSource>();
  
    }


//This stops the gun from playing the reload animation
    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    // Update is called once per frame

    //This starts the reload, plays the guns sound effect and fires bullets and also deletes them after the specified time has passed
    void Update()
    {
        if (isReloading)
        return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            clone = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
            bang.Play();
        }

        Destroy(clone, 2f);
        ammoDisplay.text = currentAmmo.ToString();

    }

    IEnumerator Reload ()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        animator.SetBool("Reloading", true);

        yield return new WaitForSeconds(reloadTime - .25f);

        animator.SetBool("Reloading", false);

        yield return new WaitForSeconds(.25f);

        currentAmmo = maxAmmo;
        isReloading = false;
    }



//This subtracts ammo from the gun and casts a ray directly from the camera
//and also checks to see if the enemy has been hit and if enemy has been hit it
//makes the enemy lose health
void Shoot ()

{

currentAmmo--;

    muzzleFlash.Play(); 
    RaycastHit hit;
   if( Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
   {
       Debug.Log(hit.transform.name);

       Enemy enemy = hit.transform.GetComponent<Enemy>();
       if (enemy != null)
       {
           enemy.TakeDamage(damage);
       }


       //This applies force to the enemy when it is hit by the gun

     GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
    Destroy(impactGO, 2f);

        if  (hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);

        }
   }

}

}
