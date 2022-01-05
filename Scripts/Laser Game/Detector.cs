using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameObject hitball;
    public bool beingHit = false;
    public Detector otherSide;
    public LineRenderer line;
    public int direction;//0 up, 1 left, 2 down, 3 right 




    void ShootLaser()
    {
        if (direction == 0)
        {
            Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, Mathf.Infinity);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);
            hitball.transform.position = hit.point;
        }
        else if (direction == 1)
        {
            Physics.Raycast(transform.position, transform.right, out RaycastHit hit, Mathf.Infinity);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);
            hitball.transform.position = hit.point;
        }
        else if (direction == 2)
        {
            Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Mathf.Infinity);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);
            hitball.transform.position = hit.point;
        }
        else if (direction == 3)
        {
            Physics.Raycast(transform.position, -transform.right, out RaycastHit hit, Mathf.Infinity);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, hit.point);
            hitball.transform.position = hit.point;
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (!otherSide.beingHit) { beingHit = true; }
        otherSide.ShootLaser();
    }
    private void OnTriggerExit(Collider other)
    {
        beingHit = false;
        hitball.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        otherSide.hitball.transform.position = new Vector3(otherSide.transform.position.x, otherSide.transform.position.y + 10, otherSide.transform.position.z);
        otherSide.line.SetPosition(0, transform.position);
        otherSide.line.SetPosition(1, transform.position);
    }


}
