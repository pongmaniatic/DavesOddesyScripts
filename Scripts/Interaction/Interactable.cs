using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ItemName))]
public class Interactable : MonoBehaviour
{
    private Outline outline;
    private GameObject player;
    public float distanceToActivate = 2;

    private void Start()
    {
        outline = gameObject.GetComponent<Outline>();
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        // if the player is close enough the outline appears.
        float distToplayer = Vector3.Distance(player.transform.position, transform.position);
        if (Input.GetKey("left ctrl") || distToplayer <= distanceToActivate) { outline.enabled = true; }
        else { outline.enabled = false; }

    }

}
