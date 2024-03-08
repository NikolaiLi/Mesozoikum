using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public float healthAmount = 100f;
    public GameObject player;


    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log("Health: " + healthAmount);
        if (healthAmount <= 0)
        {
            Destroy (gameObject);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(5);
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

    public void OnTriggerEnter(Collider other) {
        Debug.Log("Collided with " + other.gameObject.name);
        TakeDamage(20);
    }

    public void OnTriggerExit(Collider other) {
        Debug.Log("Stopped colliding with " + other.gameObject.name);
    }
}
