using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float healthAmount = 100f;
    public GameObject player;
    public AudioSource screamSound;

    void Start()
    {
        
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            Destroy (gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        healthAmount -= damage;
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Weapon") 
        {
            Debug.Log("Collided with " + other.gameObject.name);
            TakeDamage(20);
            screamSound.enabled = true;
        }
        
    }

    public void OnTriggerExit(Collider other) 
    {
        Debug.Log("Stopped colliding with " + other.gameObject.name);
        screamSound.enabled = false;
    }
}