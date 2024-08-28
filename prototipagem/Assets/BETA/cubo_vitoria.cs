using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cubo_vitoria : MonoBehaviour
{
    public bool encostou;
    // Start is called before the first frame update
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
            print("pisou");
            //anim.SetBool("Botao", true);
            encostou = true;
            //reloadScene();

        }
    }

    

    void reloadScene()
    
    {
        SceneManager.LoadScene("SampleScene");
    }
}
