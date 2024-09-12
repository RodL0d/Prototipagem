using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    [SerializeField] private string nomeDaFase;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcao;
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDaFase);
    }

    public void abrirOpcaoes()
    {
        painelMenuInicial.SetActive(false);
        painelOpcao.SetActive(true);
    }

    public void fecharOpcaoes()
    {
        painelOpcao.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void sairDoJogo()
    {
        Debug.Log("SairDoJogo");
        Application.Quit();
    }
}
