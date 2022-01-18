using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySlot : MonoBehaviour
{
    public GameObject fakeBattery;
    public bool hasBattery = false;
    public bool CheckForBattery()
    {
        if (!hasBattery) { return false; }
        else { return false; }
        
    }


    public void PlaceBattery(){ fakeBattery.SetActive(true); hasBattery = true; } 
}
