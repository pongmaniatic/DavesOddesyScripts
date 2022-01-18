using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerInput1 : MonoBehaviour
{
    public Camera cam;
    [NonSerialized] public Vector3 targetPosition;
    public GameObject targetMarkerPrefab;
    private GameObject targetMarker;
    private PickUpSystem1 pickupSystem;

    [SerializeField] private string groundTag = "Ground"; // Turn these into an enum
    [SerializeField] private string pickableTag = "Pickable object"; // Turn these into an enum
    [SerializeField] private string interactableTag = "Interactable object"; // Turn these into an enum
    [SerializeField] private string placeableTag = "Placeable object"; // Turn these into an enum
    private NavMeshPathFinding path;
    private RaycastHit currentHit;
    private bool outOfReach = false;
    private RaycastHit savedHit;
    private bool waitingToDrop = false;

    private bool pointerDown = false;
    private float pointerDownTime = 1f;
    [SerializeField] private float holdTime = 0.5f;

    public GameObject cross;
    [SerializeField] private LayerMask defaultLayerMask = default;
    [SerializeField] private LayerMask buildModeLayerMask = default;

    [Header("Pathfinding")]
    public bool usePathfinding = false;

    private bool buildingMode = false;
    [SerializeField] private GameObject customPassVolume;

    private void Awake()
    {
        //targetMarker = Instantiate(targetMarkerPrefab, Vector3.zero, quaternion.identity);
        pickupSystem = GetComponent<PickUpSystem1>();
        path = GetComponent<NavMeshPathFinding>();
    }

    private void Update()
    {
        HandleConstantActions();
        HandlePlayerRequest();
        HandleOutOfReachTarget();
    }

    private void HandlePlayerRequest()
    {
        if (Input.GetMouseButton(0)) // LMB HOLD
        {
            pointerDownTime += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(0))
        {
            pointerDownTime = 0;
            if (buildingMode)
            {
                Vector3 dropPosition = currentHit.point;
                if (!HandleRaycast(
                    cam.transform.position, (CalculateWorldPosition() - cam.transform.position).normalized,
                    defaultLayerMask)) return;
                if (currentHit.collider.CompareTag(pickableTag)) {
                    pickupSystem.ReplaceObject(currentHit, dropPosition);
                    SetBuildingMode(false);
                }
                else if (pickupSystem.DropHeldObject(dropPosition))
                    SetBuildingMode(false);
            }
            else
                HandleDefaultActions();
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (pickupSystem.GetCurrentHeldObject()) return;
            SetBuildingMode(!buildingMode); // Toggle building mode
        }
    }

    #region Raycast
    private Vector3 CalculateWorldPosition()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        return cam.ScreenToWorldPoint(mousePosition);
    }

    private bool HandleRaycast(Vector3 fromPosition, Vector3 direction, LayerMask layerMask)
    {
        if (Physics.Raycast(fromPosition, direction, out RaycastHit hit, Mathf.Infinity, layerMask.value))
        {
            currentHit = hit;
            return true;
        }
        return false;
    }
    #endregion

    public void HandleDefaultActions()
    {
        if (!HandleRaycast(
            cam.transform.position, (CalculateWorldPosition() - cam.transform.position).normalized, 
            defaultLayerMask)) return;

        if (currentHit.collider.CompareTag(groundTag)) // Ground to walk on was hit
        {
            SetTargetPosition(currentHit.point);
        }
        else if (Vector3.Distance(
            currentHit.point, transform.position) < pickupSystem.interactionReach) // Distance check
        {
            if (currentHit.collider.CompareTag(interactableTag))
            {
                pickupSystem.Interact(currentHit);
            }
            else if (currentHit.collider.CompareTag(placeableTag))
            {
                pickupSystem.InteractWithPlaceable(currentHit.collider.gameObject);
            }
            else if (currentHit.collider.CompareTag(pickableTag))
                pickupSystem.PickupObject(currentHit);
        }
        else
        {
            outOfReach = true;
            SetTargetPosition(currentHit.point + 
                (transform.position - currentHit.point).normalized * pickupSystem.interactionReach);
            savedHit = currentHit;
        }
    }

    private void HandleConstantActions()
    {
        if (!HandleRaycast(cam.transform.position, 
            (CalculateWorldPosition() - cam.transform.position).normalized,
            buildModeLayerMask)) return;

        // change cursor on hovering objects (disabled during building mode?)

        //if (!currentHit.collider.CompareTag(groundTag)) return;
        
        if (buildingMode)
        {
            if (Vector3.Distance(transform.position, currentHit.point) < pickupSystem.droppingReach)
                pickupSystem.SetHeldObjectPosition(currentHit.point, true);
            else
                pickupSystem.SetHeldObjectPosition(currentHit.point, false);
        }
        //else if (Input.GetMouseButton(0))
        //    SetTargetPosition(currentHit.point);
        // todo add this back
    }

    private void SetBuildingMode(bool b)
    {
        buildingMode = b;
        //customPassVolume.SetActive(b);
        pickupSystem.ResetHeldObjectPosition(b);
    }

    private void SetTargetPosition(Vector3 pos)
    {
        gameObject.GetComponent<PlayerMovement1>().movementEnabled = true;
        targetPosition = pos;
        //SetTargetMarkerPosition(targetPosition);
        if (!usePathfinding) return;
            path.SetTargetDestination(pos);
    }

    private void HandleOutOfReachTarget()
    {
        if (outOfReach && savedHit.collider != null)
        {
            if (Vector3.Distance(targetPosition, transform.position) < pickupSystem.interactionReach)
            {
                if (waitingToDrop)
                    pickupSystem.DropHeldObject(savedHit.point + Vector3.up);
                pickupSystem.PickupObject(currentHit);
                outOfReach = false;
                gameObject.GetComponent<PlayerMovement1>().StopWalking();
            }
        }
    }

    private void SetTargetMarkerPosition(Vector3 pos) => targetMarker.transform.position = pos + Vector3.up * 0.5f;
    public void SetTargetMarkerEnabled(bool b) => targetMarker.SetActive(b);
    public Vector3 GetMousePosition() => Input.mousePosition;
    public Vector3 GetTargetPosition() => targetPosition;

    private void OnDrawGizmos()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
        Vector3 worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(worldPosition, worldPosition + cam.transform.forward * 500f);

        Gizmos.DrawSphere(worldPosition, 0.5f);
    }
}

