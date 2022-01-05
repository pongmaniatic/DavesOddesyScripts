using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseManager : MonoBehaviour
{

    public Texture2D cursorTextureWhite;
    public Texture2D cursorTextureRed;
    public Texture2D cursorTextureOrange;
    public Texture2D cursorTextureYellow;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private void Start()
    {
        Cursor.SetCursor(cursorTextureWhite, hotSpot, cursorMode);
    }

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 200.0f))
        {
            if (hit.collider.CompareTag("Pickable object")){Cursor.SetCursor(cursorTextureYellow, hotSpot, cursorMode); }
            else if (hit.collider.CompareTag("Placeable surface")) { Cursor.SetCursor(cursorTextureOrange, hotSpot, cursorMode); }
            else  { Cursor.SetCursor(cursorTextureWhite, hotSpot, cursorMode); }
        }
    }

}
