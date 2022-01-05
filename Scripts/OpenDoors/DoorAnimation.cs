using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    [Tooltip("Put the door animator for the door to open here")]
    public Animator doorAnimator;
    
    public void AnimateDoors( bool open )
    {
        doorAnimator.SetBool("OpenDoor", open);
    }
}
