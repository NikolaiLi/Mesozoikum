using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkAnimation : MonoBehaviour
{
    Animator animator;
    public AudioSource footstepsSound;
    public AudioSource windSound;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        windSound.enabled = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            animator.SetBool("walking", true);
            footstepsSound.enabled = true;
        }
        else
        {
            animator.SetBool("walking", false);
            footstepsSound.enabled = false;
        }
    }
}