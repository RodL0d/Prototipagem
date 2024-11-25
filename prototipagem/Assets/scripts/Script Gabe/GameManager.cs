using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] public bool esticarBraço;
    [SerializeField] public bool olhoBionico;
    [SerializeField] public bool OuvidoBionico;
    GameObject exit_Menu;

    public bool EsticarBraço { get => esticarBraço; set => esticarBraço = value; }

    private void Update()
    {
        Exit_Menu();
    }
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
            exit_Menu = GameObject.Find("TeladePause");
            exit_Menu.SetActive(false);
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
    void Exit_Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (exit_Menu.activeInHierarchy)
            {
                exit_Menu.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                exit_Menu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    void FindBoolPower()
    {
        if (SceneManager.GetActiveScene().name == "1-0") // fase
        {
            puxarCaixa = true;
    
        }
        if(SceneManager.GetActiveScene().name == "2-0")
        {
            SuperPulo = true;
        }
        if(SceneManager.GetActiveScene().name == "3-0")
        {
            esticarBraço = true;
        }
        if(SceneManager.GetActiveScene().name == "4-0")
        {
            olhoBionico = true;
        }
        if(SceneManager.GetActiveScene().name == "pre fase")
        {
            OuvidoBionico = true;
        }
    }
}
