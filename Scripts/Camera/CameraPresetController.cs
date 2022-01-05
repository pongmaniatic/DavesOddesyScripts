using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPresetController : MonoBehaviour
{
    [SerializeField] private List<Collider> zones = new List<Collider>();
    //[SerializeField] private 

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0.5f, 0.5f);
        foreach (Collider zone in zones)
        {
            Gizmos.DrawCube(zone.bounds.center, zone.bounds.size);
        }
        
    }
}
