using UnityEngine;
using UnityEngine.SceneManagement;

public class Characters : MonoBehaviour
{
    public static int SelectedCharacter;

    public void SelectCharacterButton(int i)
    {
        SelectedCharacter = i;
        SceneManager.LoadScene("Difficulty");
    }
}
