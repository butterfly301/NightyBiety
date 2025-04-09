using System.Collections;
using System.Collections.Generic;
using TwoBitMachines.FlareEngine;
using UnityEngine;

public class Ice : MonoBehaviour
{
    public float shakeStrength;
    public float damage;
    void Start()
    {
        originRot = transform.rotation.eulerAngles;
    }
    float timer = 0;
    float num = 0;
    public float duration;
    int dir = 0;
    Vector3 originRot;
    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            if (dir == 0)
            {
                float t = timer / duration;
                num = Mathf.LerpUnclamped(-shakeStrength, shakeStrength, t);
                timer += Time.deltaTime;
            }
            else {
                float t = timer / duration;
                num = Mathf.LerpUnclamped(shakeStrength, -shakeStrength, t);
                timer += Time.deltaTime;
            }
        }
        else
        {
            if(dir == 0)
            {
                num = shakeStrength;
                timer = 0;
                dir = 1;
            }
            else
            {
                num = -shakeStrength;
                timer = 0;
                dir = 0;
            }

        }
        gameObject.transform.eulerAngles = originRot+ new Vector3(0,0,num);   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.name == "Player")
        {
            var health = PlayerController.instance.gameObject.GetComponent<Health>();
            health.SetValue(health.GetValue() - damage);
            Debug.Log("damage");
            
        }
    }
}
