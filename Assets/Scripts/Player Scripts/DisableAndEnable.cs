using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAndEnable : MonoBehaviour
{
    private bool active = false;
    public float activeTime = 0.5f;
    private float timer = 0;
    public Collider Tool;

    void Start() {
        Tool.enabled = false;
        active = false;
    }
 
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && active == false) 
        {
            Tool.enabled = true;
            active = true;
            timer = activeTime;
        }

        
        if(active) {
            timer -= Time.deltaTime;
        }
        if(active && timer <= 0) {
            active = false;
            Tool.enabled = false;
        }
    }
}
