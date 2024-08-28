using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seguundo_Botao : MonoBehaviour
{
    public bool pisou;
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Caixa")
        {
            pisou = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        pisou = false;     
    }
}
