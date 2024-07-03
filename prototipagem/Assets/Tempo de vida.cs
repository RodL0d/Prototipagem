using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergiaVital : MonoBehaviour
{
     float vida;
    [SerializeField] int vidaMax ;


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        vida = vida + Time.deltaTime;
        Debug.Log(vida);
        Morte();
    }
    void Morte()
    {
        if (vida > vidaMax)
        {
            reloadScenes();
        }
    }
    void reloadScenes()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
