using System;
using System.Collections;
using System.Collections.Generic;
using TwoBitMachines.FlareEngine;
using UnityEngine;

public class IcePiece : MonoBehaviour
{
    public float damage = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log(other.gameObject.name);
                   if (other.gameObject.name == "Player")
                   {
                       var health = other.gameObject.GetComponent<Health>();
                       health.SetValue(health.GetValue() - damage);
                       Debug.Log("damage");
                       
                   }
    }

   
}
