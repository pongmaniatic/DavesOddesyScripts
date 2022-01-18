using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBlock : MonoBehaviour
{
    public LineRenderer line;
    public GameObject hitball;
    // Update is called once per frame
    void Update()
    {
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, Mathf.Infinity);
        line.SetPosition(0, transform.position);
        line.SetPosition(1, hit.point);
        hitball.transform.position = hit.point;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, Mathf.Infinity);
        Gizmos.DrawLine(transform.position, hit.point);
        Gizmos.DrawSphere(hit.point, 0.05f);
    }




}
