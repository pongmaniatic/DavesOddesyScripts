using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventBlock : MonoBehaviour
{
    public bool isDoor;
    public bool twoDoors;
    public int doorToOpen;
    public int secondDoorToOpen;
    public bool finishedAfterOneCollision;
    public DoorManager doorManager;
    public GameObject activeIndicator;
    public GameObject inactiveIndicator;
    public bool hasBatteryAlternative;
    public ItemHolderObject batteryCompartment;
    public bool isHit;
    public bool areDoorsOpen;

    private void Start()
    {
        if (isDoor)
        {
            doorManager = doorManager.GetComponent<DoorManager>();

            if (batteryCompartment != null)
            {
                if (hasBatteryAlternative)
                {
                    batteryCompartment = batteryCompartment.GetComponent<ItemHolderObject>();
                }
            }
        }
    }

    private void Update()
    {
        if (hasBatteryAlternative)
        {
            bool hasBattery = batteryCompartment.holdsItem;
            if (hasBattery)
            {
                if (!areDoorsOpen)
                {
                    OpenTheDoors();
                    
                }
            }
            else
            {
                if (!isHit)
                {
                    if (areDoorsOpen)
                    {
                        CloseTheDoors();
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            isHit = true;
            if (!areDoorsOpen)
            {
                OpenTheDoors();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Detector"))
        {
            isHit = false;
            if (areDoorsOpen)
            {
                CloseTheDoors();
            }
        }  
    }

    public void OpenTheDoors()
    {
        if (isDoor)
        {
            activeIndicator.SetActive(true);
            inactiveIndicator.SetActive(false);
            doorManager.door = doorToOpen;
            doorManager.OpenDoors();
            if (twoDoors)
            {
                doorManager.door = secondDoorToOpen;
                doorManager.OpenDoors();
            }
            areDoorsOpen = true;
        }
    }

    public void CloseTheDoors()
    {
        if (isDoor)
        {
            activeIndicator.SetActive(false);
            inactiveIndicator.SetActive(true);
            doorManager.door = doorToOpen;
            doorManager.CloseDoors();
            if (twoDoors)
            {
                doorManager.door = secondDoorToOpen;
                doorManager.CloseDoors();
            }
            areDoorsOpen = false;
        }
    }
}
