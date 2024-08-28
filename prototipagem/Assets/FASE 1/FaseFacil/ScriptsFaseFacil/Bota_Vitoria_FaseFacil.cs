using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.SceneManagement;

public class Bota_Vitoria_FaseFacil : MonoBehaviour
{
    public bool pisou;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("pisou");
            pisou = true;


            SceneManager.LoadScene("FaseMedia");
        }
    }
}
