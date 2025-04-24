using System;
using System.Collections;
using System.Collections.Generic;
using TwoBitMachines.FlareEngine;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int increasedHealth;
    private bool canEat;
    private Health health;
    private Transform mouth;

    private void Update()
    {
        if(canEat)
            if(Input.GetKeyDown(KeyCode.E))
                BeEaten();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            health = other.GetComponent<Health>();
            mouth = other.transform;
            canEat = true;
            other.GetComponentInChildren<Eat>()?.ToggleEat();
            UIManager.Instance.interactTipEnable(transform);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canEat = false;
            other.GetComponent<Eat>()?.ToggleEat();
            UIManager.Instance.interactTipDisable();
        }
    }

    private void BeEaten()
    {
        transform.position=mouth.position;
        Invoke("DestroyMyself", 0.5f);
    }

    void DestroyMyself()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        health.SetValue(health.currentValue + increasedHealth);
        UIManager.Instance.interactTipDisable();
    }
}
