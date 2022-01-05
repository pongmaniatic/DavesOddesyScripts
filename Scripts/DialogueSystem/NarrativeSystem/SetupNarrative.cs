using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetupNarrative : MonoBehaviour
{

    private void Awake()
    {
        Invoke("NextScene", 2f);
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }


}
