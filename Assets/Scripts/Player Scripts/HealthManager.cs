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
            Heal(5);
        }
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Boss") 
        {
            TakeDamage(20);
        }
        Debug.Log("!!!Collided with " + other.gameObject.name);
        animator.SetBool("bite", true);
        TakeDamage(20);
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
