using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botão1_FaseDificil : MonoBehaviour
{
    //private Animator anim;
    public bool pisou;

    private bool podeAbrir;

    [SerializeField] Botão1_FaseDificil botaoSecundario;

    public float velocidade;
    bool daniel;
    Vector2 posicaoInicial;
    [SerializeField] int maxposicao = 5;
    [SerializeField] Transform barreira;
    void Start()
    {
        posicaoInicial = barreira.transform.position;
    }

    void Update()
    {




        if (podeAbrir && barreira.transform.position.y <= maxposicao + posicaoInicial.y)
        {
            barreira.transform.Translate(Vector2.up * Time.deltaTime * velocidade);

            daniel = true;
        }

        if (!podeAbrir && daniel == true && barreira.transform.position.y >= posicaoInicial.y)
        {
            barreira.transform.Translate(Vector2.down * Time.deltaTime * velocidade);
            //transform.localPosition = new Vector2(8, 3);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Caixa")
        {
            print("pisou");
            //anim.SetBool("Botao", true);

            if(botaoSecundario != null)
            {
                pisou = true;

                podeAbrir = pisou && botaoSecundario.pisou;
            }
            else
            {
                podeAbrir = true;
            }
            


        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (botaoSecundario != null)
        {
            pisou = false;

            podeAbrir = pisou && botaoSecundario.pisou;
        }
        else
        {
            podeAbrir = false;
        }
    }
}
