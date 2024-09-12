using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mecanicas : MonoBehaviour
{
    [SerializeField]LayerMask Objetoinvisivel;
    float cooldown =5;

    void Start()
    {
        Camera.main.GetComponent<Camera>().cullingMask = (1 << 0) | (1 << 1) | (1 << 2) | (1 << 3) | (1 << 4) | (1 << 5);

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown = cooldown - Time.deltaTime;
            Debug.Log(cooldown);
        }

        if (Input.GetKeyDown(KeyCode.K) && cooldown <= 0)
        {

            StartCoroutine( Olhobionico());

        }
        
    }
    IEnumerator Olhobionico() 
    {
        Camera.main.GetComponent<Camera>().cullingMask = (1 << 0) | (1 << 1) | (1 << 2) | (1 << 3) | (1 << 4) | (1 << 5) | (1 << 6);
        yield return new WaitForSeconds(5);
        Camera.main.GetComponent<Camera>().cullingMask = (1 << 0) | (1 << 1) | (1 << 2) | (1 << 3) | (1 << 4) | (1 << 5);
        cooldown = 5;



    }
}
