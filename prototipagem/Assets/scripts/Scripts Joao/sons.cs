using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sons : MonoBehaviour
{
    [SerializeField] GameObject porta;
    [SerializeField] int tiposDeSons, maxposicao;
    Vector3 posicaoInicial;
    bool abre;
    float velocidade = 5;

    private void Start()
    {
        posicaoInicial = porta.transform.position;
    }

    private void Update()
    {
        if (abre && porta.transform.position.y <= maxposicao + posicaoInicial.y )
        {
            porta.transform.Translate(Vector2.up * Time.deltaTime * velocidade);
        }
        else if(!abre && porta.transform.position.y >= posicaoInicial.y)
        {
            Debug.Log(false);
            porta.transform.Translate(Vector2.down * Time.deltaTime * velocidade);
            //transform.localPosition = new Vector2(8, 3)
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "Encaixe")
            {
                if (tiposDeSons == collision.GetComponent<TiposDeEncaixe>().tiposDeEncaixe)
                {
                    abre = true;
                }
            }
    }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Encaixe")
            {
                if (tiposDeSons == collision.GetComponent<TiposDeEncaixe>().tiposDeEncaixe)
                {
                    abre = false;
                }
            }
        }
    }
