using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDestinationChoice : MonoBehaviour
{
    public void ToEndCredit()
    {
        Invoke("ChangeScene", 1.2f);
    }
    private void ChangeScene()
    {
        SceneManager.LoadScene(2);
    }
}
