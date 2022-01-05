using UnityEngine;

//[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement1 : MonoBehaviour
{
    public bool movementEnabled = false;
    private PlayerInput1 playerInput;
    private new Rigidbody rigidbody;
    [Range(0,20)]
    public float moveSpeed = 5f;
    [Range(0,20)]
    public float rotationSpeed = 5f;
    [Range(0,5)]
    public float distanceReach = 1f;
    [Range(0.8f,1)]
    public float rotationReach = 1f;

    private Vector3 targetDirection;

    public bool isWalking = false;
    private NavMeshPathFinding path;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput1>();
        rigidbody = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        if (playerInput.usePathfinding) return;

        if (CalculateRotationFromTarget() < rotationReach)
            Rotate();
        isWalking = false;
        //playerInput.SetTargetMarkerEnabled(false); //todo called more often than needed, make this event-based

        if (CalculateDistanceFromTarget() < distanceReach) return;
        Move();
        isWalking = true;
        //playerInput.SetTargetMarkerEnabled(true); //todo called more often than needed, make this event-based
        
    }
    
    private void Move()
    {
        if (movementEnabled)
        {
            targetDirection = (new Vector3(playerInput.targetPosition.x, 0f, playerInput.targetPosition.z) -
            new Vector3(rigidbody.position.x, 0f, rigidbody.position.z)).normalized;
            rigidbody.MovePosition(rigidbody.position + moveSpeed / 100f * targetDirection); // 100f to make moveSpeed more readable
        }

    }

    private void Rotate()
    {
        if (movementEnabled)
        {
            Quaternion targetRotation = Quaternion.LookRotation(
            (targetDirection - rigidbody.transform.forward).normalized, Vector3.up);
            //Debug.Log(targetDirection);
            rigidbody.MoveRotation(Quaternion.RotateTowards(rigidbody.rotation, targetRotation, rotationSpeed));
        }
    }

    public void StopWalking()
    {
        movementEnabled = false;
    }
    
    private float CalculateDistanceFromTarget() => Vector3.Distance(playerInput.targetPosition, rigidbody.position);
    private float CalculateRotationFromTarget() => Vector3.Dot(targetDirection, transform.forward);



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up + targetDirection, transform.position + Vector3.up + targetDirection * 5f);
        Gizmos.DrawSphere(transform.position + targetDirection * 5f, 0.2f);
    }
}