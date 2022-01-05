using UnityEngine;
using PathCreation;

public class CameraController : MonoBehaviour
{
    [Tooltip("The target for the camera to follow")]
    public Transform targetCharacter;

    [Range(0, 1)]
    [Tooltip("The camera targetpoint blend between target and the mouse")]
    [SerializeField] private float cameraTargetBlend = 0.1f;

    private Camera cam;
    private Vector3 targetPoint;

    public PathCreator pathCreator;
    public Transform leftTransform;
    public Transform rightTransform;
    public PathCreator pathCreatorHeight;
    public Transform frontTransform;
    public Transform backTransform;

    public float targetUnlockDistance;

    //todo Amount of rails
    //todo rails effect multiplier

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        targetPoint = CalculateTargetPoint();
        if (RaycastFromScreenPoint().collider != null)
            transform.LookAt(targetPoint);
        float distanceFromCamera = Vector3.Distance(targetCharacter.position, transform.position);
        if (distanceFromCamera < targetUnlockDistance)
            transform.LookAt(Vector3.Lerp(targetPoint, Vector3.zero, 
                1f - (distanceFromCamera / targetUnlockDistance)));
    }

    private void FixedUpdate()
    {
        MoveCamera();
    }

    private RaycastHit RaycastFromScreenPoint()
    {
        Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity);
        return hit;
    }

    private Vector3 CalculateTargetPoint()
    {
        return Vector3.Lerp(targetCharacter.position, RaycastFromScreenPoint().point, cameraTargetBlend);
    }

    private void MoveCamera()
    {
        Vector3 direction1 = (leftTransform.position - rightTransform.position).normalized;
        float t1 = (Vector3.Dot(targetCharacter.position.normalized, direction1) + 1f) / 2f;
        Vector3 pathPosition1 = pathCreator.path.GetPointAtTime(1f - t1);

        Vector3 direction2 = (backTransform.position - frontTransform.position).normalized;
        float t2 = (Vector3.Dot(targetCharacter.position.normalized, direction2) + 1f) / 2f;
        Vector3 pathPosition2 = pathCreatorHeight.path.GetPointAtTime(1f - t2);

        Vector3 newPosition = Vector3.Lerp(pathPosition1, pathPosition2, 0.5f);

        transform.position = newPosition;
    }

    #region OnDrawGizmos
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(CalculateTargetPoint(), 0.5f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(targetPoint, 0.5f);

            Vector3 direction1 = (leftTransform.position - rightTransform.position).normalized;
            float t1 = (Vector3.Dot(targetCharacter.position.normalized, direction1) + 1f) / 2f;
            Vector3 pathPosition1 = pathCreator.path.GetPointAtTime(1f - t1);

            Vector3 direction2 = (backTransform.position - frontTransform.position).normalized;
            float t2 = (Vector3.Dot(targetCharacter.position.normalized, direction2) + 1f) / 2f;
            Vector3 pathPosition2 = pathCreatorHeight.path.GetPointAtTime(1f - t2);

            Gizmos.DrawSphere(pathPosition1, 0.5f);
            Gizmos.DrawSphere(pathPosition2, 0.5f);

            Gizmos.color = Color.green;
            Vector3 newPosition = Vector3.Lerp(pathPosition1, pathPosition2, 0.5f);
            Gizmos.DrawSphere(newPosition, 0.5f);
        }
    }
#endif
    #endregion
}
