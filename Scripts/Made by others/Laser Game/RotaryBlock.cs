using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotaryBlock : MonoBehaviour
{
    [SerializeField] private float degreesToRotate = 90f;
    [SerializeField] private float states = 90f;

    [SerializeField] private float rotationSpeed = 1f;

    private new Rigidbody rigidbody;
    private Quaternion targetRotation;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void HandleRotation()
    {
        targetRotation = transform.rotation * Quaternion.Euler(0, degreesToRotate, 0);
    }

    private void Rotate()
    {
        rigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed));
    }
}
