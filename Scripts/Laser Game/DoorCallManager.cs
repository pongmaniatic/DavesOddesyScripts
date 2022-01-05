using System;
using UnityEngine;

public class DoorCallManager : MonoBehaviour
{
    public DoorConnection[] connections;

    private bool openAllDoors;

    private void Update()
    {
        if (!openAllDoors)
            CheckDoors();
    }

    public void OpenAllDoors()
    {
        Debug.Log("open all doors");
        openAllDoors = true;
        foreach (DoorConnection connection in connections)
            connection.door.AnimateDoors(true);
    }

    private void CheckDoors()
    {
        foreach (DoorConnection connection in connections)
        {
            //if (!connection.door.Equals(door)) return;

            bool open = false;
            for (int i = 0; i < connection.callBlock.Length; i++)
            {
                if (connection.callBlock[i].GetCurrentlyActive())
                {
                    open = true;
                    break;
                }
            }
            if (open)
                connection.door.AnimateDoors(true);
            else
                connection.door.AnimateDoors(false);
        }
    }
}

[Serializable]
public struct DoorConnection
{
    public DoorOpener door;
    public CallBlock[] callBlock;
}
