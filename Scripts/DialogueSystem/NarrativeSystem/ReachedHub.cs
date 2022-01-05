using UnityEngine;

public class ReachedHub : MonoBehaviour
{
    public QueueGenerator tipTurorial;
    public QueueGenerator tutorialZone1;
    public bool tutorialComplete;
    public GameObject teleporterToTutorial;
    public float waitTime;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tipTurorial.queueActive = false;
            tutorialZone1.queueActive = false;
            tutorialComplete = true;
            teleporterToTutorial.SetActive(false);
            Invoke("ActivateTele", waitTime);
        }
    }
    private void ActivateTele()
    {
        teleporterToTutorial.SetActive(true);
    }

}
