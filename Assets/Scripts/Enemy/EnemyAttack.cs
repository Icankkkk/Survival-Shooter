using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;


    void Awake ()
    {
        // mencari game object dengan tag "Player"
        player = GameObject.FindGameObjectWithTag ("Player");

        // mendapatkan komponen player health
        playerHealth = player.GetComponent <PlayerHealth> ();

        // mendapatkan komponen animator
        anim = GetComponent <Animator> ();

        // mendapatkan enemy health
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // callback jika ada suatu object masuk kedalam trigger
    void OnTriggerEnter (Collider other)
    {
        // set player dalam range
        if(other.gameObject == player && other.isTrigger == false)
        {
            playerInRange = true;
        }
    }

    // callback jika ada object yang kelaur dari trigger
    void OnTriggerExit (Collider other)
    {
        // set player jika tidak dalam range
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        //mentrigger animasi PlayerDead jika darah player kurang dari sama dengan 0
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }


    void Attack ()
    {
        // reset timer
        timer = 0f;

        // taking damage
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
