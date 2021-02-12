using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWin : MonoBehaviour
{
    [SerializeField] private GameObject youWin;
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerController>())
        {
            youWin.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
