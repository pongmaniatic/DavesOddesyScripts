using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallBlock : MonoBehaviour
{
    [Tooltip("One event per door")]
    [SerializeField] private GameEvent[] doorEvents;
    private Collider col;

    public Renderer receiverRenderer;
    public Material activeReceiver;
    public Material inactiveReceiver;

    public Renderer[] cableRenderer;
    public Material activeCable;
    public Material inactiveCable;

    private bool currentlyActive;
    public bool GetCurrentlyActive() { return currentlyActive; }

    private void Awake()
    {
        col = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapBox(col.bounds.center, col.bounds.extents);

        bool detected = false;
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Detector")) // && !hit.Equals(col)
            {
                detected = true;
                if (currentlyActive)
                    return;
                else
                {
                    currentlyActive = true;
                    receiverRenderer.material = activeReceiver;
                    foreach (Renderer cable in cableRenderer)
                        cable.material = activeCable;
                    RaiseEvents();
                    return;
                }
            }
        }
        if (!detected)
        {
            currentlyActive = false; 
            receiverRenderer.material = inactiveReceiver;
            foreach (Renderer cable in cableRenderer)
                cable.material = inactiveCable;
        }
    }

    private void RaiseEvents()
    {
        foreach (GameEvent ge in doorEvents)
        {
            ge.Raise();
        }
    }
}
