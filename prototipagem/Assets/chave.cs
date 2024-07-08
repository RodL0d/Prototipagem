using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chave : MonoBehaviour
{

    public bool getChave;
    // Start is called before the first frame update
    void Start()
    {
        getChave = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "chave")
        {
            transform.SetParent(collision.transform, false);
            transform.localPosition = new Vector3(0.6f,0.8f, 0);
            getChave = true;
            print("pegou");
        }
    }
}
