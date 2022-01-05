using UnityEngine;

public class StayInRoomTrigger : MonoBehaviour
{
    [Tooltip("Time in seconds before dialogue is played")]
    public float timeBeforeDialogue1;
    [Tooltip("The dialogue to play")]
    public DialogueTrigger dialogue1;
    [Tooltip("Time in seconds before dialogue is played")]
    public float timeBeforeDialogue2;
    [Tooltip("The dialogue to play")]
    public DialogueTrigger dialogue2;
    [Tooltip("Time in seconds before dialogue is played")]
    public float timeBeforeDialogue3;
    [Tooltip("The dialogue to play")]
    public DialogueTrigger dialogue3;
    [Tooltip("Time in seconds before dialogue is played")]
    public float timeBeforeDialogue4;
    [Tooltip("The dialogue to play")]
    public DialogueTrigger dialogue4;
    [Tooltip("Time in seconds before dialogue is played")]
    public float timeBeforeDialogue5;
    [Tooltip("The dialogue to play")]
    public DialogueTrigger dialogue5;




    private float timeElapsed;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            timeElapsed += Time.fixedDeltaTime;
            if (timeElapsed > timeBeforeDialogue1)
            {
                dialogue1.TriggerDialogue();
            }
            if (timeElapsed > timeBeforeDialogue2)
            {
                dialogue2.TriggerDialogue();
            }
            if (timeElapsed > timeBeforeDialogue3)
            {
                dialogue3.TriggerDialogue();
            }
            if (timeElapsed > timeBeforeDialogue4)
            {
                dialogue4.TriggerDialogue();
            }
            if (timeElapsed > timeBeforeDialogue5)
            {
                dialogue5.TriggerDialogue();
            }

        }
    }
}
