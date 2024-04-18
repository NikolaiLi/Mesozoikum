using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossHealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    [SerializeField] private Transform player;
    public GameObject Bar;
    public AudioSource screamSound;

    void Start()
    {
        
    }

    float GetDistanceToPlayer()
    {
        return
            (player.position - transform.position)
            .magnitude;
    }

    void Update()
    {
        if (GetDistanceToPlayer() < 50)
        {
            Bar.SetActive(true);
        }

        if (healthAmount <= 0)
        {
            Destroy (gameObject);
            Bar.SetActive(false);
        }
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

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Weapon") 
        {
            Debug.Log("Collided with " + other.gameObject.name);
            TakeDamage(5);
            screamSound.enabled = true;
        };
    }

    public void OnTriggerExit(Collider other) 
    {
        screamSound.enabled = false;
    }
}
