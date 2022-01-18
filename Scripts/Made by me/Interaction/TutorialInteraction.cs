using UnityEngine;

public class TutorialInteraction : MonoBehaviour
{
    public GameObject detector;
    public GameObject line;
    public GameObject laserON;
    public GameObject laserOFF;
    public float timerDelay;
    private void Awake()
    {
        Invoke("ActivateLaser", timerDelay);
    }
    void ActivateLaser()
    {
        detector.SetActive(true);
        line.SetActive(true);
        laserON.SetActive(true);
        laserOFF.SetActive(false);
    }
}
