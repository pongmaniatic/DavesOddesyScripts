using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Camera cam;
    [NonSerialized] public Vector3 targetPosition;
    public GameObject targetMarkerPrefab;
    private GameObject targetMarker;
    private PickUpSystem pickUpSystem;

    [SerializeField] private string groundTag = "Ground";
    [SerializeField] private string pickableTag = "Pickable object";
    [SerializeField] private string interactableTag = "Interactable object";
    [SerializeField] private string placeableTag = "Placeable object";
    private NavMeshPathFinding path;
    private bool outOfReach = false;
    private RaycastHit currentTarget;
    private bool waitingToDrop = false;

    private bool pointerDown = false;
    private float pointerDownTime = 1f;
    [SerializeField] private float holdTime = 0.5f;

    public GameObject cross;
    [SerializeField] private LayerMask layerMask = default;

    [Header("Pathfinding")]
    public bool usePathfinding = false;

    private void Awake()
    {
        targetMarker = Instantiate(targetMarkerPrefab, Vector3.zero, quaternion.identity);
        pickUpSystem = GetComponent<PickUpSystem>();
        path = GetComponent<NavMeshPathFinding>();
    }

    private void Update()
    {
        HandlePlayerRequest();
        HandleOutOfReachTarget();
    }

    private void HandlePlayerRequest()
    {
        if (Input.GetMouseButtonDown(0))
            PointerDown(true);
        if (Input.GetMouseButtonUp(0))
            PointerDown(false);

        if (Input.GetMouseButtonUp(1))
            HandleDropAction();

        if (pointerDown)
            pointerDownTime -= Time.deltaTime;
        if (pointerDownTime <= 0)
        {
            HandleTargetAction();
            pointerDownTime = 1f;
        }
    }

    private void PointerDown(bool b)
    {
        pointerDown = b;
        if (b)
            pointerDownTime = holdTime;
        else
        {
            pointerDownTime = 1f;
            HandleTargetAction();
        }
    }

    public void HandleTargetAction()
    {
        HandleRaycast(CalculateWorldPosition(), cam.transform.forward);
    }

    private Vector3 CalculateWorldPosition()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        return cam.ScreenToWorldPoint(mousePosition);
    }

    public void HandleDropAction() // Temp fix
    {
        if (Physics.Raycast(CalculateWorldPosition(), cam.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag(groundTag))
            {
                if (Vector3.Distance(hit.point, transform.position) < pickUpSystem.interactionReach)
                {
                    pickUpSystem.DropHeldObject(hit.point + Vector3.up);
                } else
                {
                    outOfReach = true;
                    SetTargetPosition(hit.point);
                    waitingToDrop = true;
                    currentTarget = hit;
                }
            }
        }
    }

    private void HandleRaycast(Vector3 fromPosition, Vector3 direction)
    {
        if (Physics.Raycast(fromPosition, direction, out RaycastHit hit, Mathf.Infinity, layerMask.value))
            HandleRaycastHit(hit);
    }

    private void HandleRaycastHit(RaycastHit hit) // Something was hit
    {
        if (hit.collider.CompareTag(groundTag)) // Ground to walk on was hit
        {
            SetTargetPosition(hit.point);
            gameObject.GetComponent<PlayerMovement>().movementEnabled = true;
        }
        else if (Vector3.Distance(hit.point, transform.position) < pickUpSystem.interactionReach)
        {
            if (hit.collider.CompareTag(interactableTag))
            {
                if (hit.transform.gameObject.GetComponent<InteractableWithEvent>() != null)
                    hit.transform.gameObject.GetComponent<InteractableWithEvent>().InteractableActions();
            } else if (hit.collider.CompareTag(placeableTag))
            {
                pickUpSystem.HandleObjectClicked(hit);
            }
            else if (pointerDownTime <= 0)
                pickUpSystem.HandleObjectClicked(hit);
        }
        else
        {
            outOfReach = true;
            SetTargetPosition(hit.point);
            currentTarget = hit;
        }
    }

    private void SetTargetPosition(Vector3 pos)
    {
        targetPosition = pos;
        SetTargetMarkerPosition(targetPosition);
        if (!usePathfinding) return;
            path.SetTargetDestination(pos);
    }

    private void HandleOutOfReachTarget()
    {
        if (outOfReach && currentTarget.collider != null)
        {
            if (Vector3.Distance(currentTarget.point, transform.position) < pickUpSystem.interactionReach)
            {
                if (waitingToDrop)
                    pickUpSystem.DropHeldObject(currentTarget.point + Vector3.up);
                HandleRaycastHit(currentTarget);
                outOfReach = false;
            }
        }
    }

    private void SetTargetMarkerPosition(Vector3 pos) => targetMarker.transform.position = pos + Vector3.up * 0.5f;
    public void SetTargetMarkerEnabled(bool b) => targetMarker.SetActive(b);
    public Vector3 GetMousePosition() => Input.mousePosition;
    public Vector3 GetTargetPosition() => targetPosition;

    private void OnDrawGizmos()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(worldPosition, worldPosition + cam.transform.forward * 50f);

        Gizmos.DrawSphere(worldPosition, 0.5f);
    }
}

//[Header("IsometricCamera")]
//public bool useIsometricCamera = false;
//if (useIsometricCamera)
//    HandleRaycast(worldPosition, cam.transform.forward);
//else
//    HandleRaycast(cam.transform.position, (worldPosition - cam.transform.position).normalized);