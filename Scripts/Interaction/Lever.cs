using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    private bool turnedOn = false;

    public void TurnOn(bool b)
    {
        turnedOn = b;
    }
}
