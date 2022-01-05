using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTeleportActive : MonoBehaviour
{
    public LaserReceiver laserReceiver;
    public GameObject laserON;
    public GameObject laserOFF;
    public GameObject teleporterTrigger;

    void Update()
    {
        if (laserReceiver.GetCurrentlyActive() == true)
        {
            laserON.SetActive(true);
            laserOFF.SetActive(false);
            teleporterTrigger.SetActive(true);
        }
        if (laserReceiver.GetCurrentlyActive() == false)
        {
            laserON.SetActive(false);
            laserOFF.SetActive(true);
            teleporterTrigger.SetActive(false);
        }
    }
}
