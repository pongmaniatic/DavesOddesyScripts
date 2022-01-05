using UnityEngine;

public class TutorialLaserActive : MonoBehaviour
{
    public CallBlock callBlock;
    public GameObject detector;
    public GameObject line;
    public GameObject laserON;
    public GameObject laserOFF;

    void Update()
    {
        if (callBlock.GetCurrentlyActive() == true)
        {
            detector.SetActive(true);
            line.SetActive(true);
            laserON.SetActive(true);
            laserOFF.SetActive(false);
        }
    }
}
