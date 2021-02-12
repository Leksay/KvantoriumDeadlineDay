using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;

    void Awake()
    {
        if(SpawnPointPosition.instance == null)
        {
            SpawnPointPosition.instance = new SpawnPointPosition();
            SpawnPointPosition.instance.position = transform.position;
        }
        GameObject.Instantiate(characters[Characters.SelectedCharacter], SpawnPointPosition.instance.position, Quaternion.identity);
    }

}
