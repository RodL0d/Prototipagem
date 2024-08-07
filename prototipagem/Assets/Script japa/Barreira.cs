using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Barreira : MonoBehaviour
{
    TiposDeEncaixe tiposDeEncaixe;
    sons tiposDeSons;
    public float velocidade;
    [SerializeField]
    Botao botao;
    bool daniel;
    Vector2 posicaoInicial;
    [SerializeField] int maxposicao = 5;
    [SerializeField]
    chave chave;
    void Start()
    {
        posicaoInicial = transform.position;
    }
    //botao.pisou == true && transform.position.y <= maxposicao + posicaoInicial.y && chave.getChave && 
    // Update is called once per frame
    void Update()
    {



        if (tiposDeSons == tiposDeEncaixe)
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocidade);

            daniel = true;
        }

        if (botao.pisou == false && daniel == true && transform.position.y >= posicaoInicial.y)
        {
            transform.Translate(Vector2.down * Time.deltaTime * velocidade);
            //transform.localPosition = new Vector2(8, 3);
        }

    }

}

