using System;
using System.Collections;
using System.Collections.Generic;
using TwoBitMachines.FlareEngine;
using TwoBitMachines.TwoBitSprite;
using UnityEngine;

public class Eat : MonoBehaviour
{
    private SpriteEngine spriteEngine;
    private Firearm firearm;

    private void Start()
    {
        firearm = GetComponent<Firearm>();
    }

    public void ToggleEat()
    {
        firearm.enabled = !firearm.enabled;
    }
    
}
