using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger2 : MonoBehaviour
{
    bool first = false;

    public GameObject ice1;
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
            IntroManager.instance.ShowSecondStartMessage();
            ice1.GetComponent<Ice>().drop();
            
        }
    }
}
