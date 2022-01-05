using UnityEngine;

public class AntiStuckDetectorDeactivator : MonoBehaviour
{
    private float timer;
    public float timeEnabled;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeEnabled;
            gameObject.SetActive(false);
        }
    }
}
