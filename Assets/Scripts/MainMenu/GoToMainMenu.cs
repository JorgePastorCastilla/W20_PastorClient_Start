using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMainMenu : MonoBehaviour
{
    public void OnGoToMainMenuButtonClicked()
    {
        SceneManager.LoadScene(3);
    }
}
