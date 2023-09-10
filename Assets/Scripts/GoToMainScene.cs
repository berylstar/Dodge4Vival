using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainScene : MonoBehaviour
{
    public void GoToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
