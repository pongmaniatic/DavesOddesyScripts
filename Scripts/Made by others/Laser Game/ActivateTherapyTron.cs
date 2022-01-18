using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTherapyTron : MonoBehaviour
{
    public GameObject therapyGame;
    private Animation tronAnimation;
    public GameObject oxygenTron;
    void Start()
    {
        tronAnimation = therapyGame.GetComponent<Animation>();
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            therapyGame.SetActive(true);
            tronAnimation.Play("Tron_therapy_hologram_enter");
            oxygenTron.SetActive(false);
        }
    }

}
