using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusPlugins : MonoBehaviour
{
    Animator animator;
    public AudioSource drinkSound;
    public GameObject drink;
    private bool active = false;
    public float activeTime = 0.5f;
    private float timer = 0;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        active = false;
    }

    void Update()
    {
        if (drink.activeSelf && Input.GetKeyDown(KeyCode.Mouse0) && active == false)
        {
            animator.SetBool("eating", true);
            drinkSound.enabled = true;
            active = true;
            timer = activeTime;
        }
        
        if(active) 
        {
            timer -= Time.deltaTime;
        }

        if(active && timer <= 0) 
        {
            active = false;
            animator.SetBool("eating", false);
            drinkSound.enabled = false;
        }
    }
}
