using System;
using System.Collections;
using System.Collections.Generic;
using TwoBitMachines.TwoBitSprite;
using UnityEngine;

public class Eat : MonoBehaviour
{
    private SpriteEngine spriteEngine;

    private void Start()
    {
        spriteEngine = GetComponent<SpriteEngine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            spriteEngine.SetSignal("Eat");
        }
    }
}
