using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public bool OneTimeOnly = true;
    // This tracks the number of times this dialogue has appeared.
    public DialogueGenerator dialogue;
    // Dialogue appears only once or every time the trigger activates.
    private int numberOfActivations = 0;


    public void TriggerDialogue()
    {
        if (OneTimeOnly)
        {
            if (numberOfActivations == 0) 
            {
                ActivateDialogue();
            }
        }
        else
        {
            ActivateDialogue();
        }
    }

    void ActivateDialogue()
    {
        DialogueManager.dialogueManager.AddDialogueToQueue(dialogue);
        numberOfActivations += 1;
    }

}
