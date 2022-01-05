using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBlock : MonoBehaviour
{
    public GameObject greenLight;
    public bool puzzleComplete;



    private void OnTriggerStay(Collider other)
    {
        greenLight.SetActive(true);
        puzzleComplete = true;
    }
    private void OnTriggerExit(Collider other)
    {

        greenLight.SetActive(false);
        puzzleComplete = false;
    }


}
