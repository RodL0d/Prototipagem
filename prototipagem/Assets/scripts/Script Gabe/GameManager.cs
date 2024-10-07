using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        

    }
    #endregion

    [SerializeField] GameObject playerPrefab, initialPosition;
    [SerializeField] PlayerController playerController;
    [SerializeField] public bool SuperPulo;
    [SerializeField] public bool puxarCaixa;
    [SerializeField] public bool EsticarBraço;
    [SerializeField] public bool olhoBionico;
    [SerializeField] public bool OuvidoBionico;
    private void Start()
    {
        SceneManager.sceneLoaded += Initialize;
    }
    void Initialize()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        {
            initialPosition = GameObject.Find("PlayerPositon").gameObject;
            Instantiate(playerPrefab, new Vector2(initialPosition.transform.position.x, initialPosition.transform.position.y), Quaternion.identity);
            playerController = FindObjectOfType<PlayerController>();
            FindBoolPower();
        }   
    }
    private void Initialize(Scene scene, LoadSceneMode mode)
    {
            Initialize();
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= Initialize;
    }
    
    void FindBoolPower()
    {
        if (SceneManager.GetActiveScene().name == "PréFase") // fase
        {
            puxarCaixa = true;
    
        }
        if(SceneManager.GetActiveScene().name == "Pre-Fase")
        {
            SuperPulo = true;
        }
        if(SceneManager.GetActiveScene().name == "Pre fase3")
        {
            EsticarBraço = true;
        }
        if(SceneManager.GetActiveScene().name == "pre fase 4")
        {
            olhoBionico = true;
        }
        if(SceneManager.GetActiveScene().name == "pre fase")
        {
            OuvidoBionico = true;
        }
    }
}
