using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sons : MonoBehaviour
{
    [SerializeField] int tiposDeSons;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if(collision.gameObject.tag == "Encaixe")
        {
            if (tiposDeSons == collision.GetComponent<TiposDeEncaixe>().tiposDeEncaixe)
            {
                Debug.Log(true);
            }
            else 
            {
                Debug.Log(false);
            }
        }   
    }
}
