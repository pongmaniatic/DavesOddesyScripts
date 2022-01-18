using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlock : MonoBehaviour
{
    public Connection[] connections;

    public GameObject laserSFX;

    private void Awake() => ShootOnAwake();

    private void FixedUpdate() => ShootActive();

    private void ShootOnAwake()
    {
        for (int i = 0; i < connections.Length; i++)
        {
            if (connections[i].laserOnAwake)
                connections[i].inputActive = true;
            else
                for (int j = 0; j < connections[i].lasers.Length; j++)
                {
                    ActivateLaser(connections[i], j, false);
                    connections[i].inputActive = false;
                }
        }
    }

    private void ShootActive()
    {
        foreach (Connection connection in connections)
        {
            for (int i = 0; i < connection.lasers.Length; i++)
            {
                if (connection.inputActive || connection.laserOnAwake)
                {
                    ActivateLaser(connection, i, true);
                    HandleShooting(connection, i);
                }
                else
                    ActivateLaser(connection, i, false);
            }
        }
    }

    public void ShootLasers(bool b)
    {
        for (int i = 0; i < connections.Length; i++)
        {
            //if (!connections[i].input.Equals(col)) continue;
            connections[i].inputActive = b;
            for (int j = 0; j < connections[i].lasers.Length; j++)
            {
                HandleShooting(connections[i], j);
            }
        }
    }

    private void HandleShooting(Connection c, int i)
    {
        RaycastHit hit = HandleRaycast(c.lasers[i].transform.position, c.lasers[i].transform.forward);

        if (hit.collider == null) return;
        HandleLaser(c, i, c.lasers[i].transform.position, hit.point);
    }

    private RaycastHit HandleRaycast(Vector3 fromPosition, Vector3 direction)
    {
        if (Physics.Raycast(fromPosition, direction, out RaycastHit hit, Mathf.Infinity))
            return hit;
        return default;
    }

    private void HandleLaser(Connection c, int i, Vector3 a, Vector3 b)
    {
        c.lasers[i].SetPositions(new Vector3[] { a, b });
        c.detectors[i].transform.position = b;
    }

    private void ActivateLaser(Connection c, int i, bool b)
    {
        c.lasers[i].enabled = b;
        c.detectors[i].enabled = b;

        //laserSFX.SetActive(b);
    }

    #region Gizmos
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Connection connection in connections)
        {
            foreach (LineRenderer laser in connection.lasers)
            {
                Gizmos.DrawSphere(laser.transform.position, 0.1f);
                Gizmos.DrawLine(laser.transform.position, laser.transform.position + laser.transform.forward);
            }
        }
    }
#endif
    #endregion

    [System.Serializable]
    public struct Connection
    {
        public Collider input;
        public Collider[] detectors;
        public LineRenderer[] lasers;
        [HideInInspector] public bool inputActive;
        public bool laserOnAwake;
    }
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
