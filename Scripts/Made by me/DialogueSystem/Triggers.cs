using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triggers : MonoBehaviour
{
    public static Triggers triggers { get; private set; }
    public GameObject boxTriggers;
    private int playerInRoom = 0;
    /// 0 / Hub
    /// 1 / Electrical
    /// 2 / CryoSleep
    /// 3 / Therapy
    /// 4 / Kitchen
    /// 5 / Oxygen1
    /// 6 / Oxygen2
    /// 7 / Hallway
    /// 8 / Bridge

    private void Awake() { triggers = this; }





}
