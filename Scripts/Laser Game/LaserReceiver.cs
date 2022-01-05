using UnityEngine;

public class LaserReceiver : MonoBehaviour
{
    public bool useParentLaser = true;
    public LaserBlock laser;
    private Collider col;

    public new Renderer renderer;
    public GameObject[] portal;

    private bool currentlyActive;
    public bool GetCurrentlyActive() { return currentlyActive; }

    private void Awake()
    {
        if (useParentLaser)
            laser = GetComponentInParent<LaserBlock>();
        col = GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapBox(col.bounds.center, col.bounds.extents);

        bool detected = false;
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Detector") && !hit.GetComponentInParent<LaserBlock>().Equals(laser))
            {
                detected = true;
                if (currentlyActive)
                {
                    if (!useParentLaser)
                    {
                        portal[0].SetActive(false);
                        portal[1].SetActive(false);
                        portal[2].SetActive(true);
                        portal[3].SetActive(true);
                    }
                    return;
                }
                else
                {
                    currentlyActive = true;
                    HandleHit(true);
                    return;
                }
            }
        }
        if (!detected)
        {
            currentlyActive = false;
            HandleHit(false);
            if (!useParentLaser)
            {
                portal[0].SetActive(true);
                portal[1].SetActive(true);
                portal[2].SetActive(false);
                portal[3].SetActive(false);
            }
        }
    }

    private void HandleHit(bool b)
    {
        laser.ShootLasers(b);
    }
}
