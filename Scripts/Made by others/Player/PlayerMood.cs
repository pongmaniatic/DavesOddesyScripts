using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMood : MonoBehaviour
{
    [Tooltip("Put the \"Happy\"-material in this slot")]
    public Material happy;
    [Tooltip("Put the \"Angry\"-material in this slot")]
    public Material angry;
    [Tooltip("Put the \"Sad\"-material in this slot")]
    public Material sad;
    [Tooltip("Put the \"plane\" in this slot")]
    public Renderer emoteBackground;

    [Tooltip("Change face with the keys\"A\" \"S\" \"D\", and \"F\" \n when doing bug tests.")]
    public int removeVariableWhenDoneWithScript;

    private void Update() //Remove this function when the emotes been properly tested
    {
        if (Input.GetKey(KeyCode.A)) SadFace();
        if (Input.GetKey(KeyCode.S)) HappyFace();
        if (Input.GetKey(KeyCode.D)) AngryFace();
        if (Input.GetKey(KeyCode.F)) NoFace();
        
    }

    public void SadFace() //Call this function to make a sad face
    {
        emoteBackground.material = sad;
    }    
    public void HappyFace()//Call this function to make a happy face
    {
        emoteBackground.material = happy;
    }    
    public void AngryFace()//Call this function to make an angry face
    {
        emoteBackground.material = angry;
    }

    public void NoFace()//Call this function to make no face
    {
        emoteBackground.material = null;
    }
}
