using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetCharacter;
    [SerializeField] private Vector3 offset;
    [Range(0,1)][SerializeField] private float smoothTime = 0.25f;

    private new Rigidbody rigidbody;
    private Vector3 velocity;

    [Header("Look At Target Editor Mode Enabled")]
    [SerializeField] private bool lookAt = false;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        Vector3 newPosition = Vector3.SmoothDamp(
            rigidbody.position, CalculateTargetPosition(), ref velocity, smoothTime);
        rigidbody.MovePosition(newPosition);
        if (lookAt)
            transform.LookAt(targetCharacter); // todo remove, only for choosing a rotation
    }

    private Vector3 CalculateTargetPosition()
    {
        return targetCharacter.position + offset;
    }

    private void OnValidate()
    {
        if (targetCharacter == null) return;
        transform.position = CalculateTargetPosition();
        if (lookAt)
            transform.LookAt(targetCharacter); // todo remove, only for choosing a rotation
    }
}
