using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegurarObj: MonoBehaviour
{
    public string[] Tags;
    public float DistanciaMax;
    public bool Segurando;
    public GameObject ObjSegurando;
    [Space(20)]
    public GameObject local;
    public LayerMask Layoso;

 
    void Start()
    {
        
    }


    void Update()
    {
        if(Segurando == true)
        {
            Segurando = false;
            ObjSegurando.transform.parent = null;
            ObjSegurando.GetComponent<Rigidbody2D>().isKinematic = false;
            ObjSegurando = null;
            return;
        }
        if (Segurando == false)
        {
            RaycastHit Hit = new RaycastHit();
            if (Physics.Raycast(transform.position, transform.forward, out Hit, DistanciaMax, Layoso, QueryTriggerInteraction.Ignore))
            {
                Debug.DrawLine(transform.position, Hit.point, Color.green);
                for (int i = 0; i < Tags.Length; i++)
                {
                    if (Hit.transform.gameObject.tag == Tags[i])
                    {
                        if (Input.GetKeyDown(KeyCode.L))
                        {
                            Segurando = true;
                            ObjSegurando = Hit.transform.gameObject;
                            if(ObjSegurando.GetComponent<Rigidbody2D>())
                            {
                                ObjSegurando.GetComponent<Rigidbody2D>().isKinematic = true;
                                ObjSegurando.transform.position = local.transform.position;
                                ObjSegurando.transform.rotation = local.transform.rotation;
                                ObjSegurando.transform.parent = local.transform;
                            }
                            return;
                        }
                    }
                }
            }
        }


    }
}
