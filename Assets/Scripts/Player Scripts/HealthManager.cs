using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    void Start()
    {
        
    }

    void Update()
    {
        if (healthAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(5);
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
}
