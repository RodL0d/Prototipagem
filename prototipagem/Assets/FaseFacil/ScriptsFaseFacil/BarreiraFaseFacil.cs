using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraControler_FaseMedia2 : MonoBehaviour
{
    public float velocidade;
    [SerializeField]
    Botão_FaseMedia2 botao2;
    bool daniel;
    Vector2 posicaoInicial;
    [SerializeField] int maxposicao = 5;
    void Start()
    {
        posicaoInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {




        if (botao2.pisou == true && transform.position.y <= maxposicao + posicaoInicial.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocidade);

            daniel = true;
        }

        if (botao2.pisou == false && daniel == true && transform.position.y >= posicaoInicial.y)
        {
            transform.Translate(Vector2.down * Time.deltaTime * velocidade);
            //transform.localPosition = new Vector2(8, 3);
        }

    }

}
