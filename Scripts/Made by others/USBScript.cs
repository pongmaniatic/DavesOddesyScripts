using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USBScript : MonoBehaviour
{
    public Renderer computerScreen;

    public void FixComputer()
    {
        computerScreen.material.color = Color.green;
    }
}
