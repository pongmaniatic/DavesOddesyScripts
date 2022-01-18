using System;
using UnityEngine;

public class HoldPosition : MonoBehaviour
{
    private new Rigidbody rigidbody;
    [NonSerialized] public Transform targetPosition = default;
    [NonSerialized] public bool isHeld = false;
    [NonSerialized] public bool isBuildMode = false;
    [NonSerialized] public Vector3 buildModePosition = default;
    [NonSerialized] public Vector3 targetRotation = default;
    public bool useCustomYPosition;
    public float customYPosition;

    private void Awake() 
    { 
        rigidbody = GetComponent<Rigidbody>();
        targetRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        if (isBuildMode)
        {
            if (useCustomYPosition)
            {
                transform.position = new Vector3(buildModePosition.x, customYPosition, buildModePosition.z);
            }
            else
            {
                transform.position = new Vector3(buildModePosition.x, buildModePosition.y, buildModePosition.z);
            }
            
        }
            
        //Debug.Log($"held {isHeld} build{isBuildMode}");
    }

    private void FixedUpdate()
    {
        if (isHeld)
            rigidbody.position = targetPosition.position;
    }

    public void ResetRotation()
    {
        rigidbody.MoveRotation(Quaternion.Euler(targetRotation));
    }
}
