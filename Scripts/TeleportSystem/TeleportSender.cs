using UnityEngine;
[RequireComponent(typeof(Collider))]
public class TeleportSender : MonoBehaviour
{
    private bool canTeleport;
    private string teleportationObjectTag;
    private Vector3 teleportPosition;

    public AudioClip teleportSFX;
    public float teleportSFXVolume;

    public void TeleportationTag(string teleportationObjectTag)
    {
        this.teleportationObjectTag = teleportationObjectTag;
    }

    public void TeleportPosition(Vector3 teleportPosition)
    {
        this.teleportPosition = teleportPosition;
    }

    public void CanTeleport(bool canTeleport)
    {
        this.canTeleport = canTeleport;
    }
    private void OnTriggerStay(Collider teleportationObject)
    {

        if (canTeleport && teleportationObjectTag != null && teleportPosition != null &&
            teleportationObject.tag == teleportationObjectTag) 
        {
            teleportationObject.transform.position = teleportPosition;

            Debug.Log("Sending to position: " + teleportPosition);

            PlayerGenericSFX.Instance.PlayRandomPitchSFX(teleportSFX, teleportSFXVolume, 0.75f, 1.0f);
        }
        
    }
}
