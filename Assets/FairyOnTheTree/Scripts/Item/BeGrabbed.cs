using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeGrabbed : MonoBehaviour
{
    [Tooltip("包含Animation组件的子物体")]
    public Animation childAnimation;
    
    [Tooltip("正向动画片段")]
    public AnimationClip forwardClip;
    
    [Tooltip("反向动画片段")]
    public AnimationClip reverseClip;

    private void Awake()
    {
        if (childAnimation == null)
        {
            childAnimation = GetComponentInChildren<Animation>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (childAnimation != null && forwardClip != null)
        {
            childAnimation.clip = forwardClip;
            childAnimation.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (childAnimation != null && reverseClip != null)
        {
            childAnimation.clip = reverseClip;
            childAnimation.Play();
        }
    }
}