public struct MyRaycastHit
{
    public MyRaycastHit(RaycastHit hit, bool isNull)
    {
        this.hit = hit;
        this.isNull = isNull;
    }

    public MyRaycastHit(bool isNull)
    {
        this.hit = default; // breaks without this line
        this.isNull = isNull;
    }

    public RaycastHit hit;
    public bool isNull;
}

//[Header("IsometricCamera")]
//public bool useIsometricCamera = false;
//if (useIsometricCamera)
//    HandleRaycast(worldPosition, cam.transform.forward);
//else
//    HandleRaycast(cam.transform.position, (worldPosition - cam.transform.position).normalized);

// ISOMETRIC RAYCAST
//if (!HandleRaycast(CalculateWorldPosition(), cam.transform.forward)) return;
//#region Raycast
//private Vector3 CalculateWorldPosition()
//{
//    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
//    return cam.ScreenToWorldPoint(mousePosition);
//}

//private bool HandleRaycast(Vector3 fromPosition, Vector3 direction)
//{
//    if (Physics.Raycast(fromPosition, direction, out RaycastHit hit, Mathf.Infinity, layerMask.value))
//    {
//        currentHit = hit;
//        return true;
//    }
//    return false;
//}
//#endregion

//public void HandleDropAction() // Temp fix
//{
//    if (Physics.Raycast(CalculateWorldPosition(), cam.transform.forward, out RaycastHit hit, Mathf.Infinity))
//    {
//        if (hit.collider.CompareTag(groundTag))
//        {
//            if (Vector3.Distance(hit.point, transform.position) < pickupSystem.interactionReach)
//            {
//                pickupSystem.DropHeldObject(hit.point + Vector3.up);
//            }
//            else
//            {
//                outOfReach = true;
//                SetTargetPosition(hit.point);
//                waitingToDrop = true;
//                savedHit = hit;
//            }
//        }
//    }
//}