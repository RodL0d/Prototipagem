using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] GameObject playerPrefab;
    private void Start()
    {
        Initialize();
    }
    void Initialize()
    {
        Instantiate(playerPrefab);
    } 
}
