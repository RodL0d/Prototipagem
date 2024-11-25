using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tempodevida : MonoBehaviour
{
     float vida ;
   
    [SerializeField] float vidamax = 60;
    [SerializeField] HUD hud;


    void Start()
    {
        vida = vidamax;

        hud = GetComponent<HUD>();
    }

    // Update is called once per frame
    void Update()
    {
       
        vida -=  Time.deltaTime;
        Debug.Log(vida);
        Morte();
        hud.UpdateHealthBar(vida, vidamax);
    }
    void Morte()
    {
        if (vida < 0)
        {
            reloadScenes();
        }
    }
    void reloadScenes()
    {
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
