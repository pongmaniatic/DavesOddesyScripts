using UnityEngine;
using UnityEngine.Events;

public class ItemHolderObject : MonoBehaviour
{
    [Header("Name of item it recibes")]
    public string keyItem;
    [Header("Local position")]
    public Vector3 itemPosition;
    [Header("Local rotation")]
    public Vector3 itemRotation;

    public GameObject itemHeld = null;
    public bool holdsItem = false;

    #region AudioSFX
    [Header("AudioSFX")]

    public bool hasItemPlaceSFX;

    public AudioClip onItemPlaceSFX;
    public float onItemPlaceSFXVolume = 1.0f;

    public bool hasItemRemoveSFX;

    public AudioClip onItemRemoveSFX;
    public float onItemRemoveSFXVolume = 1.0f;
    #endregion

    public bool activateRoomOnPlaceItem;
    public bool deActivateRoomOnPlaceItem;
    public enum RoomToActivate { ELECTRICAL, CRYO, KITCHEN, OXYGEN, THERAPY, COCKPIT }
    
    public RoomToActivate roomToActivate = RoomToActivate.ELECTRICAL;

    public UnityEvent eventOnPlaceItem;
    public UnityEvent eventOnTakeBackItem;

    public bool CheckForKeyItem(string Item)
    {
        if (Item == keyItem && !holdsItem) { return true; }
        else { return false; }
    }

    public void PlaceItem(GameObject Item)
    {
        holdsItem = true;
        itemHeld = Item;
        Item.transform.parent = gameObject.transform;
        Item.transform.localPosition = new Vector3(itemPosition.x, itemPosition.y, itemPosition.z);
        Item.transform.localRotation = Quaternion.Euler(itemRotation.x, itemRotation.y, itemRotation.z);
        Item.GetComponent<Collider>().enabled = false;

        if (hasItemPlaceSFX)
        {
            PlayerGenericSFX.Instance.PlaySFX(onItemPlaceSFX, onItemPlaceSFXVolume);
        }

        if (activateRoomOnPlaceItem)
        {
            ActivateRoom(roomToActivate, true);
        }

        eventOnPlaceItem.Invoke();

    }

    public bool CheckForItem()
    {
        if (holdsItem) { return true; }
        else { return false; }
    }

    public GameObject TakeBackItem()
    {
        itemHeld.GetComponent<Collider>().enabled = true;
        holdsItem = false;

        if (hasItemRemoveSFX)
        {
            PlayerGenericSFX.Instance.PlaySFX(onItemRemoveSFX, onItemRemoveSFXVolume);
        }

        eventOnTakeBackItem.Invoke();

        if (deActivateRoomOnPlaceItem)
        {
            ActivateRoom(roomToActivate, false);
        }

        return itemHeld;
    }

    public void ActivateRoom(RoomToActivate room, bool Setter)
    {
        switch (room)
        {
            case RoomToActivate.ELECTRICAL:
                ShipSystemsStatusManager.Instance.Electrical = Setter;
                break;
            case RoomToActivate.CRYO:
                ShipSystemsStatusManager.Instance.CryoSleep = Setter;
                break;
            case RoomToActivate.KITCHEN:
                ShipSystemsStatusManager.Instance.Kitchen = Setter;
                break;
            case RoomToActivate.OXYGEN:
                ShipSystemsStatusManager.Instance.Oxygen = Setter;
                break;
            case RoomToActivate.THERAPY:
                ShipSystemsStatusManager.Instance.CryoSleep = Setter;
                break;
            case RoomToActivate.COCKPIT:
                ShipSystemsStatusManager.Instance.Cockpit = Setter;
                break;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x + itemPosition.x, transform.position.y + itemPosition.y, transform.position.z + itemPosition.z), 0.05f);
    }


    
}