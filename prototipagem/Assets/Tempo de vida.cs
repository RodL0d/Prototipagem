using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnergiaVital : MonoBehaviour
{
    [SerializeField] float tempodevida;

    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        tempodevida = tempodevida + Time.deltaTime;
        Debug.Log(tempodevida);
        Morte();
    }
    void Morte()
    {
        if (tempodevida > 10)
        {
            reloadScenes();
        }
    }
    void reloadScenes()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
