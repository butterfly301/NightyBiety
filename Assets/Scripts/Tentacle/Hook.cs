using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private bool isGrabed;
    private Transform grabbedItemTransform;

    private void Update()
    {
        if (isGrabed)
        {
            grabbedItemTransform.transform.position = transform.position;
            grabbedItemTransform.transform.rotation = transform.rotation;
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            isGrabed = false;
            grabbedItemTransform = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer==LayerMask.NameToLayer("GrabItem")  && Input.GetMouseButton(1))
        {
            isGrabed = true;
            grabbedItemTransform=other.gameObject.transform;
        }
    }
}
