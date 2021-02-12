using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartGameButton()
    {
        SceneManager.LoadScene("Characters");
    }
}
