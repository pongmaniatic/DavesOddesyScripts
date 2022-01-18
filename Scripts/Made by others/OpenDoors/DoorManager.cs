using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class DoorManager : MonoBehaviour
{

    [Header("Doors")]
    [Tooltip("Put the parent objects of the doors in the array")]
    public DoorAnimation[] openDoor;
    [Tooltip("Which door index to open \nFirs element is: \"0\", second: \"1\" and so on")] public int door;
    public void OpenDoors()
    {
        openDoor[door].AnimateDoors(true);
    }
    public void CloseDoors()
    {
        openDoor[door].AnimateDoors(false);
    }

}
