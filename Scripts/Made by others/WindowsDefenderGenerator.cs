using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsDefenderGenerator : MonoBehaviour
{
    public BatterySlot batterySlot1;
    public BatterySlot batterySlot2;
    public GameObject itemToCreate;
    private bool itemCreated = false;

    private void Update()
    {
        if (!itemCreated)
        {
            // Debug.Log("hasBattery1: " + batterySlot1.hasBattery);
            // Debug.Log("hasBattery2: " + batterySlot2.hasBattery);
            if (batterySlot1.hasBattery && batterySlot2.hasBattery) { itemToCreate.SetActive(true); itemCreated = true; }
        }
    }
}
