using UnityEngine;
using UnityEngine.AI;

public class PickUpSystem1 : MonoBehaviour
{
    private CraftingSystem craftingSystem;
    public GameObject currentlyHeldObject;
    private string HeldItemName;
    public float interactionReach = 5.0f;
    public float droppingReach = 5.0f;
    public GameObject NewItem;

    public Transform playerHold;
    private Vector3 defaultPosition;

    [Tooltip("To prevent picking up laserblocks from the otherside of a wall, ignore player and laserblock in this layer")]
    public LayerMask layerMask;

    public LineRenderer line;

    #region SFX
    [Header("SFX")]
    public bool hasOnHandleHeldObjectSFX;
    public float onHandleHeldObjectSFXVolume = 1.0f;
    public AudioClip[] onHandleHeldObjectSFX;

    public bool hasOnDropHeldObjectSFX;
    public AudioClip onDropHeldObjectSFX;
    public float onDropHeldObjectSFXVolume = 1.0f;

    #endregion
    private void Update()
    {
        
    }

    private void Awake()
    {
        craftingSystem = GetComponent<CraftingSystem>();
        defaultPosition = playerHold.position;
    }

    public void PickupObject(RaycastHit rayHit)
    {
        if (currentlyHeldObject != null) return;
        if (RaycastCollider(transform.position + Vector3.up, rayHit.point)) return;
        HandleHeldObject(rayHit.collider.gameObject, true, playerHold.transform, playerHold.position);
        if ( HeldItemName != "ToiletPaper" )
        {
            Upsize(false);
        }
    }

    public bool DropHeldObject(Vector3 dropPosition)
    {
        if (currentlyHeldObject == null) return false;
        if (Vector3.Distance(transform.position, dropPosition) > droppingReach) return false;
        if (RaycastCollider(transform.position + Vector3.up, dropPosition)) return false; 
        if (currentlyHeldObject.GetComponent<MaterialBlock>() != null)
            currentlyHeldObject.GetComponent<MaterialBlock>().SetDefaultMode();
        HandleHeldObject(null, false, null, dropPosition);
        if (hasOnDropHeldObjectSFX)
            PlayerGenericSFX.Instance.PlayRandomPitchSFX(onDropHeldObjectSFX, onDropHeldObjectSFXVolume, 0.75f, 1.0f);

        return true;
    }

    public void ReplaceObject(RaycastHit rayHit, Vector3 dropPosition)
    {
        DropHeldObject(dropPosition);
        PickupObject(rayHit);
    }

    public void Interact(RaycastHit rayHit)
    {
        if (RaycastCollider(transform.position + Vector3.up, rayHit.point)) return;
        rayHit.transform.gameObject.GetComponent<InteractableWithEvent>().InteractableActions();
    }

    private bool RaycastCollider(Vector3 fromPosition, Vector3 endPosition)
    {
        if (Physics.Raycast(fromPosition, (endPosition - fromPosition).normalized, out RaycastHit hit, Vector3.Distance(endPosition, fromPosition), layerMask.value))
        {
            return true;
        }
        return false;
    }
    
    public void InteractWithPlaceable(GameObject go)
    {
        if (currentlyHeldObject != null)
            PickupObjectFromPlaceable(go);
        else
            PlaceObject(go);
    }

    private void PlaceObject(GameObject go)
    {
        bool rightItem = go.GetComponent<ItemHolderObject>().CheckForKeyItem(HeldItemName);
        if (rightItem) { go.GetComponent<ItemHolderObject>().PlaceItem(currentlyHeldObject); currentlyHeldObject = null; }
    }

    private void PickupObjectFromPlaceable(GameObject go)
    {
        bool rightItem = go.GetComponent<ItemHolderObject>().CheckForItem();
        if (rightItem)
            HandleHeldObject(
                go.GetComponent<ItemHolderObject>().TakeBackItem(),
                true, gameObject.transform, playerHold.position);
    }

    public void TryCraftingObject(GameObject go)
    {
        bool craftableItem = craftingSystem.Craft(HeldItemName, go.GetComponent<ItemName>().itemName);
        if (craftableItem)
        {
            Destroy(go);
            Destroy(currentlyHeldObject);
            GameObject craftingResult = Instantiate(NewItem, playerHold.position, transform.rotation, gameObject.transform);
            craftingResult.tag = "Pickable object";//maybe not necessary if the prefab has this tag already
            Rigidbody craftingResultRig = craftingResult.GetComponent<Rigidbody>();
            HandleHeldObject(craftingResult, true, gameObject.transform, playerHold.position);
            HeldItemName = currentlyHeldObject.GetComponent<ItemName>().name;
            currentlyHeldObject.GetComponent<Outline>().enabled = false;
        }
    }

