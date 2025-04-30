using System;
using System.Collections;
using System.Collections.Generic;
using TwoBitMachines.FlareEngine;
using UnityEngine;

public class IcePiece : MonoBehaviour
{
    //来自同一个冰柱的碎片一起计算伤害，打中的碎片越多造成的伤害越高
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
                   if (other.gameObject.name == "Player")
                   {
                       var health = other.gameObject.GetComponent<Health>();
                       health.SetValue(health.GetValue() - damage);
                       Debug.Log("damage");
                       
                   }
    }

   
}
