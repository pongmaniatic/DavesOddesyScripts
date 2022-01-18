using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerEnter : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {  dialogueTrigger.TriggerDialogue(); }
    }
}
