using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlock1 : MonoBehaviour
{
    public int laserIndex;
    public Collider input;
    public Collider[] detectors;
    public LineRenderer[] lasers;
    public bool inputActive = false;
    public bool laserOnAwake;

    public GameObject laserSFX;

    private void Awake() => ShootOnAwake();

    private void FixedUpdate() => ShootActive();

    private void ShootOnAwake()
    {
        if (laserOnAwake)
            inputActive = true;
        else
            for (int j = 0; j < lasers.Length; j++)
            {
                ActivateLaser(j, false);
                inputActive = false;
            }
    }

    private void ShootActive()
    {
        for (int i = 0; i < lasers.Length; i++)
        {
            if (inputActive || laserOnAwake)
            {
                ActivateLaser(i, true);
                HandleShooting(i);
            }
            else
                ActivateLaser(i, false);
        }
    }

    public void ShootLasers(bool b)
    {
        //if (laserIndex == i) { Debug.Log("no"); return; }
        inputActive = b;
        for (int j = 0; j < lasers.Length; j++)
        {
            HandleShooting(j);
        }
    }

    private void HandleShooting(int i)
    {
        RaycastHit hit = HandleRaycast(lasers[i].transform.position, lasers[i].transform.forward);

        if (hit.collider == null) return;
        
        HandleLaser(i, lasers[i].transform.position, hit.point);
    }

    private RaycastHit HandleRaycast(Vector3 fromPosition, Vector3 direction)
    {
        if (Physics.Raycast(fromPosition, direction, out RaycastHit hit, Mathf.Infinity))
            return hit;
        return default;
    }

    private void HandleLaser(int i, Vector3 a, Vector3 b)
    {
        lasers[i].SetPositions(new Vector3[] { a, b });
        detectors[i].transform.position = b;
    }

    private void ActivateLaser(int i, bool b)
    {
        lasers[i].enabled = b;
        detectors[i].enabled = b;

        //laserSFX.SetActive(b);
    }

    #region Gizmos
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (LineRenderer laser in lasers)
        {
            Gizmos.DrawSphere(laser.transform.position, 0.1f);
            Gizmos.DrawLine(laser.transform.position, laser.transform.position + laser.transform.forward);
        }
    }
#endif
    #endregion

    //[System.Serializable]
    //public struct Connection
    //{
    //    public Collider input;
    //    public Collider[] detectors;
    //    public LineRenderer[] lasers;
    //    [HideInInspector] public bool inputActive;
    //    public bool laserOnAwake;
    //}
}

//[SerializeField] private LineRenderer[] lasers = default;
//[SerializeField] private Collider[] laserEnds = default;

//[SerializeField] private bool lasersOnAwake = false;

//public void Update()
//{
//    ShootLasers();
//}

//    public void ShootLasers()
//    {
//        for (int i = 0; i < lasers.Length; i++)
//        {
//            RaycastHit hit = HandleRaycast(lasers[i].transform.position, lasers[i].transform.forward);
//            if (hit.collider == null) return;

//            HandleLaser(i, lasers[i].transform.position, hit.point);
//        }
//    }

//    public void ShootLasers(int[] indices)
//    {
//        for (int i = 0; i < lasers.Length; i++)
//        {
//            for (int j = 0; j < indices.Length; j++)
//            {
//                if (!indices[j].Equals(i))
//                    break;
//            }
//            RaycastHit hit = HandleRaycast(lasers[i].transform.position, lasers[i].transform.forward);
//            if (hit.collider == null) return;

//            HandleLaser(i, lasers[i].transform.position, hit.point);
//        }
//    }

//    private RaycastHit HandleRaycast(Vector3 fromPosition, Vector3 direction)
//    {
//        if (Physics.Raycast(fromPosition, direction, out RaycastHit hit, Mathf.Infinity))
//            return hit;
//        return default;
//    }

//    private void HandleLaser(int i, Vector3 a, Vector3 b)
//    {
//        lasers[i].SetPositions(new Vector3[] { a, b });
//        laserEnds[i].transform.position = b;
//    }

//    #region Gizmos
//#if UNITY_EDITOR
//    private void OnDrawGizmos()
//    {
//        Gizmos.color = Color.red;
//        foreach (LineRenderer laser in lasers)
//        {
//            Gizmos.DrawSphere(laser.transform.position, 0.1f);
//            Gizmos.DrawLine(laser.transform.position, laser.transform.position + laser.transform.forward);
//        }
//    }
//#endif
//    #endregion
