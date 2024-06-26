using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegurarObj : MonoBehaviour
{
    public string[] tags;
    public float distanciaMax;
    public bool segurando;
    public GameObject objSecundario;
    [Space(20)]
    public GameObject Local;
    public LayerMask LayerMask;


    private void Start()
    {
        
    }

    private void Update()
    {
        if (segurando == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                segurando = false; 
                objSecundario = null;
                return;
            }
        }
        if(segurando == false)
        {
            RaycastHit Hit;
            if (Physics.Raycast(transform.position, transform.forward, out Hit, distanciaMax, LayerMask, QueryTriggerInteraction.Ignore)) ;
            {
                Debug.DrawLine(transform.position, Hit.point, Color.green);

                for (int i = 0; i < tags.Length; i++)
                {
                    if (Hit.transform.gameObject.tag == tags[i])
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            segurando = true;
                            objSecundario = Hit.transform.gameObject;
                        }
                    }
                }
            }
        }        
    }
}

