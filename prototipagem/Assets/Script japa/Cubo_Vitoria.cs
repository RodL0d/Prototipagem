using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cubo_Vitoria : MonoBehaviour
{
    public bool encostou;
    [SerializeField] string proximaCena;

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
            LoadScene(proximaCena);

        }
    }



    void LoadScene(string sceneName)

    {
        SceneManager.LoadScene(sceneName);
    }

}
