using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegundaBarreira : MonoBehaviour
{
    public float velocidade;

    [SerializeField]  Seguundo_Botao seguundo_Botao;
    bool daniel;
    Vector2 posicaoInicial;
    [SerializeField] int maxposicaoDonw = 5;
    void Start()
    {
        posicaoInicial = transform.position;
       
    }
    public float delay = 100;
    float timer;
    void Update()
    {

        if (seguundo_Botao.pisou == true && transform.position.y >= posicaoInicial.y - maxposicaoDonw)
        {
            transform.Translate(Vector2.down * Time.deltaTime * velocidade);

            daniel = true;
            timer += Time.deltaTime;
            if (timer > delay)
            {
                seguundo_Botao.pisou = false;
                timer -= delay;
            }
        }

        if (seguundo_Botao.pisou == false && daniel == true && transform.position.y <= posicaoInicial.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocidade);
        }

    }
}
