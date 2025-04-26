using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrigger : MonoBehaviour
{
    bool first = false;

    public GameObject ice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!first && collision.gameObject
                .name == "Player")
        {
            first = true;
            ice.GetComponent<Ice>().drop();
        }
    }
}
