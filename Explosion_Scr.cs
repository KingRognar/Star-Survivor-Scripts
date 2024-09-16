using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Scr : MonoBehaviour
{
    //private Animation anim;
    private Animator animator;
    private float animationTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("Explosion");
        //anim = GetComponent<Animation>();
        //anim.Play("Explosion");
        //animationTime = anim.clip.length;
    }
    private void Start()
    {
        animationTime = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        Destroy(gameObject, animationTime);
    }
}
