using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreiraController : MonoBehaviour
{
    public float velocidade;
    [SerializeField]
    Botao[] botoes;
    [SerializeField] bool all;
    Vector2 posicaoInicial;
    [SerializeField] int maxposicao = 5;
    

    bool CheckBotoes()
    {
        foreach (Botao botao in botoes)
        {
            if (all)
            {
                if (!botao.pisou)
                {
                    return false;
                }
            }
            else
            {
                if (botao.pisou)
                {
                    return true;
                }
            }
        }

        return all;
    }
    
    void Start()
    {
        posicaoInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (CheckBotoes() && transform.position.y <= maxposicao + posicaoInicial.y)
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocidade);

        }

        if (!CheckBotoes() && transform.position.y >= posicaoInicial.y )
        {
            transform.Translate(Vector2.down * Time.deltaTime * velocidade);
        }

    }

}
