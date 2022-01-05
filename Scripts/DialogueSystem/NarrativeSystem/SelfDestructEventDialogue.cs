using UnityEngine;

public class SelfDestructEventDialogue : MonoBehaviour
{
    [Tooltip("The Self Destruct Event Dialogue")]
    public DialogueTrigger dialogue;
    [Tooltip("Add the DialogueManager Prefab")]
    public DialogueManager dialogueManager;
    [Tooltip("Write at what time you want the event to occure")]
    public float whatTimeToActivate;
    void Update()
    {
        if (dialogueManager.timeLeft <= -whatTimeToActivate)
        {
            dialogue.TriggerDialogue();
        }
    }
}
