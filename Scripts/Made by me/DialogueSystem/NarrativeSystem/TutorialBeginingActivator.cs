using UnityEngine;

public class TutorialBeginingActivator : MonoBehaviour
{
    public DialogueTrigger firstStoryDialouge;
    public float waitTime;
    public ZoneManager zoneManager;
    public float waitTimeZone;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Invoke("Wait", waitTime);
            Invoke("Activate", waitTimeZone);
        }
    }
    private void Wait()
    {
        firstStoryDialouge.TriggerDialogue();
    }
    private void Activate()
    {
        zoneManager.enabled = true;
    }
}
