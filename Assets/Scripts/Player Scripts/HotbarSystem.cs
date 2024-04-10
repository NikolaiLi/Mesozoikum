using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarSystem : MonoBehaviour
{

    public GameObject Slot1;
    public GameObject Slot2;
    public GameObject Slot3;
    public GameObject Slot4;
    public GameObject Slot5;

    void Start()
    {

    }

    void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            Equip1();
        }

        if(Input.GetKeyDown("2"))
        {
            Equip2();
        }

        if(Input.GetKeyDown("3"))
        {
            Equip3();
        }
        
        if(Input.GetKeyDown("4"))
        {
            Equip4();
        }     

        if(Input.GetKeyDown("5"))
        {
            Equip5();
        }     
    }

    void Equip1()
    {
        Slot1.SetActive(true);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        Slot4.SetActive(false);
        Slot5.SetActive(false);
    }

    void Equip2()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(true);
        Slot3.SetActive(false);
        Slot4.SetActive(false);
        Slot5.SetActive(false);

    }

    void Equip3()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(true);
        Slot4.SetActive(false);
        Slot5.SetActive(false);
    }

    void Equip4()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        Slot4.SetActive(true);
        Slot5.SetActive(false);
    }

    void Equip5()
    {
        Slot1.SetActive(false);
        Slot2.SetActive(false);
        Slot3.SetActive(false);
        Slot4.SetActive(false);
        Slot5.SetActive(true);
    }
}