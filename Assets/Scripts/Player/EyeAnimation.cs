using System;
using System.Collections;
using System.Collections.Generic;
using TwoBitMachines.FlareEngine.ThePlayer;
using TwoBitMachines.TwoBitSprite;
using UnityEngine;

public class EyeAnimation : MonoBehaviour
{
    private Animator animator;
    private SpriteEngine spriteEngine;
    private SpriteRenderer spriteRendererPlayer;
    private SpriteRenderer spriteRendererEye;
    public GameObject eye;
    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteEngine = GetComponentInParent<SpriteEngine>();
        spriteRendererPlayer = GetComponentInParent<SpriteRenderer>();
    }

    private void Update()
    {
        CheckDirection();
        switch (spriteEngine.currentAnimation)
        {
            case "JumpUp":
                animator.Play("EyeJumpUp");
                break;
            case "JumpDown":
                animator.Play("EyeJumpDown");
                break;
            case "JumpUpRight":
                animator.Play("EyeJumpUpRight");
                break;
            case "JumpDownRight":
                animator.Play("EyeJumpDownRight");
                break;
            case "Eat":
                animator.Play("EyeEat");
                break;
            default:
                animator.Play("Empty");
                break;
        }
    }

    private void CheckDirection()
    {
        if(spriteRendererPlayer.flipX)
            spriteRendererPlayer.flipX = true;
        else
        {
            spriteRendererPlayer.flipX = false;
        }
    }
}