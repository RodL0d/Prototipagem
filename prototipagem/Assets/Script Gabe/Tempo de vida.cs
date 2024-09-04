using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergiaVital : MonoBehaviour
{
    [SerializeField] float vida ;
    


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        vida -=  Time.deltaTime;
        Debug.Log(vida);
        Morte();
    }
    void Morte()
    {
        if (vida < 0)
        {
            reloadScenes();
        }
    }
    void reloadScenes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
