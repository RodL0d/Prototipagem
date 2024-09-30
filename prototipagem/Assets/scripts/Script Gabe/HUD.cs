using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] Image healthBar;
    [SerializeField] Image superPuloBar;

    private void Start()
    {
            healthBar = GameObject.Find("Image").GetComponent<Image>();
            superPuloBar = GameObject.Find("SuperPuloIMG").GetComponent<Image>();
    }
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
    public void UpdateSuperPuloBar(float timerdoPulo, float limiteSuperPulo)
    {
        superPuloBar.fillAmount = timerdoPulo / limiteSuperPulo;
    }
}
