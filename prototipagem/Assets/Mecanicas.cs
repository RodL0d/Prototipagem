using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Mecanicas : MonoBehaviour
{
    [SerializeField]LayerMask objetoInvisivel;
    float cooldown;

    void Start()
    {
        StartCoroutine(Olhobionico());
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldown = cooldown - Time.deltaTime;
    }
    IEnumerator Olhobionico() 
    {
        if (Input.GetKeyDown(KeyCode.L) && cooldown <= 0)
        {
            Camera.main.GetComponent<Camera>().cullingMask = (1 << 0) | (1 << 1) | (1 << 2) | (1 << 3) | (1 << 4) | (1 << 5) | (1 << 6);
            
            Camera.main.GetComponent<Camera>().cullingMask = (1 << 0) | (1 << 1) | (1 << 2) | (1 << 3) | (1 << 4) | (1 << 5);
        }
        
    }
}
