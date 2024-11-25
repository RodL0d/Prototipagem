using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextUpdater : MonoBehaviour
{
    TextMeshProUGUI faseName;

    private void Awake()
    {
        faseName = GetComponent<TextMeshProUGUI>();
        faseName.text = "Fase: " + SceneManager.GetActiveScene().name;
    }
}
