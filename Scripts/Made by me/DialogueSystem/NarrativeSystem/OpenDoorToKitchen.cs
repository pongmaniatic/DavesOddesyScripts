using UnityEngine;

public class OpenDoorToKitchen : MonoBehaviour
{
    public DoorOpener doorOpener;
    public float timer;
    public float timeToOpenDoor;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timer += Time.deltaTime;
        }
        if (timer > timeToOpenDoor)
        {
            doorOpener.AnimateDoors(true);
        }
    }
}
