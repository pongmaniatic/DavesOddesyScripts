using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private Animator animator;

    private void Awake() => animator = GetComponent<Animator>();

    public void AnimateDoors(bool open)
    {
        animator.SetBool("Open", open);
    }
}
