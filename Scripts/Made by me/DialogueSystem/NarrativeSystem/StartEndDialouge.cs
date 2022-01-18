using UnityEngine;

public class StartEndDialouge : MonoBehaviour
{
    public CallBlock callBlock;
    public ZoneManager zoneManager;

    private void Update()
    {
        if (callBlock.GetCurrentlyActive() == true)
        {
            zoneManager.enabled = true;
        }
    }
}
