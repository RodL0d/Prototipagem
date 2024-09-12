using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botao : MonoBehaviour
{
    //private Animator anim;
    public bool pisou;
    chave chave;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Caixa")
        {
            print("pisou");
            //anim.SetBool("Botao", true);
            pisou = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Caixa")
        //{
        //anim.SetBool("Botao", false);
        pisou = false;
        // }
    }

}