    private void HandleHeldObject(GameObject go, bool isKinematic, Transform parent, Vector3 position)
    {
        if (go != null) {
            currentlyHeldObject = go;
            HeldItemName = currentlyHeldObject.GetComponent<ItemName>().itemName;
            if (hasOnHandleHeldObjectSFX)
                PlayerGenericSFX.Instance.PlayRandomSFX(onHandleHeldObjectSFX, onHandleHeldObjectSFXVolume);
        }

        currentlyHeldObject.GetComponent<Collider>().enabled = !isKinematic;
        currentlyHeldObject.GetComponent<Rigidbody>().isKinematic = isKinematic;
        if (currentlyHeldObject.GetComponent<HoldPosition>() != null)
        {
            currentlyHeldObject.GetComponent<HoldPosition>().targetPosition = playerHold;
            currentlyHeldObject.GetComponent<HoldPosition>().isHeld = isKinematic;
            currentlyHeldObject.GetComponent<HoldPosition>().isBuildMode = !isKinematic;
            currentlyHeldObject.GetComponent<HoldPosition>().ResetRotation();
            //currentlyHeldObject.GetComponent<MaterialBlock>().SetBuildableMode();
        } 
        else
            currentlyHeldObject.transform.parent = parent;
        //currentlyHeldObject.GetComponent<NavMeshObstacle>().carving = !isKinematic;
        if (position != Vector3.down)
            currentlyHeldObject.transform.position = position;
        if (go == null)
            currentlyHeldObject = go;
    }

    public void CreateNewItem(string item)
    {
        NewItem = Resources.Load<GameObject>("Prefabs/" + item);
    }

    public void SetHeldObjectPosition(Vector3 newPosition, bool checkBuildable)
    {
        if (currentlyHeldObject == null) return;
        Vector3 position = newPosition;
        currentlyHeldObject.GetComponent<HoldPosition>().buildModePosition = position;
        if (checkBuildable && !RaycastCollider(transform.position + Vector3.up, newPosition))
        {
            if (currentlyHeldObject.GetComponent<MaterialBlock>() != null)
                currentlyHeldObject.GetComponent<MaterialBlock>().CheckBuildable(position);
        }
        else
        {
            if (currentlyHeldObject.GetComponent<MaterialBlock>() != null)
                currentlyHeldObject.GetComponent<MaterialBlock>().SetUnbuildableMode();
        }
    }

    public void ResetHeldObjectPosition(bool b)
    {
        if (currentlyHeldObject == null) return;
        if (HeldItemName != "ToiletPaper")
        {
            Upsize(b);
        }
        currentlyHeldObject.GetComponent<HoldPosition>().isBuildMode = b;
        currentlyHeldObject.GetComponent<HoldPosition>().isHeld = !b; 
        if (currentlyHeldObject.GetComponent<MaterialBlock>() != null)
            currentlyHeldObject.GetComponent<MaterialBlock>().SetDefaultMode();
    }

    public bool GetCurrentHeldObject()
    {
        return currentlyHeldObject != null ? false : true;
    }

    public void Upsize(bool b)
    {
        if (b) 
            currentlyHeldObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        else
            currentlyHeldObject.transform.localScale = new Vector3(1f, 1f, 1f);
    }
}



//public void HandleObjectClicked(RaycastHit hit)
//{
//    GameObject go = hit.collider.gameObject;
//    float distToObject = Vector3.Distance(hit.transform.position, transform.position);

//    if (distToObject < interactionReach) // distance check
//        if (currentlyHeldObject == null) // not holding an object
//        {
//            if (hit.collider.CompareTag("Pickable object"))
//            {
//            }
//            if (hit.collider.CompareTag("Placeable surface"))
//            {
//            }
//        }
//        else // holding an object
//        {
//            if (hit.collider.CompareTag("Pickable object"))
//            {
//            }
//            if (hit.collider.CompareTag("Placeable surface"))
//            {
//            }
//        } 
//    }
//}

//if (hit.collider.CompareTag("Player"))
//{
//    HandleHeldObject(null, false, null, Vector3.down);
//    if (hasOnDropHeldObjectSFX)
//    {
//        PlayerGenericSFX.Instance.PlaySFX(onDropHeldObjectSFX, onDropHeldObjectSFXVolume);
//    }
//}
