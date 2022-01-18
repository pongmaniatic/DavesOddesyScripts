using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    public PickUpSystem1 pickUpSystem;

    public AudioClip pickupElectroLoopSFX;
    public float pickupSFXVolume;


    private bool isPlayPickupSFX;
    private bool isPlayDropSFX;


    public GameObject electroLeft;
    public GameObject electroRight;

    private void Update()
    {
        if (pickUpSystem.currentlyHeldObject == null)
        {
            animator.SetBool("Grabing", false);
            if (!isPlayDropSFX)
            {
                PlayerGenericSFX.Instance.thirdAudioSourceLoop.Stop();
                isPlayDropSFX = true;
                isPlayPickupSFX = false;
                electroLeft.SetActive(false);
                electroRight.SetActive(false);
            }
        }
        else
        {
            animator.SetBool("Grabing", true);
            if (!isPlayPickupSFX)
            {
                PlayerGenericSFX.Instance.PlayLoopSFX(pickupElectroLoopSFX, pickupSFXVolume);
                isPlayPickupSFX = true;
                isPlayDropSFX = false;
                electroLeft.SetActive(true);
                electroRight.SetActive(true);
            }
        }
    }

}
