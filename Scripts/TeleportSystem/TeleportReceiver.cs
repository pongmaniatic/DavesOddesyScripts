using System;
using UnityEngine;
using UnityEngine.AI;

public class TeleportReceiver : MonoBehaviour
{
    private string tag;

    public void SetTag(string tag)
    {
        this.tag = tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tag))
        {
            if (other.transform.GetComponent<NavMeshPathFinding>() != null)
            {
                other.transform.GetComponent<NavMeshPathFinding>().SetTargetDestination(other.transform.position);

            }

            if (other.transform.GetComponent<PlayerMovement1>() != null)
            {
                other.transform.GetComponent<PlayerMovement1>().movementEnabled = false;
            }
        }
    }
}
