using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    public GameObject Food;
    public Animator animator;
    public AudioSource biteSound;
    public AudioSource tailSound;
    private bool active = false;
    public float activeTime = 0.5f;
    private float timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Food.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Heal(2);
        }
        
        if(active) 
        {
            timer -= Time.deltaTime;
        }

        if(active && timer <= 0) 
        {
            active = false;
            biteSound.enabled = false;
            tailSound.enabled = false;
        }
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "CollideChecker") 
        {
            TakeDamage(40);

            if(active == false)
            {
                biteSound.enabled = true;
                active = true;
                timer = activeTime;
            }
        }

        if(other.gameObject.tag == "Tail") 
        {
            TakeDamage(40);

            if(active == false)
            {
                tailSound.enabled = true;
                active = true;
                timer = activeTime;
            }
        }

        if(other.gameObject.tag == "Boss") 
        {
            TakeDamage(0);
            animator.SetBool("bite", true);
        }

        if(other.gameObject.tag == "Enemy") 
        {
            TakeDamage(20);
        }

        Debug.Log("!!!Collided with " + other.gameObject.name);
    }

    public void OnTriggerExit(Collider other) 
    {
        Debug.Log("!!!Stopped colliding with " + other.gameObject.name);
        animator.SetBool("bite", false);
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }
}
