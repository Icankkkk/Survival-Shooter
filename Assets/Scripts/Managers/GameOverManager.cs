using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text warningText;
    public PlayerHealth playerHealth;       
    public float restartDelay = 5f;

    AudioSource bgm;

    Animator anim;                          
    float restartTimer;
    bool isDead;

    void Awake()
    {
        anim = GetComponent<Animator>();
        bgm = GameObject.Find("BackGroundMusic").GetComponent<AudioSource>();
    }


    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            // debug untuk log mati
            if (!isDead)
            {
                Debug.Log("Game over is active");
                anim.SetTrigger("GameOver");
                isDead = true;
                bgm.Stop();
            }

            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = string.Format("! {0} m", Mathf.RoundToInt(enemyDistance));        
        anim.SetBool("Warning", true);
    }

   
    public void HideWarning()
    {
        anim.SetBool("Warning", false);
    }
    
    
}