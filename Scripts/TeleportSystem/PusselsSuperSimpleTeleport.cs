using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusselsSuperSimpleTeleport : MonoBehaviour
{

    public GameObject player;
    public Vector3 teleportPosition;
    private float interval = 0f;
    public GameObject otherTeleport;

    public void Update()
    {
        if (interval <= 0)
        {
            otherTeleport.GetComponent<BoxCollider>().enabled = true;
            otherTeleport.GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {
            interval -= Time.deltaTime;
            otherTeleport.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interval = 3f;
            player.transform.position = teleportPosition;
        }
    }

}
