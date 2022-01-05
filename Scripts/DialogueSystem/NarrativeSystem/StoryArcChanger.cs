using UnityEngine;

public class StoryArcChanger : MonoBehaviour
{
    public QueueGenerator act1;
    public QueueGenerator act2;
    public QueueGenerator act3;
    public CallBlock arc3;
    public ZoneManager zoneManager;

    private void Start()
    {
        act1.queueActive = true;
        act2.queueActive = true;
    }

    private void Update()
    { 
        if (arc3.GetCurrentlyActive() == true)
        {
            ActivateAct3();
        }
    }

    public void ActivateAct2()
    {
        act1.queueActive = false;
        act2.queueActive = true;
    }
    public void ActivateAct3()
    {
        act2.queueActive = false;
        act3.queueActive = true;
        zoneManager.paceTimer = 1;
    }
    public void EndAct3()
    {
        act1.queueActive = false;
        act2.queueActive = false;
        act3.queueActive = false;
    }

}
