using UnityEngine;

public class SetButtonActive : MonoBehaviour
{
    public GameObject button;
    public float waitTime;
    void Start()
    {
        Invoke("ActivateButton", waitTime);
    }

   public void ActivateButton()
    {
        button.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            button.SetActive(true);
        }
    }
}
