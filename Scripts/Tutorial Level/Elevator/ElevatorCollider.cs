using UnityEngine;

public class ElevatorCollider : MonoBehaviour
{
    public bool isActivated;
    private bool _isUsed;
    public EventBlock eventBlock;
    private bool _waitInterval;
    public TutorialElevator elevator;
    public FadeToBlack fadeToBlack;
    
    private void Start()
    {
        eventBlock = eventBlock.GetComponent<EventBlock>();
    }

    private void Update()
    {
        bool isItHit = eventBlock.isHit;
        if (isItHit == true)
        {
            isActivated = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isActivated)
            {
                if (!_isUsed)
                {
                    elevator.RideElevatorStart();
                    _isUsed = true;
                }
            }
        }
    }
}