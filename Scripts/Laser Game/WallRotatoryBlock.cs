using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotatoryBlock : MonoBehaviour
{
    public Detector detector1;
    public Detector detector2;
    public Detector detector3;
    public Detector detector4;
    public bool state = true;
    public GameObject OrangeLine;

    void OnMouseOver()
    { 
        // Check for mouse input
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);
            if (hit.collider.gameObject.tag == "Rotatory")
            {

                OrangeLine.transform.Rotate(0, 90, 0);
                RestartLasers();
                state = !state;
                if (state)
                {
                    detector1.otherSide = detector2;
                    detector2.otherSide = detector1;
                    detector3.otherSide = detector4;
                    detector4.otherSide = detector3;
                }
                if (!state)
                {
                    detector3.otherSide = detector2;
                    detector2.otherSide = detector3;
                    detector1.otherSide = detector4;
                    detector4.otherSide = detector1;
                }
            }
            detector1.hitball.transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
            detector2.hitball.transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
            detector3.hitball.transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
            detector4.hitball.transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);
            RestartLasers();

        }
    }

    void RestartLasers()
    {
        detector1.line.SetPosition(0, transform.position);
        detector1.line.SetPosition(1, transform.position);
        detector2.line.SetPosition(0, transform.position);
        detector2.line.SetPosition(1, transform.position);
        detector3.line.SetPosition(0, transform.position);
        detector3.line.SetPosition(1, transform.position);
        detector4.line.SetPosition(0, transform.position);
        detector4.line.SetPosition(1, transform.position);
    }
}
