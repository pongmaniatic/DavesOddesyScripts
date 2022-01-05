using UnityEngine;
using UnityEngine.AI;

public class PickUpSystem : MonoBehaviour
{
    private CraftingSystem craftingSystem;
    public GameObject currentlyHeldObject;
    private string HeldItemName;
    public float interactionReach = 5.0f;
    public GameObject NewItem;

    public Transform playerHold;

    #region SFX
    [Header("SFX")]
    public bool hasOnHandleHeldObjectSFX;
    public float onHandleHeldObjectSFXVolume = 1.0f;
    public AudioClip onHandleHeldObjectSFX;

    public bool hasOnDropHeldObjectSFX;
    public AudioClip onDropHeldObjectSFX;
    public float onDropHeldObjectSFXVolume = 1.0f;

    #endregion

    private void Awake()
    {
        craftingSystem = GetComponent<CraftingSystem>();
    }

    public void HandleObjectClicked(RaycastHit hit)
    {
        GameObject hitObj = hit.collider.gameObject;
        float distToObject = Vector3.Distance(hit.transform.position, transform.position);

        if (distToObject < interactionReach)
        {
            if (currentlyHeldObject == null)
            {
                if (hit.collider.CompareTag("Pickable object"))
                {
                    HandleHeldObject(hitObj, true, playerHold.transform, playerHold.position);
                }
                if (hit.collider.CompareTag("Placeable surface"))
                {
                    bool rightItem = hitObj.GetComponent<ItemHolderObject>().CheckForItem();
                    if (rightItem) { HandleHeldObject(hitObj.GetComponent<ItemHolderObject>().TakeBackItem(), true, gameObject.transform, playerHold.position);  }
                }
            }
            else
            {
                if (hit.collider.CompareTag("Pickable object"))
                {
                    bool craftableItem = craftingSystem.Craft(HeldItemName, hitObj.GetComponent<ItemName>().itemName);

                   // Debug.Log("Can " + hitObj.GetComponent<ItemName>().itemName + " be crafted with " + HeldItemName + ", result: " + craftableItem);

                    if (craftableItem)
                    {
                        Destroy(hitObj);
                        Destroy(currentlyHeldObject);
                        GameObject craftingResult = Instantiate(NewItem, playerHold.position, transform.rotation, gameObject.transform);
                        craftingResult.tag = "Pickable object";//maybe not necessary if the prefab has this tag already
                        Rigidbody craftingResultRig = craftingResult.GetComponent<Rigidbody>();
                        HandleHeldObject(craftingResult, true, gameObject.transform, playerHold.position);
                        HeldItemName = currentlyHeldObject.GetComponent<ItemName>().name;
                        currentlyHeldObject.GetComponent<Outline>().enabled = false;
                        
                    }    
                }
                if (hit.collider.CompareTag("Placeable surface"))
                {
                    bool rightItem = hitObj.GetComponent<ItemHolderObject>().CheckForKeyItem(HeldItemName);
                    if (rightItem){hitObj.GetComponent<ItemHolderObject>().PlaceItem(currentlyHeldObject); currentlyHeldObject = null; }
                }
            }
        }
    }

    public void DropHeldObject(Vector3 dropPosition)
    {
        if (currentlyHeldObject == null) return;
        HandleHeldObject(null, false, null, dropPosition);
        if (hasOnDropHeldObjectSFX)
            PlayerGenericSFX.Instance.PlaySFX(onDropHeldObjectSFX, onDropHeldObjectSFXVolume);
    }

    private void HandleHeldObject(GameObject go, bool isKinematic, Transform parent, Vector3 position)
    {
        if (go != null)
            currentlyHeldObject = go;
        HeldItemName = currentlyHeldObject.GetComponent<ItemName>().itemName;

        if (hasOnHandleHeldObjectSFX)
            PlayerGenericSFX.Instance.PlaySFX(onHandleHeldObjectSFX, onHandleHeldObjectSFXVolume);
        currentlyHeldObject.GetComponent<Collider>().enabled = !isKinematic;
        currentlyHeldObject.GetComponent<Rigidbody>().isKinematic = isKinematic;
        if (currentlyHeldObject.GetComponent<HoldPosition>() != null)
        {
            currentlyHeldObject.GetComponent<HoldPosition>().targetPosition = playerHold;
            currentlyHeldObject.GetComponent<HoldPosition>().isHeld = isKinematic;
            currentlyHeldObject.GetComponent<HoldPosition>().ResetRotation();
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
}

//if (hit.collider.CompareTag("Player"))
//{
//    HandleHeldObject(null, false, null, Vector3.down);
//    if (hasOnDropHeldObjectSFX)
//    {
//        PlayerGenericSFX.Instance.PlaySFX(onDropHeldObjectSFX, onDropHeldObjectSFXVolume);
//    }
//}
