using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerManager : MonoBehaviour
{
    public Image hungerBar;
    public float hungerAmount = 100f;
    private bool active = true;
    private float timer = 0;
    private float starvationInterval = 10f;
    public float starvationRate = 0.2f;
    public GameObject Food;

    void Start()
    {
        timer = starvationInterval;
    }

    void Update()
    {
        if (active) 
        {
            timer -= Time.deltaTime;

            if (timer <= 0) 
            {
                StarveDamage(starvationRate);
                timer = starvationInterval;
            }
        }

        if (hungerAmount <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Food.activeSelf && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Eating(5);
        }
    }

    public void StarveDamage(float starvingRate)
    {
        hungerAmount -= starvingRate;
        hungerBar.fillAmount = hungerAmount / 100f;
    }

    public void Eating(float eatingAmount)
    {
        hungerAmount += eatingAmount;
        hungerAmount = Mathf.Clamp(hungerAmount, 0, 100);

        hungerBar.fillAmount = hungerAmount / 100f;
    }
}