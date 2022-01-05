using System;
using System.Collections;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public enum FogRoom
    {
        Tutorial,
        Hub,
        Bridge,
        Hallway,
        Electrical,
        Cryosleep,
        Oxygen,
        Therapy,
        Kitchen,
        Bedroom
    };
    [Tooltip("Set this variable to the room that should be revealed")]
    public FogRoom roomTorReveal;
    [Tooltip("Set this variable to the room that should be hidden")]
    public FogRoom roomToDim;
    [Tooltip("Array for the blocking objects's renderer.")]
    public Renderer[] fogOfWarObjects;
    [Tooltip("The alpha value of the blocking object.\n\"1\" to hide room, \"0\" to reveal room")] 
    [Range(0f, 1f)] public float alfa = 0;

    public void HideObject(int fogToHide) //Call this function to hide rooms.
    {
        roomToDim = (FogRoom) fogToHide;
        fogOfWarObjects[(int)roomToDim].material.color = new Color(1f, 1f, 1f, alfa);
    }

    public void RevealRoom(int fogToReveal) //Call this fuction to reveal rooms.
    {
        roomTorReveal = (FogRoom) fogToReveal;
        fogOfWarObjects[(int)roomTorReveal].material.color = new Color(1f, 1f, 1f, alfa);
    }

}
