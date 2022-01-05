using UnityEngine;

public class ReturnedToTutorial : MonoBehaviour
{
    public ReachedHub reachedHub;
    public DialogueTrigger dialogueTrigger;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && reachedHub.tutorialComplete == true)
        {
            dialogueTrigger.TriggerDialogue();
        }
    }



}
