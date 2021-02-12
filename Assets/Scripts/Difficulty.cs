using UnityEngine;
using UnityEngine.SceneManagement;

public class Difficulty : MonoBehaviour
{
    public static bool isHard;
    public void StartLeveButton(bool hard)
    {
        isHard = hard;
        SceneManager.LoadScene("Level_1");
    }
}
