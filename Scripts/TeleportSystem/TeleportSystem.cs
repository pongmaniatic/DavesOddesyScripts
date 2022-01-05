using UnityEngine;

public class TeleportSystem : MonoBehaviour
{
    
    public TeleportSender teleportSender;
    public TeleportReceiver teleportReceiver;
    [Tooltip("Write the tag of the object to teleport")]
    public string tagToTeleport;
    
    public bool canTeleport;
    private void OnEnable()
    {
        SetReceiver();
        TeleportationTag();

            teleportReceiver.SetTag(tagToTeleport);
        
    }

    private void Update()
    {
        ActivateTeleport();
    }

    public void ActivateTeleport()
    {
        teleportSender.CanTeleport(canTeleport);
    }
    public void SetReceiver()
    {
        teleportSender.TeleportPosition(teleportReceiver.transform.position);
    }

    public void TeleportationTag()
    {
        teleportSender.TeleportationTag(tagToTeleport);
    }
}
