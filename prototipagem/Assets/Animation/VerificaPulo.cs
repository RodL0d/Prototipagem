using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerificaPulo : MonoBehaviour
{
   [SerializeField] LayerMask mask;
    public bool estaNoChao;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       estaNoChao = Physics2D.OverlapBox(gameObject.transform.position, new Vector2(0.6787214f, 0.1889343f), 5, mask);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.layer == 4 )
        {
            estaNoChao= true;
        }
    }
}
