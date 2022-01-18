using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public bool hasDelayUntilEvent;
    public float eventDelay = 5.0f;

    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable() => Event.RegisterListener(this);
    private void OnDisable() => Event.UnregisterListener(this);
    public void OnEventRaised() 
    {
        if (hasDelayUntilEvent)
        {
            StartCoroutine("InvokeEventOnDelay", eventDelay);
        }
        else
        {
            Response.Invoke();
        }
    }


    public IEnumerator InvokeEventOnDelay(float eventDelay)
    {
        float normalizedTime = 0;
        while (normalizedTime <= 1f) // In a while loop while counting down
        {
            normalizedTime += Time.deltaTime / eventDelay;
            yield return null;
        }
        Response.Invoke();
    }

}
