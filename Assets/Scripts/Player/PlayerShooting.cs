using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;                  
    public float timeBetweenBullets = 0.15f;        
    public float range = 100f;                      

    float timer;                                    
    Ray shootRay;                                   
    RaycastHit shootHit;                            
    int shootableMask;                             
    ParticleSystem gunParticles;                    
    LineRenderer gunLine;                           
    AudioSource gunAudio;                           
    Light gunLight;                                 
    float effectsDisplayTime = 0.2f;                

    void Awake()
    {
        // getmask
        shootableMask = LayerMask.GetMask("Shootable");

        // mendapatkan refrence component
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        // disble line renderer
        gunLine.enabled = false;

        // disable light
        gunLight.enabled = false;
    }

    public void Shoot()
    {
        timer = 0f;

        // play audio 
        gunAudio.Play();

        // enable light
        gunLight.enabled = true;

        // play gun particle
        gunParticles.Stop();
        gunParticles.Play();

        // enable line renreder dan set first position
        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        // set posisi ray shoot dan direction
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        // lakukan raycast jika mendeteksi id enemy hit apapun
        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // lakukan tag damage
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }

            // set line and position ke hit position
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            // set line end position ke range from barrel
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }
    }
}