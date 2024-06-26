using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Barreira : MonoBehaviour
{
    public float velocidade;
    [SerializeField]
    Botao botao;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (botao.pisou == true)
        {
            transform.Translate(Vector2.up * Time.deltaTime * velocidade);
        }
       // else
       // {
       //     transform.Translate(Vector2.down * Time.deltaTime * velocidade);
      //   }
    }
}
