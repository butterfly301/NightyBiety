using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    private bool isGrabed;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isGrabed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GrabItem") )
        {
            isGrabed = true;
            other.gameObject.transform.SetParent(transform);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("GrabItem")&&isGrabed)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((other.transform.position - transform.position).normalized, ForceMode2D.Force);
        }
    }
}
