using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cubo_Vitoria : MonoBehaviour
{
    public bool encostou;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Caixa_vitoria")
        {

            encostou = true;
            print("GANHOU!!!!");
            reloadScene();

        }
    }



    void reloadScene()

    {
        SceneManager.LoadScene("SampleScene");
    }

}
